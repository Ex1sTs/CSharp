using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab
{
    public partial class Form1 : Form
    {
        Bitmap imageOne;
 

        public Form1()
        {
            InitializeComponent();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                imageOne = new Bitmap(@"D:\_Desktop\1 лаба\3.jpg", true);
                pictureBox1.Image = imageOne;
            }
            catch (ArgumentException)
            {
                MessageBox.Show("There was an error." +
                    "Check the path to the image file.");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (imageOne == null)
            {
                MessageBox.Show("There was an error." +
                   "Check the path to the image file.");
            }
            else
            {
                // определяем размеры гистограммы. В идеале, ширина должны быть кратна 768 - 
                // по пикселю на каждый столбик каждого из каналов
                int width = 256, height = 256;
                // получаем битмап из изображения
                Bitmap bmp = imageOne;
				// создаем саму гистограмму        
				Bitmap histGreen = new Bitmap(width,height);
				Bitmap histRed = new Bitmap(width,height);
				Bitmap histBlue = new Bitmap(width,height);
				// создаем массивы, в котором будут содержаться количества повторений для каждого из значений каналов.
				// индекс соответствует значению канала
				int[] R = new int[256];
                int[] G = new int[256];
                int[] B = new int[256];
                int i, j;
				for (i = 0; i < width; i++)
					for (j = 0; j < height; j++)
					{
						histRed.SetPixel(i, j, Color.White);
						histGreen.SetPixel(i, j, Color.White);
						histBlue.SetPixel(i, j, Color.White);
					}
                // собираем статистику для изображения
                for (i = 0; i < bmp.Width; ++i)
                    for (j = 0; j < bmp.Height; ++j)
                    {
						R[bmp.GetPixel(i, j).R]++;
						G[bmp.GetPixel(i, j).G]++;
						B[bmp.GetPixel(i, j).B]++;
					}
                // находим самый высокий столбец, чтобы корректно масштабировать гистограмму по высоте
                int max = 0;
                for (i = 0; i < 256; ++i)
                {
                    if (R[i] > max)
                        max = R[i];
                    if (G[i] > max)
                        max = G[i];
                    if (B[i] > max)
                        max = B[i];
                }
				for (i = 0; i < R.Length; ++i)
				{
					R[i] = height * R[i] / max;
					G[i] = height * G[i] / max;
					B[i] = height * B[i] / max;
				}
				for (i = 0; i < width; i++)
				{
					for (j = height - 1; j >= height - R[i]; j--)
						histRed.SetPixel(i, j, Color.Red);
					for (j = height - 1; j >= height - G[i]; j--)
						histGreen.SetPixel(i, j, Color.Green);
					for (j = height - 1; j >= height - B[i]; j--)
						histBlue.SetPixel(i, j, Color.Blue);
				}
				
				histBlue.Save("BlueHist.jpeg");
				histGreen.Save("GreenHist.jpeg");
				histRed.Save("RedHist.jpeg");
				pictureBox2.Image = histRed;
				pictureBox3.Image = histGreen;
				pictureBox4.Image = histBlue;
				MessageBox.Show("Гистограмма построена");
            }
			
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

		private void PictureBox3_Click(object sender, EventArgs e)
		{

		}

		private void PictureBox4_Click(object sender, EventArgs e)
		{

		}
	}
}
