using System;
using System.Windows.Forms;
using KernelSistema;
using System.Threading;
using System.ComponentModel;
using System.Linq;

namespace frmGUI
{
    public partial class frmGUI : Form
    {

        #region [Atributos]

        private clsKernel objKernel;
        private clsCerradoInstancias objCerrarInstancia;
        private clsPasoMensajes objPasoMensajes;
        private clsRecibeMensajes objRecibeMsg;

        private bool estaArrancado;
        private int intCantPIDActivos;

        private String[] strMensajesLista;
        private String[] strCalcsLista;
        private int[] intPIDsActivos;

        #endregion

        #region [Constructor]

        public frmGUI()
        {
            InitializeComponent();

            this.objKernel = new clsKernel();
            this.objCerrarInstancia = new clsCerradoInstancias();
            this.objPasoMensajes = new clsPasoMensajes();

            this.estaArrancado = false;
            this.intCantPIDActivos = 0;

            this.strMensajesLista = new string[1];
            this.strCalcsLista = new string[1];
            this.intPIDsActivos = new int[1];


            this.wrkMsgTextBox.WorkerReportsProgress = true;
            this.wrkMsgTextBox.WorkerSupportsCancellation = true;

            ArrancarBGW("arranque");
        }

        #endregion

        #region [Métodos Privados]

        #region Métodos Listos

        private void NuevaInstancia()
            {
                objKernel.LanzaForm("mod-aplicaciones");
            }

