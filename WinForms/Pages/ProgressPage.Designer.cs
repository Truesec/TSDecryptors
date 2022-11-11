using System.Windows.Forms;

namespace Truesec.Decryptors.Pages
{
    partial class ProgressPage
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
            this.pbMaxProgress = new System.Windows.Forms.ProgressBar();
            this.lstFiles = new System.Windows.Forms.ListView();
            this.colName = new System.Windows.Forms.ColumnHeader();
            this.colProgress = new System.Windows.Forms.ColumnHeader();
            this.pbMidProgress = new System.Windows.Forms.ProgressBar();
            this.pbMinProgress = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // pbMaxProgress
            // 
            this.pbMaxProgress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbMaxProgress.Location = new System.Drawing.Point(3, 432);
            this.pbMaxProgress.Name = "pbMaxProgress";
            this.pbMaxProgress.Size = new System.Drawing.Size(627, 10);
            this.pbMaxProgress.Step = 5;
            this.pbMaxProgress.TabIndex = 0;
            // 
            // lstFiles
            // 
            this.lstFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstFiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colName,
            this.colProgress});
            this.lstFiles.Location = new System.Drawing.Point(4, 67);
            this.lstFiles.Margin = new System.Windows.Forms.Padding(8, 4, 8, 8);
            this.lstFiles.Name = "lstFiles";
            this.lstFiles.Size = new System.Drawing.Size(624, 347);
            this.lstFiles.TabIndex = 1;
            this.lstFiles.UseCompatibleStateImageBehavior = false;
            this.lstFiles.View = System.Windows.Forms.View.Details;
            // 
            // colName
            // 
            this.colName.Text = "File Name";
            this.colName.Width = 500;
            // 
            // colProgress
            // 
            this.colProgress.Text = "Progress";
            this.colProgress.Width = 200;
            // 
            // pbMidProgress
            // 
            this.pbMidProgress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbMidProgress.Location = new System.Drawing.Point(3, 424);
            this.pbMidProgress.Name = "pbMidProgress";
            this.pbMidProgress.Size = new System.Drawing.Size(627, 10);
            this.pbMidProgress.Step = 5;
            this.pbMidProgress.TabIndex = 2;
            // 
            // pbMinProgress
            // 
            this.pbMinProgress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbMinProgress.Location = new System.Drawing.Point(3, 415);
            this.pbMinProgress.Name = "pbMinProgress";
            this.pbMinProgress.Size = new System.Drawing.Size(627, 10);
            this.pbMinProgress.Step = 5;
            this.pbMinProgress.TabIndex = 3;
            // 
            // ProgressPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pbMinProgress);
            this.Controls.Add(this.pbMidProgress);
            this.Controls.Add(this.lstFiles);
            this.Controls.Add(this.pbMaxProgress);
            this.Name = "ProgressPage";
            this.Size = new System.Drawing.Size(633, 444);
            this.ResumeLayout(false);

        }

        #endregion

        private ProgressBar pbMaxProgress;
        private ListView lstFiles;
        private ColumnHeader colName;
        private ColumnHeader colProgress;
        private ProgressBar pbMidProgress;
        private ProgressBar pbMinProgress;
    }
}
