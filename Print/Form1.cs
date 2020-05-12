using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Print
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            printDialog1.ShowDialog();
            var t = printDialog1.PrinterSettings;
            //textBox1.Text=printDialog1.
            printDocument1.PrinterSettings = t;
        }
        private string printText = "";

        private void Button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(comboBox1.Text))
            {
                MessageBox.Show("请选择字体");
                return;
            }
            if (string.IsNullOrEmpty(fontSize_TextBox.Text))
            {
                MessageBox.Show("请设置字体大小");
                return;
            }
            printPreviewControl1.Document = printDocument1;
            printPreviewControl1.Visible = true;
            printText = textBox1.Text + textBox7.Text + textBox8.Text;
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            var min = int.Parse(textBox8.Text);
            var max = int.Parse(textBox9.Text);
            var count = int.Parse(textBox10.Text);
            for (int i = min; i <= max; i++)
            {
                printText = textBox1.Text + textBox7.Text + i;
                for (int j = 0; j < count; j++)
                {
                    printDocument1.Print();
                }
            }
        }

        private void PrintDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            int x = e.PageBounds.Left + 10;
            int y = e.PageBounds.Top + 50;
            Graphics g = e.Graphics;
            Font font = new Font(comboBox1.Text, Convert.ToInt32(fontSize_TextBox.Text), checkBox1.Checked ? FontStyle.Bold : FontStyle.Regular);
            SolidBrush brush = new SolidBrush(Color.Green);
            string text = printText;
            SizeF sf = g.MeasureString(text, font); // 计算出来文字所占矩形区域
            // 左上角定位 
            float x1 = float.Parse(textBox2.Text);
            float y1 = float.Parse(textBox4.Text);
            Matrix matrix = g.Transform;
            var isTry = float.TryParse(textBox3.Text, out float jiaodu);
            matrix.RotateAt(isTry ? jiaodu : 0F, new PointF(x1 + sf.Width / 2, y1 + sf.Height / 2));
            matrix.Scale(float.Parse(textBox5.Text), float.Parse(textBox6.Text));   //
            g.Transform = matrix;
            // 写上自定义角度的文字
            g.DrawString(text, font, new SolidBrush(Color.Black), x1, y1);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InstalledFontCollection MyFont = new InstalledFontCollection();
            comboBox1.Items.AddRange(MyFont.Families.Select(i => i.Name).ToArray());
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();
            var fileUrl = saveFileDialog1.FileName;
            var json = JsonConvert.SerializeObject(printDocument1.PrinterSettings);
            File.WriteAllText(fileUrl, json);
        }

        private void Button5_Click(object sender, EventArgs e)
        {

        }
    }
}