        private void ArrancarBGW(String tipo)
            {
                switch (tipo)
                {
                    case "arranque":
                        wrkMsgTextBox.RunWorkerAsync();
                        wrkMsgPID.RunWorkerAsync();
                        wrkArranque.RunWorkerAsync();
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

        private void DetenerBGW()
            {
                if (wrkMsgTextBox.WorkerSupportsCancellation)
                {
                    wrkMsgTextBox.CancelAsync();
                }
            }

        private bool EnviarMensaje(String tipoMsg)
            {
                try
                {
                    switch (tipoMsg.ToLower())
                    {
                        case "listo":
                            objPasoMensajes.TipoMensaje = "started";
                            objPasoMensajes.Origen = this.Text;
                            break;
                        default:
                            break;
                    }
                    if (!objPasoMensajes.EnviarMsg())
                    {
                        MessageBox.Show(objPasoMensajes.Error, "Módulo Principal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Módulo Principal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }

        private bool EstaListo()
            {
                CheckForIllegalCrossThreadCalls = false;
                this.txtMensajes.Text = "Cargando Formulario...";
                this.Enabled = false;
                Random espera = new Random();
                Thread.Sleep(espera.Next(1000, 3000));
                this.txtMensajes.Text = "";
                this.Enabled = true;
                wrkArranque.CancelAsync();
                return true;
            }

        #endregion

        private void MostrarMensaje(String tipo)
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
        }

        private void RecuperarMensajes(object sender, DoWorkEventArgs e, String tipo)
        {
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
                        MostrarMensaje(tipo);
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
            BackgroundWorker wrkMsgPID = sender as BackgroundWorker;
            CheckForIllegalCrossThreadCalls = false;
            int[] intInstanciasCalc = new int[1];
            int contador = 0, contadorCalcs = 0;

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
                                this.intPIDsActivos = this.intPIDsActivos.Where(val => val != objRecibeMsg.MensajeRetorno.intPID).ToArray();
                                this.intCantPIDActivos--;
                                this.lstPIDActuales.Items.Remove(objRecibeMsg.MensajeCerrado);
                                strMensajesLista = strMensajesLista.Where(val => val != objRecibeMsg.MensajeCerrado).ToArray();
                                contador--;
                                switch (objRecibeMsg.MensajeRetorno.strOrigen.ToLower())
                                {
                                    case "calculadora de suma":
                                    case "calculadora de resta":
                                    case "calculadora de multiplicación":
                                    case "calculadora de división":
                                        intInstanciasCalc = intInstanciasCalc.Where(val => val != objRecibeMsg.MensajeRetorno.intPID).ToArray();
                                        contadorCalcs--;
                                        break;
                                    default:
                                        break;
                                }
                            }
                            if (objRecibeMsg.MensajeRetorno.strComando == "started")
                            {
                                this.intPIDsActivos[intCantPIDActivos] = objRecibeMsg.MensajeRetorno.intPID;
                                Array.Resize<int>(ref intPIDsActivos, intPIDsActivos.Length + 1);
                                this.intCantPIDActivos++;
                                this.lstPIDActuales.Items.Add(objRecibeMsg.Mensaje);
                                this.lstHistorialPIDs.Items.Add(objRecibeMsg.Mensaje);
                                this.lstPIDActuales.SelectedIndex = lstPIDActuales.Items.Count - 1;
                                this.lstHistorialPIDs.SelectedIndex = lstHistorialPIDs.Items.Count - 1;
                                strMensajesLista[contador] = objRecibeMsg.Mensaje;
                                contador++;



                                Array.Resize<String>(ref strMensajesLista, strMensajesLista.Length+1);
                                switch (objRecibeMsg.MensajeRetorno.strOrigen.ToLower())
                                {
                                    case "calculadora de suma":
                                    case "calculadora de resta":
                                    case "calculadora de multiplicación":
                                    case "calculadora de división":
                                        Array.Resize<int>(ref intInstanciasCalc, intInstanciasCalc.Length+1);
                                        intInstanciasCalc[contadorCalcs] = objRecibeMsg.MensajeRetorno.intPID;
                                        Array.Resize<String>(ref strCalcsLista, strCalcsLista.Length + 1);
                                        strCalcsLista[contadorCalcs] = objRecibeMsg.Mensaje;
                                        contadorCalcs++;
                                        break;
                                    default:
                                        break;
                                }
                            }
                            if (objRecibeMsg.MensajeRetorno.strComando == "stop-all-calc")
                            {
                                objCerrarInstancia.PIDS = intInstanciasCalc;

                                foreach (string calcViva in strCalcsLista)
                                {
                                    this.lstPIDActuales.Items.Remove(calcViva);
                                }
                                foreach (int pid in intPIDsActivos)
                                {
                                    this.intPIDsActivos = this.intPIDsActivos.Where(val => val != pid).ToArray();
                                    this.intCantPIDActivos--;
                                }
                                intInstanciasCalc = null;
                                intInstanciasCalc = new int[1];
                                strCalcsLista = null;
                                strCalcsLista = new string[1];
                                contadorCalcs = 0;
                                this.intPIDsActivos = new int[1];
                                this.intCantPIDActivos = 0;
                                objCerrarInstancia.Cerrar("stop-all-calc");
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



        private void FinalizarPrograma(String tipo)
        {
            objKernel.RecuperaPID("maestro");
            this.intPIDsActivos = this.intPIDsActivos.Where(val => val != objKernel.IdProceso).ToArray();
            objCerrarInstancia.PIDS = this.intPIDsActivos;
            objCerrarInstancia.Cerrar(tipo);
        }

        private bool CerradoForm()
        {
            if (objCerrarInstancia.ConfirmaCerrado("gui"))
            {
                return true;
            }
            else return false;
        }

        #endregion

        #region [Eventos]

        #region Eventos Listos

        private void tsmiNuevaInstancia_Click(object sender, EventArgs e)
        {
            NuevaInstancia();
        }

        private void bgwRecibeMensajes_DoWork(object sender, DoWorkEventArgs e)
        {
            RecuperarMensajes(sender, e, "textbox");
        }

        private void wrkMsgPID_DoWork(object sender, DoWorkEventArgs e)
        {
            RecuperarPID(sender, e, "listbox");
        }

        private void wrkArranque_DoWork(object sender, DoWorkEventArgs e)
        {
            if (EstaListo())
            {
                EnviarMensaje("listo");
            }
        }

        private void btnActuMsgs_Click(object sender, EventArgs e)
        {
            ActuEstado("arrancado");
            ArrancarBGW("default");
        }

        private void btnDetenerActu_Click(object sender, EventArgs e)
        {
            ActuEstado("detenido");
            DetenerBGW();
        }

        #endregion

        private void frmGUI_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!CerradoForm())
            {
                e.Cancel = true;
            }
            else
            {
                FinalizarPrograma("stop-all-calc");
            }
        }

        private void tsmiCerrarCalcs_Click(object sender, EventArgs e)
        {
            if (!CerradoForm())
            {
            }
            else
            {
                FinalizarPrograma("calc");
            }
        }

        private void tsmiCerrarModApps_Click(object sender, EventArgs e)
        {

        }

        #endregion


    }
}
