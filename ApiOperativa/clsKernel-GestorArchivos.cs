using System;
using System.IO;

namespace KernelSistema
{
    public class clsKernel_GestorArchivos
    {
        #region [Atributos]

        private string strMensaje;
        private string strError;
        private string strOrigen;
        private int i;
        private int intPID;
        private string strFecha;

        #endregion

        #region [Constantes]

        private const string RutaPrincipal = "C:\\Users\\Public\\Documents\\HistorialPasoMensajes";
        private const string RutaHstGrl = "C:\\Users\\Public\\Documents\\HistorialPasoMensajes\\General";
        private const string RutaHstIndv = "C:\\Users\\Public\\Documents\\HistorialPasoMensajes\\Individual";
        private const string ArchTextPath = "C:\\Users\\Public\\Documents\\HistorialPasoMensajes\\";
        private const string VACIO = "";

        #endregion

        #region [Constructor]

        public clsKernel_GestorArchivos()
        {
            this.strMensaje = VACIO;
            this.i = 0;
            this.intPID = 0;
        }

        #endregion

        #region [Propiedades]

        public int PID { set => intPID = value; }
        public string Mensaje { set => strMensaje = value; }
        public string Origen { set => strOrigen = value; }
        public string Error { get => strError; }

        #endregion

        #region [Métodos Privados]

        private bool validar()
        {
            if (strMensaje == VACIO)
            {
                strError = "No se recibió ningún mensaje GesArch";
                return false;
            }
            return true;
        }

        private string Contador()
        {
            i = i+1;
            return Convert.ToString(i);
        }

        private void CrearDirectorios(string Tipo)
        {
            try
            {
                switch (Tipo.ToLower())
                {
                    case "inicio":
                        if (!Directory.Exists(RutaPrincipal))
                        {
                            Directory.CreateDirectory(RutaPrincipal);  // Se crea la carpeta que contendra toda la información

                            if (!Directory.Exists(RutaHstGrl))
                            {
                                Directory.CreateDirectory(RutaHstGrl);
                            }
                            if (!Directory.Exists(RutaHstIndv))
                            {
                                Directory.CreateDirectory(RutaHstIndv);
                            }
                        }
                        break;
                    case "registros":
                        regDoc(RutaHstGrl + "\\" + "HistorialGeneral.txt");     // Se añade cada mensaje en el historial General
                        regDoc(RutaHstIndv + "\\" + "Historial " + Contador() + " - Proceso " + intPID + ".txt");   // Se Copia el mensaje actual (el ultimo) en el historial correspondiente
                        break;
                    case "finalizar":
                        if (Directory.Exists(RutaPrincipal))
                        {
                            Directory.Delete(RutaPrincipal, true);
                        }
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void regDoc(string Ruta)
        {
            StreamWriter writer;
            try
            {
                if (!validar())
                {
                    return;
                }

                writer = new StreamWriter(Ruta, true);
                string contenido = null;

                contenido = string.Format("{0},{1}", strMensaje, strFecha);
                writer.WriteLine(contenido);

                writer.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                writer = null;
            }
        }

        #endregion

        #region [Métodos Públicos]

        public bool registrar(string tipo)
        {
            try
            {
                this.strFecha = DateTime.Now.ToString();
                CrearDirectorios(tipo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.strFecha = null;
            }
            return true;
        }

        #endregion
    }
}