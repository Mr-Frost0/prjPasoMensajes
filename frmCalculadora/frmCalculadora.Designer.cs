
namespace frmCalculadora
{
    partial class frmCalculadora
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.grbMakeOpp = new System.Windows.Forms.GroupBox();
            this.txtValor2 = new System.Windows.Forms.TextBox();
            this.lblSegundoValor = new System.Windows.Forms.Label();
            this.txtValor1 = new System.Windows.Forms.TextBox();
            this.lblPrimerValor = new System.Windows.Forms.Label();
            this.btnOperacion = new System.Windows.Forms.Button();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.grbResultado = new System.Windows.Forms.GroupBox();
            this.lblResultado = new System.Windows.Forms.Label();
            this.wrkArranque = new System.ComponentModel.BackgroundWorker();
            this.mnuCerrar = new System.Windows.Forms.MenuStrip();
            this.tsmiSalir = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCerrarActual = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCerrarTodasCalc = new System.Windows.Forms.ToolStripMenuItem();
            this.grbMakeOpp.SuspendLayout();
            this.grbResultado.SuspendLayout();
            this.mnuCerrar.SuspendLayout();
            this.SuspendLayout();
            // 
            // grbMakeOpp
            // 
            this.grbMakeOpp.Controls.Add(this.txtValor2);
            this.grbMakeOpp.Controls.Add(this.lblSegundoValor);
            this.grbMakeOpp.Controls.Add(this.txtValor1);
            this.grbMakeOpp.Controls.Add(this.lblPrimerValor);
            this.grbMakeOpp.Location = new System.Drawing.Point(12, 27);
            this.grbMakeOpp.Name = "grbMakeOpp";
            this.grbMakeOpp.Size = new System.Drawing.Size(435, 174);
            this.grbMakeOpp.TabIndex = 0;
            this.grbMakeOpp.TabStop = false;
            this.grbMakeOpp.Text = "Realizar {operation}";
            // 
            // txtValor2
            // 
            this.txtValor2.Location = new System.Drawing.Point(192, 96);
            this.txtValor2.Name = "txtValor2";
            this.txtValor2.Size = new System.Drawing.Size(205, 29);
            this.txtValor2.TabIndex = 3;
            // 
            // lblSegundoValor
            // 
            this.lblSegundoValor.AutoSize = true;
            this.lblSegundoValor.Location = new System.Drawing.Point(31, 99);
            this.lblSegundoValor.Name = "lblSegundoValor";
            this.lblSegundoValor.Size = new System.Drawing.Size(137, 24);
            this.lblSegundoValor.TabIndex = 2;
            this.lblSegundoValor.Text = "Segundo Valor";
            // 
            // txtValor1
            // 
            this.txtValor1.Location = new System.Drawing.Point(192, 51);
            this.txtValor1.Name = "txtValor1";
            this.txtValor1.Size = new System.Drawing.Size(205, 29);
            this.txtValor1.TabIndex = 1;
            // 
            // lblPrimerValor
            // 
            this.lblPrimerValor.AutoSize = true;
            this.lblPrimerValor.Location = new System.Drawing.Point(31, 54);
            this.lblPrimerValor.Name = "lblPrimerValor";
            this.lblPrimerValor.Size = new System.Drawing.Size(114, 24);
            this.lblPrimerValor.TabIndex = 0;
            this.lblPrimerValor.Text = "Primer Valor";
            // 
            // btnOperacion
            // 
            this.btnOperacion.Location = new System.Drawing.Point(95, 216);
            this.btnOperacion.Name = "btnOperacion";
            this.btnOperacion.Size = new System.Drawing.Size(133, 60);
            this.btnOperacion.TabIndex = 4;
            this.btnOperacion.Text = "Realizar {Operation}";
            this.btnOperacion.UseVisualStyleBackColor = true;
            this.btnOperacion.Click += new System.EventHandler(this.btnOperacion_Click);
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.Location = new System.Drawing.Point(234, 216);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(133, 60);
            this.btnLimpiar.TabIndex = 5;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.UseVisualStyleBackColor = true;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // grbResultado
            // 
            this.grbResultado.Controls.Add(this.lblResultado);
            this.grbResultado.Location = new System.Drawing.Point(12, 292);
            this.grbResultado.Name = "grbResultado";
            this.grbResultado.Size = new System.Drawing.Size(435, 106);
            this.grbResultado.TabIndex = 4;
            this.grbResultado.TabStop = false;
            this.grbResultado.Text = "Resultado";
            // 
            // lblResultado
            // 
            this.lblResultado.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lblResultado.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblResultado.Location = new System.Drawing.Point(35, 55);
            this.lblResultado.Name = "lblResultado";
            this.lblResultado.Size = new System.Drawing.Size(362, 34);
            this.lblResultado.TabIndex = 0;
            this.lblResultado.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // wrkArranque
            // 
            this.wrkArranque.WorkerSupportsCancellation = true;
            this.wrkArranque.DoWork += new System.ComponentModel.DoWorkEventHandler(this.wrkArranque_DoWork);
            // 
            // mnuCerrar
            // 
            this.mnuCerrar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiSalir});
            this.mnuCerrar.Location = new System.Drawing.Point(0, 0);
            this.mnuCerrar.Name = "mnuCerrar";
            this.mnuCerrar.Size = new System.Drawing.Size(470, 24);
            this.mnuCerrar.TabIndex = 6;
            this.mnuCerrar.Text = "mnuSalir";
            // 
            // tsmiSalir
            // 
            this.tsmiSalir.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiCerrarActual,
            this.tsmiCerrarTodasCalc});
            this.tsmiSalir.Name = "tsmiSalir";
            this.tsmiSalir.Size = new System.Drawing.Size(41, 20);
            this.tsmiSalir.Text = "&Salir";
            // 
            // tsmiCerrarActual
            // 
            this.tsmiCerrarActual.Name = "tsmiCerrarActual";
            this.tsmiCerrarActual.Size = new System.Drawing.Size(227, 22);
            this.tsmiCerrarActual.Text = "&Cerrar Instancia Actual";
            this.tsmiCerrarActual.Click += new System.EventHandler(this.tsmiCerrarActual_Click);
            // 
            // tsmiCerrarTodasCalc
            // 
            this.tsmiCerrarTodasCalc.Name = "tsmiCerrarTodasCalc";
            this.tsmiCerrarTodasCalc.Size = new System.Drawing.Size(227, 22);
            this.tsmiCerrarTodasCalc.Text = "&Cerrar Todas las Calculadoras";
            this.tsmiCerrarTodasCalc.Click += new System.EventHandler(this.tsmiCerrarTodasCalc_Click);
            // 
            // frmCalculadora
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(470, 410);
            this.Controls.Add(this.grbResultado);
            this.Controls.Add(this.btnLimpiar);
            this.Controls.Add(this.btnOperacion);
            this.Controls.Add(this.grbMakeOpp);
            this.Controls.Add(this.mnuCerrar);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.MainMenuStrip = this.mnuCerrar;
            this.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.Name = "frmCalculadora";
            this.Text = "Calculadora de {operation}";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmCalculadora_FormClosing);
            this.Load += new System.EventHandler(this.frmCalculadora_Load);
            this.grbMakeOpp.ResumeLayout(false);
            this.grbMakeOpp.PerformLayout();
            this.grbResultado.ResumeLayout(false);
            this.mnuCerrar.ResumeLayout(false);
            this.mnuCerrar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grbMakeOpp;
        private System.Windows.Forms.TextBox txtValor2;
        private System.Windows.Forms.Label lblSegundoValor;
        private System.Windows.Forms.TextBox txtValor1;
        private System.Windows.Forms.Label lblPrimerValor;
        private System.Windows.Forms.Button btnOperacion;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.GroupBox grbResultado;
        private System.Windows.Forms.Label lblResultado;
        private System.ComponentModel.BackgroundWorker wrkArranque;
        private System.Windows.Forms.MenuStrip mnuCerrar;
        private System.Windows.Forms.ToolStripMenuItem tsmiSalir;
        private System.Windows.Forms.ToolStripMenuItem tsmiCerrarActual;
        private System.Windows.Forms.ToolStripMenuItem tsmiCerrarTodasCalc;
    }
}

