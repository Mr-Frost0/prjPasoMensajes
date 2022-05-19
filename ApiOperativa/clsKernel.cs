using System;
using System.Diagnostics;
using System.Collections.Generic;

namespace KernelSistema
{

    public class clsKernel
    {

        #region [Comentarios]

        //Añadir terminación de todos los procesos en ejecución

        /*
         * Kernel: Encargado de la inicialización, ejecución y terminación del programa
         */

        #endregion

        #region [Atributos]

        private String strError;
        private int intIdProcMaestro;
        List<int> lstProcesosHijo;
        
        #endregion

        #region [Constructor]

        public clsKernel()
        {
            intIdProcMaestro = 0;
            lstProcesosHijo = new List<int>();
            strError = "";
        }

        #endregion

        #region [Propiedades]

        public String Error { get => this.strError; }
        public int IdProcMaestro { get => this.intIdProcMaestro; }

        #endregion

        #region [Métodos Privados]



        #endregion

        #region [Métodos Públicos]

        public bool RecovPID(String tipoProceso)
        {
            switch (tipoProceso.ToLower())
            {
                case "maestro":
                case "pasomensaje":
                    Process procesoActual = Process.GetCurrentProcess();
                    this.intIdProcMaestro = procesoActual.Id;
                    procesoActual = null;
                    return true;
                default:
                    strError = "El método sólo puede ser llamado para recuperar la ID del programa principal o para paso de mensajes [test purposes only]";
                    return false;
            }
        }

        public String LanzaForm(String operacion = "default")
        {
            Process arrancaForm = Process.Start(clsConstantes.@RUTAINICIO, operacion.ToLower());
            this.lstProcesosHijo.Add(arrancaForm.Id);
            return arrancaForm.Id.ToString();
        }

        #endregion

    }

}
