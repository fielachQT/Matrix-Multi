using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Log
{
    public partial class Form2 : Form
    {
        private static int Count { get; set; }
        private static TextBox[,] T_Array { get; set; }
        //Constructor which can dynamically generate the textboxes for the values
        public Form2(int count)
        {
            InitializeComponent();
            Count = count;
            //Dynamically generated form's window
            this.Size = new Size { Width = (40 * count) + 15, Height = 300 };
            TextBox[,] Text_Arr = new TextBox[2, count];
            int x = 30;
            int y = 5;
            for (int i = 0; i < 2; i++)
            {
                y = 5;
                for (int g = 0; g < count; g++)
                {
                    TextBox box = new TextBox();
                    box.Location = new Point(y, x);
                    box.Size = new Size { Width = 30, Height = 40 };
                    this.Controls.Add(box);
                    Text_Arr[i, g] = box;

                    y += 40;
                }
                x += 50;
            }
            T_Array = Text_Arr;
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Calling the third form's constructor
            Form3 form3 = new Form3(T_Array, Count);
            form3.Show();
            this.Hide();
        }
    }
}
