namespace Service_Import_and_Export
{
    partial class Export
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Export));
            this.label2 = new System.Windows.Forms.Label();
            this.txt_ConsoleLog = new System.Windows.Forms.TextBox();
            this.menu = new System.Windows.Forms.MenuStrip();
            this.clearLog = new System.Windows.Forms.ToolStripMenuItem();
            this.CopyLog = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rd_Regularisation = new System.Windows.Forms.RadioButton();
            this.rd_rembourssement = new System.Windows.Forms.RadioButton();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.txtId = new System.Windows.Forms.TextBox();
            this.rd_DossiersEnvoyee = new System.Windows.Forms.RadioButton();
            this.rd_Bordereau = new System.Windows.Forms.RadioButton();
            this.lblId = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.output_Grid = new System.Windows.Forms.DataGridView();
            this.Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DateGeneration = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Path = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.menu.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.output_Grid)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(12, 175);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 23);
            this.label2.TabIndex = 5;
            this.label2.Text = "Console";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txt_ConsoleLog
            // 
            this.txt_ConsoleLog.BackColor = System.Drawing.Color.LightGray;
            this.txt_ConsoleLog.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_ConsoleLog.Location = new System.Drawing.Point(12, 201);
            this.txt_ConsoleLog.Multiline = true;
            this.txt_ConsoleLog.Name = "txt_ConsoleLog";
            this.txt_ConsoleLog.ReadOnly = true;
            this.txt_ConsoleLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txt_ConsoleLog.Size = new System.Drawing.Size(409, 248);
            this.txt_ConsoleLog.TabIndex = 4;
            // 
            // menu
            // 
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clearLog,
            this.CopyLog});
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(860, 24);
            this.menu.TabIndex = 6;
            this.menu.Text = "menuStrip1";
            // 
            // clearLog
            // 
            this.clearLog.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.clearLog.Image = global::Service_Import_and_Export.Properties.Resources.cancel;
            this.clearLog.Name = "clearLog";
            this.clearLog.Size = new System.Drawing.Size(28, 20);
            this.clearLog.Text = "Vider console";
            this.clearLog.Click += new System.EventHandler(this.clearLog_Click);
            // 
            // CopyLog
            // 
            this.CopyLog.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.CopyLog.Image = global::Service_Import_and_Export.Properties.Resources.copy;
            this.CopyLog.Name = "CopyLog";
            this.CopyLog.Size = new System.Drawing.Size(28, 20);
            this.CopyLog.Text = "Copier messages du console";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.rd_Regularisation);
            this.panel1.Controls.Add(this.rd_rembourssement);
            this.panel1.Controls.Add(this.btnGenerate);
            this.panel1.Controls.Add(this.txtId);
            this.panel1.Controls.Add(this.rd_DossiersEnvoyee);
            this.panel1.Controls.Add(this.rd_Bordereau);
            this.panel1.Controls.Add(this.lblId);
            this.panel1.Location = new System.Drawing.Point(12, 33);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(836, 139);
            this.panel1.TabIndex = 7;
            // 
            // rd_Regularisation
            // 
            this.rd_Regularisation.AutoSize = true;
            this.rd_Regularisation.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rd_Regularisation.Location = new System.Drawing.Point(12, 96);
            this.rd_Regularisation.Name = "rd_Regularisation";
            this.rd_Regularisation.Size = new System.Drawing.Size(113, 20);
            this.rd_Regularisation.TabIndex = 10;
            this.rd_Regularisation.TabStop = true;
            this.rd_Regularisation.Text = "Régularisation";
            this.rd_Regularisation.UseVisualStyleBackColor = true;
            this.rd_Regularisation.CheckedChanged += new System.EventHandler(this.Options_CheckedChanged);
            // 
            // rd_rembourssement
            // 
            this.rd_rembourssement.AutoSize = true;
            this.rd_rembourssement.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rd_rembourssement.Location = new System.Drawing.Point(12, 70);
            this.rd_rembourssement.Name = "rd_rembourssement";
            this.rd_rembourssement.Size = new System.Drawing.Size(133, 20);
            this.rd_rembourssement.TabIndex = 9;
            this.rd_rembourssement.TabStop = true;
            this.rd_rembourssement.Text = "Rembourssement";
            this.rd_rembourssement.UseVisualStyleBackColor = true;
            this.rd_rembourssement.CheckedChanged += new System.EventHandler(this.Options_CheckedChanged);
            // 
            // btnGenerate
            // 
            this.btnGenerate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGenerate.Location = new System.Drawing.Point(673, 78);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(145, 34);
            this.btnGenerate.TabIndex = 8;
            this.btnGenerate.Text = "Générer le fichier";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // txtId
            // 
            this.txtId.BackColor = System.Drawing.Color.LightGray;
            this.txtId.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtId.Location = new System.Drawing.Point(673, 49);
            this.txtId.Name = "txtId";
            this.txtId.Size = new System.Drawing.Size(145, 23);
            this.txtId.TabIndex = 7;
            // 
            // rd_DossiersEnvoyee
            // 
            this.rd_DossiersEnvoyee.AutoSize = true;
            this.rd_DossiersEnvoyee.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rd_DossiersEnvoyee.Location = new System.Drawing.Point(12, 18);
            this.rd_DossiersEnvoyee.Name = "rd_DossiersEnvoyee";
            this.rd_DossiersEnvoyee.Size = new System.Drawing.Size(220, 20);
            this.rd_DossiersEnvoyee.TabIndex = 6;
            this.rd_DossiersEnvoyee.TabStop = true;
            this.rd_DossiersEnvoyee.Text = "Les dossiers envoyés à la CMIM";
            this.rd_DossiersEnvoyee.UseVisualStyleBackColor = true;
            this.rd_DossiersEnvoyee.CheckedChanged += new System.EventHandler(this.Options_CheckedChanged);
            // 
            // rd_Bordereau
            // 
            this.rd_Bordereau.AutoSize = true;
            this.rd_Bordereau.Checked = true;
            this.rd_Bordereau.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rd_Bordereau.Location = new System.Drawing.Point(12, 44);
            this.rd_Bordereau.Name = "rd_Bordereau";
            this.rd_Bordereau.Size = new System.Drawing.Size(90, 20);
            this.rd_Bordereau.TabIndex = 5;
            this.rd_Bordereau.TabStop = true;
            this.rd_Bordereau.Text = "Bordereau";
            this.rd_Bordereau.UseVisualStyleBackColor = true;
            this.rd_Bordereau.CheckedChanged += new System.EventHandler(this.Options_CheckedChanged);
            // 
            // lblId
            // 
            this.lblId.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F);
            this.lblId.Location = new System.Drawing.Point(673, 20);
            this.lblId.Name = "lblId";
            this.lblId.Size = new System.Drawing.Size(145, 23);
            this.lblId.TabIndex = 4;
            this.lblId.Text = "L\'identifiant:";
            this.lblId.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(427, 175);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 23);
            this.label1.TabIndex = 8;
            this.label1.Text = "Output";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // output_Grid
            // 
            this.output_Grid.AllowUserToAddRows = false;
            this.output_Grid.AllowUserToDeleteRows = false;
            this.output_Grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.output_Grid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Name,
            this.DateGeneration,
            this.Type,
            this.Path});
            this.output_Grid.Location = new System.Drawing.Point(427, 201);
            this.output_Grid.Name = "output_Grid";
            this.output_Grid.ReadOnly = true;
            this.output_Grid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.output_Grid.Size = new System.Drawing.Size(421, 248);
            this.output_Grid.TabIndex = 9;
            this.output_Grid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.output_Grid_CellContentClick);
            // 
            // Name
            // 
            this.Name.HeaderText = "Nom";
            this.Name.Name = "Name";
            this.Name.ReadOnly = true;
            // 
            // DateGeneration
            // 
            this.DateGeneration.HeaderText = "Date de génération";
            this.DateGeneration.Name = "DateGeneration";
            this.DateGeneration.ReadOnly = true;
            // 
            // Type
            // 
            this.Type.HeaderText = "Type";
            this.Type.Name = "Type";
            this.Type.ReadOnly = true;
            // 
            // Path
            // 
            this.Path.HeaderText = "Path";
            this.Path.Name = "Path";
            this.Path.ReadOnly = true;
            // 
            // Export
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(860, 461);
            this.Controls.Add(this.output_Grid);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menu);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txt_ConsoleLog);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            //this.Name = "Export";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Service d\'exportation";
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.output_Grid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_ConsoleLog;
        private System.Windows.Forms.MenuStrip menu;
        private System.Windows.Forms.ToolStripMenuItem clearLog;
        private System.Windows.Forms.ToolStripMenuItem CopyLog;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.TextBox txtId;
        private System.Windows.Forms.RadioButton rd_DossiersEnvoyee;
        private System.Windows.Forms.RadioButton rd_Bordereau;
        private System.Windows.Forms.Label lblId;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView output_Grid;
        private System.Windows.Forms.DataGridViewTextBoxColumn Name;
        private System.Windows.Forms.DataGridViewTextBoxColumn DateGeneration;
        private System.Windows.Forms.DataGridViewTextBoxColumn Type;
        private System.Windows.Forms.DataGridViewTextBoxColumn Path;
        private System.Windows.Forms.RadioButton rd_Regularisation;
        private System.Windows.Forms.RadioButton rd_rembourssement;
    }
}