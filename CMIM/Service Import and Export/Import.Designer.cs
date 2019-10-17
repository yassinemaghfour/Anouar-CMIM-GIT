namespace Service_Import_and_Export
{
    partial class Import
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Import));
            this.txt_ConsoleLog = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnChooseFile = new System.Windows.Forms.Button();
            this.txtFile = new System.Windows.Forms.TextBox();
            this.rd_ImportEmployees = new System.Windows.Forms.RadioButton();
            this.rdRembourssement = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.menu = new System.Windows.Forms.MenuStrip();
            this.clearLog = new System.Windows.Forms.ToolStripMenuItem();
            this.CopyLog = new System.Windows.Forms.ToolStripMenuItem();
            this.btnImporter = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.menu.SuspendLayout();
            this.SuspendLayout();
            // 
            // txt_ConsoleLog
            // 
            this.txt_ConsoleLog.BackColor = System.Drawing.Color.LightGray;
            this.txt_ConsoleLog.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_ConsoleLog.Location = new System.Drawing.Point(12, 201);
            this.txt_ConsoleLog.Multiline = true;
            this.txt_ConsoleLog.Name = "txt_ConsoleLog";
            this.txt_ConsoleLog.ReadOnly = true;
            this.txt_ConsoleLog.Size = new System.Drawing.Size(836, 248);
            this.txt_ConsoleLog.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btnImporter);
            this.panel1.Controls.Add(this.btnChooseFile);
            this.panel1.Controls.Add(this.txtFile);
            this.panel1.Controls.Add(this.rd_ImportEmployees);
            this.panel1.Controls.Add(this.rdRembourssement);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Location = new System.Drawing.Point(12, 33);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(836, 139);
            this.panel1.TabIndex = 1;
            // 
            // btnChooseFile
            // 
            this.btnChooseFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnChooseFile.Location = new System.Drawing.Point(676, 47);
            this.btnChooseFile.Name = "btnChooseFile";
            this.btnChooseFile.Size = new System.Drawing.Size(145, 34);
            this.btnChooseFile.TabIndex = 8;
            this.btnChooseFile.Text = "Choisissez un fichier";
            this.btnChooseFile.UseVisualStyleBackColor = true;
            this.btnChooseFile.Click += new System.EventHandler(this.btnChooseFile_Click);
            // 
            // txtFile
            // 
            this.txtFile.BackColor = System.Drawing.Color.LightGray;
            this.txtFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFile.Location = new System.Drawing.Point(145, 53);
            this.txtFile.Name = "txtFile";
            this.txtFile.ReadOnly = true;
            this.txtFile.Size = new System.Drawing.Size(521, 23);
            this.txtFile.TabIndex = 7;
            // 
            // rd_ImportEmployees
            // 
            this.rd_ImportEmployees.AutoSize = true;
            this.rd_ImportEmployees.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rd_ImportEmployees.Location = new System.Drawing.Point(422, 14);
            this.rd_ImportEmployees.Name = "rd_ImportEmployees";
            this.rd_ImportEmployees.Size = new System.Drawing.Size(199, 20);
            this.rd_ImportEmployees.TabIndex = 6;
            this.rd_ImportEmployees.TabStop = true;
            this.rd_ImportEmployees.Text = "Importer liste des employées";
            this.rd_ImportEmployees.UseVisualStyleBackColor = true;
            // 
            // rdRembourssement
            // 
            this.rdRembourssement.AutoSize = true;
            this.rdRembourssement.Checked = true;
            this.rdRembourssement.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdRembourssement.Location = new System.Drawing.Point(212, 14);
            this.rdRembourssement.Name = "rdRembourssement";
            this.rdRembourssement.Size = new System.Drawing.Size(196, 20);
            this.rdRembourssement.TabIndex = 5;
            this.rdRembourssement.TabStop = true;
            this.rdRembourssement.Text = "Importer un rembourssement";
            this.rdRembourssement.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F);
            this.label3.Location = new System.Drawing.Point(11, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(132, 23);
            this.label3.TabIndex = 4;
            this.label3.Text = "Le fichier à importer:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(12, 175);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 23);
            this.label2.TabIndex = 3;
            this.label2.Text = "Console";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // menu
            // 
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clearLog,
            this.CopyLog});
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(860, 24);
            this.menu.TabIndex = 4;
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
            // btnImporter
            // 
            this.btnImporter.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImporter.Location = new System.Drawing.Point(252, 87);
            this.btnImporter.Name = "btnImporter";
            this.btnImporter.Size = new System.Drawing.Size(307, 34);
            this.btnImporter.TabIndex = 9;
            this.btnImporter.Text = "Importer les données depuis le fichier";
            this.btnImporter.UseVisualStyleBackColor = true;
            this.btnImporter.Click += new System.EventHandler(this.button1_Click);
            // 
            // Import
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(860, 461);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.txt_ConsoleLog);
            this.Controls.Add(this.menu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Import";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Service d\'importation";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_ConsoleLog;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnChooseFile;
        private System.Windows.Forms.TextBox txtFile;
        private System.Windows.Forms.RadioButton rd_ImportEmployees;
        private System.Windows.Forms.RadioButton rdRembourssement;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.MenuStrip menu;
        private System.Windows.Forms.ToolStripMenuItem clearLog;
        private System.Windows.Forms.ToolStripMenuItem CopyLog;
        private System.Windows.Forms.Button btnImporter;
    }
}