using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageUploader.Common
{
    public static class FormImageUploadValidator
    {
        public static bool ValidFileSize(FileInfo fileInfo) 
        {
            long megabytes = (fileInfo.Length / 1024) / 1024;

            if (megabytes > 2)
                return false;
            else
                return true;
        }

        public static bool ValidRequiredTextBoxFields(ErrorProvider errorProvider,params TextBox[] textBoxes) 
        {
            foreach (var textBox in textBoxes)
            {
                if (string.IsNullOrWhiteSpace(textBox.Text))
                {
                    errorProvider.SetError(textBox, LanguageConstants.FieldIsRequired);
                    return false;
                }
                else
                    errorProvider.SetError(textBox, string.Empty);
            }

            return true;
        }

        public static bool ValidateFileInputField(ErrorProvider errorProvider, OpenFileDialog openFileDialog, Button fileDialogOpenButton)
        {
            if (string.IsNullOrEmpty(openFileDialog.FileName))
            {
                errorProvider.SetError(fileDialogOpenButton, LanguageConstants.ImageIsRequired);
                return false;
            }
            else 
            {
                errorProvider.SetError(fileDialogOpenButton, string.Empty);
                return true;
            }
        }

        public static bool ValidateRequestNumber(ErrorProvider errorProvider, TextBox textBoxRequestNumber) 
        {
            if (textBoxRequestNumber.TextLength != 8)
            {
                errorProvider.SetError(textBoxRequestNumber, LanguageConstants.RequestNumberDigitsCount);
                return false;
            }
            else 
            {
                errorProvider.SetError(textBoxRequestNumber, string.Empty);
                return true;
            }
        }
    }
}
