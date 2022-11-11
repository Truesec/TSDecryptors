using System.Windows.Forms;

namespace Truesec.Decryptors.Pages
{
    partial class WelcomePage
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WelcomePage));
            this.SuspendLayout();
            // 
            // WelcomePage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.Description = resources.GetString("$this.Description");
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "WelcomePage";
            this.Size = new System.Drawing.Size(600, 406);
            this.Style = CristiPotlog.Controls.WizardPageStyle.Welcome;
            this.Title = "Chaos Family Decryptors by Truesec";
            this.ResumeLayout(false);

        }

        #endregion
    }
}
