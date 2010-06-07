using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Diagnostics;

namespace WindowsFormsControlLibrary1
{

  

    public partial class LoadingControl : UserControl
    {
       


        //Class variables
        private float degreesOffset = 0;     
        private int numOfElements = 15;
        private Point[] points1;
        private SolidBrush[] brushArray;
        private int tDist = 60;

        //Shrinking variables
        private Timer shrinkTimer;
        private Boolean shrinkingEnabled = false;
        //private Control cToShow;
        public float shrinkFactor = 0.5F;
        //private Form parent;


        


        /// <summary>
        /// Overloaded constructor
        /// to set custom values
        /// </summary>
        /// <param name="brushColor"></param>
        /// <param name="backgroundColor"></param>
        /// <param name="tickInterval"></param>
        public LoadingControl(Color brushColor ,
            Color backgroundColor, 
            int tickInterval)
        {
            
    
            
            
            InitializeComponent();

            this.BackColor = backgroundColor;
            //SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint |ControlStyles.DoubleBuffer, true);
            this.DoubleBuffered = true;


            timer1.Interval = tickInterval;
            timer1.Start();
            

            shrinkTimer = new Timer();

            //create polygon for basic animation shape
            points1 = trapezoidConstructor(20, 50);

            // Grab 6 brushes with varying alpha values to draw trapezoids w/
            brushArray = new SolidBrush[6];
            populateBrushes(brushColor);

        }

        /// <summary>
        /// Original Constructor
        /// </summary>
        public LoadingControl()
        {
            InitializeComponent();
            timer1.Start();
            //this.DoubleBuffered = true;

            //create polygon for basic animation shape
            points1 = trapezoidConstructor(20, 50);
         
            // Grab 6 brushes with varying alpha values to draw trapezoids w/
            brushArray = new SolidBrush[6];
            populateBrushes(Color.DarkSlateBlue);
            
        }

        ////
        //// Method 1
        ////
        //protected override CreateParams CreateParams
        //{
        //    get
        //    {
        //        CreateParams cp = base.CreateParams;
        //        cp.ExStyle |= 0x00000020; //WS_EX_TRANSPARENT       
        //        return cp;
        //    }
        //}

        //protected override void OnPaintBackground(PaintEventArgs pevent)
        //{
        //    //do not allow the background to be painted 
        //}

        
        //// Ensures that the parent control gets painted before 
        ////graphics output of this control
        //protected void InvalidateEx()
        //{
        //    if (Parent == null)
        //        return;
        //    Rectangle rc = new Rectangle(this.Location, this.Size);
        //    Parent.Invalidate(rc, true);
        //}

        public void shrink(int tickInterval)
        {

            shrinkTimer.Dispose();
            //Set up another timer to keep track of shrinking time intervals
            shrinkingEnabled = true;
            shrinkTimer = new Timer();
            shrinkTimer.Interval = tickInterval;
            shrinkTimer.Start();
            shrinkTimer.Tick += new System.EventHandler(this.shrinkTimer_Tick);

            //set class variable to know what control to reference in 
            //shrinkTimer_tick event handler
     

        }

        private void shrinkTimer_Tick(object sender, EventArgs e)
        {
            
            //shrink and call for a repaint
            if (shrinkFactor > 0.3)
            {
                shrinkFactor = shrinkFactor * 0.9F;
            }
            else
            {
                shrinkFactor = shrinkFactor - 0.05F;
            }
            this.Invalidate();
            //this.InvalidateEx();

            Console.WriteLine("Ticking w/ shirnkFactor of : " + shrinkFactor + "timer value is " + shrinkTimer);

            //When animation has shrunk to almost nothing,
            //Dispose of shrinkTimer resources
            //reset shrinkFactors
            //Set control as invisible
            if (shrinkFactor < 0.1)
            {             
                shrinkTimer.Stop();
                shrinkFactor = 1.0F;
                shrinkingEnabled = false;

                
                    Console.WriteLine("shrink done");
                    Stopwatch stopWatch = new Stopwatch();
                    stopWatch.Start();
                    
                    stopWatch.Stop();
                    TimeSpan ts = stopWatch.Elapsed;

                    // Format and display the TimeSpan value.
                    string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                        ts.Hours, ts.Minutes, ts.Seconds,
                        ts.Milliseconds / 10);
                    Console.WriteLine("Show method takes ::" + elapsedTime);

                   
                
              
                this.Hide();
                
            }
           
        }


        private void populateBrushes(Color brushColor)
        {
            brushArray[0] = new SolidBrush(Color.FromArgb(255, brushColor));
            brushArray[1] = new SolidBrush(Color.FromArgb(170, brushColor));
            brushArray[2] = new SolidBrush(Color.FromArgb(130, brushColor));
            brushArray[3] = new SolidBrush(Color.FromArgb(50, brushColor));
            brushArray[4] = new SolidBrush(Color.FromArgb(30, brushColor));
            brushArray[5] = new SolidBrush(Color.FromArgb(20, brushColor));

        }

        private void disposeOfBrushesAndTimer()
        {
            for (int i = 0; i < brushArray.Length; i++)
            {
                brushArray[i].Dispose();
            }
            if (shrinkTimer != null)
            {
                shrinkTimer.Dispose();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
           
            //this.InvalidateEx();
            this.Invalidate();

           
        }
        
        private void UserControl1_Paint(object sender, PaintEventArgs e)
        {
            //Put Loading icon onto centre of Control
            e.Graphics.TranslateTransform(130, 130);
         
            
           if (shrinkingEnabled)
            {
                e.Graphics.ScaleTransform(shrinkFactor, shrinkFactor);
            }

            //Smooth out drawing edges 
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
         
           

            //Rotate initial starting position each time it is redrawn
            degreesOffset += (360 / numOfElements);
            if (degreesOffset > 360) degreesOffset = degreesOffset % 360;
            e.Graphics.RotateTransform(degreesOffset);

            //DRAW circle of elements, providing alpha variation
            for (int i = 0; i < numOfElements; i++)
            {         
                //decrease alpha value as circle progress around
                switch (i)
                {
                    case 0:         
                        drawShape(e, brushArray[0]);         
                        break;
                    case 1:
                        drawShape(e, brushArray[1]);
                        break;
                    case 2:
                        drawShape(e, brushArray[2]);
                        break;
                    case 3:
                        drawShape(e, brushArray[3]);
                        break;
                    case 4:
                        drawShape(e, brushArray[4]);
                        break;
                    default:
                        drawShape(e, brushArray[5]);
                        break;   
                }
                e.Graphics.RotateTransform(- (360 / numOfElements));
            } 
         
        }

        private void drawShape(PaintEventArgs e, SolidBrush aBrush)
        {
            e.Graphics.TranslateTransform(0, tDist);
            e.Graphics.FillPolygon(aBrush, points1);
            e.Graphics.TranslateTransform(0, -tDist);
        }

        private Point[] trapezoidConstructor(int width, int height)
        {
            //Construct trapezoidal wedge shape
            Point[] p = new Point[4];
            p[0] = new Point(-(width / 4), -(height / 2));
            p[1] = new Point(-(width / 2), (height / 2));
            p[2] = new Point((width / 2), (height / 2));
            p[3] = new Point((width / 4), -(height / 2));

            return p;
        }
    }
}
