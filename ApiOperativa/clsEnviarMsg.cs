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
        private int intIdProceso;
        clsKernel objApiOperativa;
        private Message mensaje;
        private MessageQueue objEnviaMensaje;
        private MsgRecibe msgRecibe;

        #endregion

        #region [Constructor]

        public clsPasoMensajes()
        {
            this.strTipoMensaje = "";
            this.strError = "";
            this.TipoCalc = "";
            this.mensaje = new Message();
            this.objEnviaMensaje = new MessageQueue(clsConstantes.strRutaCnlPrivMsg);
            this.objApiOperativa = new clsKernel();
            this.objApiOperativa.RecovPID("pasomensaje");
            this.intIdProceso = objApiOperativa.IdProcMaestro;
            this.msgRecibe = new MsgRecibe();
        }

        #endregion

        #region [Propiedades]

        public String TipoMensaje { set => strTipoMensaje = value; }
        public String TipoCalc { set => strTipoCalc = value; }
        public String Error { get => strError; }

        #endregion

        #region [Métodos Privados]



        #endregion

        #region [Métodos Públicos]

        public bool EnviarMsg()
        {
            try
            {
                switch (strTipoMensaje.ToLower())
                {
                    //Enviar mensaje con operación exitosa, independientemente del tipo de operación

                    case "operacion-exito":
                        msgRecibe.mensaje = "La calculadora de " + strTipoCalc + ", con el PID número [" + intIdProceso + "] ha hecho una operación con éxito";
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
