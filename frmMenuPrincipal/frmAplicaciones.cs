using System;
using System.Windows.Forms;
using KernelSistema;

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
        private clsCerrarPorPID objCerrarInstancia;

        #endregion

        #region [Constructor]

        public frmPrincipal()
        {
            InitializeComponent();
            objKernel = new clsKernel();
            RecuperaIdMaestro();
            objCerrarInstancia = new clsCerrarPorPID();
        }

        #endregion

        #region [Métodos Privados]

        private void RecuperaIdMaestro()
        {
            if (!objKernel.RecuperaPID("calculadora"))
            {
                MessageBox.Show(objKernel.Error,"Final Sistemas Operativos",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }
        private void CerradoForm()
        {
            if (objCerrarInstancia.ConfirmaCerrado())
            {
                objCerrarInstancia.CerrarInstancia("cerrar-calculadora");
            }
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
            CerradoForm();
        }

        #endregion


    }
}
