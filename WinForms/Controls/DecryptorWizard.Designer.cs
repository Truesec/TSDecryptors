using Truesec.Decryptors.Pages;

namespace Truesec.Decryptors.Controls
{
    partial class DecryptorWizard
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DecryptorWizard));
            this.wizardPanel = new CristiPotlog.Controls.Wizard();
            this.SuspendLayout();
            // 
            // wizardPanel
            // 
            this.wizardPanel.HeaderImage = ((System.Drawing.Image)(resources.GetObject("wizardPanel.HeaderImage")));
            this.wizardPanel.HelpVisible = true;
            this.wizardPanel.Location = new System.Drawing.Point(0, 0);
            this.wizardPanel.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.wizardPanel.Name = "wizardPanel";
            this.wizardPanel.Size = new System.Drawing.Size(702, 412);
            this.wizardPanel.TabIndex = 0;
            this.wizardPanel.WelcomeImage = ((System.Drawing.Image)(resources.GetObject("wizardPanel.WelcomeImage")));
            this.wizardPanel.AfterSwitchPages += new CristiPotlog.Controls.Wizard.AfterSwitchPagesEventHandler(this.wizardPanel_AfterSwitchPages);
            this.wizardPanel.Cancel += new System.ComponentModel.CancelEventHandler(this.wizardPanel_Cancel);
            this.wizardPanel.Help += new System.EventHandler(this.wizardPanel_Help);
            // 
            // DecryptorWizard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(702, 412);
            this.Controls.Add(this.wizardPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.Name = "DecryptorWizard";
            this.Text = "Chaos Family Decryptors by Truesec";
            this.ResumeLayout(false);

        }

        #endregion

        private CristiPotlog.Controls.Wizard wizardPanel;
    }
}