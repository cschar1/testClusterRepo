using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
            Clicked = false;

            //Console commit done from GITHUB
            Console.WriteLine("Form created ");
        }
        Boolean Clicked;
        private void button1_Click(object sender, EventArgs e)
        {
            Clicked = !Clicked;
            if (Clicked)
                button1.Text += "1";
            else 
                button1.Text = "clicked";
        }
    }
}
