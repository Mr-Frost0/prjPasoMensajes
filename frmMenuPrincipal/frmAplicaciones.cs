using System;
using System.Windows.Forms;
using KernelSistema;
using System.Threading;

namespace frmMenuPrincipal
{

    /*
     * Este es el módulo de aplicaciones, por lo cual le aplican las siguientes normas
      
     * Si este módulo se cierra, deberá cerrar todos los módulos hijos, pero se debe poder inicializar de nuevo desde el módulo GUI     
     
     * Controlará sus instancias a través del paso de mensajes
     
     * Se comunicará con la GUI mediante el comando 'info', a la vez que envía el PID de las instancias hijas
      
     * Cuando se cierre una instancia o todas mediante el módulo Aplicación, notificará al gestor de archivos y lo registrará en el registro de transacciones
      
     * Este módulo puede generar un mensaje 'ocupado' con un tiempo de espera entre 1 y 3 segundos, para luego pasar a estar disponibles y actualizar su estado en tiempo real
      
     */

    public partial class frmPrincipal : Form
    {

        #region [Atributos]

        private clsKernel objKernel;
        private clsPasoMensajes objPasoMensajes;
        private clsCerradoInstancias objCerrarInstancia;

        #endregion

        #region [Constructor]

        public frmPrincipal()
        {
            InitializeComponent();
            objKernel = new clsKernel();
            this.objPasoMensajes = new clsPasoMensajes();
            RecuperaIdMaestro();
            objCerrarInstancia = new clsCerradoInstancias();
            wrkArranque.RunWorkerAsync();
        }

        #endregion

        #region [Métodos Privados]

        private bool EstaListo()
        {
            this.Text = "Cargando Formulario...";
            this.Enabled = false;
            Random espera = new Random();
            Thread.Sleep(espera.Next(1000, 3000));
            this.Text = "Final Sistemas Operativos";
            this.Enabled = true;
            wrkArranque.CancelAsync();
            return true;
        }

        private bool EnviarMensaje(String tipoMsg)
        {
            try
            {
                switch (tipoMsg.ToLower())
                {
                    case "listo":
                        objPasoMensajes.TipoMensaje = "started";
                        objPasoMensajes.Origen = this.Text;
                        break;
                    case "stop":
                        objPasoMensajes.TipoMensaje = "stop";
                        objPasoMensajes.Origen = this.Text;
                        break;
                    case "stop-all-calc":
                        objPasoMensajes.TipoMensaje = "stop-all-calc";
                        objPasoMensajes.Origen = this.Text;
                        break;
                    default:
                        break;
                }

                if (!objPasoMensajes.EnviarMsg())
                {
                    MessageBox.Show(objPasoMensajes.Error, "Módulo Aplicaciones", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.Message, "Módulo Aplicaciones", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void RecuperaIdMaestro()
        {
            if (!objKernel.RecuperaPID("calculadora"))
            {
                MessageBox.Show(objKernel.Error,"Final Sistemas Operativos",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }

        }

        private bool CerradoForm(String s)
        {
            if (objCerrarInstancia.ConfirmaCerrado(s))
            {
                return true;
            }
            else return false;
        }

        #endregion

        #region [Eventos]

        private void btnSuma_Click(object sender, EventArgs e)
        {
            objKernel.LanzaForm("sumar");
        }

        private void btnResta_Click(object sender, EventArgs e)
        {
            objKernel.LanzaForm("restar");
        }

        private void btnMultipl_Click(object sender, EventArgs e)
        {
            objKernel.LanzaForm("multiplicar");
        }

        private void btnDivision_Click(object sender, EventArgs e)
        {
            objKernel.LanzaForm("dividir");
        }

        private void frmPrincipal_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!CerradoForm("aplicaciones"))
            {
                e.Cancel = true;
            }
            else
            {
                EnviarMensaje("stop");
                EnviarMensaje("stop-all-calc");
            }
        }

        private void wrkArranque_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            if (EstaListo())
            {
                EnviarMensaje("listo");
            }
        }

        private void tsmiSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

    }
}