using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WindowsFormsControlLibrary1;

namespace WindowsFormsApplication1
{
    public partial class LoadingForm : Form
    {
        private LoadingControl loader;
   
        public LoadingForm()
        {
            InitializeComponent();
            loader = new LoadingControl(Color.DarkOliveGreen,
                Color.LightGoldenrodYellow, 150);

            this.Controls.Add(loader);
            loader.Show();
            loader.Location = new Point(40, 40);
                                        
        }
    }
}
