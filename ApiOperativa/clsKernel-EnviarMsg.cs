using System;
using System.Messaging;

namespace KernelSistema
{
    public class clsPasoMensajes
    {

        #region [Atributos]

        private String strTipoMensaje;
        private String strTipoCalc;
        private String strError;
        private String strOrigen;
        private String strComando;
        private int intIdProceso;
        private int intCodTerm;
        clsKernel objKernelArranque;
        private Message mensaje;
        private MessageQueue objEnviaMensaje;
        private MsgRecibe msgRecibe;

        #endregion

        #region [Constructor]

        public clsPasoMensajes()
        {
            this.strTipoMensaje = "";
            this.strError = "";
            this.strTipoCalc = "";
            this.strOrigen = "";
            this.strComando = "";
            this.intCodTerm = 0;
            this.intIdProceso = 0;
            this.mensaje = new Message();
            this.msgRecibe = new MsgRecibe();
        }

        #endregion

        #region [Propiedades]

        public String TipoMensaje { set => strTipoMensaje = value; }
        public String TipoCalc { set => strTipoCalc = value; }
        public String Origen { set => strOrigen = value; }
        public String Comando { set => strComando = value; }
        public int IdProceso { set => intIdProceso = value; }
        public int CodTerm { set => intCodTerm = value; }
        public String Error { get => strError; }

        #endregion

        #region [Métodos Públicos]

        public bool EnviarMsg()
        {
            try
            {
                this.objKernelArranque = new clsKernel();
                this.objKernelArranque.RecuperaPID("pasomensaje");
                this.msgRecibe.intPID = this.objKernelArranque.IdProceso;

                switch (strTipoMensaje.ToLower())
                {
                    case "operacion-exito":
                        this.objEnviaMensaje = new MessageQueue(clsConstantes.strRutaCanalMensajes);
                        msgRecibe.intCodTerm = this.intCodTerm;
                        msgRecibe.strComando = strTipoMensaje;
                        msgRecibe.strOrigen = this.strOrigen;
                        msgRecibe.strMensaje = "operación Exitosa";
                        mensaje.Body = msgRecibe;
                        objEnviaMensaje.Send(mensaje);
                        break;
                    case "started":
                        this.objEnviaMensaje = new MessageQueue(clsConstantes.strRutaCanalPID);
                        msgRecibe.intCodTerm = 0;
                        msgRecibe.strComando = "started";
                        msgRecibe.strOrigen = this.strOrigen;
                        msgRecibe.strMensaje = "aplicación iniciada";
                        mensaje.Body = msgRecibe;
                        objEnviaMensaje.Send(mensaje);
                        break;
                    case "stop":
                        this.objEnviaMensaje = new MessageQueue(clsConstantes.strRutaCanalPID);
                        this.intCodTerm = 3;
                        this.msgRecibe.strComando = "stop";
                        this.msgRecibe.strOrigen = this.strOrigen;
                        this.msgRecibe.strMensaje = "módulo detenido";
                        mensaje.Body = msgRecibe;
                        objEnviaMensaje.Send(mensaje);
                        break;
                    default:
                        break;
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

    }
}
