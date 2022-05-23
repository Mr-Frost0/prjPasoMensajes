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

        private clsKernel objKernel;
        private clsKernel_PIDActuales objListaPIDActuales;
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
            this.objListaPIDActuales = new clsKernel_PIDActuales();
            RecuperaIdMaestro();
            this.wrkMsgTextBox.WorkerReportsProgress = true;
            this.wrkMsgTextBox.WorkerSupportsCancellation = true;
            ArrancarBGW("arranque");
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

        private bool CerradoForm()
        {
            if (objCerrarInstancia.ConfirmaCerrado())
            {
                objCerrarInstancia.CerrarInstancia("cerrar-gui");
                return true;
            }
            else return false;
        }

        private void ArrancarBGW(String tipo)
        {
            switch (tipo)
            {
                case "arranque":
                    wrkMsgTextBox.RunWorkerAsync();
                    wrkMsgPID.RunWorkerAsync();
                    break;
                case "default":
                    if (!wrkMsgTextBox.IsBusy)
                    {
                        wrkMsgTextBox.RunWorkerAsync();
                    }
                    break;
                default:
                    break;
            }
        }

        private void DetenerBGW()
        {
            if (wrkMsgTextBox.WorkerSupportsCancellation)
            {
                wrkMsgTextBox.CancelAsync();
            }
        }
        
        private void RecuperarMensajes(object sender, DoWorkEventArgs e, String tipo)
        {
            wrkMsgTextBox = sender as BackgroundWorker;

            while (wrkMsgTextBox.CancellationPending == false)
            {
                objRecibeMsg = new clsRecibeMensajes();
                try
                {
                    if (wrkMsgTextBox.CancellationPending == true)
                    {
                        e.Cancel = true;
                        estaArrancado = false;
                    }
                    else
                    {                      
                        estaArrancado = true;
                        if (!objRecibeMsg.RecibirMsg(tipo))
                        {
                            MessageBox.Show(objRecibeMsg.Error, "Final Sistemas Operativos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        CheckForIllegalCrossThreadCalls = false;
                        this.txtMensajes.Text += objRecibeMsg.Mensaje;
                        this.txtMensajes.Text += Environment.NewLine;
                        this.txtMensajes.SelectionStart = txtMensajes.Text.Length;
                        this.txtMensajes.ScrollToCaret();
                        Thread.Sleep(500);
                    }
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

        private void RecuperarPID(object sender, DoWorkEventArgs e, string tipo)
        {
            wrkMsgPID = sender as BackgroundWorker;
            CheckForIllegalCrossThreadCalls = false;
            Thread.Sleep(3000);
            while (wrkMsgPID.CancellationPending == false)
            {
                try
                {
                    objRecibeMsg = new clsRecibeMensajes();
                    if (wrkMsgPID.CancellationPending == true)
                    {
                        e.Cancel = true;
                    }
                    else
                    {
                        if (objRecibeMsg.RecibirMsg(tipo))
                        {
                            
                            this.lstHistorialPIDs.Items.Add(objRecibeMsg.Mensaje);
                            this.objListaPIDActuales.MensajeRecibe = objRecibeMsg.MensajeRetorno;
                            this.objListaPIDActuales.ProcesarPID();
                            this.lstHistorialPIDs.SelectedIndex = lstHistorialPIDs.Items.Count - 1;
                            this.lstPIDActuales.DataSource = objListaPIDActuales.ProcesosActivos;
                            if (this.objListaPIDActuales.SeElimino)
                            {
                                this.txtMensajes.Text += this.objListaPIDActuales.Mensaje;
                                this.txtMensajes.Text += Environment.NewLine;
                            }
                            Thread.Sleep(2500);
                        }
                    }
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

        private void bgwRecibeMensajes_DoWork(object sender, DoWorkEventArgs e)
        {
            RecuperarMensajes(sender, e, "listbox");
        }

        private void btnActuMsgs_Click(object sender, EventArgs e)
        {
            ActuEstado("arrancado");
            ArrancarBGW("defaul");
        }

        private void btnDetenerActu_Click(object sender, EventArgs e)
        {
            ActuEstado("detenido");
            DetenerBGW();
        }

        private void frmGUI_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!CerradoForm())
            {
                e.Cancel = false;
            }
        }

        private void wrkMsgPID_DoWork(object sender, DoWorkEventArgs e)
        {
            RecuperarPID(sender, e, "listbox");
        }

        #endregion


    }
}
