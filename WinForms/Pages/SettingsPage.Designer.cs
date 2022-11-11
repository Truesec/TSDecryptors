using System.Windows.Forms;

namespace Truesec.Decryptors.Pages
{
    partial class SettingsPage
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
            this.cbRecursive = new System.Windows.Forms.CheckBox();
            this.tbExtension = new System.Windows.Forms.TextBox();
            this.lblExtension = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnIdentify = new System.Windows.Forms.Button();
            this.rbOnyx2 = new System.Windows.Forms.RadioButton();
            this.rbChaos = new System.Windows.Forms.RadioButton();
            this.rbSolidbit = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lblProgress = new System.Windows.Forms.Label();
            this.pbMinimalPartialDownload = new System.Windows.Forms.ProgressBar();
            this.pbPartialDownload = new System.Windows.Forms.ProgressBar();
            this.pbDownload = new System.Windows.Forms.ProgressBar();
            this.btnDownload = new System.Windows.Forms.Button();
            this.lblDatabase = new System.Windows.Forms.Label();
            this.btnSelectDatabase = new System.Windows.Forms.Button();
            this.tbDatabaseFile = new System.Windows.Forms.TextBox();
            this.dlgSelectDatabase = new System.Windows.Forms.OpenFileDialog();
            this.dlgIdentify = new System.Windows.Forms.OpenFileDialog();
            this.dlgSaveFile = new System.Windows.Forms.SaveFileDialog();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbRecursive
            // 
            this.cbRecursive.AutoSize = true;
            this.cbRecursive.Location = new System.Drawing.Point(6, 19);
            this.cbRecursive.Name = "cbRecursive";
            this.cbRecursive.Size = new System.Drawing.Size(178, 17);
            this.cbRecursive.TabIndex = 1;
            this.cbRecursive.Text = "Recursively parse subdirectories";
            this.cbRecursive.UseVisualStyleBackColor = true;
            // 
            // tbExtension
            // 
            this.tbExtension.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbExtension.Location = new System.Drawing.Point(6, 41);
            this.tbExtension.Name = "tbExtension";
            this.tbExtension.Size = new System.Drawing.Size(100, 20);
            this.tbExtension.TabIndex = 2;
            // 
            // lblExtension
            // 
            this.lblExtension.AutoSize = true;
            this.lblExtension.Location = new System.Drawing.Point(112, 43);
            this.lblExtension.Name = "lblExtension";
            this.lblExtension.Size = new System.Drawing.Size(94, 13);
            this.lblExtension.TabIndex = 3;
            this.lblExtension.Text = "File search pattern";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.btnIdentify);
            this.groupBox1.Controls.Add(this.rbOnyx2);
            this.groupBox1.Controls.Add(this.rbChaos);
            this.groupBox1.Controls.Add(this.rbSolidbit);
            this.groupBox1.Location = new System.Drawing.Point(8, 149);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(624, 57);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Decryptor";
            // 
            // btnIdentify
            // 
            this.btnIdentify.Location = new System.Drawing.Point(227, 19);
            this.btnIdentify.Name = "btnIdentify";
            this.btnIdentify.Size = new System.Drawing.Size(104, 23);
            this.btnIdentify.TabIndex = 7;
            this.btnIdentify.Text = "Help me identify";
            this.btnIdentify.UseVisualStyleBackColor = true;
            this.btnIdentify.Click += new System.EventHandler(this.btnIdentify_Click);
            // 
            // rbOnyx2
            // 
            this.rbOnyx2.AutoSize = true;
            this.rbOnyx2.Location = new System.Drawing.Point(67, 22);
            this.rbOnyx2.Name = "rbOnyx2";
            this.rbOnyx2.Size = new System.Drawing.Size(83, 17);
            this.rbOnyx2.TabIndex = 6;
            this.rbOnyx2.TabStop = true;
            this.rbOnyx2.Text = "Onyx/VSOP";
            this.rbOnyx2.UseVisualStyleBackColor = true;
            // 
            // rbChaos
            // 
            this.rbChaos.AutoSize = true;
            this.rbChaos.Location = new System.Drawing.Point(6, 22);
            this.rbChaos.Name = "rbChaos";
            this.rbChaos.Size = new System.Drawing.Size(55, 17);
            this.rbChaos.TabIndex = 5;
            this.rbChaos.TabStop = true;
            this.rbChaos.Text = "Chaos";
            this.rbChaos.UseVisualStyleBackColor = true;
            // 
            // rbSolidbit
            // 
            this.rbSolidbit.AutoSize = true;
            this.rbSolidbit.Location = new System.Drawing.Point(157, 22);
            this.rbSolidbit.Name = "rbSolidbit";
            this.rbSolidbit.Size = new System.Drawing.Size(59, 17);
            this.rbSolidbit.TabIndex = 7;
            this.rbSolidbit.TabStop = true;
            this.rbSolidbit.Text = "Solidbit";
            this.rbSolidbit.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.cbRecursive);
            this.groupBox2.Controls.Add(this.lblExtension);
            this.groupBox2.Controls.Add(this.tbExtension);
            this.groupBox2.Location = new System.Drawing.Point(8, 71);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(624, 72);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "File settings";
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.lblProgress);
            this.groupBox3.Controls.Add(this.pbMinimalPartialDownload);
            this.groupBox3.Controls.Add(this.pbPartialDownload);
            this.groupBox3.Controls.Add(this.pbDownload);
            this.groupBox3.Controls.Add(this.btnDownload);
            this.groupBox3.Controls.Add(this.lblDatabase);
            this.groupBox3.Controls.Add(this.btnSelectDatabase);
            this.groupBox3.Controls.Add(this.tbDatabaseFile);
            this.groupBox3.Location = new System.Drawing.Point(8, 212);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(624, 127);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Database settings";
            // 
            // lblProgress
            // 
            this.lblProgress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblProgress.Location = new System.Drawing.Point(540, 82);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(78, 18);
            this.lblProgress.TabIndex = 13;
            this.lblProgress.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // pbMinimalPartialDownload
            // 
            this.pbMinimalPartialDownload.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbMinimalPartialDownload.Location = new System.Drawing.Point(92, 77);
            this.pbMinimalPartialDownload.Name = "pbMinimalPartialDownload";
            this.pbMinimalPartialDownload.Size = new System.Drawing.Size(442, 10);
            this.pbMinimalPartialDownload.TabIndex = 12;
            // 
            // pbPartialDownload
            // 
            this.pbPartialDownload.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbPartialDownload.Location = new System.Drawing.Point(92, 84);
            this.pbPartialDownload.Name = "pbPartialDownload";
            this.pbPartialDownload.Size = new System.Drawing.Size(442, 10);
            this.pbPartialDownload.TabIndex = 11;
            // 
            // pbDownload
            // 
            this.pbDownload.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbDownload.Location = new System.Drawing.Point(92, 93);
            this.pbDownload.Name = "pbDownload";
            this.pbDownload.Size = new System.Drawing.Size(442, 10);
            this.pbDownload.TabIndex = 7;
            // 
            // btnDownload
            // 
            this.btnDownload.Location = new System.Drawing.Point(11, 77);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(75, 23);
            this.btnDownload.TabIndex = 10;
            this.btnDownload.Text = "Download";
            this.btnDownload.UseVisualStyleBackColor = true;
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // lblDatabase
            // 
            this.lblDatabase.AutoSize = true;
            this.lblDatabase.Location = new System.Drawing.Point(8, 52);
            this.lblDatabase.Name = "lblDatabase";
            this.lblDatabase.Size = new System.Drawing.Size(215, 13);
            this.lblDatabase.TabIndex = 9;
            this.lblDatabase.Text = "Select database file (not needed for Solidbit)";
            // 
            // btnSelectDatabase
            // 
            this.btnSelectDatabase.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelectDatabase.Location = new System.Drawing.Point(538, 24);
            this.btnSelectDatabase.Margin = new System.Windows.Forms.Padding(8);
            this.btnSelectDatabase.Name = "btnSelectDatabase";
            this.btnSelectDatabase.Size = new System.Drawing.Size(75, 23);
            this.btnSelectDatabase.TabIndex = 8;
            this.btnSelectDatabase.Text = "Select";
            this.btnSelectDatabase.UseVisualStyleBackColor = true;
            this.btnSelectDatabase.Click += new System.EventHandler(this.btnSelectDatabase_Click);
            // 
            // tbDatabaseFile
            // 
            this.tbDatabaseFile.AcceptsTab = true;
            this.tbDatabaseFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbDatabaseFile.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbDatabaseFile.Location = new System.Drawing.Point(11, 24);
            this.tbDatabaseFile.Margin = new System.Windows.Forms.Padding(8);
            this.tbDatabaseFile.Name = "tbDatabaseFile";
            this.tbDatabaseFile.Size = new System.Drawing.Size(522, 20);
            this.tbDatabaseFile.TabIndex = 7;
            this.tbDatabaseFile.TabStop = false;
            this.tbDatabaseFile.TextChanged += new System.EventHandler(this.tbDatabaseFile_TextChanged);
            // 
            // dlgSelectDatabase
            // 
            this.dlgSelectDatabase.DefaultExt = "db";
            this.dlgSelectDatabase.FileName = "onyx.db";
            this.dlgSelectDatabase.Filter = "Database file|*.db";
            // 
            // SettingsPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "SettingsPage";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.Size = new System.Drawing.Size(640, 480);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private CheckBox cbRecursive;
        private TextBox tbExtension;
        private Label lblExtension;
        private GroupBox groupBox1;
        private RadioButton rbChaos;
        private RadioButton rbOnyx2;
        private RadioButton rbSolidbit;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private Label lblDatabase;
        private Button btnSelectDatabase;
        private TextBox tbDatabaseFile;
        private OpenFileDialog dlgSelectDatabase;
        private Button btnIdentify;
        private OpenFileDialog dlgIdentify;
        private Button btnDownload;
        private SaveFileDialog dlgSaveFile;
        private ProgressBar pbDownload;
        private ProgressBar pbPartialDownload;
        private ProgressBar pbMinimalPartialDownload;
        private Label lblProgress;
    }
}
