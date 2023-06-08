using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlServerCe;

namespace differences
{
    public partial class Form1 : Form
    {
        Rectangle rec = new Rectangle();
        Rectangle rec2 = new Rectangle();
        List<Rectangle> recList = new List<Rectangle>();
        List<Rectangle> recList2 = new List<Rectangle>();
        Pen pen1 = new Pen(System.Drawing.Color.Red, 1.0f);
        Graphics sur;    
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.ResizeRedraw, true); 
            pictureBox1.SendToBack();
          
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            Stream myStream = null;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Multiselect = false;
         
            openFileDialog1.Filter = "png files (*.png)|*.png|jpg files (*.jpg)|*.jpg";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((myStream = openFileDialog1.OpenFile()) != null)
                    {
                        using (myStream)
                        {
                            // Insert code to read the stream here.
                            string file = openFileDialog1.FileNames[0];
                            this.pictureBox1.ImageLocation=file;
                            this.textBox2.Text = file;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }
        protected override void OnPaint(PaintEventArgs e) {
        }


   
  
        private void button2_Click(object sender, EventArgs e)
        {
            {
                Stream myStream = null;
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                openFileDialog1.Multiselect = false;

                openFileDialog1.Filter = "png files (*.png)|*.png|jpg files (*.jpg)|*.jpg";
                openFileDialog1.FilterIndex =1;
                openFileDialog1.RestoreDirectory = true;

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        if ((myStream = openFileDialog1.OpenFile()) != null)
                        {
                            using (myStream)
                            {
                                // Insert code to read the stream here.
                                string file = openFileDialog1.FileNames[0];
                                this.pictureBox2.ImageLocation = file;
                                this.textBox3.Text=file;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                    }
                }
            }
        }

        private void pictureBox2_MouseDown(object sender, MouseEventArgs e)
        {
            this.SX.Text = (e.X - pictureBox2.Left).ToString();
            this.SY.Text = (e.Y - pictureBox2.Top).ToString();
            this.FX.Text = "0";
            this.FY.Text = "0";

        
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {   
           

            this.SX.Text = (e.X - pictureBox1.Left).ToString();
            this.SY.Text= (e.Y - pictureBox1.Top).ToString();
            this.FX.Text = "0" ;
            this.FY.Text = "0";
        
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            this.FX.Text = (e.X - pictureBox1.Left).ToString();
            this.FY.Text = (e.Y - pictureBox1.Top).ToString();
        
        }

        private void pictureBox2_MouseUp(object sender, MouseEventArgs e)
        {
            this.FX.Text = (e.X - pictureBox2.Left).ToString();
            this.FY.Text = (e.Y - pictureBox2.Top).ToString();
        
        }

        private void pictureBox2_DragOver(object sender, DragEventArgs e)
        {
          
        }

        private void pictureBox1_DragOver(object sender, DragEventArgs e)
        {
            
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
         
        
        }

        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {
             
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

       private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if ((textBox3.Text != "") && (textBox2.Text != ""))
            {
            //e.Graphics.DrawImage(Image.FromFile(textBox2.Text),0,0,this.pictureBox1.Width,pictureBox1.Height);
            e.Graphics.DrawRectangle(pen1,rec);
            if (radioButton2.Checked)
            {

                foreach (Rectangle rectemp in recList)
                {
                    e.Graphics.DrawRectangle(pen1, rectemp);
                }


            }
            };
        }

        private void pictureBox2_Paint(object sender, PaintEventArgs e)
        {
            if ((textBox3.Text != "")&&(textBox2.Text!=""))
            {
              //  e.Graphics.DrawImage(Image.FromFile(textBox3.Text), 0, 0, this.pictureBox2.Width, pictureBox2.Height);
                e.Graphics.DrawRectangle(pen1, rec2);
            if (radioButton2.Checked){
        
            foreach (Rectangle rectemp in recList2)
            {
                e.Graphics.DrawRectangle(pen1, rectemp);
            }
           
            
            }
            };
        }
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (((SX.Text) != "0") && ((SY.Text) != "0") && (FX.Text == "0") && (FY.Text == "0"))
            {


                rec.X = Math.Min(pictureBox1.Left + Convert.ToInt16(SX.Text), e.X);
                rec.Y = Math.Min(pictureBox1.Top + Convert.ToInt16(SY.Text), e.Y);
                rec.Width = Math.Abs(pictureBox1.Left + Convert.ToInt16(SX.Text) - e.X);
                rec.Height = Math.Abs(pictureBox1.Top + Convert.ToInt16(SY.Text) - e.Y);
                //sur.Clear(System.Drawing.Color.Black);
                rec2.X = rec.X;// -pictureBox1.Left + pictureBox2.Left;
                rec2.Y = rec.Y;// -pictureBox1.Top + pictureBox2.Top;
                rec2.Width =rec.Width;
                rec2.Height = rec.Height;

                pictureBox2.Refresh(); pictureBox1.Refresh();

            }
        }

      
        private void pictureBox2_MouseMove(object sender, MouseEventArgs e)
        {
            if (((SX.Text) != "0") && ((SY.Text) != "0") && (FX.Text == "0") && (FY.Text == "0"))
            {


                rec2.X = Math.Min(pictureBox2.Left + Convert.ToInt16(SX.Text), e.X);
                rec2.Y = Math.Min(pictureBox2.Top + Convert.ToInt16(SY.Text), e.Y);
                rec2.Width = Math.Abs(pictureBox2.Left + Convert.ToInt16(SX.Text) - e.X);
                rec2.Height = Math.Abs(pictureBox2.Top + Convert.ToInt16(SY.Text) - e.Y);

                rec.X = rec2.X; //- pictureBox2.Left + pictureBox1.Left;
                rec.Y = rec2.Y; //- pictureBox1.Top + pictureBox2.Top;
                rec.Width = rec2.Width;
                rec.Height = rec2.Height;
                

                pictureBox2.Refresh(); pictureBox1.Refresh();
               
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            recList.Add(rec);
            recList2.Add(rec2);
            pictureBox1.Refresh();
            pictureBox2.Refresh();
            ListViewItem item = new ListViewItem();
            item.Text = textBox4.Text;
            item.SubItems.Add(rec.X.ToString());
            item.SubItems.Add(rec.Y.ToString());
            item.SubItems.Add((rec.X+rec.Width).ToString());
            item.SubItems.Add((rec.Y+rec.Height).ToString());
            
            listView1.Items.Add(item);
            this.text.Text = "";
            

        }

        private void button4_Click(object sender, EventArgs e)
        {
            string pathapp = Application.StartupPath;
            SqlCeConnection con = new SqlCeConnection();
            con.ConnectionString = @"Data Source="+pathapp+@"\differnces.sdf";
            SqlCeEngine engine = new SqlCeEngine(con.ConnectionString);
         
            // had to run the following command once
            // engine.Upgrade(con.ConnectionString);

            con.Open();

            SqlCeCommand com = new SqlCeCommand("Select max(pairID) as pAI from imagespair", con);
            SqlCeDataReader r = com.ExecuteReader();
            int pairAI=0, diffAI=0;
            while (r.Read())
            {
                if (r["pAI"].ToString()=="") pairAI = 1; else pairAI = 1 + Convert.ToInt16(r["pAI"]);
            }


            com = new SqlCeCommand("Select max(diffID) as dAI from differences", con);
             r = com.ExecuteReader();
            while (r.Read())
            {
                if (r["dAI"].ToString() == "") diffAI = 1; else diffAI = 1 + Convert.ToInt16(r["dAI"]);
    
            }
           // MessageBox.Show(pairAI + "/" + diffAI);
            string file1 = (textBox2.Text).Substring((textBox2.Text).LastIndexOf(@"\") + 1);
            string file2 = (textBox3.Text).Substring((textBox3.Text).LastIndexOf(@"\") + 1);
            string pathpix = pathapp + "\\diffpix\\" + pairAI.ToString()+"\\";
            string relativepathpix = "\\diffpix\\" + pairAI.ToString() + "\\";
            if (!System.IO.Directory.Exists(pathpix))
            {
                System.IO.Directory.CreateDirectory(pathpix);
            }
            System.IO.File.Copy(textBox2.Text, pathpix+file1, true);
            System.IO.File.Copy(textBox3.Text, pathpix+file2, true);
            com = new SqlCeCommand("insert into imagespair(pairID,image1,image2,description,horizontal) values(" + pairAI + ",'" + relativepathpix + file1 + "','" + relativepathpix + file2 + "','" + textBox1.Text + "',1)", con);
            com.ExecuteNonQuery();

            for(int i=0;i<listView1.Items.Count;i++){
                com = new SqlCeCommand("insert into differences(diffID,minx,miny,maxx,maxy,text,pairIDFkey) values(" + diffAI + "," + listView1.Items[i].SubItems[1].Text + "," + listView1.Items[i].SubItems[2].Text + "," + listView1.Items[i].SubItems[3].Text + "," + listView1.Items[i].SubItems[4].Text + ",'" + listView1.Items[i].SubItems[0].Text + "'," + pairAI + ")", con);
                com.ExecuteNonQuery();
                diffAI++;
                
            }

            MessageBox.Show("Οι διαφορές καταχωρήθηκαν");
            con.Close();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

            this.pictureBox1.Refresh();
            this.pictureBox2.Refresh();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            this.pictureBox1.Refresh();
            this.pictureBox2.Refresh();

        }




    }
}
