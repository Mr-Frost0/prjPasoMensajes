using System;
using System.Messaging;

namespace KernelSistema
{
    public class clsColasMensajes
    {
        public void CrearColas()
        {
            try
            {
                if (!MessageQueue.Exists(".\\Private$\\canalpasomensajes"))
                {
                    MessageQueue.Create(".\\Private$\\canalpasomensajes");
                }
                if (!MessageQueue.Exists(".\\Private$\\canalpasopid"))
                {
                    MessageQueue.Create(".\\Private$\\canalpasopid");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void BorrarColas()
        {
            try
            {
                if (MessageQueue.Exists(".\\Private$\\canalpasomensajes"))
                {
                    MessageQueue.Delete(".\\Private$\\canalpasomensajes");
                }
                if (MessageQueue.Exists(".\\Private$\\canalpasopid"))
                {
                    MessageQueue.Delete(".\\Private$\\canalpasopid");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
