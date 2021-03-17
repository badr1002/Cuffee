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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();           
            Log_Manager m = new Log_Manager();
            m.ShowDialog();
            this.Close();
        }

        private void login_btn_Click(object sender, EventArgs e)
        {
            this.Hide();
            Log_Emplyees emp = new Log_Emplyees();
            emp.ShowDialog();
            this.Close();
        }
    }
}
