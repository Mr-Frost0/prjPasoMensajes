
namespace frmMenuPrincipal
{
    partial class frmPrincipal
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
            this.grbMenuPrincipal = new System.Windows.Forms.GroupBox();
            this.btnDivision = new System.Windows.Forms.Button();
            this.btnMultipl = new System.Windows.Forms.Button();
            this.btnResta = new System.Windows.Forms.Button();
            this.btnSuma = new System.Windows.Forms.Button();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.wrkArranque = new System.ComponentModel.BackgroundWorker();
            this.mnuCerrar = new System.Windows.Forms.MenuStrip();
            this.tsmiSalir = new System.Windows.Forms.ToolStripMenuItem();
            this.grbMenuPrincipal.SuspendLayout();
            this.mnuCerrar.SuspendLayout();
            this.SuspendLayout();
            // 
            // grbMenuPrincipal
            // 
            this.grbMenuPrincipal.Controls.Add(this.btnDivision);
            this.grbMenuPrincipal.Controls.Add(this.btnMultipl);
            this.grbMenuPrincipal.Controls.Add(this.btnResta);
            this.grbMenuPrincipal.Controls.Add(this.btnSuma);
            this.grbMenuPrincipal.Location = new System.Drawing.Point(12, 91);
            this.grbMenuPrincipal.Name = "grbMenuPrincipal";
            this.grbMenuPrincipal.Size = new System.Drawing.Size(324, 301);
            this.grbMenuPrincipal.TabIndex = 1;
            this.grbMenuPrincipal.TabStop = false;
            this.grbMenuPrincipal.Text = "Opciones";
            // 
            // btnDivision
            // 
            this.btnDivision.Location = new System.Drawing.Point(80, 215);
            this.btnDivision.Name = "btnDivision";
            this.btnDivision.Size = new System.Drawing.Size(141, 49);
            this.btnDivision.TabIndex = 3;
            this.btnDivision.Text = "División";
            this.btnDivision.UseVisualStyleBackColor = true;
            this.btnDivision.Click += new System.EventHandler(this.btnDivision_Click);
            // 
            // btnMultipl
            // 
            this.btnMultipl.Location = new System.Drawing.Point(80, 160);
            this.btnMultipl.Name = "btnMultipl";
            this.btnMultipl.Size = new System.Drawing.Size(141, 49);
            this.btnMultipl.TabIndex = 2;
            this.btnMultipl.Text = "Multiplicación";
            this.btnMultipl.UseVisualStyleBackColor = true;
            this.btnMultipl.Click += new System.EventHandler(this.btnMultipl_Click);
            // 
            // btnResta
            // 
            this.btnResta.Location = new System.Drawing.Point(80, 105);
            this.btnResta.Name = "btnResta";
            this.btnResta.Size = new System.Drawing.Size(141, 49);
            this.btnResta.TabIndex = 1;
            this.btnResta.Text = "Resta";
            this.btnResta.UseVisualStyleBackColor = true;
            this.btnResta.Click += new System.EventHandler(this.btnResta_Click);
            // 
            // btnSuma
            // 
            this.btnSuma.Location = new System.Drawing.Point(80, 50);
            this.btnSuma.Name = "btnSuma";
            this.btnSuma.Size = new System.Drawing.Size(141, 49);
            this.btnSuma.TabIndex = 0;
            this.btnSuma.Text = "Suma";
            this.btnSuma.UseVisualStyleBackColor = true;
            this.btnSuma.Click += new System.EventHandler(this.btnSuma_Click);
            // 
            // lblTitulo
            // 
            this.lblTitulo.Font = new System.Drawing.Font("Verdana", 14.25F);
            this.lblTitulo.Location = new System.Drawing.Point(12, 24);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(324, 114);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "[Test] Instanciador de Calculadoras - Versión no Final";
            this.lblTitulo.TextAlign = System.Drawing.ContentAlignment.TopCenter;
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
            this.mnuCerrar.Size = new System.Drawing.Size(354, 24);
            this.mnuCerrar.TabIndex = 7;
            this.mnuCerrar.Text = "mnuSalir";
            // 
            // tsmiSalir
            // 
            this.tsmiSalir.Name = "tsmiSalir";
            this.tsmiSalir.Size = new System.Drawing.Size(41, 20);
            this.tsmiSalir.Text = "&Salir";
            this.tsmiSalir.Click += new System.EventHandler(this.tsmiSalir_Click);
            // 
            // frmPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(354, 410);
            this.Controls.Add(this.mnuCerrar);
            this.Controls.Add(this.grbMenuPrincipal);
            this.Controls.Add(this.lblTitulo);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "frmPrincipal";
            this.Text = "Final Sistemas Operativos";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmPrincipal_FormClosing);
            this.grbMenuPrincipal.ResumeLayout(false);
            this.mnuCerrar.ResumeLayout(false);
            this.mnuCerrar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox grbMenuPrincipal;
        private System.Windows.Forms.Button btnDivision;
        private System.Windows.Forms.Button btnMultipl;
        private System.Windows.Forms.Button btnResta;
        private System.Windows.Forms.Button btnSuma;
        private System.Windows.Forms.Label lblTitulo;
        private System.ComponentModel.BackgroundWorker wrkArranque;
        private System.Windows.Forms.MenuStrip mnuCerrar;
        private System.Windows.Forms.ToolStripMenuItem tsmiSalir;
    }
}

