using System;
using System.IO;

namespace KernelSistema
{
    class clsKernel_GestorArchivos
    {
       /*#region [Atributos]

        private string strMensaje;
        private string strError;
        private int i;

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
        }

        #endregion

        #region [Propiedades]

        public string Mensaje { set => strMensaje = value; }

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
            switch (Tipo.ToLower())
            {
                case "inicio":
                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }
                    if (!Directory.Exists(ArchTextPath + @"HistorialGeneral.txt"))
                    {
                        Directory.CreateDirectory(ArchTextPath + @"HistorialGeneral.txt");
                    }
                    break;
                case "registros":
                    Directory.CreateDirectory(ArchTextPath + @"Historial"+ Contador() +".txt");
                    //Crear método para añadir texto al archivo
                    break;
                case "exit":
                    if (Directory.Exists(folderPath))
                    {
                        Directory.Delete(folderPath,true);
                    }
                    break;
                default:
                    break;
            }
        }

        private void regDoc()
        {
            try
            {
                if (!validar())
                {
                    return;
                }

                StreamWriter writer = new StreamWriter(RUTA, true);
                string contenido = null;

                contenido = string.Format("{0},{1},{2},{3},{4},{5},{6}", TipoSoli, Area, IdCliente, Servicio, Fecha, IdFelicitacion, Mensaje);
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

        public bool registrar()
        {
            try
            {
                regDoc();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;
        }

        #endregion*/
    }
}