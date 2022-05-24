using System;
using System.Windows.Forms;

namespace KernelSistema
{
    public class clsCerrarPorPID
    {

        #region [Atributos]

        private int intPID;
        private clsPasoMensajes enviarTerminacion;
        
        
        #endregion

        #region [Constructor]

        public clsCerrarPorPID()
        {
            this.intPID = 0;
            this.enviarTerminacion = new clsPasoMensajes();
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
            else if (resultado == DialogResult.No)
            {
                return false;
            }
            return false;
        }

        public void CerrarInstancia(String param = "default")
        {
            try
            {
                enviarTerminacion.TipoMensaje = param;
                enviarTerminacion.EnviarMsg();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        
        #endregion

    }
}
