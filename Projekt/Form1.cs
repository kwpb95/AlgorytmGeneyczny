using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Projekt
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            listBox1.SelectedIndex = 0;
            listBox2.SelectedIndex = 0;
            textBox1.Text = Convert.ToString(hScrollBar1.Value);
            textBox2.Text = Convert.ToString(hScrollBar2.Value);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label2.Text = Convert.ToString(listBox1.SelectedIndex);
        }

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            textBox1.Text = Convert.ToString(hScrollBar1.Value);
        }

        private void hScrollBar2_Scroll(object sender, ScrollEventArgs e)
        {
            textBox2.Text = Convert.ToString(hScrollBar2.Value);
        }

        private void genetic()
        {
            textBox3.Text = "";
            int selectedFunction = listBox1.SelectedIndex;
            int populationSize = hScrollBar2.Value;
            int iterationsNumber = hScrollBar1.Value;
            int selectedMethod = listBox2.SelectedIndex;
            double[,] result;
            GeneticAlgorithm geneticAlgorithm = new GeneticAlgorithm(selectedFunction, populationSize, iterationsNumber, selectedMethod);
            result = geneticAlgorithm.returnResult();
            int l = result.GetLength(0);
            for (int i = 0; i < l; i++)
            {
                
                double x = result[i, 0];
                double y = result[i, 1];
                String text = "";
                text += "Iteracja " + (i + 1) + ": ";
                text += "x = " + x + " ";
                text += "y = " + y + "      ";
                string[] f = new string[4];
                f[0] = "x^2y + 8x - xy^2 + 5y";
                f[1] = "x^2y+xy^2-2x";
                f[2] = "4x+2y^2+xy";
                f[3] = "y^2+2xy^2+3x";
                double functionResult;
                switch (listBox1.SelectedIndex)
                {
                    case 0:
                        functionResult = Math.Pow(x, 2) * y + 8 * x - x * Math.Pow(y, 2) + 5 * y;
                        break;
                    case 1:
                        functionResult = Math.Pow(x, 2) * y + x * Math.Pow(y, 2) - 2 * x;
                        break;
                    case 2:
                        functionResult = 4 * x + 2 * Math.Pow(y, 2) + x * y;
                        break;
                    case 3:
                        functionResult = Math.Pow(y, 2) + 2 * x * Math.Pow(y, 2) + 3 * x;
                        break;
                    default:
                        functionResult = 0;
                        break;


                }
                text += f[listBox1.SelectedIndex] + " = " + functionResult + "\r\n";
                textBox3.Text += text;
            }
        }
        private void button1_Click_1(object sender, EventArgs e)
        {

          
            genetic();
            
        }
    }
}
