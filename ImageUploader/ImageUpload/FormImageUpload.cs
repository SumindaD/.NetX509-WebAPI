using ImageUploader.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageUploader
{
    public partial class FormImageUpload : Form
    {
        public FormImageUpload()
        {
            InitializeComponent();
            labelFileName.Text = string.Empty;
            labelUploadSuccess.Text = string.Empty;
        }

        private void ButtonAddFile_Click(object sender, EventArgs e)
        {
            openFileDialog.CheckFileExists = true;
            openFileDialog.AddExtension = true;
            openFileDialog.Filter = "Image files (*.png)| *.png;";
            openFileDialog.Multiselect = false;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                labelFileName.Text = openFileDialog.SafeFileName;
                errorProvider.SetError(buttonAddFile, "");
            }
        }

        private void TextBoxRequestNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Allow numerics only
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
        }

        private async void buttonUpload_Click(object sender, EventArgs e)
        {
            if (
                    FormImageUploadValidator.ValidRequiredTextBoxFields(errorProvider, textBoxRequestNumber, textBoxUserName) &&
                    FormImageUploadValidator.ValidateFileInputField(errorProvider, openFileDialog, buttonAddFile) &&
                    FormImageUploadValidator.ValidateRequestNumber(errorProvider, textBoxRequestNumber)
                )
            {
                disableAllButtons();

                var response = await HTTPUtility.PostData(
                    ConfigurationManager.AppSettings["SurityRestAPIBaseURL"] + ConfigurationManager.AppSettings["ImageUploadEndpoint"],
                    new 
                    { 
                        RequestNumber = textBoxRequestNumber.Text,
                        UserName = textBoxUserName.Text,
                        ImageName = openFileDialog.SafeFileName,
                        ImageData = File.ReadAllBytes(openFileDialog.FileName)
                    }
                );

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    errorProvider.SetError(buttonUpload, string.Empty);
                    showUploadSuccessText();
                }
                else 
                {
                    errorProvider.SetError(buttonUpload, await response.Content.ReadAsStringAsync());
                    labelUploadSuccess.Text = string.Empty;
                }

                enableAllButtons();
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            openFileDialog.Reset();
            labelFileName.Text = openFileDialog.SafeFileName;
            errorProvider.SetError(buttonAddFile, string.Empty);

            textBoxRequestNumber.Text = string.Empty;
            errorProvider.SetError(textBoxRequestNumber, string.Empty);

            textBoxUserName.Text = string.Empty;
            errorProvider.SetError(textBoxUserName, string.Empty);

            errorProvider.SetError(buttonUpload, string.Empty);

            labelUploadSuccess.Text = string.Empty;
        }

        private void showUploadSuccessText() 
        {
            labelUploadSuccess.Text = "✓";
            labelUploadSuccess.ForeColor = Color.Green;
        }

        private void disableAllButtons() 
        {
            buttonUpload.Enabled = false;
            buttonCancel.Enabled = false;
            buttonAddFile.Enabled = false;
        }

        private void enableAllButtons() 
        {
            buttonUpload.Enabled = true;
            buttonCancel.Enabled = true;
            buttonAddFile.Enabled = true;
        }
    }
}
