using LB1.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LB1
{
    public partial class AddOrder : Form
    {
        public AddOrder()
        {
            InitializeComponent();
        }

        private void AddOrder_Load(object sender, EventArgs e)
        {
            using (var context = new ApplicationContext())
            {
                comboBox1.Items.AddRange(context.Clients.ToArray());
                comboBox2.Items.AddRange(context.Products.ToArray());
                dateTimePicker1.Value = DateTime.Now;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (var context = new ApplicationContext())
                {
                    context.Orders.Add(new Order
                    {
                        Client = context.Clients.FirstOrDefault(x => x.Id == ((Client)comboBox1.SelectedItem).Id),
                        OrderDate = dateTimePicker1.Value.ToUniversalTime(),
                        Product = context.Products.FirstOrDefault(x => x.Id == ((Product)comboBox2.SelectedItem).Id),
                        DeliveryAdress = textBox1.Text
                    });
                    context.SaveChanges();
                    MessageBox.Show($"Order Added", "Sucess");
                    Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }
    }
}
