using System;
using System.Windows.Forms;
using KernelSistema;
using System.Threading;
using System.Linq;
using clsMatarMaestro;

namespace frmGUI
{
    public partial class frmGUI : Form
    {

        #region [Atributos]

        private Thread recibeMensajes;
        private Thread cargaForm;
        private Thread verProcesosVivos;

        private clsKernel_GestorArchivos GA;
        private clsKernel_Arranque objKernelArranque;
        private clsCerradoInstancias objCerrarInstancia;
        private clsPasoMensajes objPasoMensajes;
        private clsColasMensajes objColasMensajes;

        private bool estaArrancado;
        private bool hiloMensajesVivo;
        private int contadorCalcs;

        private String[] strLstProcesosVivos;
        private String[] strCalcsLista;
        private int[] intPIDsActivos;
        private int[] intInstanciasCalc;

        #endregion

        #region [Constructor]

        public frmGUI()
        {
            InitializeComponent();

            this.cargaForm = new Thread(ThCargaForm);
            this.verProcesosVivos = new Thread(ThVerProcesosActivos);

            this.GA = new clsKernel_GestorArchivos();
            this.objKernelArranque = new clsKernel_Arranque();
            this.objCerrarInstancia = new clsCerradoInstancias();
            this.objPasoMensajes = new clsPasoMensajes();
            this.objColasMensajes = new clsColasMensajes();

            this.hiloMensajesVivo = true;
            this.estaArrancado = false;
            this.contadorCalcs = 0;

            this.strLstProcesosVivos = new string[1];
            this.strCalcsLista = new string[1];
            this.intPIDsActivos = new int[1];
            this.intInstanciasCalc = new int[1];

            ColasMensajes("crear");
            ArrancaHilos("arranque");
            GA.registrar("inicio");
        }

        #endregion

        #region [Métodos Privados]

        #region "Métodos Hilo Arranque"

        private void ThCargaForm()
        {
            if (ThEstaListo())
            {
                EnviarMensaje("listo");
                cargaForm.Abort();
            }
        }

        private bool ThEstaListo()
        {
            Action a = () => this.txtMensajes.Text = "Cargando Formulario...";
            Action b = () => this.Enabled = false;
            Random espera = new Random();
            Action c = () => this.txtMensajes.Text = "";
            Action d = () => this.Enabled = true;
            Thread.Sleep(500);
            Invoke(a);
            Invoke(b);
            Thread.Sleep(espera.Next(500, 2500));
            Invoke(c);
            Invoke(d);
            return true;
        }

        #endregion

        #region "Métodos Hilo Formulario"

