using System;
using System.Messaging;
using System.Collections.Generic;
using System.Text;

namespace KernelSistema
{

    public class clsRecibeMensajes
    {

        #region [Atributos]


        private MessageQueue msgCola;
        private String strMensaje;
        private String strMsgCerrado;
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
            this.strMsgCerrado = "";
            this.msgRecibe = new MsgRecibe();
            this.o = new Object();
            this.arrTipos = new Type[2];
            this.arrTipos[0] = msgRecibe.GetType();
            this.arrTipos[1] = o.GetType();
            this.sb = new StringBuilder();
        }

        #endregion

        #region [Propiedades]

        public String Mensaje { get => strMensaje; }
        public String MensajeCerrado { get => strMsgCerrado; }
        public MsgRecibe MensajeRetorno { get => msgRecibe; }
        public String Error { get => strError; }

        #endregion


        #region [Métodos Públicos]

        public bool RecibirMsg(String tipoRecibida)
        {
            try
            {
                switch (tipoRecibida.ToLower())
                {
                    case "textbox":
                        this.msgCola = new MessageQueue(clsConstantes.strRutaCanalMensajes);
                        this.msgCola.Formatter = new XmlMessageFormatter(arrTipos);
                        msgRecibe = ((MsgRecibe)msgCola.Receive().Body);
                        strMensaje = sb.Append("Mensaje de: " + msgRecibe.strOrigen + "; [" + msgRecibe.intPID + "]; cod:" + msgRecibe.intCodTerm + "; cmd:" + msgRecibe.strComando + "; msg: " + msgRecibe.strMensaje).ToString() + ";";
                        return true;
                    case "listbox":
                        this.msgCola = new MessageQueue(clsConstantes.strRutaCanalPID);
                        this.msgCola.Formatter = new XmlMessageFormatter(arrTipos);
                        msgRecibe = ((MsgRecibe)msgCola.Receive().Body);
                        if (msgRecibe.strComando == "stop")
                        {
                            strMsgCerrado = sb.Append("[" + msgRecibe.intPID + "] " + msgRecibe.strOrigen).ToString();
                        }
                        else if (msgRecibe.strComando == "started")
                        {
                            strMensaje = sb.Append("[" + msgRecibe.intPID + "] " + msgRecibe.strOrigen).ToString();
                            return true;
                        }
                        return true;
                    default:
                        return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

    }

}
