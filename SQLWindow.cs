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

namespace LB1
{
    public partial class SQLWindow : Form
    {
        public SQLWindow()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (var context = new ApplicationContext())
                {
                    if (radioButton1.Checked)
                    {
                        if (textBox1.Text.Contains("Orders"))
                        {
                            dataGridView1.DataSource = context.Orders.FromSqlRaw($"{textBox1.Text}").Include(x => x.Product).Include(x => x.Client).ToList();
                        }
                        else if (textBox1.Text.Contains("Clients"))
                        {
                            dataGridView1.DataSource = context.Clients.FromSqlRaw($"{textBox1.Text}").ToList();
                        }
                        else if (textBox1.Text.Contains("Products"))
                        {
                            dataGridView1.DataSource = context.Products.FromSqlRaw($"{textBox1.Text}").ToList();
                        }
                        else
                        {
                            MessageBox.Show("Table does not exist!", "Error");
                        }
                    }
                    else
                    {
                        string text = context.Database.ExecuteSqlRaw($"{textBox1.Text}").ToString();
                        if(text == "1")
                        {
                            textBox2.Text = "Success";
                        }
                        else
                        {
                            textBox2.Text = text;
                        }
                    }
                }
        }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error");
            }
}

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                dataGridView1.Visible = true;
                textBox2.Visible = false;
            }
            else
            {
                dataGridView1.Visible = false;
                textBox2.Visible = true;
            }
        }
    }

    public class GetSQL<T>
    {
        public string GetSQLString(string SQLQERY)
        {
            using (var context = new ApplicationContext())
            {
                return context.Database.SqlQuery<T>($"{SQLQERY}").ToString();

            }
        }
    }
}
