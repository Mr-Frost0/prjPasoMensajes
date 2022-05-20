using System;
using System.Messaging;
using System.Text;

namespace KernelSistema
{

    public class clsRecibeMensajes
    {

        #region [Atributos]

        private MessageQueue msgCola;
        private String strMensaje;
        private String strError;
        private MsgRecibe msgRecibe;
        private Object o;
        private Type[] arrTipos;
        private StringBuilder sb;

        #endregion

        #region [Constructor]

        public clsRecibeMensajes()
        {
            this.strMensaje = "";
            this.strError = "";
            this.msgCola = new MessageQueue(clsConstantes.strRutaCanalMensajes);
            this.msgRecibe = new MsgRecibe();
            this.o = new Object();
            this.arrTipos = new Type[2];
            this.arrTipos[0] = msgRecibe.GetType();
            this.arrTipos[1] = o.GetType();
            this.msgCola.Formatter = new XmlMessageFormatter(arrTipos);
            this.sb = new StringBuilder();
        }

        #endregion

        #region [Propiedades]

        public String Mensaje { get => strMensaje; }
        public String Error { get => strError; }

        #endregion

        #region [Métodos Privados]



        #endregion

        #region [Métodos Públicos]

        public bool RecibirMsg(String tipoRecibida)
        {
            try
            {
                msgRecibe = ((MsgRecibe)msgCola.Receive().Body);
                switch (tipoRecibida.ToLower())
                {
                    case "operacion-exito":
                        strMensaje = sb.Append("Mensaje de: " + msgRecibe.strOrigen + "; [" + msgRecibe.intPID + "]; cod:" + msgRecibe.intCodTerm + "; cmd:" + msgRecibe.strComando + "; msg: " + msgRecibe.strMensaje).ToString() + ";";
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
