using LB1.Models;
using Microsoft.EntityFrameworkCore;
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

namespace LB1
{
    public partial class ChangeOrder : Form
    {
        Order _changeOrder;
        public ChangeOrder(Order order)
        {
            InitializeComponent();
            _changeOrder = order;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (var context = new ApplicationContext())
                {
                    _changeOrder.Client = context.Clients.FirstOrDefault(x => x.Id == (comboBox1.SelectedItem as Client).Id);
                    _changeOrder.Product = context.Products.FirstOrDefault(x => x.Id == (comboBox2.SelectedItem as Product).Id);
                    _changeOrder.DeliveryAdress = textBox1.Text;
                    _changeOrder.OrderDate = dateTimePicker1.Value.ToUniversalTime();


                    context.Update(_changeOrder);
                    context.SaveChanges();
                    MessageBox.Show($"Row {_changeOrder.Id} Are Updated", "Sucess");
                    Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error");
            }
        }

        private void ChangeOrder_Load(object sender, EventArgs e)
        {
            using (var context = new ApplicationContext())
            {
                comboBox1.Items.AddRange(context.Clients.ToArray());
                comboBox2.Items.AddRange(context.Products.ToArray());
                comboBox1.SelectedItem = context.Clients.FirstOrDefault(x => x.Id == _changeOrder.Client.Id);
                comboBox2.SelectedItem = context.Products.FirstOrDefault(x => x.Id == _changeOrder.Product.Id);
                dateTimePicker1.Value = _changeOrder.OrderDate;
                textBox1.Text = _changeOrder.DeliveryAdress;
            }
        }
    }
}
