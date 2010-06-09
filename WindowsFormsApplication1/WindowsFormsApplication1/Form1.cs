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
using Microsoft.VisualBasic.ApplicationServices;
using System.Threading;

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
            Console.WriteLine("Components size" + this.Controls.Count);

            bool done = false;
            ThreadPool.QueueUserWorkItem((x) =>
            {
                using (var splashForm = new LoadingForm())
                {
                    
                    splashForm.Show();
                    while (!done)
                        Application.DoEvents();

                    splashForm.Close();
                }
            });

            //Simulate WOrk
            Thread.Sleep(1000);

            //end loading screen
            done = true;
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

        //private void Form1_Load(object sender, EventArgs e)
        //{
        //    Hide();
        //    bool done = false;
        //    ThreadPool.QueueUserWorkItem((x) =>
        //        {
        //            using (var splashForm = new LoadingForm())
        //            {
        //                splashForm.Show();
        //                while (!done)
        //                    Application.DoEvents();
                        
        //                splashForm.Close();
        //            }
        //        });

        //    for (int i = 0; i < 30; i++)
        //    {
        //        for (int j = 0; j < 20; j++)
        //        {
        //            Console.WriteLine("working ... " + i + " " + j);
        //        }
        //    }
        //    Console.WriteLine("Done Work.... Showing main form");
        //    done = true;
        //    Show();
        //}
    }
}
