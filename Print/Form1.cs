using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Drawing.Text;
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
            printText = textBox1.Text;
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            printText = textBox1.Text;
            printDocument1.Print();
        }

        private void PrintDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            int x = e.PageBounds.Left + 10;
            int y = e.PageBounds.Top + 50;
            Graphics g = e.Graphics;
            Font font = new Font(comboBox1.Text, Convert.ToInt32(fontSize_TextBox.Text));
            SolidBrush brush = new SolidBrush(Color.Green);
            string text = printText;
            g.DrawString(text, font, brush, x, y);
            SizeF sf = g.MeasureString(text, font); // 计算出来文字所占矩形区域
            // 左上角定位
            var locationLeftTop = "0,0";
            string[] location = locationLeftTop.Split(',');
            float x1 = float.Parse(location[0]);
            float y1 = float.Parse(location[1]);
            Matrix matrix = g.Transform;
            matrix.RotateAt(160, new PointF(x1 + sf.Width / 2, y1 + sf.Height / 2));
            g.Transform = matrix;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InstalledFontCollection MyFont = new InstalledFontCollection();
            comboBox1.Items.AddRange(MyFont.Families.Select(i => i.Name).ToArray());
        }
    }
}
