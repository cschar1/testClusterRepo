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
           //
            //
            //

            //
            //testing PuTTy
            //

            //Double buffered activated in form.designer
            this.BackColor = Color.LightGray;

            loader = new LoadingControl(Color.Honeydew,
                Color.Transparent, 150);
            this.Controls.Add(loader);
            loader.Show();
            
                                        
        }
    }
}
