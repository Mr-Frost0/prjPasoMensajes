using System;

namespace KernelSistema
{
    public class clsConstantes
    {
        private string NameDesktop = System.Windows.Forms.SystemInformation.ComputerName;
        public const String strRutaCnlPrivMsg = NameDesktop + "\\Private$\\operacionesTest";
        public String RUTAINICIO = AppDomain.CurrentDomain.BaseDirectory.Replace("frmPrincupal", "frmCalculadora") + "frmCalculadora.exe";
        //"C:\\Users\\MrFro\\Desktop\\Final Sistemas Operativos\\libOpeSO\\frmCalculadora\\bin\\Debug\\frmCalculadora.exe"
    }

    public struct MsgRecibe
    {
        public String mensaje;
    }

}
