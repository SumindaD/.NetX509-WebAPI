using System.Windows.Forms;

namespace ImageUploader
{
    partial class FormImageUpload
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
            this.buttonUpload = new System.Windows.Forms.Button();
            this.textBoxUserName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxRequestNumber = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonAddFile = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.labelValidationMessage = new System.Windows.Forms.Label();
            this.labelFileName = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonUpload
            // 
            this.buttonUpload.Location = new System.Drawing.Point(27, 222);
            this.buttonUpload.Name = "buttonUpload";
            this.buttonUpload.Size = new System.Drawing.Size(362, 23);
            this.buttonUpload.TabIndex = 13;
            this.buttonUpload.Text = "Upload";
            this.buttonUpload.UseVisualStyleBackColor = true;
            // 
            // textBoxUserName
            // 
            this.textBoxUserName.Location = new System.Drawing.Point(154, 83);
            this.textBoxUserName.Name = "textBoxUserName";
            this.textBoxUserName.Size = new System.Drawing.Size(235, 20);
            this.textBoxUserName.TabIndex = 12;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 90);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "User Name";
            // 
            // textBoxRequestNumber
            // 
            this.textBoxRequestNumber.AccessibleName = "Request number";
            this.textBoxRequestNumber.Location = new System.Drawing.Point(154, 28);
            this.textBoxRequestNumber.MaxLength = 8;
            this.textBoxRequestNumber.Name = "textBoxRequestNumber";
            this.textBoxRequestNumber.Size = new System.Drawing.Size(235, 20);
            this.textBoxRequestNumber.TabIndex = 10;
            this.textBoxRequestNumber.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxRequestNumber_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Request Number";
            // 
            // buttonAddFile
            // 
            this.buttonAddFile.Location = new System.Drawing.Point(27, 145);
            this.buttonAddFile.Name = "buttonAddFile";
            this.buttonAddFile.Size = new System.Drawing.Size(75, 23);
            this.buttonAddFile.TabIndex = 8;
            this.buttonAddFile.Text = "Add File";
            this.buttonAddFile.UseVisualStyleBackColor = true;
            this.buttonAddFile.Click += new System.EventHandler(this.buttonAddFile_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog";
            // 
            // labelValidationMessage
            // 
            this.labelValidationMessage.AutoSize = true;
            this.labelValidationMessage.Location = new System.Drawing.Point(151, 188);
            this.labelValidationMessage.Name = "labelValidationMessage";
            this.labelValidationMessage.Size = new System.Drawing.Size(35, 13);
            this.labelValidationMessage.TabIndex = 15;
            this.labelValidationMessage.Text = "label3";
            // 
            // labelFileName
            // 
            this.labelFileName.AutoSize = true;
            this.labelFileName.Location = new System.Drawing.Point(151, 150);
            this.labelFileName.Name = "labelFileName";
            this.labelFileName.Size = new System.Drawing.Size(35, 13);
            this.labelFileName.TabIndex = 16;
            this.labelFileName.Text = "label3";
            // 
            // FormImageUpload
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(421, 257);
            this.Controls.Add(this.labelFileName);
            this.Controls.Add(this.labelValidationMessage);
            this.Controls.Add(this.buttonUpload);
            this.Controls.Add(this.textBoxUserName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxRequestNumber);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonAddFile);
            this.MaximizeBox = false;
            this.Name = "FormImageUpload";
            this.Text = "Image Uploader";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonUpload;
        private System.Windows.Forms.TextBox textBoxUserName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxRequestNumber;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonAddFile;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Label labelValidationMessage;
        private System.Windows.Forms.Label labelFileName;
    }
}