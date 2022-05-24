using System;
using System.Windows.Forms;

namespace KernelSistema
{
    static class clsConstantes
    {
        public static String strRutaCanalMensajes = SystemInformation.ComputerName + "\\Private$\\canalpasomensajes";
        public static String strRutaCanalPID = SystemInformation.ComputerName + "\\Private$\\canalpasopid";
        public static String strRutaFinalizacion = SystemInformation.ComputerName + "\\Private$\\canalfinalizaciones";
        public static String strRutaCalculadora = AppDomain.CurrentDomain.BaseDirectory.Replace("frmMenuPrincipal", "frmCalculadora") + "frmCalculadora.exe";
        public static String strRutaModApps = AppDomain.CurrentDomain.BaseDirectory.Replace("frmGUI","frmMenuPrincipal") + "frmMenuPrincipal.exe";
    }

    public struct MsgRecibe
    {
        public int intCodTerm;
        public String strComando;
        public String strOrigen;
        public String strMensaje;
        public int intPID;
    }

}
