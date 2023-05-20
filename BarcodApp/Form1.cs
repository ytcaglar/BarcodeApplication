using BarcodeLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZXing;

namespace BarcodApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            BarcodeGen();
        }

        private void BarcodeGen()
        {
            Barcode barcode = new Barcode();
            Color backColor = Color.Transparent;
            Color foreColor = Color.Black;
            Image image = barcode.Encode(TYPE.CODE128, txtBarcode.Text, foreColor, backColor, 400, 350);
            barcodPic.Image = image;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            using(FolderBrowserDialog saveFolderDialog = new FolderBrowserDialog() { Description = "Select path."})
            {
                if (saveFolderDialog.ShowDialog() == DialogResult.OK)
                {
                    string path = saveFolderDialog.SelectedPath;
                    barcodPic.Image.Save(path + $"/{txtBarcode.Text}.png");
                    MessageBox.Show("Barcode saved.");
                    barcodPic.Image = null;
                    txtBarcode.Text = null;
                }
            }

        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            string path = "";
            using (OpenFileDialog FolderDialog = new OpenFileDialog() { Filter = "PNG|*.png"})
            {
                if (FolderDialog.ShowDialog() == DialogResult.OK)
                {
                    path = FolderDialog.FileName;
                }
            }
            if (path != "")
            {
                BarcodeReader barcodeReader = new BarcodeReader();
                Bitmap bmp = new Bitmap(path);
                var decodeBarcode = barcodeReader.Decode(bmp);

                if (decodeBarcode != null)
                {
                    txtBarcode.Text = decodeBarcode.ToString();
                    BarcodeGen();
                    MessageBox.Show("Barcode Readed!");
                }
                else
                {
                    MessageBox.Show("You made the wrong choice. Please, select a barcode!");
                }
            }
            
        }
    }
}
