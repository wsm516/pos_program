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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
 
namespace WindowsFormsApp2
{
    public partial class Sale : Form
    {
        int sell_count = 0;
        int sell_price = 0;
        public void view_update()
        {
            listView1.Items.Clear();
            string num;
            string str_count;
            string str_price;
            string name;
            ListViewItem view_item;


            using (MySqlConnection connection = new MySqlConnection(database.db.get_adress()))
            {
                connection.Open();
                string insertQuery = ("select * from pos");
                MySqlCommand cmd = new MySqlCommand(insertQuery, connection);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    name = reader["name"].ToString();
                    str_count = reader["count"].ToString();
                    str_price = reader["price"].ToString();
                    num = reader["number"].ToString();

                    view_item = new ListViewItem(new string[] { name, str_count, str_price, num });
                    listView1.Items.Add(view_item);
                }
                reader.Close();
                connection.Close();
            }
        }

        public Sale()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            Main f1 = new Main();
            f1.ShowDialog();
        }

        private void Sale_Load(object sender, EventArgs e)
        {
            listView1.FullRowSelect = true;
            view_update();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int buy_price;
            if(listView1.SelectedItems.Count != 0)
            {
                string index = listView1.SelectedItems[0].SubItems[1].Text;
                if (index == "0")
                {
                    MessageBox.Show("물품이 없습니다.", "경고");
                }
                else if (textBox5.Text == "")
                {
                    MessageBox.Show("물품 수량을 입력해 주세요.", "경고");
                }
                else if(int.Parse(index) < int.Parse(textBox5.Text))
                {
                    MessageBox.Show("물품이 부족합니다.", "경고");
                }
                else
                {
                    string buy_count = textBox5.Text;
                    string name = listView1.Items[listView1.FocusedItem.Index].SubItems[0].Text.ToString();
                    int count = int.Parse(listView1.Items[listView1.FocusedItem.Index].SubItems[1].Text.ToString());
                    string price = listView1.Items[listView1.FocusedItem.Index].SubItems[2].Text.ToString();
                    string number = listView1.Items[listView1.FocusedItem.Index].SubItems[3].Text.ToString();

                    buy_price = int.Parse(price) * int.Parse(buy_count);
                    count -= int.Parse(buy_count);
                    ListViewItem view_item;

                    view_item = new ListViewItem(new string[] { name, buy_count, buy_price.ToString(), number });
                    listView2.Items.Add(view_item);

                    sell_count += int.Parse(buy_count);
                    sell_price += buy_price;

                    label8.Text = sell_count.ToString();
                    label9.Text = sell_price.ToString();

                }
                
            }
        }
        public void keypress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == Convert.ToChar(Keys.Back)))
            {
                e.Handled = true;
            }
        }
        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if(textBox4.Text != "")
            {
                if (int.Parse(textBox4.Text) < sell_price ||
                    int.Parse(textBox4.Text) < 10)
                {
                    textBox2.Text = "";
                }
                else
                {
                    int money = int.Parse(textBox4.Text);
                    int change_money = money - sell_price;
                    textBox2.Text = change_money.ToString();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string name = "";
            int num;
            int count;
            int price;
            int total_price;
            DateTime sell_date = DateTime.Today;
            string str_date = sell_date.ToString("yyyyMMdd");
            if (label8.Text == "" || label9.Text == "")
            {
                MessageBox.Show("상품과 수량을 선택해 주세요.", "결제 실패");
            }
            else if(textBox2.Text == "" || textBox4.Text == "")
            {
                MessageBox.Show("받은 금액을 다시 입력해 주세요.", "결제 실패");
            }
            else
            {
                for(int i = 0; i<listView2.Items.Count; i++)
                {
                    ListViewItem item = listView2.Items[i];
                    name = (item.SubItems[0].Text);
                    num = int.Parse(item.SubItems[3].Text);
                    total_price = int.Parse(item.SubItems[2].Text);
                    count = int.Parse(item.SubItems[1].Text);
                    price = total_price / count;
                    database.db.sell_update(num, count);
                    database.db.sell_insert(num, name, count, price, total_price, str_date);
                }
                MessageBox.Show("결제가 완료되었습니다.");
                sell_count = 0;
                sell_price = 0;

                view_update();
                listView2.Items.Clear();
                label8.Text = "";
                label9.Text = "";
                textBox4.Text = "";
                textBox2.Text = "";
                textBox5.Text = "";
            }
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            keypress(sender, e);
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }
    }
}
 
