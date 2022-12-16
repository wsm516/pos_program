using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using MySql.Data.MySqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApp2
{
    public partial class Add : Form
    {
        public void view_update()
        {
            listView1.Items.Clear();

            string str_num;
            string str_count;
            string str_price;
            string name;
            ListViewItem view_item;


            using (MySqlConnection connection = new MySqlConnection(database.db.get_adress()))
            {
                connection.Open();
                string insertQuery = ("select number, name, count, price from pos");
                MySqlCommand cmd = new MySqlCommand(insertQuery, connection);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    str_num = reader["number"].ToString();
                    name = reader["name"].ToString();
                    str_count = reader["count"].ToString();
                    str_price = reader["price"].ToString();

                    view_item = new ListViewItem(new string[] { str_num, name, str_count, str_price });
                    listView1.Items.Add(view_item);
                }
                reader.Close();
                connection.Close();
            }
        }
        
        public Add()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            Main f1 = new Main();
            f1.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "")
            {
                MessageBox.Show("상품 정보를 모두 입력해 주세요.", "등록 실패");
            }
            else
            {
                int number = int.Parse(textBox1.Text);
                string name = textBox2.Text;
                int count = int.Parse(textBox3.Text);
                int price = int.Parse(textBox4.Text);

                if (database.db.num_check(number))
                {
                    database.db.num_update(number, name, count, price);
                }
                else
                {
                    database.db.insert(number, name, count, price);

                }
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                view_update();
            }
        }     
        private void button2_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count != 0)
            {
                string number = listView1.Items[listView1.FocusedItem.Index].SubItems[0].Text.ToString();
                database.db.num_delete(int.Parse(number));
                view_update();
            }
            else
            {
                MessageBox.Show("삭제하실 상품을 선택해 주세요.");
            }
        }

        private void Add_Load(object sender, EventArgs e)
        {
            listView1.FullRowSelect = true;
            view_update();

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count != 0)
            {
                string name = listView1.Items[listView1.FocusedItem.Index].SubItems[1].Text.ToString();
                string price = listView1.Items[listView1.FocusedItem.Index].SubItems[3].Text.ToString();
                string number = listView1.Items[listView1.FocusedItem.Index].SubItems[0].Text.ToString();

                textBox1.Text = number;
                textBox2.Text = name;
                textBox4.Text = price;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            keypress(sender, e);
        }
        public void keypress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == Convert.ToChar(Keys.Back)))
            {
                e.Handled = true;
            }
        }
        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            keypress(sender, e);
        }

    }
}
