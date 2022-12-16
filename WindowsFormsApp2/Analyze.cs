using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
  
namespace WindowsFormsApp2
{
    public partial class Analyze : Form
    {
        Panel.Panel1 P1 = new Panel.Panel1();
        Panel.Panel2 P2 = new Panel.Panel2();
        public Analyze()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            panel1.Controls.Add(P1);     
        }


        private void button3_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            Main f1 = new Main();
            f1.ShowDialog();
        }
 
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            panel1.Controls.Add(P2);
        }
    }
}
