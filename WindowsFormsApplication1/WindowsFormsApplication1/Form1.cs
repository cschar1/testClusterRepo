using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WindowsFormsControlLibrary1;
using CalculatorForm;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        private LoadingForm myLoaderForm;
        
        public Form1()
        {
            //
            // Commit safety 1
            //
            InitializeComponent();
            ClickCount = 0;

            //Console commit done from GITHUB
            Console.WriteLine("Form created ");

            


        }
        int ClickCount;
        //
        // added in improveButton
        //

       
        private void button1_Click(object sender, EventArgs e)
        {
            //Button Code to count number of clicks up to a limit of 20
            //Added in the ImprovedButton branch

            ClickCount++;
            if (ClickCount >= 20){
                button1.Text = "clicked";
                 ClickCount = 0;
             }
            else
                button1.Text = "clicked" + ClickCount;    
        }

        private void button2_Click(object sender, EventArgs e)
        {
            myLoaderForm = new LoadingForm();
            myLoaderForm.Show();

            ////Delay a while for loader to run
            //for (int i = 0; i < 30; i++)
            //{
            //    for (int j = 0; j < 30; j++)
            //    {
            //        Console.WriteLine("Waiting with loader" + i + " " + j);
            //    }
            //}
        }


        private void button3_Click(object sender, EventArgs e)
        {
            if (myLoaderForm != null)
            {
                myLoaderForm.Dispose();
            }
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            calcForm aCalcForm = new calcForm();

            aCalcForm.Show();
            aCalcForm.Location = new Point(400, 100);
        }
    }
}
