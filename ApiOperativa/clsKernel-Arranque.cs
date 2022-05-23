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
        private int intIdProceso;
        Process arrancaForm;
        clsPasoMensajes enviarPID;
        Process procesoActual;

        #endregion

        #region [Constructor]

        public clsKernel()
        {
            this.intIdProceso = 0;
            this.strError = "";
            this.enviarPID = new clsPasoMensajes();            
        }

        #endregion

        #region [Propiedades]

        public String Error { get => this.strError; }
        public int IdProceso { get => this.intIdProceso; }

        #endregion

        #region [Métodos Públicos]

        public bool RecuperaPID(String tipoProceso)
        {
            switch (tipoProceso.ToLower())
            {
                case "maestro":
                case "pasomensaje":
                case "calculadora":
                    procesoActual = Process.GetCurrentProcess();
                    this.intIdProceso = procesoActual.Id;
                    break;
                default:
                    strError = "El método sólo puede ser llamado para recuperar la ID del programa principal, para el módulo Calculadora o para paso de mensajes [test purposes only]";
                    return false;
            }
            return true;
        }

        public void LanzaForm(String operacion = "default")
        {
            switch (operacion.ToLower())
            {
                case "sumar":
                case "restar":
                case "multiplicar":
                case "dividir":
                    arrancaForm = Process.Start(clsConstantes.@strRutaCalculadora, operacion.ToLower());
                    break;
                case "mod-aplicaciones":
                    arrancaForm = Process.Start(clsConstantes.@strRutaModApps);
                    break;
                default:
                    break;
            }
        }

        #endregion

    }

}
