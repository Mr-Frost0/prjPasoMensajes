using System;
using System.Windows.Forms;
using KernelSistema;
using OperacionesMatematicas;

namespace frmCalculadora
{
    public partial class frmCalculadora : Form
    {

        #region [Variables Globales]

        clsHacerOperaciones objHacerOpe;
        clsPasoMensajes objPasoMensajes;
        clsCerrarPorPID objCerrarForm;
        private String strTipoOpe;
        private String strReplace;
        private Double valor1, valor2;
        String[] args = Environment.GetCommandLineArgs();
        bool debeReiniciar = false;

        #endregion

        #region [Constructor]

        public frmCalculadora()
        {
            InitializeComponent();
            this.strTipoOpe = args[1];
            this.objHacerOpe = new clsHacerOperaciones();
            this.objPasoMensajes = new clsPasoMensajes();
            this.objCerrarForm = new clsCerrarPorPID();

        }

        #endregion

        #region [Métodos Privados]

        private bool HayErrorArranque()
        {

            if (this.debeReiniciar)
            {
                return false;
            }
            else
                return true;
        }

        private void SetForm()
        {
            switch (strTipoOpe.ToLower())
            {
                case "sumar":
                    strReplace = "Suma";
                    break;
                case "restar":
                    strReplace = "Resta";
                    break;
                case "multiplicar":
                    strReplace = "Multiplicación";
                    break;
                case "dividir":
                    strReplace = "División";
                    break;
                case "default":
                default:
                    strReplace = "Error";
                    lblResultado.Text = "Por favor reinicie la aplicación";
                    debeReiniciar = true;
                    break;
            }
            this.grbMakeOpp.Text = "Realizar " + strReplace;
            this.btnOperacion.Text = "Realizar " + strReplace;
            this.Text = "Calculadora de " + strReplace;
        }

        private void Limpiar()
        {
            if (!HayErrorArranque())
            {
                return;
            }
            this.txtValor1.Text = "";
            this.txtValor2.Text = "";
            this.lblResultado.Text = "";
        }

        private void Calcular()
        {
            try
            {
                if (!HayErrorArranque())
                {
                    return;
                }
                if (!Validar())
                {
                    return;
                }
                if (!EnviarDatos())
                {
                    MessageBox.Show(objHacerOpe.Error, "Calculadora - " + strReplace, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                ImpResult();

                if (!EnviarMensaje())
                {
                    MessageBox.Show("Ha ocurrido un error enviando el mensaje de confirmación", "Calculadora - " + strReplace, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.Message, "Calculadora - " + strReplace, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool Validar()
        {
            try
            {
                double result;

                if (!Double.TryParse(txtValor1.Text, out result))
                {
                    MessageBox.Show("El valor ingresado en el primer campo no es un número válido, por favor intente de nuevo", "Calculadora - " + strReplace, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
                if (!Double.TryParse(txtValor2.Text, out result))
                {
                    MessageBox.Show("El valor ingresado en el segundo campo no es un número válido, por favor intente de nuevo", "Calculadora - " + strReplace, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }

                ConvertVars();

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.Message, "Calculadora - " + strReplace, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void ConvertVars()
        {
            valor1 = Double.Parse(txtValor1.Text.Trim().Replace(".", ","));
            valor2 = Double.Parse(txtValor2.Text.Trim().Replace(".", ","));
        }

        private bool EnviarDatos()
        {
            try
            {
                objHacerOpe.ValorUno = this.valor1;
                objHacerOpe.ValorDos = this.valor2;
                objHacerOpe.TipoOpe = strTipoOpe;
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.Message, "Calculadora - " + strReplace, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void ImpResult()
        {
            if (!objHacerOpe.MakeOpp())
            {
                MessageBox.Show(objHacerOpe.Error, "Calculadora - " + strReplace, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }            
            this.lblResultado.Text = Convert.ToString(objHacerOpe.Resultado);
        }

        private bool EnviarMensaje()
        {
            try
            {

                objPasoMensajes.TipoMensaje = "suma-exito";

                if (!objPasoMensajes.EnviarMsg())
                {
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.Message, "Calculadora - " + strReplace, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void CerradoForm()
        {
            objCerrarForm.CerrarInstancia("form-calculadora");
        }

        #endregion

        #region [Eventos]

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void btnOperacion_Click(object sender, EventArgs e)
        {
            Calcular();
        }

        private void frmCalculadora_FormClosing(object sender, FormClosingEventArgs e)
        {
            CerradoForm();
        }

        private void frmCalculadora_Load(object sender, EventArgs e)
        {
            SetForm();
        }

        #endregion

    }
}
