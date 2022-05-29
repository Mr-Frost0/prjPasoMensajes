using System;
using System.Messaging;

namespace KernelSistema
{
    public class clsPasoMensajes
    {

        #region [Atributos]

        private String strTipoMensaje;
        private String strError;
        private String strOrigen;
        private int intCodTerm;
        private clsKernel_Arranque objKernelArranque;
        private Message mensaje;
        private MessageQueue objEnviaMensaje;
        private MessageQueue objEnviaMensaje2;
        private MsgRecibe msgRecibe;

        #endregion

        #region [Constructor]

        public clsPasoMensajes()
        {
            this.strTipoMensaje = "";
            this.strError = "";
            this.strOrigen = "";
            this.intCodTerm = 0;
            this.mensaje = new Message();
            this.msgRecibe = new MsgRecibe();
        }

        #endregion

        #region [Propiedades]

        public String TipoMensaje { set => strTipoMensaje = value; }
        public String Origen { set => strOrigen = value; }
        public int CodTerm { set => intCodTerm = value; }
        public String Error { get => strError; }

        #endregion

        #region [Métodos Públicos]

        public bool EnviarMsg()
        {
            try
            {
                this.objKernelArranque = new clsKernel_Arranque();
                this.objKernelArranque.RecuperaPID("pasomensaje");
                this.msgRecibe.intPID = this.objKernelArranque.IdProceso;

                switch (strTipoMensaje.ToLower())
                {
                    case "operacion-exito":
                        this.objEnviaMensaje = new MessageQueue(clsConstantes.strRutaCanalMensajes);
                        msgRecibe.intCodTerm = 0;
                        msgRecibe.intCodTerm = this.intCodTerm;
                        msgRecibe.strComando = strTipoMensaje;
                        msgRecibe.strOrigen = this.strOrigen;
                        msgRecibe.strMensaje = "operación Exitosa";
                        mensaje.Body = msgRecibe;
                        objEnviaMensaje.Send(mensaje);
                        break;
                    case "started":
                        this.objEnviaMensaje = new MessageQueue(clsConstantes.strRutaCanalPID);
                        this.objEnviaMensaje2 = new MessageQueue(clsConstantes.strRutaCanalMensajes);
                        msgRecibe.intCodTerm = 0;
                        msgRecibe.strComando = "started";
                        msgRecibe.strOrigen = this.strOrigen;
                        msgRecibe.strMensaje = "aplicación iniciada - lista";
                        mensaje.Body = msgRecibe;
                        objEnviaMensaje.Send(mensaje);
                        objEnviaMensaje2.Send(mensaje);
                        break;
                    case "stop":
                        this.objEnviaMensaje = new MessageQueue(clsConstantes.strRutaCanalPID);
                        this.objEnviaMensaje2 = new MessageQueue(clsConstantes.strRutaCanalMensajes);
                        this.msgRecibe.intCodTerm = 3;
                        this.msgRecibe.strComando = "stop";
                        this.msgRecibe.strOrigen = this.strOrigen;
                        this.msgRecibe.strMensaje = "módulo detenido";
                        mensaje.Body = msgRecibe;
                        objEnviaMensaje.Send(mensaje);
                        objEnviaMensaje2.Send(mensaje);
                        break;
                    case "stop-all-calc":
                        this.objEnviaMensaje = new MessageQueue(clsConstantes.strRutaCanalPID);
                        this.objEnviaMensaje2 = new MessageQueue(clsConstantes.strRutaCanalMensajes);
                        this.msgRecibe.intCodTerm = 4;
                        this.msgRecibe.strComando = "stop-all-calc";
                        this.msgRecibe.strOrigen = this.strOrigen;                        
                        this.msgRecibe.strMensaje = "todos los módulos de calculadora detenidos";
                        mensaje.Body = msgRecibe;
                        objEnviaMensaje.Send(mensaje);
                        objEnviaMensaje2.Send(mensaje);
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
