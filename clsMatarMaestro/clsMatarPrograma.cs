using KernelSistema;

namespace clsMatarMaestro
{
    public class clsMatarPrograma
    {

        private clsCerradoInstancias matarMaestro = new clsCerradoInstancias();
        private int intPIDMaestro;
        public int PIDMaestro { set => intPIDMaestro = value; }


        static void Main(string[] args)
        {

        }

        public void MatarMaestro()
        {
            matarMaestro.PID = intPIDMaestro;
            matarMaestro.Cerrar("maestro");
        }

    }
}
