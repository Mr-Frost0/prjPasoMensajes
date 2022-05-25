namespace frmGUI
{
    partial class frmGUI
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
            this.grbMensajes = new System.Windows.Forms.GroupBox();
            this.txtMensajes = new System.Windows.Forms.TextBox();
            this.grbPIDs = new System.Windows.Forms.GroupBox();
            this.lstPIDActuales = new System.Windows.Forms.ListBox();
            this.lstHistorialPIDs = new System.Windows.Forms.ListBox();
            this.btnDetenerActu = new System.Windows.Forms.Button();
            this.btnActuMsgs = new System.Windows.Forms.Button();
            this.wrkMsgTextBox = new System.ComponentModel.BackgroundWorker();
            this.mnuOpciones = new System.Windows.Forms.MenuStrip();
            this.tsmiIniciar = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiNuevaInstancia = new System.Windows.Forms.ToolStripMenuItem();
            this.wrkMsgPID = new System.ComponentModel.BackgroundWorker();
            this.wrkArranque = new System.ComponentModel.BackgroundWorker();
            this.grbMensajes.SuspendLayout();
            this.grbPIDs.SuspendLayout();
            this.mnuOpciones.SuspendLayout();
            this.SuspendLayout();
            // 
            // grbMensajes
            // 
            this.grbMensajes.Controls.Add(this.txtMensajes);
            this.grbMensajes.Location = new System.Drawing.Point(12, 50);
            this.grbMensajes.Name = "grbMensajes";
            this.grbMensajes.Size = new System.Drawing.Size(656, 460);
            this.grbMensajes.TabIndex = 9;
            this.grbMensajes.TabStop = false;
            this.grbMensajes.Text = "Ventana Mensajes";
            // 
            // txtMensajes
            // 
            this.txtMensajes.BackColor = System.Drawing.SystemColors.Control;
            this.txtMensajes.Location = new System.Drawing.Point(6, 28);
            this.txtMensajes.Multiline = true;
            this.txtMensajes.Name = "txtMensajes";
            this.txtMensajes.ReadOnly = true;
            this.txtMensajes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtMensajes.Size = new System.Drawing.Size(644, 426);
            this.txtMensajes.TabIndex = 8;
            // 
            // grbPIDs
            // 
            this.grbPIDs.Controls.Add(this.lstPIDActuales);
            this.grbPIDs.Controls.Add(this.lstHistorialPIDs);
            this.grbPIDs.Location = new System.Drawing.Point(674, 50);
            this.grbPIDs.Name = "grbPIDs";
            this.grbPIDs.Size = new System.Drawing.Size(555, 379);
            this.grbPIDs.TabIndex = 8;
            this.grbPIDs.TabStop = false;
            this.grbPIDs.Text = "Ventana PIDs";
            // 
            // lstPIDActuales
            // 
            this.lstPIDActuales.BackColor = System.Drawing.SystemColors.Control;
            this.lstPIDActuales.FormattingEnabled = true;
            this.lstPIDActuales.ItemHeight = 24;
            this.lstPIDActuales.Location = new System.Drawing.Point(12, 216);
            this.lstPIDActuales.Name = "lstPIDActuales";
            this.lstPIDActuales.ScrollAlwaysVisible = true;
            this.lstPIDActuales.Size = new System.Drawing.Size(537, 148);
            this.lstPIDActuales.TabIndex = 2;
            // 
            // lstHistorialPIDs
            // 
            this.lstHistorialPIDs.BackColor = System.Drawing.SystemColors.Control;
            this.lstHistorialPIDs.FormattingEnabled = true;
            this.lstHistorialPIDs.ItemHeight = 24;
            this.lstHistorialPIDs.Location = new System.Drawing.Point(12, 38);
            this.lstHistorialPIDs.Name = "lstHistorialPIDs";
            this.lstHistorialPIDs.ScrollAlwaysVisible = true;
            this.lstHistorialPIDs.Size = new System.Drawing.Size(537, 172);
            this.lstHistorialPIDs.TabIndex = 1;
            // 
            // btnDetenerActu
            // 
            this.btnDetenerActu.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.btnDetenerActu.Location = new System.Drawing.Point(864, 439);
            this.btnDetenerActu.Name = "btnDetenerActu";
            this.btnDetenerActu.Size = new System.Drawing.Size(176, 71);
            this.btnDetenerActu.TabIndex = 10;
            this.btnDetenerActu.Text = "Detener Actualización";
            this.btnDetenerActu.UseVisualStyleBackColor = true;
            this.btnDetenerActu.Click += new System.EventHandler(this.btnDetenerActu_Click);
            // 
            // btnActuMsgs
            // 
            this.btnActuMsgs.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.btnActuMsgs.Location = new System.Drawing.Point(674, 439);
            this.btnActuMsgs.Name = "btnActuMsgs";
            this.btnActuMsgs.Size = new System.Drawing.Size(176, 71);
            this.btnActuMsgs.TabIndex = 9;
            this.btnActuMsgs.Text = "Iniciar Actualización";
            this.btnActuMsgs.UseVisualStyleBackColor = true;
            this.btnActuMsgs.Click += new System.EventHandler(this.btnActuMsgs_Click);
            // 
            // wrkMsgTextBox
            // 
            this.wrkMsgTextBox.WorkerSupportsCancellation = true;
            this.wrkMsgTextBox.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwRecibeMensajes_DoWork);
            // 
            // mnuOpciones
            // 
            this.mnuOpciones.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiIniciar});
            this.mnuOpciones.Location = new System.Drawing.Point(0, 0);
            this.mnuOpciones.Name = "mnuOpciones";
            this.mnuOpciones.Size = new System.Drawing.Size(1241, 33);
            this.mnuOpciones.TabIndex = 11;
            this.mnuOpciones.Text = "menuStrip1";
            // 
            // tsmiIniciar
            // 
            this.tsmiIniciar.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiNuevaInstancia});
            this.tsmiIniciar.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.tsmiIniciar.Name = "tsmiIniciar";
            this.tsmiIniciar.Size = new System.Drawing.Size(76, 29);
            this.tsmiIniciar.Text = "&Iniciar";
            // 
            // tsmiNuevaInstancia
            // 
            this.tsmiNuevaInstancia.Name = "tsmiNuevaInstancia";
            this.tsmiNuevaInstancia.Size = new System.Drawing.Size(400, 30);
            this.tsmiNuevaInstancia.Text = "&Nueva Instancia Módulo Aplicaciones";
            this.tsmiNuevaInstancia.Click += new System.EventHandler(this.tsmiNuevaInstancia_Click);
            // 
            // wrkMsgPID
            // 
            this.wrkMsgPID.DoWork += new System.ComponentModel.DoWorkEventHandler(this.wrkMsgPID_DoWork);
            // 
            // wrkArranque
            // 
            this.wrkArranque.WorkerSupportsCancellation = true;
            this.wrkArranque.DoWork += new System.ComponentModel.DoWorkEventHandler(this.wrkArranque_DoWork);
            // 
            // frmGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1241, 522);
            this.Controls.Add(this.btnDetenerActu);
            this.Controls.Add(this.grbMensajes);
            this.Controls.Add(this.btnActuMsgs);
            this.Controls.Add(this.grbPIDs);
            this.Controls.Add(this.mnuOpciones);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.MainMenuStrip = this.mnuOpciones;
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "frmGUI";
            this.Text = "Módulo GUI - Final Sistemas Operativos";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmGUI_FormClosing);
            this.grbMensajes.ResumeLayout(false);
            this.grbMensajes.PerformLayout();
            this.grbPIDs.ResumeLayout(false);
            this.mnuOpciones.ResumeLayout(false);
            this.mnuOpciones.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grbMensajes;
        private System.Windows.Forms.TextBox txtMensajes;
        private System.Windows.Forms.GroupBox grbPIDs;
        private System.Windows.Forms.ListBox lstHistorialPIDs;
        private System.Windows.Forms.Button btnDetenerActu;
        private System.Windows.Forms.Button btnActuMsgs;
        private System.ComponentModel.BackgroundWorker wrkMsgTextBox;
        private System.Windows.Forms.MenuStrip mnuOpciones;
        private System.Windows.Forms.ToolStripMenuItem tsmiIniciar;
        private System.Windows.Forms.ToolStripMenuItem tsmiNuevaInstancia;
        private System.ComponentModel.BackgroundWorker wrkMsgPID;
        private System.Windows.Forms.ListBox lstPIDActuales;
        private System.ComponentModel.BackgroundWorker wrkArranque;
    }
}

