using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlServerCe;

namespace differences
{
    public partial class choosepix : Form
    {
        public choosepix()
        {
            InitializeComponent();
        }

        private void choosepix_Load(object sender, EventArgs e)
        {


            string pathapp = Application.StartupPath;


            SqlCeConnection con = new SqlCeConnection();
            con.ConnectionString = @"Data Source=" + pathapp + @"\differnces.sdf";
            SqlCeEngine engine = new SqlCeEngine(con.ConnectionString);
            //engine.Upgrade(con.ConnectionString);
            SqlCeConnection con2 = new SqlCeConnection();
         
            con2.ConnectionString = @"Data Source=" + pathapp + @"\differnces.sdf";

            con2.Open();

            SqlCeCommand com = new SqlCeCommand("Select pairID,image1,image2,description,horizontal from imagespair", con2);
            SqlCeDataReader r = com.ExecuteReader();
                while (r.Read())
                {
                     
                    ListViewItem newitem = this.listView1.Items.Add(r["pairID"].ToString());
                    newitem.SubItems.Add(r["image1"].ToString());
                    newitem.SubItems.Add(r["image2"].ToString());
                    newitem.SubItems.Add(r["description"].ToString());
                    newitem.SubItems.Add(r["horizontal"].ToString());
                    this.comboBox1.Items.Add(r["description"]);
                }
                comboBox1.SelectedIndex = 0;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            findDiffs formfinddiffs = new findDiffs(listView1.Items[comboBox1.SelectedIndex].SubItems[1].Text,
                                                    listView1.Items[comboBox1.SelectedIndex].SubItems[2].Text,
                                                    listView1.Items[comboBox1.SelectedIndex].SubItems[3].Text,
                                    Convert.ToInt16(listView1.Items[comboBox1.SelectedIndex].SubItems[0].Text));
            formfinddiffs.ShowDialog();


        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
