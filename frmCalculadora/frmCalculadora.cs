using System;
using System.Windows.Forms;
using KernelSistema;
using OperacionesMatematicas;
using System.Threading;

namespace frmCalculadora
{
    public partial class frmCalculadora : Form
    {

        #region [Variables Globales]

        private clsHacerOperaciones objHacerOpe;
        private clsPasoMensajes objPasoMensajes;
        private clsCerradoInstancias objCerrarForm;
        private String strTipoOpe;
        private String strReplace;
        private Double valor1, valor2;
        private String[] args;
        private bool debeReiniciar = false;
        private String strQueCierra;

        #endregion

        #region [Constantes]

        private const int UNO = 1;
        private const string VACIO = "";

        #endregion 

        #region [Constructor]

        public frmCalculadora()
        {
            InitializeComponent();
            args = Environment.GetCommandLineArgs();
            this.strTipoOpe = args[UNO];
            this.objHacerOpe = new clsHacerOperaciones();
            this.objPasoMensajes = new clsPasoMensajes();
            this.objCerrarForm = new clsCerradoInstancias();
            wrkArranque.RunWorkerAsync();
            this.ControlBox = false;
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

        private bool EstaListo()
        {
            this.lblResultado.Text = "Cargando Formulario...";
            this.Enabled = false;
            Random espera = new Random();
            Thread.Sleep(espera.Next(1000, 3000));
            this.lblResultado.Text = VACIO;
            this.Enabled = true;
            wrkArranque.CancelAsync();
            return true;
        }

        private void InicializadoForm()
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
            EnviarMensaje("pid");
        }

        private void Limpiar()
        {
            if (!HayErrorArranque())
            {
                return;
            }
            this.txtValor1.Text = VACIO;
            this.txtValor2.Text = VACIO;
            this.lblResultado.Text = VACIO;
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

                if (ImpResult())
                {
                    if (!EnviarMensaje("op-exito"))
                    {
                        MessageBox.Show("Ha ocurrido un error enviando el mensaje de confirmación", "Calculadora - " + strReplace, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else
                {
                    if (objHacerOpe.CodError == "div-0")
                    {
                        if (!EnviarMensaje("div-0"))
                        {
                            MessageBox.Show("Ha ocurrido un error enviando el mensaje de confirmación", "Calculadora - " + strReplace, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
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

        private bool ImpResult()
        {
            if (!objHacerOpe.MakeOpp())
            {
                MessageBox.Show(objHacerOpe.Error, "Calculadora - " + strReplace, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }            
            this.lblResultado.Text = Convert.ToString(objHacerOpe.Resultado);
            return true;
        }

        private bool EnviarMensaje(String tipoMsg)
        {
            try
            {
                switch (tipoMsg.ToLower())
                {
                    case "op-exito":
                        objPasoMensajes.TipoMensaje = "operacion-exito";
                        objPasoMensajes.CodTerm = 0;
                        break;
                    case "listo":
                        objPasoMensajes.TipoMensaje = "started";
                        break;
                    case "stop":
                        objPasoMensajes.TipoMensaje = "stop";
                        break;
                    case "stop-all-calc":
                        objPasoMensajes.TipoMensaje = "stop-all-calc";
                        break;
                    case "div-0":
                        objPasoMensajes.TipoMensaje = "div-0";
                        break;
                    default:
                        break;
                }

                objPasoMensajes.Origen = this.Text;

                if (!objPasoMensajes.EnviarMsg())
                {
                    MessageBox.Show(objPasoMensajes.Error, "Calculadora - " + strReplace, MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private bool CerradoForm(String s)
        {
            if (objCerrarForm.ConfirmaCerrado(s))
            {
                return true;
            }
            else return false;
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
            if (strQueCierra == "calculadora")
            {
                EnviarMensaje("stop");
            }
        }

        private void wrkArranque_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            if (EstaListo())
            {
                EnviarMensaje("listo");
            }
        }

        private void tsmiCerrarActual_Click(object sender, EventArgs e)
        {
            strQueCierra = "calculadora";
            if (CerradoForm(strQueCierra))
            {
                this.Close();
            }
        }

        private void tsmiCerrarTodasCalc_Click(object sender, EventArgs e)
        {
            strQueCierra = "aplicaciones";
            if (CerradoForm(strQueCierra))
            {
                EnviarMensaje("stop-all-calc");
            }          
        }

        private void frmCalculadora_Load(object sender, EventArgs e)
        {
            InicializadoForm();
        }

        #endregion

    }
}