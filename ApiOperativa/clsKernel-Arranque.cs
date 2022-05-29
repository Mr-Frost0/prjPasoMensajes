using System;
using System.Diagnostics;
using System.Collections.Generic;

namespace KernelSistema
{

    public class clsKernel_Arranque
    {

        #region [Atributos]

        private String strError;
        private int intIdProceso;
        Process arrancaForm;
        Process procesoActual;

        #endregion

        #region [Constructor]

        public clsKernel_Arranque()
        {
            this.intIdProceso = 0;
            this.strError = "";       
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
