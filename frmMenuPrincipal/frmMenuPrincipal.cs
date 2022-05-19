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

        private clsKernel objKernel;
        private clsCerrarPorPID objCerrarInstancia;
        private clsRecibeMensajes objRecibeMsg;
        private bool estaArrancado = false;

        #endregion

        #region [Constructor]

        public frmPrincipal()
        {
            InitializeComponent();
            objKernel = new clsKernel();
            RecuperaIdMaestro();
            this.lstPIDs.Items.Add(objKernel.IdProcMaestro.ToString());
            bgwRecibeMensajes.WorkerReportsProgress = true;
            bgwRecibeMensajes.WorkerSupportsCancellation = true;
            ArrancarBGW();

        }

        #endregion

        #region [Métodos Privados]

        private void RecuperaIdMaestro()
        {
            if (!objKernel.RecovPID("maestro"))
            {
                MessageBox.Show(objKernel.Error,"Final Sistemas Operativos",MessageBoxButtons.OK,MessageBoxIcon.Error);
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

        private void CerradoForm()
        {
            if (objCerrarInstancia.ConfirmaCerrado())
            {
                objCerrarInstancia.CerrarInstancia("form-calculadora");
            }
        }

        #endregion

        #region [Eventos]

        private void btnSuma_Click(object sender, EventArgs e)
        {
            this.lstPIDs.Items.Add(objKernel.LanzaForm("sumar").ToString());
        }

        private void btnResta_Click(object sender, EventArgs e)
        {
            this.lstPIDs.Items.Add(objKernel.LanzaForm("restar"));
        }

        private void btnMultipl_Click(object sender, EventArgs e)
        {
            this.lstPIDs.Items.Add(objKernel.LanzaForm("multiplicar"));
        }

        private void btnDivision_Click(object sender, EventArgs e)
        {
            this.lstPIDs.Items.Add(objKernel.LanzaForm("dividir"));
        }

        private void frmPrincipal_FormClosing(object sender, FormClosingEventArgs e)
        {
            CerradoForm();
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

        #endregion


    }
}
