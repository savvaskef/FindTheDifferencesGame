using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlServerCe;
using System.Drawing.Printing;
using System.Media;

namespace differences
{
    public partial class findDiffs : Form
    {
        public findDiffs()
        {
            InitializeComponent();
        }

        public findDiffs(string image1, string image2, string Description, int pairID)
        {
            InitializeComponent();
            string pathapp = Application.StartupPath;

            this.pictureBox1.ImageLocation = pathapp + image1;
            this.pictureBox2.ImageLocation = pathapp + image2;
            this.Text = Description;
            SoundPlayer simpleSound = new SoundPlayer(pathapp+@"\\vres.wav");
            simpleSound.Play();

            SqlCeConnection con = new SqlCeConnection();
            con.ConnectionString = @"Data Source=" + pathapp + @"\differnces.sdf";
            con.Open();
                        
            SqlCeCommand com = new SqlCeCommand("Select minx,miny,maxx,maxy,text from differences where pairIDFkey=" + pairID.ToString() + ";", con);
            SqlCeDataReader r = com.ExecuteReader();
            while (r.Read())
            {

                ListViewItem newitem = this.togo.Items.Add(r["text"].ToString());
                newitem.SubItems.Add(r["minx"].ToString());
                newitem.SubItems.Add(r["miny"].ToString());
                newitem.SubItems.Add(r["maxx"].ToString());
                newitem.SubItems.Add(r["maxy"].ToString());
            }

        }

        private void findDiffs_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

 
        Rectangle rec = new Rectangle();
        Pen pen1 = new Pen(System.Drawing.Color.Red, 1.0f);
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            for (int i = 0; i < found.Items.Count; i++)
            {
                ListViewItem diff = found.Items[i];
                rec = new Rectangle();
                rec.X = Convert.ToInt16(diff.SubItems[1].Text);
                rec.Y = Convert.ToInt16(diff.SubItems[2].Text);
                rec.Width = Convert.ToInt16(diff.SubItems[3].Text) - Convert.ToInt16(diff.SubItems[1].Text);
                rec.Height = Convert.ToInt16(diff.SubItems[4].Text) - Convert.ToInt16(diff.SubItems[2].Text);
                e.Graphics.DrawRectangle(pen1, rec);
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Paint(object sender, PaintEventArgs e)
        {

            for (int i = 0; i < found.Items.Count; i++)
            {
                ListViewItem diff = found.Items[i];
                rec = new Rectangle();
                rec.X = Convert.ToInt16(diff.SubItems[1].Text);
                rec.Y = Convert.ToInt16(diff.SubItems[2].Text);
                rec.Width = Convert.ToInt16(diff.SubItems[3].Text) - Convert.ToInt16(diff.SubItems[1].Text);
                rec.Height = Convert.ToInt16(diff.SubItems[4].Text) - Convert.ToInt16(diff.SubItems[2].Text);
                e.Graphics.DrawRectangle(pen1, rec);
            }

        }
        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            int relativeCursorX = e.X;
            int relativeCursorY = e.Y;

            for (int i = 0; i < togo.Items.Count; i++)
            {
                ListViewItem diff = togo.Items[i];
                if ((relativeCursorX > Convert.ToInt16(diff.SubItems[1].Text)) && (relativeCursorX < Convert.ToInt16(diff.SubItems[3].Text)) &&
                    (relativeCursorY > Convert.ToInt16(diff.SubItems[2].Text)) && (relativeCursorY < Convert.ToInt16(diff.SubItems[4].Text)))
                {

                    found.Items.Add(diff.Clone() as ListViewItem);
                    togo.Items.RemoveAt(i);
                    pictureBox1.Refresh(); pictureBox2.Refresh();

                    if (togo.Items.Count==0){
                        congrats dialogCongrats = new congrats();
                        dialogCongrats.ShowDialog();
                        findDiffs.ActiveForm.Close();
                    }
                        break;
                    MessageBox.Show(diff.SubItems[0].Text);
                }
            }

        }
        private void pictureBox2_MouseClick(object sender, MouseEventArgs e)
        {
            int relativeCursorX = e.X;
            int relativeCursorY = e.Y;

            for (int i = 0; i < togo.Items.Count; i++)
            {
                ListViewItem diff = togo.Items[i];
                if ((relativeCursorX > Convert.ToInt16(diff.SubItems[1].Text)) && (relativeCursorX < Convert.ToInt16(diff.SubItems[3].Text)) &&
                    (relativeCursorY > Convert.ToInt16(diff.SubItems[2].Text)) && (relativeCursorY < Convert.ToInt16(diff.SubItems[4].Text)))
                {

                    found.Items.Add(diff.Clone() as ListViewItem);
                    togo.Items.RemoveAt(i);

                    pictureBox1.Refresh(); pictureBox2.Refresh();
                    if (togo.Items.Count == 0)
                    {
                        congrats dialogCongrats = new congrats();
                        dialogCongrats.ShowDialog();
                        findDiffs.ActiveForm.Close();
                    }
                    break;
                    MessageBox.Show(diff.SubItems[0].Text);
                }
            }
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            button1.Visible = false;
            button2.Visible = false;
            button3.Visible = false;
            btnPrint.Visible = false;


            PrintDocument pd = new PrintDocument();
            pd.PrintPage += new PrintPageEventHandler(PrintImage);
            pd.Print();

            button1.Visible = true;
            button2.Visible = true;
            button3.Visible = true;
            btnPrint.Visible = true;
        }

        void PrintImage(object o, PrintPageEventArgs e)
        {
            int x = SystemInformation.WorkingArea.X;
            int y = SystemInformation.WorkingArea.Y;
            int width = this.Width;
            int height = this.Height;

            Rectangle bounds = new Rectangle(x, y, width, height);

            Bitmap img = new Bitmap(width, height);

            this.DrawToBitmap(img, bounds);
            Point p = new Point(100, 100);
            e.Graphics.DrawImage(img, p);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            findDiffs.ActiveForm.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < togo.Items.Count; i++)
            {
                ListViewItem diff = togo.Items[i];
                 

                    found.Items.Add(diff.Clone() as ListViewItem);
                    togo.Items.RemoveAt(i);
                    i--;
                 
                  
                   
                 
            }
            pictureBox1.Refresh(); pictureBox2.Refresh();
        }
            
        private void button2_Click(object sender, EventArgs e)
        {   
             
            Random random = new Random();
            int maxValue = togo.Items.Count;
            int r=0;
             for (int i = 0; i < maxValue; i++) {
                 r = random.Next(maxValue);
            
            }
            MessageBox.Show("Ψάξε " + togo.Items[r].SubItems[0].Text);    
        }

        private void findDiffs_MouseMove(object sender, MouseEventArgs e)
        {

            button1.Visible = true;
            button2.Visible = true;
            button3.Visible = true;
            btnPrint.Visible = true;
        }

 
    }
}