using System;

namespace KernelSistema
{
    static class clsConstantes
    {
        public static String strRutaCnlPrivMsg = System.Windows.Forms.SystemInformation.ComputerName + "\\Private$\\operacionesTest";
        public static String RUTAINICIO = AppDomain.CurrentDomain.BaseDirectory.Replace("frmMenuPrincupal", "frmCalculadora") + "frmCalculadora.exe";
    }

    public struct MsgRecibe
    {
        public int intComando;
        public String strOrigen;
        public String strMensaje;
        public int intPID;
    }

}
