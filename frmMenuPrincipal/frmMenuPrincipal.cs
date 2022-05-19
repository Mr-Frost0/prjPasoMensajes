using System;
using System.Windows.Forms;
using KernelSistema;
using System.ComponentModel;
using System.Threading;

namespace frmMenuPrincipal
{

    /*
     * Este es el módulo de aplicaciones, por lo cual le aplican las siguientes normas
      
     * Si este módulo se cierra, deberá cerrar todos los módulos hijos, pero se debe poder inicializar de nuevo desde el módulo GUI     
     
     * Controlará sus instancias a través del paso de mensajes
     
     * Se comunicará con la GUI mediante el comando 'info', a la vez que envía el PID de las instancias hijas
      
     * Cuando se cierre una instancia o todas mediante el módulo Aplicación, notificará al gestor de archivos y lo registrará en el registro de transacciones
      
     * Este módulo puede generar un mensaje 'ocupado' con un tiempo de espera entre 1 y 3 segundos, para luego pasar a estar disponibles y actualizar su estado en tiempo real
      
     */

    public partial class frmPrincipal : Form
    {

        #region [Atributos]

        private clsKernel apiOperativa;
        private clsRecibeMensajes objRecibeMsg;
        private bool estaArrancado = false;

        #endregion

        #region [Constructor]

        public frmPrincipal()
        {
            InitializeComponent();
            apiOperativa = new clsKernel();
            RecuperaIdMaestro();
            this.lstPIDs.Items.Add(apiOperativa.IdProcMaestro.ToString());
            bgwRecibeMensajes.WorkerReportsProgress = true;
            bgwRecibeMensajes.WorkerSupportsCancellation = true;
            ArrancarBGW();

        }

        #endregion

        #region [Métodos Privados]

        private void RecuperaIdMaestro()
        {
            if (!apiOperativa.RecovPID("maestro"))
            {
                MessageBox.Show(apiOperativa.Error,"Final Sistemas Operativos",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void RecuperarMsg(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            while (worker.CancellationPending == false)
            {
                objRecibeMsg = new clsRecibeMensajes();
                try
                {
                    if (worker.CancellationPending == true)
                    {
                        e.Cancel = true;
                    }
                    else
                    {
                        if (!objRecibeMsg.RecibirMsg())
                        {
                            MessageBox.Show(objRecibeMsg.Error, "Final Sistemas Operativos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        CheckForIllegalCrossThreadCalls = false;
                        this.txtMensajes.Text += objRecibeMsg.Mensaje;
                        this.txtMensajes.Text += Environment.NewLine;
                    }
                    Thread.Sleep(500);
                
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Final Sistemas Operativos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    objRecibeMsg = null;
                }
            }
        }

        private void ArrancarBGW()
        {
            if (!bgwRecibeMensajes.IsBusy)
            {
                bgwRecibeMensajes.RunWorkerAsync();
            }
        }

        private void DetenerBGW()
        {
            if (bgwRecibeMensajes.WorkerSupportsCancellation)
            {
                bgwRecibeMensajes.CancelAsync();
            }
        }

        private void ActuEstado(String estado)
        {

            switch (estado)
            {
                case "arrancado":
                    if (estaArrancado)
                    {
                        this.txtMensajes.Text += "La Actualización de Mensajes ya está Activada";
                        this.txtMensajes.Text += Environment.NewLine;
                    }
                    else
                    {
                        estaArrancado = true;
                        this.txtMensajes.Text += "Actualización de Mensajes Activada";
                        this.txtMensajes.Text += Environment.NewLine;
                    }
                    break;
                case "detenido":
                    if (estaArrancado)
                    {
                        this.txtMensajes.Text += "Actiualización de Mensajes Desactivada, esperando último mensaje en camino (si existe)...";
                        this.txtMensajes.Text += Environment.NewLine;
                        estaArrancado = false;
                    }
                    else
                    {
                        this.txtMensajes.Text += "La Actiualización de Mensajes ya está Desactivada";
                        this.txtMensajes.Text += Environment.NewLine;
                    }
                    break;
                default:
                    break;                    
            }
        }

        #endregion

        #region [Eventos]

        private void btnSuma_Click(object sender, EventArgs e)
        {
            this.lstPIDs.Items.Add(apiOperativa.LanzaForm("sumar").ToString());
        }

        private void btnResta_Click(object sender, EventArgs e)
        {
            this.lstPIDs.Items.Add(apiOperativa.LanzaForm("restar"));
        }

        private void btnMultipl_Click(object sender, EventArgs e)
        {
            this.lstPIDs.Items.Add(apiOperativa.LanzaForm("multiplicar"));
        }

        private void btnDivision_Click(object sender, EventArgs e)
        {
            this.lstPIDs.Items.Add(apiOperativa.LanzaForm("dividir"));
        }

        private void frmPrincipal_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult resultado = MessageBox.Show("Desea salir del programa?\nEsto implica cerrar todas las demás instancias de programas abiertos mediante esta aplicación.", "Confirmacion",
               MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            e.Cancel = (resultado == DialogResult.No);
            apiOperativa = null;
        }

        private void btnActuMsgs_Click(object sender, EventArgs e)
        {
            ActuEstado("arrancado");
            ArrancarBGW();
        }

        private void btnDetenerActu_Click(object sender, EventArgs e)
        {
            ActuEstado("detenido");
            DetenerBGW();
        }

        private void bgwRecibeMensajes_DoWork(object sender, DoWorkEventArgs e)
        {
            RecuperarMsg(sender, e);
        }

        private void bgwRecibeMensajes_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }

        #endregion


    }
}
