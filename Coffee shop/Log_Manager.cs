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
    public partial class Log_Manager : Form
    {
        public Log_Manager()
        {
            InitializeComponent();
        }
        // set connection
        private static string GetConnection = @"Server=localhost;port=5432;User Id=postgres;Password=123;Database=postgres";
        private NpgsqlConnection con = new NpgsqlConnection(GetConnection);

        private void login_btn_Click(object sender, EventArgs e)
        {
            try
            {
                if (Properties.Settings.Default.manUser == username.Text & Properties.Settings.Default.manPass == password.Text)
                {
                    this.Hide();
                    Manager_Form man = new Manager_Form();
                    man.ShowDialog();
                    this.Close();
                }
                else 
                {
                    this.Show();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("ERROR: " + ex.Message);
            }
        }

        

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login log = new Login();
            log.ShowDialog();
            this.Close();
        }

        private void Log_Manager_Load(object sender, EventArgs e)
        {
            try
            {
                if (Properties.Settings.Default.manUser == "admin" & Properties.Settings.Default.manPass == "admin")
                {
                    this.Hide();
                    Manager_Form man = new Manager_Form();
                    man.ShowDialog();
                    this.Close();
                }
                else
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
