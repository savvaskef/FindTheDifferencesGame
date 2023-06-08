using System;
using System.Drawing;
using System.Windows.Forms;
namespace GeneticPaint{ 
    public partial class Form2 : Form    
    {        Graphics front, back;
             Bitmap buffer;
             Timer t;   
            
            public Form2()
            {
                InitializeComponent();
                buffer = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);
                back = Graphics.FromImage(buffer);
                front = this.CreateGraphics();
                t = new Timer();
                t.Tick += new EventHandler(t_Tick);
                t.Interval = 1000;
                t.Start();
            }        
        void t_Tick(object sender, EventArgs e)        
        {
            t.Stop();//Do all your drawing to the "back" graphics object//Draw in order from back to front, this is called "Painter's Algorithm"
            back.Clear(Color.CornflowerBlue);
            back.DrawRectangle(Pens.Red, 10, 10, 20, 30); 
            back.Flush();//"flip" the back to the front, this is called double buffering
            front.DrawImageUnscaled(buffer, ClientRectangle.X, ClientRectangle.Y); 
            front.Flush();
            t.Start();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(Image.FromFile(@"C:\Users\savvas.MIK3\Documents\Visual Studio 2010\Projects\differences\differences\PIC1.png"), new Point(0, 0));
            e.Graphics.DrawRectangle(Pens.Red, 10, 10, 20, 30);

        }
    }
}