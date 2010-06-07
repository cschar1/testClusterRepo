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
            ClickCount = 0;
        }
        int ClickCount;
        private void button1_Click(object sender, EventArgs e)
        {
            ClickCount++;
            if (ClickCount >= 20){
                button1.Text = "clicked";
                 ClickCount = 0;
             }
            else
                button1.Text = "clicked" + ClickCount;    
        }
    }
}
