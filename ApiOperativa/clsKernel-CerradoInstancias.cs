using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace KernelSistema
{
    public class clsCerradoInstancias
    {

        #region [Atributos]

        private int intPID;
        private clsPasoMensajes enviarTerminacion;
        private int[] intPIDS;   
        
        #endregion

        #region [Constructor]

        public clsCerradoInstancias()
        {
            this.intPID = 0;
            this.enviarTerminacion = new clsPasoMensajes();
        }

        #endregion

        #region [Propiedades]

        public int PID { set => intPID = value; }
        public int[] PIDS { set => intPIDS = value; }

        #endregion

        #region [Métodos Públicos]

        public bool ConfirmaCerrado(String modACerrar)
        {
            DialogResult resultado;

            switch (modACerrar.ToLower())
            {
                case "aplicaciones":
                case "gui":
                    resultado = MessageBox.Show("Desea salir del programa?\nEsto implica cerrar todas las demás instancias de programas abiertos mediante esta aplicación.", "Confirmacion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    break;
                case "calculadora":
                    resultado = MessageBox.Show("Desea cerrar el módulo actual?","Confirmacion",MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    break;
                default:
                    return false;
            }
            if (resultado == DialogResult.Yes)
            {
                return true;
            }
            else if (resultado == DialogResult.No)
            {
                return false;
            }
            else return false;
        }

        public bool Cerrar(String tipo)
        {
            switch (tipo.ToLower())
            {
                case "all":
                case "stop-all-calc":
                case "modapps-calcs":
                    foreach (int pid in intPIDS)
                    {
                        if (pid != 0)
                        {
                            Process prcAMatar = Process.GetProcessById(pid);
                            prcAMatar.Kill();
                        }                        
                    }
                    break;
                case "maestro":
                    Process matarMaestro = Process.GetProcessById(intPID);
                    matarMaestro.Kill();
                    break;
                default:
                    break;
            }
            return true;
        }

        #endregion

    }
}
