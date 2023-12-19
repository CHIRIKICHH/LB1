using LB1.Models;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices.ObjectiveC;

namespace LB1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void toolStrip2_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = GetOrders();
            label1.Text = "Orders";
            toolStripProgressBar1.Value = 1;
            toolStripLabel3.Text = "1";
        }

        private List<Order> GetOrders()
        {
            try
            {
                using (var context = new ApplicationContext())
                {
                    return context.Orders.Include(u => u.Client).Include(u => u.Product).ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
                return new List<Order>();
            }
        }

        private List<Client> GetClients()
        {
            try
            {
                using (var context = new ApplicationContext())
                {
                    return context.Clients.ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
                return new List<Client>();
            }
        }

        private List<Product> GetProducts()
        {
            try
            {
                using (var context = new ApplicationContext())
                {
                    return context.Products.ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
                return new List<Product>();
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.DataSource is List<Order>)
            {
                dataGridView1.DataSource = GetClients();
                label1.Text = "Clients";
                toolStripProgressBar1.Value = 2;
                toolStripLabel3.Text = "2";
            }
            else if (dataGridView1.DataSource is List<Client>)
            {
                dataGridView1.DataSource = GetProducts();
                label1.Text = "Products";
                toolStripProgressBar1.Value = 3;
                toolStripLabel3.Text = "3";
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.DataSource is List<Client>)
            {
                dataGridView1.DataSource = GetOrders();
                label1.Text = "Orders";
                toolStripProgressBar1.Value = 1;
                toolStripLabel3.Text = "1";
            }
            else if (dataGridView1.DataSource is List<Product>)
            {
                dataGridView1.DataSource = GetClients();
                label1.Text = "Clients";
                toolStripProgressBar1.Value = 2;
                toolStripLabel3.Text = "2";
            }
        }

        private void databaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new SQLWindow().Show();
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            if (dataGridView1.DataSource is List<Order>)
            {
                var temp = new AddOrder();
                temp.Show();
                temp.FormClosed += delegate { dataGridView1.DataSource = GetOrders(); };
            }
            else if (dataGridView1.DataSource is List<Product>)
            {
                var temp = new AddProduct();
                temp.Show();
                temp.FormClosed += delegate { dataGridView1.DataSource = GetProducts(); };
            }
            else if (dataGridView1.DataSource is List<Client>)
            {
                var temp = new AddClient();
                temp.Show();
                temp.FormClosed += delegate { dataGridView1.DataSource = GetClients(); };
            }
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            deleteToolStripMenuItem_Click(sender, e);
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = GetProducts();
            label1.Text = "Products";
            toolStripProgressBar1.Value = 3;
            toolStripLabel3.Text = "3";
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = GetOrders();
            label1.Text = "Orders";
            toolStripProgressBar1.Value = 1;
            toolStripLabel3.Text = "1";
        }

        private void orderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var temp = new AddOrder();
            temp.Show();
            temp.FormClosed += delegate { dataGridView1.DataSource = GetOrders(); };
        }

        private void clientToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var temp = new AddClient();
            temp.Show();
            temp.FormClosed += delegate { dataGridView1.DataSource = GetClients(); };
        }

        private void productToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var temp = new AddProduct();
            temp.Show();
            temp.FormClosed += delegate { dataGridView1.DataSource = GetProducts(); };
        }

        private void updateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
                if (MessageBox.Show($"Update {id} Row?", "Update", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    using (var context = new ApplicationContext())
                    {
                        if (dataGridView1.DataSource is List<Order>)
                        {
                            var temp = new ChangeOrder(context.Orders.Include(u => u.Client).Include(u => u.Product).FirstOrDefault(x => x.Id == id));
                            temp.Show();
                            temp.FormClosed += delegate { dataGridView1.DataSource = GetOrders(); };

                        }
                        if (dataGridView1.DataSource is List<Client>)
                        {
                            var temp = new ChangeClient(context.Clients.FirstOrDefault(x => x.Id == id));
                            temp.Show();
                            temp.FormClosed += delegate { dataGridView1.DataSource = GetClients(); };

                        }
                        if (dataGridView1.DataSource is List<Product>)
                        {
                            var temp = new ChangeProduct(context.Products.FirstOrDefault(x => x.Id == id));
                            temp.Show();
                            temp.FormClosed += delegate { dataGridView1.DataSource = GetProducts(); };

                        }
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error");
            }
        }


        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
                if (MessageBox.Show($"Delete {id} Row?", "Deleting", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    using (var context = new ApplicationContext())
                    {
                        if (dataGridView1.DataSource is List<Order>)
                        {
                            context.Orders.Remove(context.Orders.FirstOrDefault(x => x.Id == id));
                            context.SaveChanges();
                            dataGridView1.DataSource = GetOrders();
                            MessageBox.Show($"Row {id} are Deleted!", "Sucess");
                        }
                        if (dataGridView1.DataSource is List<Client>)
                        {
                            context.Clients.Remove(context.Clients.FirstOrDefault(x => x.Id == id));
                            context.SaveChanges();
                            dataGridView1.DataSource = GetClients();
                            MessageBox.Show($"Row {id} are Deleted!", "Sucess");
                        }
                        if (dataGridView1.DataSource is List<Product>)
                        {
                            context.Products.Remove(context.Products.FirstOrDefault(x => x.Id == id));
                            context.SaveChanges();
                            dataGridView1.DataSource = GetProducts();
                            MessageBox.Show($"Row {id} are Deleted!", "Sucess");
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error");
            }
        }
    }
}