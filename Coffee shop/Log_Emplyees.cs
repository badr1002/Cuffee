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
    public partial class Log_Emplyees : Form
    {
        public Log_Emplyees()
        {
            InitializeComponent();
        }
        private static string GetConnection = @"Server=localhost;port=5432;User Id=postgres;Password=123;Database=postgres";
        NpgsqlConnection con = new NpgsqlConnection(GetConnection);


        //void login
        private void Login()
        {
            try
            {
                con.Open();
                NpgsqlCommand command = new NpgsqlCommand($"SELECT * FROM Employees where username='{username.Text}'and password='{password.Text}'", con);
                NpgsqlDataReader dr = command.ExecuteReader();
                dr.Read();                
                if (username.Text != "" & password.Text != "")
                {
                    if (dr.GetString(4) == username.Text & dr.GetString(5) == password.Text)
                    {
                        this.Hide();
                        Employee_Form f = new Employee_Form();
                        f.welcome.Text = dr.GetString(1) + " " + dr.GetString(2);
                        f.ShowDialog();
                        this.Close();
                    }
                }
                else
                {

                    MessageBox.Show("Please, Complete this fields!");
                }
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show("ERROR: " + ex.Message);
            }
        }


        private void cancel_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login log = new Login();
            log.ShowDialog();
            this.Close();
        }

        private void login_btn_Click(object sender, EventArgs e)
        {
            Login();
        }

        private void check_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (check.Checked == true)
                {
                    Properties.Settings.Default.empUser = username.Text;
                    Properties.Settings.Default.empPass = password.Text;
                    Properties.Settings.Default.Save();
                }
                else if (check.Checked == false)
                {
                    Properties.Settings.Default.empUser = "";
                    Properties.Settings.Default.empPass = "";
                    Properties.Settings.Default.Save();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("ERROR: " + ex.Message);
            }
        }

        private void Log_Emplyees_Load(object sender, EventArgs e)
        {
            try
            {
                if (Properties.Settings.Default.empUser != "" & Properties.Settings.Default.empPass != "")
                {
                    username.Text = Properties.Settings.Default.empUser;
                    password.Text = Properties.Settings.Default.empPass;
                    Login();
                }
                else if (Properties.Settings.Default.empUser == "" & Properties.Settings.Default.empPass == "")
                {
                    this.Show();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("ERROR: " + ex.Message);
            }
        }
    }
}
