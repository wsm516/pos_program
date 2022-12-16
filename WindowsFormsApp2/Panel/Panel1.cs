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
using System.Windows.Forms.DataVisualization.Charting;
  
namespace WindowsFormsApp2.Panel
{
    public partial class Panel1 : UserControl
    {
        public Panel1()
        {
            InitializeComponent();
        }

        private void Panel1_Load(object sender, EventArgs e)
        {
            DateTime date;
            for (int i = -6; i < 1 ; i++)
            {
                date = DateTime.Now.AddDays(i);
                string str_date = date.ToString("yyyyMMdd");
                string x_name = date.ToString("yyyy/MM/dd");
                int sum = 0;
                sum = database.db.price_sum(str_date); 
                this.chart1.Series["매출"].Points.AddXY(x_name, sum);
                chart1.Series[0].Points[i+6].Label = sum.ToString();
            }
            chart1.Titles[0].Text = "일주일 매출 현황";
        }
    }
}
 
