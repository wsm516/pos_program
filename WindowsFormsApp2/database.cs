using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using Google.Protobuf.WellKnownTypes;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Crypto.Engines;

namespace WindowsFormsApp2
{
    class database
    {
        public static database db = new database();

        private string Adress = 
            ("Server=localhost;Port=3306;Database=pos_program;Uid=root;Pwd=50797890");
        public string get_adress()
        {
            return Adress;
        }
        public bool num_check(int number)
        {
            bool flag = false;
            string Query = ("select * from pos where number = " + number);
            MySqlConnection connection = new MySqlConnection(Adress);

            MySqlCommand command = new MySqlCommand(Query, connection);
            MySqlDataReader Reader;
            try
            {
                connection.Open();

                Reader = command.ExecuteReader();

                while (Reader.Read())
                {
                    int db_number = int.Parse(Reader["number"].ToString());
                    if(number == db_number)
                    {
                        flag = true;
                    }
                    else
                    {
                        flag = false;
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            connection.Close();
            return flag;

        }
        public void insert(int number, string name, int count, int price)
        {
            using (MySqlConnection connection = new MySqlConnection(Adress))
            {
                string insertQuery =
                    ("INSERT INTO pos(number,name,count, price) VALUES(" + number + ",\"" + name + "\"," + count + "," + price + ")");
                connection.Open();
                MySqlCommand command = new MySqlCommand(insertQuery, connection);
                try
                {
                    if (command.ExecuteNonQuery() == 1)
                    {
                        MessageBox.Show("상품 등록이 완료되었습니다.");
                    }
                    else
                    {
                        MessageBox.Show("상품 등록에 실패하였습니다.");
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
                connection.Close();
            }
        }
        public void sell_update(int num, int count)
        {
            string Query = "update pos set count = count - " + count + " where number = " + num + ";";
            MySqlConnection connection = new MySqlConnection(Adress);

            MySqlCommand command = new MySqlCommand(Query, connection);
            MySqlDataReader reader;
            try
            {
                connection.Open();

                reader = command.ExecuteReader();

                while (reader.Read())
                {

                }
            }catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
            connection.Close();
        }
        public void num_update(int num, string name, int count, int price)
        {
            string Query = 
                ("update pos set count = count + " + count + 
                ", name = \""+name+"\", price = "+price+" where number = " + num + ";");
            MySqlConnection connection = new MySqlConnection(Adress);

            MySqlCommand command = new MySqlCommand(Query, connection);
            try
            {
                connection.Open();

                if (command.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("상품 등록이 완료되었습니다.");
                }
                else
                {
                    MessageBox.Show("상품 등록에 실패하였습니다.");
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            connection.Close();
        }
        public void num_delete(int num)
        {
            string Query =
               ("delete from pos where number =" + num);
            MySqlConnection connection = new MySqlConnection(Adress);

            MySqlCommand command = new MySqlCommand(Query, connection);
            try
            {
                connection.Open();

                if (command.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("상품 삭제가 완료되었습니다.");
                }
                else
                {
                    MessageBox.Show("상품 삭제 실패하였습니다.");
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            connection.Close();
        }
        public void sell_insert(int num, string name, int count, int price, int total_price, string date)
        {
            string Query = ("insert into sell_data (number, name, count, price, total_price, sell_date)" +
                " values ("+num+", \""+name+"\", "+count+", "+price+", "+total_price+", "+date+")");
            MySqlConnection connection = new MySqlConnection(Adress);

            MySqlCommand command = new MySqlCommand(Query, connection);
            MySqlDataReader reader;
            try
            {
                connection.Open();

                reader = command.ExecuteReader();

                while (reader.Read())
                {

                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            connection.Close();
        }
        public int price_sum(string date)
        {
            MySqlConnection connection = new MySqlConnection(Adress);
            int sum = 0;
            connection.Open();
            string query = ("select * from sell_data where sell_date = " + date);
            MySqlCommand cmd = new MySqlCommand(query, connection);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int total_price = int.Parse(reader["total_price"].ToString());
                sum += total_price;
            }
            connection.Close();
            return sum;
        }
    }
}