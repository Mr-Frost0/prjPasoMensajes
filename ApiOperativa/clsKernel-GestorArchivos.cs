using System;
using System.IO;

namespace KernelSistema
{
    class clsKernel_GestorArchivos
    {
       #region [Atributos]

        private string strMensaje;
        private string strError;
        private int i;
        private string strFecha;

        #endregion

        #region [Constantes]

        private const string folderPath = @"C:\\Users\\Public\\Documents\\HistorialPasoMensajes";
        private const string ArchTextPath = @"C:\\Users\\Public\\Documents\\HistorialPasoMensajes\\";
        private const string VACIO = "";

        #endregion

        #region [Constructor]

        public clsKernel_GestorArchivos()
        {
            this.strMensaje = VACIO;
            this.i = 0;
            this.strFecha = DateTime.Now.ToString(); ;
        }

        #endregion

        #region [Propiedades]

        public string Mensaje { set => strMensaje = value; }
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
            i = i++;
            return Convert.ToString(i);
        }

        private void CrearDirectorios(string Tipo)
        {
            try
            {
                switch (Tipo.ToLower())
                {
                    case "inicio":
                        if (!Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);  // Se crea la carpeta que contendra toda la información
                        }
                        if (!File.Exists(ArchTextPath + @"HistorialGeneral.txt"))
                        {
                            File.Create(ArchTextPath + @"HistorialGeneral.txt");  // Se crea el archivo donde se ingresara el historial de todos los mensajes
                        }
                        break;
                    case "registros":
                        File.Create(ArchTextPath + @"Historial" + Contador() + ".txt");   // Se crea un archivo para ingresar el ultimo mensaje enviado
                        regDoc(ArchTextPath + @"HistorialGeneral.txt");     // Se añade cada mensaje en el historial General
                        regDoc(ArchTextPath + @"Historial" + i + ".txt");   // Se Copia el mensaje actual (el ultimo) en el historial correspondiente
                        break;
                    case "exit":
                        if (Directory.Exists(folderPath))
                        {
                            Directory.Delete(folderPath, true);
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
            try
            {
                if (!validar())
                {
                    return;
                }

                StreamWriter writer = new StreamWriter(Ruta, true);
                string contenido = null;

                contenido = string.Format("{0},{1}", strMensaje, strFecha);
                writer.WriteLine(contenido);

                writer.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region [Métodos Públicos]

        public bool registrar(string tipo)
        {
            try
            {
                CrearDirectorios(tipo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;
        }

        #endregion
    }
}
/*clsKernel_GestorArchivos GA = new clsKernel_GestorArchivos();
GA.registrar("inicio");*/