        private void ColasMensajes(string s)
        {
            try
            {
                switch (s.ToLower())
                {
                    case "crear":
                        objColasMensajes.CrearColas();
                        break;
                    case "borrar":
                        objColasMensajes.BorrarColas();
                        break;
                    default:
                        break;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.Message, "Fallo de creación de colas de mensajes", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void NuevoModuloApps()
        {
            objKernelArranque.LanzaForm("mod-aplicaciones");
        }

        private bool CerradoForm()
        {
            if (objCerrarInstancia.ConfirmaCerrado("gui"))
            {
                return true;
            }
            else return false;
        }

        private void ArrancaHilos(String tipo)
        {
            switch (tipo)
            {
                case "arranque":
                    this.recibeMensajes = new Thread(ThMuestraMensajes);
                    recibeMensajes.Start();
                    cargaForm.Start();
                    verProcesosVivos.Start();
                    break;
                case "default":
                    if (recibeMensajes == null)
                    {
                        this.recibeMensajes = new Thread(ThMuestraMensajes);
                        this.hiloMensajesVivo = true;
                        recibeMensajes.Start();
                    }
                    break;
                default:
                    break;
            }
        }

        private void DetenerHiloMensajes()
        {
            if (hiloMensajesVivo)
            {
                if (recibeMensajes.IsAlive)
                {
                    estaArrancado = false;
                    this.hiloMensajesVivo = false;
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

        private void FinalizarPrograma(String tipo)
        {
            switch (tipo.ToLower())
            {
                case "stop-all-calc":
                    objCerrarInstancia.PIDS = intInstanciasCalc;
                    objCerrarInstancia.Cerrar("stop-all-calc");
                    for (int i = 0; i < intInstanciasCalc.Length - 1; i++)
                    {
                        this.intPIDsActivos = this.intPIDsActivos.Where(val => val != intInstanciasCalc[i]).ToArray();
                    }
                    break;
                case "modapps-calcs":
                    objKernelArranque.RecuperaPID("maestro");
                    Action borraPIDMaestro = () => this.intPIDsActivos = this.intPIDsActivos.Where(val => val != objKernelArranque.IdProceso).ToArray();
                    Invoke(borraPIDMaestro);
                    Action igualaPIDs = () => objCerrarInstancia.PIDS = this.intPIDsActivos;
                    Invoke(igualaPIDs);
                    objCerrarInstancia.Cerrar(tipo);
                    String pidMaestro = lstHistorialPIDs.Items[0].ToString();
                    Action renuevLst = () => this.lstPIDActuales.Items.Clear();
                    Invoke(renuevLst);
                    Action renuevLst2 = () => this.lstPIDActuales.Items.Add(pidMaestro);
                    Invoke(renuevLst2);
                    intPIDsActivos = null;
                    intPIDsActivos = new int[1];
                    break;
                default:
                    break;
            }
            foreach (String calcViva in strCalcsLista)
            {
                Action elimLstCalcsVivas = () => this.lstPIDActuales.Items.Remove(calcViva);
                Invoke(elimLstCalcsVivas);
            }
            intInstanciasCalc = null;
            intInstanciasCalc = new int[1];
            strCalcsLista = null;
            strCalcsLista = new string[1];
            contadorCalcs = 0;
        }

        #endregion

        #region "Métodos Hilo Recuperación Mensajes"

        private void ThMuestraMensajes()
        {
            while (this.hiloMensajesVivo)
            {
                estaArrancado = true;
                ThRecuperaMensajes();
            }
            recibeMensajes = null;
        }

        private void ThRecuperaMensajes()
        {
            clsRecibeMensajes objRecibeMsg = new clsRecibeMensajes();

            if (!objRecibeMsg.RecibirMsg("textbox"))
            {
                MessageBox.Show(objRecibeMsg.Error, "Final Sistemas Operativos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            GA.Mensaje = objRecibeMsg.Mensaje;
            GA.PID = objRecibeMsg.MensajeRetorno.intPID;
            GA.Origen = objRecibeMsg.MensajeRetorno.strOrigen;
            if (!GA.registrar("registros"))
            {
                MessageBox.Show("Error:" + GA.Error, "Gestor Archivos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            } 
            Action nuevoMsg = () => this.txtMensajes.AppendText(objRecibeMsg.Mensaje + Environment.NewLine);

            Invoke(nuevoMsg);
            objRecibeMsg = null;
        }

        #endregion

        #region "Métodos Hilo Recuperación Procesos Vivos"

        private void ThVerProcesosActivos()
        {
            while (true)
            {
                clsRecibeMensajes objRecibeMsg = new clsRecibeMensajes();
                try
                {
                    objRecibeMsg.RecibirMsg("listbox");
                    String strMensaje = objRecibeMsg.Mensaje;
                    MsgRecibe mensajeRecibido = objRecibeMsg.MensajeRetorno;
                    ThRecuperarProcesosActivos(mensajeRecibido, objRecibeMsg.Mensaje, objRecibeMsg.MensajeCerrado, strMensaje);
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

        private void ThRecuperarProcesosActivos(MsgRecibe msgRecibe, String Mensaje, String MensajeCerrado, String strMensaje)
        {
            int intCalcVivas = 0;

            switch (msgRecibe.strComando.ToLower())
            {
                case "started":
                    Action guardaPID = () => this.intPIDsActivos[intPIDsActivos.Length - 1] = msgRecibe.intPID;
                    Action agrandaLista = () => Array.Resize<int>(ref intPIDsActivos, intPIDsActivos.Length + 1);
                    Action muestraPIDActu = () => this.lstPIDActuales.Items.Add(Mensaje);
                    Action muestraHistoPID = () => this.lstHistorialPIDs.Items.Add(Mensaje);
                    Invoke(guardaPID); Invoke(agrandaLista); Invoke(muestraPIDActu); Invoke(muestraHistoPID);
                    Action selecIndActu = () => this.lstPIDActuales.SelectedIndex = lstPIDActuales.Items.Count - 1;
                    Action selecIndHisto = () => this.lstHistorialPIDs.SelectedIndex = lstHistorialPIDs.Items.Count - 1;
                    Invoke(selecIndActu); Invoke(selecIndHisto);
                    Action actuStrVivos = () => this.strLstProcesosVivos[strLstProcesosVivos.Length - 1] = Mensaje;
                    Action agrandaStrVivos = () => Array.Resize<String>(ref strLstProcesosVivos, strLstProcesosVivos.Length + 1);
                    Invoke(actuStrVivos); Invoke(agrandaStrVivos);
                    switch (msgRecibe.strOrigen.ToLower())
                    {
                        case "calculadora de suma":
                        case "calculadora de resta":
                        case "calculadora de multiplicación":
                        case "calculadora de división":
                            intCalcVivas = 1;
                            break;
                        default:
                            break;
                    }
                    break;
                case "stop":
                    Action elimPID = () => this.intPIDsActivos = this.intPIDsActivos.Where(val => val != msgRecibe.intPID).ToArray();
                    Action elimPIDActual = () => this.lstPIDActuales.Items.Remove(MensajeCerrado);
                    Action elimStrPIDLst = () => strLstProcesosVivos = strLstProcesosVivos.Where(val => val != MensajeCerrado).ToArray();
                    Invoke(elimPID); Invoke(elimPIDActual); Invoke(elimStrPIDLst);
                    switch (msgRecibe.strOrigen.ToLower())
                    {
                        case "calculadora de suma":
                        case "calculadora de resta":
                        case "calculadora de multiplicación":
                        case "calculadora de división":
                            intCalcVivas = 2;
                            break;
                        default:
                            break;
                    }
                    break;
                case "stop-all-calc":
                    objCerrarInstancia.PIDS = intInstanciasCalc;
                    objCerrarInstancia.Cerrar("stop-all-calc");
                    foreach (String calcViva in strCalcsLista)
                    {
                        Action elimLstCalcsVivas = () => this.lstPIDActuales.Items.Remove(calcViva);
                        Invoke(elimLstCalcsVivas);
                    }
                    for (int i = 0; i < intInstanciasCalc.Length - 1; i++)
                    {
                        this.intPIDsActivos = this.intPIDsActivos.Where(val => val != intInstanciasCalc[i]).ToArray();
                    }
                    intInstanciasCalc = null;
                    intInstanciasCalc = new int[1];
                    strCalcsLista = null;
                    strCalcsLista = new string[1];
                    contadorCalcs = 0;
                    break;
            }
            switch (intCalcVivas)
            {
                case 1:
                    Action agrandaArrInstVivas = () => Array.Resize<int>(ref intInstanciasCalc, intInstanciasCalc.Length + 1);
                    Action guardaInstcalcViv = () => intInstanciasCalc[contadorCalcs] = msgRecibe.intPID;
                    Action agrandaLstCalcs = () => Array.Resize<String>(ref strCalcsLista, strCalcsLista.Length + 1);
                    Action guardaLstCalcs = () => strCalcsLista[contadorCalcs] = strMensaje;
                    Action aumeContCalcs = () => contadorCalcs++;
                    Invoke(agrandaArrInstVivas); Invoke(guardaInstcalcViv); Invoke(agrandaLstCalcs); Invoke(guardaLstCalcs); Invoke(aumeContCalcs);
                    break;
                case 2:
                    intInstanciasCalc = intInstanciasCalc.Where(val => val != msgRecibe.intPID).ToArray();
                    contadorCalcs--;
                    break;
            }
        }

        #endregion

        #endregion

        #region [Eventos]

        private void btnActuMsgs_Click(object sender, EventArgs e)
        {
            ActuEstado("arrancado");
            ArrancaHilos("default");
        }

        private void btnDetenerActu_Click(object sender, EventArgs e)
        {
            ActuEstado("detenido");
            DetenerHiloMensajes();
        }

        private void tsmiNuevaInstancia_Click(object sender, EventArgs e)
        {
            NuevoModuloApps();
        }

        private void tsmiCerrarCalcs_Click(object sender, EventArgs e)
        {
            if (CerradoForm())
            {
                if (this.intInstanciasCalc.Length > 2)
                {
                    FinalizarPrograma("stop-all-calc");
                }
                else
                {
                    MessageBox.Show("No hay módulos que cerrar", "Final Sistemas Operativos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void tsmiCerrarModApps_Click(object sender, EventArgs e)
        {
            if (CerradoForm())
            {
                if (this.intPIDsActivos.Length > 2)
                {
                    FinalizarPrograma("modapps-calcs");
                }
                else
                {
                    MessageBox.Show("No hay módulos que cerrar", "Final Sistemas Operativos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void frmGUI_FormClosing(object sender, FormClosingEventArgs e)        
        {
            if (!CerradoForm())
            {
                e.Cancel = true;
            }
            else
            {
                DetenerHiloMensajes();
                verProcesosVivos.Abort();
                if (this.intPIDsActivos.Length > 2)
                {
                    FinalizarPrograma("modapps-calcs");
                }
            }
        }

        private void frmGUI_FormClosed(object sender, FormClosedEventArgs e)
        {
            GA.registrar("finalizar");
            ColasMensajes("borrar");
            clsMatarPrograma autoSuicidarse = new clsMatarPrograma();
            objKernelArranque.RecuperaPID("maestro");
            autoSuicidarse.PIDMaestro = objKernelArranque.IdProceso;
            autoSuicidarse.MatarMaestro();
        }

        #endregion

    }
}
