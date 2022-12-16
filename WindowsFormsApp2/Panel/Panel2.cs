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
  
namespace WindowsFormsApp2.Panel
{
    public partial class Panel2 : UserControl
    {
        public void create_chart(string str_date)
        {
            MySqlConnection connection = new MySqlConnection(database.db.get_adress());
            connection.Open();
            string query = ("select name, sum(total_price), sell_date from sell_data where sell_date = " + str_date + " group by number,name");
            MySqlCommand cmd = new MySqlCommand(query, connection);
            MySqlDataReader reader = cmd.ExecuteReader();
            int i = 0;
            while (reader.Read())
            {
                string name = reader["name"].ToString();
                int total_price = int.Parse(reader["sum(total_price)"].ToString());
                this.chart1.Series["매출"].Points.AddXY(name, total_price);
                chart1.Series["매출"].Points[i].Label = total_price.ToString();
                i++;
            }
            chart1.Titles[0].Text = "물품별 판매 현황";
            connection.Close();
        }
        public Panel2()
        {
            InitializeComponent();
        }

        private void Panel2_Load(object sender, EventArgs e)
        {
            DateTime date = DateTime.Now;
            string str_date = date.ToString("yyyyMMdd");
            create_chart(str_date);
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            chart1.Series.Clear();
            chart1.Series.Add("매출");
            string str_date = dateTimePicker1.Value.ToString("yyyyMMdd");
            create_chart(str_date);
        }
    }
}
 
