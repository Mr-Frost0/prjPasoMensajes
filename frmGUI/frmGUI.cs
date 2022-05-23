using System;
using System.Windows.Forms;
using KernelSistema;
using System.Threading;
using System.ComponentModel;
using System.Collections.Generic;

namespace frmGUI
{
    public partial class frmGUI : Form
    {

        #region [Atributos]

        private clsKernel objKernel;
        private clsCerrarPorPID objCerrarInstancia;
        private bool estaArrancado = false;
        private int[] intPIDsActivos;
        private int intCantPIDActivos = 0;

        #endregion

        #region [Constructor]

        public frmGUI()
        {
            InitializeComponent();
            this.objKernel = new clsKernel();
            this.objCerrarInstancia = new clsCerrarPorPID();
            this.intPIDsActivos = new int[1];
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
            clsRecibeMensajes objRecibeMsg;
            wrkMsgTextBox = sender as BackgroundWorker;
            CheckForIllegalCrossThreadCalls = false;

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
            clsRecibeMensajes objRecibeMsg;
            BackgroundWorker wrkMsgPID = sender as BackgroundWorker;
            CheckForIllegalCrossThreadCalls = false;
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
                            if (objRecibeMsg.MensajeRetorno.strComando == "stop")
                            {
                                this.intPIDsActivos[intCantPIDActivos] = objRecibeMsg.MensajeRetorno.intPID;
                                this.intCantPIDActivos--;
                                this.lstPIDActuales.Items.Remove(objRecibeMsg.Mensaje);
                                this.txtMensajes.Text += objRecibeMsg.Mensaje;
                            }
                            if (objRecibeMsg.MensajeRetorno.strComando == "started")
                            {
                                this.lstPIDActuales.Items.Add(objRecibeMsg.Mensaje);
                                this.lstHistorialPIDs.Items.Add(objRecibeMsg.Mensaje);
                                this.lstPIDActuales.SelectedIndex = lstPIDActuales.Items.Count - 1;
                                this.lstHistorialPIDs.SelectedIndex = lstHistorialPIDs.Items.Count - 1;
                                Array.Resize<int>(ref intPIDsActivos, intPIDsActivos.Length+1);
                                this.intCantPIDActivos++;
                            }                            
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
            RecuperarMensajes(sender, e, "textbox");
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
