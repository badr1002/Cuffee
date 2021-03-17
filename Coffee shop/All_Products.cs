using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;

namespace Coffee_shop
{
    public partial class All_Products : Form
    {
        public All_Products()
        {
            InitializeComponent();
        }
        private static string GetConnection = @"Server=localhost;port=5432;User Id=postgres;Password=123;Database=postgres";
        NpgsqlConnection con = new NpgsqlConnection(GetConnection);


        private void getProducts()
        {
            try
            {
                con.Open();
                NpgsqlCommand cmd = new NpgsqlCommand($"select * from Products", con);
                NpgsqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {                   
                    int id = rdr.GetInt32(0);
                    string name = rdr.GetString(1);
                    string type = rdr.GetString(2);
                    double price = rdr.GetDouble(3);
                    string code = rdr.GetInt32(4).ToString();
                    DateTime time = rdr.GetDateTime(5).ToLocalTime();
                    dataGridView1.Rows.Add(id.ToString(), name, type, price, code, time);
                }
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show("ERROR: " + ex.Message);
            }
        }

        private void All_Products_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'employees.products' table. You can move, or remove it, as needed.
            //this.productsTableAdapter.Fill(this.employees.products);
            getProducts();
        }

        //void search Products 
        private void searchProduct(string _name)
        {
            try
            {
                con.Open();
                NpgsqlCommand cmd = new NpgsqlCommand($"select * from Products where name like('%{_name}%')", con);
                NpgsqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    
                    int id = rdr.GetInt32(0);
                    string name = rdr.GetString(1);
                    string type = rdr.GetString(2);
                    double price = rdr.GetDouble(3);
                    string code = rdr.GetInt32(4).ToString();                   
                    DateTime time = rdr.GetDateTime(5).ToLocalTime();
                    dataGridView1.Rows.Add(id.ToString(), name, type, price, code, time);
                }
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show("ERROR: " + ex.Message);
            }
        }


        private void search_Click(object sender, EventArgs e)
        {
            if (searchBox.Text != "")
            {
                dataGridView1.Rows.Clear();
                searchProduct(searchBox.Text);
            }
        }

        private void searchBox_TextChanged(object sender, EventArgs e)
        {
            if (searchBox.Text == "")
            {
                dataGridView1.Rows.Clear();
                getProducts();
            }
        }
    }
}
