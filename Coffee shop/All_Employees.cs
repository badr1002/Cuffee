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
using System.Data.SqlClient;

namespace Coffee_shop
{
    public partial class All_Employees : Form
    {
        public All_Employees()
        {
            InitializeComponent();
        }
        private static string GetConnection = @"Server=localhost;port=5432;User Id=postgres;Password=123;Database=postgres";
        NpgsqlConnection con = new NpgsqlConnection(GetConnection);

        //void get Employees
        private void getEmployees()
        {
            try
            {
                con.Open();
                NpgsqlCommand cmd = new NpgsqlCommand($"select * from Employees", con);
                NpgsqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    int id = rdr.GetInt32(0);
                    string firstname = rdr.GetString(1).ToString();
                    string lastname = rdr.GetString(2).ToString();
                    double salary = rdr.GetDouble(3);
                    string username = rdr.GetString(4).ToString();
                    string password = rdr.GetString(5).ToString();
                    int code = rdr.GetInt32(6);
                    DateTime time = rdr.GetDateTime(7).ToLocalTime();                    
                    dataGridView1.Rows.Add(id.ToString(), firstname, lastname, salary, username, password, code, time);
                }
                //SqlConnection scon = new SqlConnection(GetConnection);
                //SqlDataAdapter sqlData = new SqlDataAdapter($"select * from Employees", scon);
                //DataTable db = new DataTable();

                //sqlData.Fill(db);
                //dataGridView1.DataSource = sqlData;
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show("ERROR: " + ex.Message);
            }
        }


        //void search Employee 
        private void searchEmployee(string _firstname,string _lastname)
        {
            try
            {
                con.Open();
                NpgsqlCommand cmd = new NpgsqlCommand($"select * from Employees where firstname like('%{_firstname}%') or lastname like('%{_lastname}%')", con);
                NpgsqlDataReader rdr = cmd.ExecuteReader();
                
                while (rdr.Read())
                {
                  
                    int id = rdr.GetInt32(0);
                    string firstname = rdr.GetString(1).ToString();
                    string lastname = rdr.GetString(2).ToString();
                    double salary = rdr.GetDouble(3);
                    string username = rdr.GetString(4).ToString();
                    string password = rdr.GetString(5).ToString();
                    int code = rdr.GetInt32(6);
                    DateTime time = rdr.GetDateTime(7).ToLocalTime();                    
                    dataGridView1.Rows.Add(id.ToString(), firstname, lastname, salary, username, password, code, time);
                }              
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show("ERROR: " + ex.Message);
            }
        }

        private void All_Employees_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'employees.employees' table. You can move, or remove it, as needed.
            //this.employeesTableAdapter.Fill(this.employees.employees);
            getEmployees();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void search_Click(object sender, EventArgs e)
        {
           
            if (searchBox.Text != "")
            {
                dataGridView1.Rows.Clear();
                searchEmployee(searchBox.Text, searchBox.Text);
            }
        }
        private void searchBox_TextChanged(object sender, EventArgs e)
        {
            if (searchBox.Text == "")
            {
                dataGridView1.Rows.Clear();
                getEmployees();
            }
        }
    }
}
