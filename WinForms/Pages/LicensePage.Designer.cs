using System.Windows.Forms;

namespace Truesec.Decryptors.Pages
{
    partial class LicensePage
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
            this.rtbLicense = new System.Windows.Forms.RichTextBox();
            this.cbLicenseAccepted = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.rtbLicense.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbLicense.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rtbLicense.Location = new System.Drawing.Point(8, 72);
            this.rtbLicense.Margin = new System.Windows.Forms.Padding(8);
            this.rtbLicense.Multiline = true;
            this.rtbLicense.Name = "textBox1";
            this.rtbLicense.Size = new System.Drawing.Size(624, 365);
            this.rtbLicense.TabIndex = 0;
            this.rtbLicense.TabStop = false;
            // 
            // checkBox1
            // 
            this.cbLicenseAccepted.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbLicenseAccepted.AutoSize = true;
            this.cbLicenseAccepted.Location = new System.Drawing.Point(8, 453);
            this.cbLicenseAccepted.Margin = new System.Windows.Forms.Padding(8);
            this.cbLicenseAccepted.Name = "checkBox1";
            this.cbLicenseAccepted.Size = new System.Drawing.Size(186, 19);
            this.cbLicenseAccepted.TabIndex = 1;
            this.cbLicenseAccepted.Text = "I accept the license agreement";
            this.cbLicenseAccepted.UseVisualStyleBackColor = true;
            this.cbLicenseAccepted.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // LicensePanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cbLicenseAccepted);
            this.Controls.Add(this.rtbLicense);
            this.Name = "LicensePanel";
            this.Size = new System.Drawing.Size(640, 480);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private RichTextBox rtbLicense;
        private CheckBox cbLicenseAccepted;
    }
}
