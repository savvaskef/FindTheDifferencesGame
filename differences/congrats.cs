using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Media;

namespace differences
{
    public partial class congrats : Form
    {
        public congrats()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void congrats_Load(object sender, EventArgs e)
        {
            this.pictureBox1.ImageLocation = Application.StartupPath + "\\congrats.jpg";
            SoundPlayer simpleSound = new SoundPlayer(Application.StartupPath + @"\\congrats.wav");
            simpleSound.Play();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            congrats.ActiveForm.Close();
        }
    }
}
