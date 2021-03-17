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
    public partial class Employee_Form : Form
    {
        public Employee_Form()
        {
            InitializeComponent();
          
        }
        private static string GetConnection = @"Server=localhost;port=5432;User Id=postgres;Password=123;Database=postgres";
        NpgsqlConnection con = new NpgsqlConnection(GetConnection);


        public void welcome_Click(object sender, EventArgs e)
        {

        }

        private double priceDrinks;
        private  double priceFood;
       
        private void drinks(string name,string type,double price,int code, int y)
        {
            CheckBox drink = new CheckBox();
            drink.Text = name;
            drink.Location = new Point(100, 70 + y);
            drink.Cursor = Cursors.Hand;
            this.Controls.Add(drink);

            TextBox count = new TextBox();
            count.Name = name;
            count.Location = new Point(250, 70 + y);
            count.Cursor = Cursors.Hand;
            count.Size = new Size(40, 30);
            this.Controls.Add(count);

           

            drink.Click += new EventHandler(drink_CheckedChanged);
            void drink_CheckedChanged(object sender, EventArgs e)
            {              
                if (drink.Checked)
                {                  
                    if (count.Text != "")
                    {                      
                        priceDrinks += price * Convert.ToInt32(count.Text);
                        Reset.Items.Add(name + "  " + price + " $  " + count.Text);
                    }
                    else if(count.Text=="")
                    {                        
                        MessageBox.Show("Enter the count!");                       
                    }
                }
                if(!drink.Checked)
                {                
                        Reset.Items.Remove(name + "  " + price + " $  " + count.Text);
                        priceDrinks -= price * Convert.ToInt32(count.Text);
                        count.Text = "";                   
                }

            }
        }
        
        private void getDrinks()
        {
            con.Open();
            NpgsqlCommand command = new NpgsqlCommand($"SELECT * FROM Products where type='drink'", con);
            NpgsqlDataReader dr = command.ExecuteReader();
            int u = 0;
            while (dr.Read())
            {
              
                string name = dr.GetString(1);
                string type = dr.GetString(2);
                double price = dr.GetDouble(3);
                int code = dr.GetInt32(4);
                drinks(name,type,price,code,u += 30);
            }

            con.Close();
        }

      
        private void foods(string name, string type, double price, int code, int y)
        {
            CheckBox food = new CheckBox();
            food.Text = name;
            food.Location = new Point(600, 70 + y);
            food.Cursor = Cursors.Hand;
            this.Controls.Add(food);

            TextBox count = new TextBox();
            count.Name = name;
            count.Location = new Point(750, 70 + y);
            count.Cursor = Cursors.Hand;
            count.Size = new Size(40, 30);
            this.Controls.Add(count);


            food.Click += new EventHandler(food_CheckedChanged);
            void food_CheckedChanged(object sender, EventArgs e)
            {
                if (food.Checked)
                {
                    if (count.Text != "")
                    {
                        priceFood += price * Convert.ToInt32(count.Text);
                        Reset.Items.Add(name + "  " + price + " $  " + count.Text);
                    }
                    else
                    {
                       
                        MessageBox.Show("Enter the count!");
                    }
                }
                if (!food.Checked)
                {
                    Reset.Items.Remove(name + "  " + price + " $  " + count.Text);
                    priceFood -= price * Convert.ToInt32(count.Text);
                    count.Text = "";
                }

            }
        }
        private void getFoods()
        {
            con.Open();
            NpgsqlCommand command = new NpgsqlCommand($"SELECT * FROM Products where type='food'", con);
            NpgsqlDataReader dr = command.ExecuteReader();
            int u = 0;
            while (dr.Read())
            {

                string name = dr.GetString(1);
                string type = dr.GetString(2);
                double price = dr.GetDouble(3);
                int code = dr.GetInt32(4);
                foods(name, type, price, code, u += 30);
            }

            con.Close();
        }

        private void Employee_Form_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'products.products' table. You can move, or remove it, as needed.
            this.productsTableAdapter1.Fill(this.products.products);
            getFoods();
            getDrinks();
           
        }

       
           
           

        private void buy_Click(object sender, EventArgs e)
        {
            string selectedItem = "";
            for (int i = 0; i < Reset.Items.Count; i++)
            {

                selectedItem += Reset.Items[i].ToString() + "\n" ;

            }

            DateTime now = DateTime.Now;

            if (
               MessageBox.Show(@"Are you Sure?", @"My Message Box", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                MessageBox.Show("              Cuffee Elsa3ada"+"\n"+
                    $"       emp_name:       {welcome.Text}" + "\n" + "\n" + "\n" + "\n" +
                    $"Order:       {selectedItem}" + "\n" +
                    $"Total_price: {priceFood + priceDrinks + 0.5} $" + "\n"+"\n"+"\n" +
                    now.ToString());
                Reset.Items.Clear();
                ReseView.Rows.Clear();
               
            }
            else
            {
                //NO --->NO IM STUCK
            }

        }

        private void done_Click(object sender, EventArgs e)
        {
            ReseView.Rows.Clear();
            ReseView.Rows.Add(priceDrinks + " $", priceFood + " $", "0.5 $", priceDrinks + priceFood + 0.5 + " $");
        }

        private void logout_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.empUser = "";
            Properties.Settings.Default.empPass = "";
            Properties.Settings.Default.Save();
            this.Hide();
            Login log = new Login();
            log.ShowDialog();
            this.Close();
        }

        private void deny_Click(object sender, EventArgs e)
        {
            
            Reset.Items.Clear();
            ReseView.Rows.Clear();
            priceDrinks = 0;
            priceFood = 0;
        }
    }
}
