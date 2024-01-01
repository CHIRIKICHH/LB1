using LB1.Models;
using Microsoft.EntityFrameworkCore;
using Npgsql;
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

                var cs = "Host=217.19.212.166;Port=5454;Database=LB1;Username=admin;Password=admin";

                using var con = new NpgsqlConnection(cs);
                con.Open();

                var sql = textBox1.Text;
                using var cmd = new NpgsqlCommand(sql, con);

                if (radioButton1.Checked)
                {
                    using NpgsqlDataReader rdr = cmd.ExecuteReader();
                    string name = "";
                    DataTable dt = new DataTable();
                    dt.Load(rdr);
                    dataGridView1.DataSource = dt;
                }
                else
                {
                    string text = cmd.ExecuteNonQuery().ToString();
                    textBox2.Text = text;
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
