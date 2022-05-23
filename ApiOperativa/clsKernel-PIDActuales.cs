using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KernelSistema
{
    public class clsKernel_PIDActuales
    {
        #region [Atributos]

        private List<String> lstProcesosActivos;
        private MsgRecibe msgRecibe;
        private String strMensaje;
        private bool blnSeElimino;
        private StringBuilder sb;

        #endregion

        #region [Constructor]

        public clsKernel_PIDActuales()
        {
            this.lstProcesosActivos = new List<string>();
            this.blnSeElimino = false;
            this.msgRecibe = new MsgRecibe();
            this.sb = new StringBuilder();
        }

        #endregion

        #region [Propiedades]

        public List<String> ProcesosActivos { get => lstProcesosActivos; }
        public MsgRecibe MensajeRecibe { set => msgRecibe = value; }
        public String Mensaje { get => strMensaje; }
        public bool SeElimino { get => blnSeElimino; }

        #endregion

        #region [Métodos Privados]

        private bool AlmacenarPID()
        {
            try
            {
                this.lstProcesosActivos.Add(strMensaje);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool EliminarPID()
        {
            try
            {
                this.lstProcesosActivos.Remove(strMensaje);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region [Métodos Públicos]

        public bool ProcesarPID()
        {
            if (msgRecibe.strComando == "stop")
            {
                this.blnSeElimino = true;
                if (msgRecibe.intCodTerm == 0)
                {
                    EliminarPID();
                }
                else if (msgRecibe.intCodTerm == 2)
                {
                    EliminarPID();
                }
                strMensaje = sb.Append("Mensaje de: " + msgRecibe.strOrigen + "; [" + msgRecibe.intPID + "]; cod:" + msgRecibe.intCodTerm + "; cmd:" + msgRecibe.strComando + "; msg: " + msgRecibe.strMensaje + ";").ToString();
                return true;
            }
            else if (msgRecibe.strComando == "started")
            {
                strMensaje = sb.Append("[" + msgRecibe.intPID + "] " + msgRecibe.strOrigen).ToString();
                AlmacenarPID();
                return true;
            }
            else { return false; }
        }

        #endregion

    }
}
