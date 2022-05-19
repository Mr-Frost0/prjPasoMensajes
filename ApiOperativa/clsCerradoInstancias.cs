using System;
using System.Diagnostics;
using System.Collections.Generic;

namespace KernelSistema
{
    public class clsCerrarPorPID
    {

        #region [Atributos]

        private int intPID;
        
        
        #endregion

        #region [Constructor]

        public clsCerrarPorPID()
        {
            this.intPID = 0;
        }
        
        #endregion

        #region [Propiedades]

        public int PID { set => intPID = value; }
        
        #endregion

        #region [Métodos Privados]


        
        #endregion

        #region [Métodos Públicos]

        public void CerrarInstancia(String param = "default")
        {
            try
            {
                switch (param.ToLower())
                {
                    case "modulo-principal":
                        break;
                    case "form-calculadora":
                        break;
                    default:
                        break;
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
