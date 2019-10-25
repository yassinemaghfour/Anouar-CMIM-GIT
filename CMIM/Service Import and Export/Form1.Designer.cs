namespace Service_Import_and_Export
{
    partial class Form1
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.menu = new System.Windows.Forms.MenuStrip();
            this.importationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menu.SuspendLayout();
            this.SuspendLayout();
            // 
            // menu
            // 
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importationToolStripMenuItem,
            this.exportationToolStripMenuItem});
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(904, 24);
            this.menu.TabIndex = 7;
            this.menu.Text = "menuStrip1";
            // 
            // importationToolStripMenuItem
            // 
            this.importationToolStripMenuItem.Name = "importationToolStripMenuItem";
            this.importationToolStripMenuItem.Size = new System.Drawing.Size(82, 20);
            this.importationToolStripMenuItem.Text = "Importation";
            this.importationToolStripMenuItem.Click += new System.EventHandler(this.importationToolStripMenuItem_Click);
            // 
            // exportationToolStripMenuItem
            // 
            this.exportationToolStripMenuItem.Name = "exportationToolStripMenuItem";
            this.exportationToolStripMenuItem.Size = new System.Drawing.Size(80, 20);
            this.exportationToolStripMenuItem.Text = "Exportation";
            this.exportationToolStripMenuItem.Click += new System.EventHandler(this.exportationToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(904, 531);
            this.Controls.Add(this.menu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Service d\'importation et exportation";
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menu;
        private System.Windows.Forms.ToolStripMenuItem importationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportationToolStripMenuItem;
    }
}

