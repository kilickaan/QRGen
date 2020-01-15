using QRCoder;
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

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        [Obsolete]
        private void btnGenerate_Click(object sender, EventArgs e)
        {
            GenerateBarcode();

        }

        [Obsolete]
        public void GenerateBarcode()
        {
            try
            {
                String data = txtBarcodeValue.Text;

                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(data, QRCodeGenerator.ECCLevel.Q);
                QRCode qrCode = new QRCode(qrCodeData);
                Bitmap qrCodeImage = qrCode.GetGraphic(10);

                Base64QRCode qrCode2 = new Base64QRCode(qrCodeData);
                string qrCodeImageAsBase64 = qrCode2.GetGraphic(10);

                stringBox.Text = qrCodeImageAsBase64.ToString().Trim();
                picBarcode.Image = qrCodeImage;

                String Imagename = "C://BarcodeData//" + data + "_" + System.DateTime.Now.ToString("ddMMyyyyHHMMss") + ".png";
                if (!File.Exists(Imagename)) File.Create(Imagename).Close();
                picBarcode.Image = System.Drawing.Image.FromFile(@"C://BarcodeData//" + data + "_" + System.DateTime.Now.ToString("ddMMyyyyHHMMss") + ".png");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fdb = new FolderBrowserDialog();
            fdb.Description = "fdf";
            if (fdb.ShowDialog() == DialogResult.OK)
            {
                picBarcode.Image.Save(@fdb.SelectedPath + "\\Image" + System.DateTime.Now.ToString("ddMMyyyyHHmmss") + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                MessageBox.Show("Barcode Image saved Successfully. Path : " + fdb.SelectedPath);
            }
        }

        
    }
}
