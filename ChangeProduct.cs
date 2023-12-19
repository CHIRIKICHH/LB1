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
    public partial class ChangeProduct : Form
    {
        Product _changeProduct;
        public ChangeProduct(Product product)
        {
            InitializeComponent();
            _changeProduct = product;
        }

        private void ChangeProduct_Load(object sender, EventArgs e)
        {
            textBox1.Text = _changeProduct.ProductName;
            textBox2.Text = _changeProduct.ProductCategory;
            textBox3.Text = _changeProduct.Price.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                _changeProduct.ProductName = textBox1.Text;
                _changeProduct.ProductCategory = textBox2.Text;
                _changeProduct.Price = double.Parse(textBox3.Text);

                using(var context = new ApplicationContext())
                {
                    context.Update(_changeProduct);
                    context.SaveChanges();
                    MessageBox.Show($"Row {_changeProduct} Are Updated","Sucess");
                    Close();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message,"error");
            }
        }
    }
}
