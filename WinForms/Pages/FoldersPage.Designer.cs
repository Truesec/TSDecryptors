using System.Windows.Forms;

namespace Truesec.Decryptors.Pages
{
    partial class FoldersPage
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
            this.lstFiles = new System.Windows.Forms.ListView();
            this.colName = new System.Windows.Forms.ColumnHeader();
            this.btnAddFolders = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.ofdGetFiles = new System.Windows.Forms.OpenFileDialog();
            this.fbdFolders = new System.Windows.Forms.FolderBrowserDialog();
            this.SuspendLayout();
            // 
            // lstFiles
            // 
            this.lstFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstFiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colName});
            this.lstFiles.Location = new System.Drawing.Point(8, 107);
            this.lstFiles.Margin = new System.Windows.Forms.Padding(8, 4, 8, 8);
            this.lstFiles.Name = "lstFiles";
            this.lstFiles.Size = new System.Drawing.Size(624, 365);
            this.lstFiles.TabIndex = 0;
            this.lstFiles.UseCompatibleStateImageBehavior = false;
            this.lstFiles.View = System.Windows.Forms.View.Details;
            // 
            // colName
            // 
            this.colName.Text = "Folder Path";
            this.colName.Width = 500;
            // 
            // btnAddFolders
            // 
            this.btnAddFolders.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddFolders.Location = new System.Drawing.Point(532, 69);
            this.btnAddFolders.Margin = new System.Windows.Forms.Padding(4, 8, 8, 4);
            this.btnAddFolders.Name = "btnAddFolders";
            this.btnAddFolders.Size = new System.Drawing.Size(100, 30);
            this.btnAddFolders.TabIndex = 1;
            this.btnAddFolders.Text = "Add folder(s)";
            this.btnAddFolders.UseVisualStyleBackColor = true;
            this.btnAddFolders.Click += new System.EventHandler(this.btnAddFolders_Click);
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClear.Location = new System.Drawing.Point(424, 69);
            this.btnClear.Margin = new System.Windows.Forms.Padding(4, 8, 4, 4);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(100, 30);
            this.btnClear.TabIndex = 3;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // ofdGetFiles
            // 
            this.ofdGetFiles.Multiselect = true;
            // 
            // FoldersPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnAddFolders);
            this.Controls.Add(this.lstFiles);
            this.Name = "FoldersPage";
            this.Size = new System.Drawing.Size(640, 480);
            this.ResumeLayout(false);

        }

        #endregion

        private ListView lstFiles;
        private ColumnHeader colName;
        private Button btnAddFolders;
        private Button btnClear;
        private OpenFileDialog ofdGetFiles;
        private FolderBrowserDialog fbdFolders;
    }
}
