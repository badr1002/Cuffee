using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Coffee_shop
{
    public partial class Manager_Form : Form
    {
        public Manager_Form()
        {
            InitializeComponent();
        }

        private void add_emp_product_Click(object sender, EventArgs e)
        {
            SignUp sign = new SignUp();
            sign.Show();
        }

        private void edit_btn_Click(object sender, EventArgs e)
        {
            Edit sign = new Edit();
            sign.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            All_Employees emps = new All_Employees();
            emps.Show();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            All_Products pros = new All_Products();
            pros.Show();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login log = new Login();
            log.ShowDialog();
            this.Close();
        }
    }
}
