using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Windows.Forms;

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

        public bool ConfirmaCerrado()
        {
            DialogResult resultado = MessageBox.Show("Desea salir del programa?\nEsto implica cerrar todas las demás instancias de programas abiertos mediante esta aplicación.", "Confirmacion",
   MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (resultado == DialogResult.Yes)
            {
                return true;
            }
            else return false;
        }

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
