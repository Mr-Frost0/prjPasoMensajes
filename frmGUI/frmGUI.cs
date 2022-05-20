using System;
using System.Windows.Forms;
using KernelSistema;
using System.Threading;
using System.ComponentModel;

namespace frmGUI
{
    public partial class frmGUI : Form
    {

        #region [Atributos]

        clsKernel objKernel;
        private clsCerrarPorPID objCerrarInstancia;
        private clsRecibeMensajes objRecibeMsg;
        private bool estaArrancado = false;

        #endregion

        #region [Constructor]

        public frmGUI()
        {
            InitializeComponent();
            this.objKernel = new clsKernel();
            this.objCerrarInstancia = new clsCerrarPorPID();
            RecuperaIdMaestro();
            this.bgwRecibeMensajes.WorkerReportsProgress = true;
            this.bgwRecibeMensajes.WorkerSupportsCancellation = true;
            ArrancarBGW();
        }

        #endregion

        #region [Métodos Privados]

        private void RecuperaIdMaestro()
        {
            if (!objKernel.RecuperaPID("maestro"))
            {
                MessageBox.Show(objKernel.Error, "Final Sistemas Operativos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            this.lstHistorialPIDs.Items.Add("[" + objKernel.IdProceso.ToString() + "] " + this.Text);
            this.lstPIDActuales.Items.Add("[" + objKernel.IdProceso.ToString() + "] " + this.Text);
        }

        private void NuevaInstancia()
        {
            objKernel.LanzaForm("mod-aplicaciones");
        }

        private void CerradoForm()
        {
            if (objCerrarInstancia.ConfirmaCerrado())
            {
                objCerrarInstancia.CerrarInstancia("cerrar-gui");
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
                        estaArrancado = false;
                    }
                    else
                    {
                        estaArrancado = true;
                        if (!objRecibeMsg.RecibirMsg("operacion-exito"))
                        {
                            MessageBox.Show(objRecibeMsg.Error, "Final Sistemas Operativos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        CheckForIllegalCrossThreadCalls = false;
                        this.txtMensajes.Text += objRecibeMsg.Mensaje;
                        this.txtMensajes.Text += Environment.NewLine;
                        this.txtMensajes.SelectionStart = txtMensajes.Text.Length;
                        this.txtMensajes.ScrollToCaret();
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
                        this.txtMensajes.Text += "Actualización de Mensajes Activada";
                        this.txtMensajes.Text += Environment.NewLine;
                    }
                    break;
                case "detenido":
                    if (estaArrancado)
                    {
                        this.txtMensajes.Text += "Actiualización de Mensajes Desactivada, esperando último mensaje en camino (si existe)...";
                        this.txtMensajes.Text += Environment.NewLine;
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

        private void tsmiNuevaInstancia_Click(object sender, EventArgs e)
        {
            NuevaInstancia();
        }

        private void bgwRecibeMensajes_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            RecuperarMsg(sender, e);
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

        private void frmGUI_FormClosing(object sender, FormClosingEventArgs e)
        {
            CerradoForm();
        }

        private void bgwRefrescaPID_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        #endregion


    }
}
