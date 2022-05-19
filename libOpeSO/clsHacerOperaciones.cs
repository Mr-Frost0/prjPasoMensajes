using System;

namespace libOpeSO
{
    public class clsHacerOperaciones
    {

        #region [Atributos]

        private double dblValorUno, dblValorDos;
        private String strTipoOpe;
        private String strError;
        private double dblResultado;

        #endregion

        #region [Constructor]

        public clsHacerOperaciones()
        {
            this.dblValorUno = 0;
            this.dblValorDos = 0;
            this.strTipoOpe = "";
            this.strError = "";
            this.dblResultado = 0;
        }

        #endregion

        #region [Propiedades]

        public double ValorUno { set => dblValorUno = value; }
        public double ValorDos { set => dblValorDos = value; }
        public String TipoOpe { set => strTipoOpe = value; }
        public String Error { get => strError; }
        public double Resultado { get => dblResultado; }

        #endregion

        #region [Métodos Privados]

        private bool Validar()
        {
            try
            {
                switch (strTipoOpe.ToLower())
                {
                    case "sumar":
                    case "restar":
                    case "multiplicar":
                        break;
                    case "dividir":
                        if (dblValorDos == 0)
                        {
                            strError = "No se puede realizar una división por cero";
                            return false;
                        }
                        break;
                    default:
                        strError = "Ha ocurrido un error en la validación de los datos ingresados";
                        return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool Operar()
        {
            try
            {
                switch (strTipoOpe.ToLower())
                {
                    case "sumar":
                        dblResultado = dblValorUno + dblValorDos;
                        break;
                    case "restar":
                        dblResultado = dblValorUno - dblValorDos;
                        break;
                    case "multiplicar":
                        dblResultado = dblValorUno * dblValorDos;
                        break;
                    case "dividir":
                        dblResultado = dblValorUno / dblValorDos;
                        break;                    
                    default:
                        strError = "Ha ocurrido un error procesando el cálculo solicitado";
                        return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region [Métodos Públicos]

        public bool MakeOpp()
        {
            try
            {
                if (!Validar())
                {
                    return false;
                }

                if(!Operar())
                {
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

    }
}
