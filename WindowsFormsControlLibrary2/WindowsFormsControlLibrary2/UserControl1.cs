using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsControlLibrary2
{
    public partial class UserControl1 : UserControl
    {

        private float rotationDegrees;
        public UserControl1()
        {
            InitializeComponent();
            timer1.Start();
            timer1.Interval = 100;
            rotationDegrees = 0.0F;
            this.DoubleBuffered = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Invalidate();
        }

        private void UserControl1_Paint(object sender, PaintEventArgs e)
        {
            //
            //User Control draws Ellipse and rotates about 
            //to timer1_Tick
            //
            //Added in rotateSHape branch
            e.Graphics.TranslateTransform(60, 50);

            if (rotationDegrees >= 360)
                rotationDegrees = 0.0F;
            else
                rotationDegrees += 10.0F;

            e.Graphics.RotateTransform(rotationDegrees);
            e.Graphics.DrawEllipse(Pens.Black, new Rectangle(-40, 0, 90, 30));

        }

        
    }
}
