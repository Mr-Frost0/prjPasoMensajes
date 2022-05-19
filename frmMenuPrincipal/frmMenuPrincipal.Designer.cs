
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
            this.components = new System.ComponentModel.Container();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.grbMenuPrincipal = new System.Windows.Forms.GroupBox();
            this.btnDetenerActu = new System.Windows.Forms.Button();
            this.btnDivision = new System.Windows.Forms.Button();
            this.btnActuMsgs = new System.Windows.Forms.Button();
            this.btnMultipl = new System.Windows.Forms.Button();
            this.btnResta = new System.Windows.Forms.Button();
            this.btnSuma = new System.Windows.Forms.Button();
            this.bgwRecibeMensajes = new System.ComponentModel.BackgroundWorker();
            this.tmrEspera = new System.Windows.Forms.Timer(this.components);
            this.lstPIDs = new System.Windows.Forms.ListBox();
            this.grbPIDs = new System.Windows.Forms.GroupBox();
            this.txtMensajes = new System.Windows.Forms.TextBox();
            this.grbMensajes = new System.Windows.Forms.GroupBox();
            this.grbMenuPrincipal.SuspendLayout();
            this.grbPIDs.SuspendLayout();
            this.grbMensajes.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitulo
            // 
            this.lblTitulo.Font = new System.Drawing.Font("Verdana", 23.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.Location = new System.Drawing.Point(12, 24);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(1124, 114);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "[Test] Instanciador de Calculadoras - Versión no Final";
            this.lblTitulo.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // grbMenuPrincipal
            // 
            this.grbMenuPrincipal.Controls.Add(this.btnDetenerActu);
            this.grbMenuPrincipal.Controls.Add(this.btnDivision);
            this.grbMenuPrincipal.Controls.Add(this.btnActuMsgs);
            this.grbMenuPrincipal.Controls.Add(this.btnMultipl);
            this.grbMenuPrincipal.Controls.Add(this.btnResta);
            this.grbMenuPrincipal.Controls.Add(this.btnSuma);
            this.grbMenuPrincipal.Location = new System.Drawing.Point(12, 141);
            this.grbMenuPrincipal.Name = "grbMenuPrincipal";
            this.grbMenuPrincipal.Size = new System.Drawing.Size(207, 414);
            this.grbMenuPrincipal.TabIndex = 1;
            this.grbMenuPrincipal.TabStop = false;
            this.grbMenuPrincipal.Text = "Opciones";
            // 
            // btnDetenerActu
            // 
            this.btnDetenerActu.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.btnDetenerActu.Location = new System.Drawing.Point(29, 349);
            this.btnDetenerActu.Name = "btnDetenerActu";
            this.btnDetenerActu.Size = new System.Drawing.Size(141, 49);
            this.btnDetenerActu.TabIndex = 5;
            this.btnDetenerActu.Text = "Detener Actualización";
            this.btnDetenerActu.UseVisualStyleBackColor = true;
            this.btnDetenerActu.Click += new System.EventHandler(this.btnDetenerActu_Click);
            // 
            // btnDivision
            // 
            this.btnDivision.Location = new System.Drawing.Point(29, 212);
            this.btnDivision.Name = "btnDivision";
            this.btnDivision.Size = new System.Drawing.Size(141, 49);
            this.btnDivision.TabIndex = 3;
            this.btnDivision.Text = "División";
            this.btnDivision.UseVisualStyleBackColor = true;
            this.btnDivision.Click += new System.EventHandler(this.btnDivision_Click);
            // 
            // btnActuMsgs
            // 
            this.btnActuMsgs.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.btnActuMsgs.Location = new System.Drawing.Point(29, 294);
            this.btnActuMsgs.Name = "btnActuMsgs";
            this.btnActuMsgs.Size = new System.Drawing.Size(141, 49);
            this.btnActuMsgs.TabIndex = 4;
            this.btnActuMsgs.Text = "Actualizar Mensajes";
            this.btnActuMsgs.UseVisualStyleBackColor = true;
            this.btnActuMsgs.Click += new System.EventHandler(this.btnActuMsgs_Click);
            // 
            // btnMultipl
            // 
            this.btnMultipl.Location = new System.Drawing.Point(29, 157);
            this.btnMultipl.Name = "btnMultipl";
            this.btnMultipl.Size = new System.Drawing.Size(141, 49);
            this.btnMultipl.TabIndex = 2;
            this.btnMultipl.Text = "Multiplicación";
            this.btnMultipl.UseVisualStyleBackColor = true;
            this.btnMultipl.Click += new System.EventHandler(this.btnMultipl_Click);
            // 
            // btnResta
            // 
            this.btnResta.Location = new System.Drawing.Point(29, 102);
            this.btnResta.Name = "btnResta";
            this.btnResta.Size = new System.Drawing.Size(141, 49);
            this.btnResta.TabIndex = 1;
            this.btnResta.Text = "Resta";
            this.btnResta.UseVisualStyleBackColor = true;
            this.btnResta.Click += new System.EventHandler(this.btnResta_Click);
            // 
            // btnSuma
            // 
            this.btnSuma.Location = new System.Drawing.Point(29, 47);
            this.btnSuma.Name = "btnSuma";
            this.btnSuma.Size = new System.Drawing.Size(141, 49);
            this.btnSuma.TabIndex = 0;
            this.btnSuma.Text = "Suma";
            this.btnSuma.UseVisualStyleBackColor = true;
            this.btnSuma.Click += new System.EventHandler(this.btnSuma_Click);
            // 
            // bgwRecibeMensajes
            // 
            this.bgwRecibeMensajes.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwRecibeMensajes_DoWork);
            this.bgwRecibeMensajes.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwRecibeMensajes_RunWorkerCompleted);
            // 
            // tmrEspera
            // 
            this.tmrEspera.Interval = 2000;
            // 
            // lstPIDs
            // 
            this.lstPIDs.BackColor = System.Drawing.SystemColors.Control;
            this.lstPIDs.FormattingEnabled = true;
            this.lstPIDs.ItemHeight = 24;
            this.lstPIDs.Location = new System.Drawing.Point(6, 38);
            this.lstPIDs.Name = "lstPIDs";
            this.lstPIDs.Size = new System.Drawing.Size(238, 364);
            this.lstPIDs.TabIndex = 0;
            // 
            // grbPIDs
            // 
            this.grbPIDs.Controls.Add(this.lstPIDs);
            this.grbPIDs.Location = new System.Drawing.Point(886, 141);
            this.grbPIDs.Name = "grbPIDs";
            this.grbPIDs.Size = new System.Drawing.Size(250, 414);
            this.grbPIDs.TabIndex = 4;
            this.grbPIDs.TabStop = false;
            this.grbPIDs.Text = "Ventana PIDs";
            // 
            // txtMensajes
            // 
            this.txtMensajes.BackColor = System.Drawing.SystemColors.Control;
            this.txtMensajes.Location = new System.Drawing.Point(6, 40);
            this.txtMensajes.Multiline = true;
            this.txtMensajes.Name = "txtMensajes";
            this.txtMensajes.ReadOnly = true;
            this.txtMensajes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtMensajes.Size = new System.Drawing.Size(644, 362);
            this.txtMensajes.TabIndex = 8;
            // 
            // grbMensajes
            // 
            this.grbMensajes.Controls.Add(this.txtMensajes);
            this.grbMensajes.Location = new System.Drawing.Point(224, 141);
            this.grbMensajes.Name = "grbMensajes";
            this.grbMensajes.Size = new System.Drawing.Size(656, 416);
            this.grbMensajes.TabIndex = 7;
            this.grbMensajes.TabStop = false;
            this.grbMensajes.Text = "Ventana Mensajes";
            // 
            // frmPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1148, 616);
            this.Controls.Add(this.grbMensajes);
            this.Controls.Add(this.grbPIDs);
            this.Controls.Add(this.grbMenuPrincipal);
            this.Controls.Add(this.lblTitulo);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "frmPrincipal";
            this.Text = "Final Sistemas Operativos";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmPrincipal_FormClosing);
            this.grbMenuPrincipal.ResumeLayout(false);
            this.grbPIDs.ResumeLayout(false);
            this.grbMensajes.ResumeLayout(false);
            this.grbMensajes.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.GroupBox grbMenuPrincipal;
        private System.Windows.Forms.Button btnDivision;
        private System.Windows.Forms.Button btnMultipl;
        private System.Windows.Forms.Button btnResta;
        private System.Windows.Forms.Button btnSuma;
        private System.Windows.Forms.Button btnActuMsgs;
        private System.ComponentModel.BackgroundWorker bgwRecibeMensajes;
        private System.Windows.Forms.Button btnDetenerActu;
        private System.Windows.Forms.Timer tmrEspera;
        private System.Windows.Forms.ListBox lstPIDs;
        private System.Windows.Forms.GroupBox grbPIDs;
        private System.Windows.Forms.TextBox txtMensajes;
        private System.Windows.Forms.GroupBox grbMensajes;
    }
}

