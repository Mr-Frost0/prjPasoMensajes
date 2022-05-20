using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KernelSistema
{
    public class clsKernel_ListasPID
    {

        #region [Atributos]

        private List<String> lstHistorialProcesos;
        private List<String> lstProcesosActivos;
        private int intPID;

        #endregion

        #region [Constructor]

        public clsKernel_ListasPID()
        {
            this.lstHistorialProcesos = new List<String>();
            this.lstProcesosActivos = new List<String>();
            this.intPID = 0;
        }

        #endregion

        #region [Propiedades]

        public int PID { set => intPID = value; }

        #endregion

        #region [Métodos Privados]



        #endregion

        #region [Métodos Públicos]



        #endregion

    }
}
