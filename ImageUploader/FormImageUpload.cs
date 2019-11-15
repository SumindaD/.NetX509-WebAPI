using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
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
            labelValidationMessage.Text = "";
            labelFileName.Text = "";
        }

        private void buttonAddFile_Click(object sender, EventArgs e)
        {
            openFileDialog.CheckFileExists = true;
            openFileDialog.AddExtension = true;
            openFileDialog.Multiselect = true;
            openFileDialog.Filter = "Image files (*.png)| *.png;";
            openFileDialog.Multiselect = false;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                FileInfo file = new FileInfo(openFileDialog.FileName);

                long fLengthMb = (file.Length / 1024) / 1024;
                if (fLengthMb > 2)
                {
                    labelValidationMessage.Text = "File is larger than 2Mb";
                    openFileDialog.FileName.Remove(1);
                }
                else
                {
                    labelFileName.Text = openFileDialog.SafeFileName;
                    labelValidationMessage.Text = "";
                }
            }
        }

        private void textBoxRequestNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Allow numerics only
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
        }
    }
}
