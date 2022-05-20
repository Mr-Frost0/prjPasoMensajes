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
                switch (strTipoMensaje.ToLower())
                {
                    case "operacion-exito":
                        this.objEnviaMensaje = new MessageQueue(clsConstantes.strRutaCanalMensajes);
                        this.objKernelArranque.RecuperaPID("pasomensaje");
                        msgRecibe.intCodTerm = this.intCodTerm;
                        msgRecibe.strOrigen = this.strOrigen;
                        msgRecibe.strComando = strTipoMensaje;
                        msgRecibe.strMensaje = "Operación Exitosa";
                        msgRecibe.intPID = objKernelArranque.IdProceso;
                        mensaje.Body = msgRecibe;
                        objEnviaMensaje.Send(mensaje);
                        break;
                    case "enviar-pid":
                        this.objEnviaMensaje = new MessageQueue(clsConstantes.strRutaCanalPID);
                        msgRecibe.intPID = this.objKernelArranque.IdProceso;
                        msgRecibe.strOrigen = this.strOrigen;
                        mensaje.Body = msgRecibe;
                        objEnviaMensaje.Send(mensaje);
                        break;
                    case "fin-frmCalculadora":
                        msgRecibe.strMensaje = "";
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
