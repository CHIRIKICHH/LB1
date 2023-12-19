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
    public partial class AddClient : Form
    {
        public AddClient()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (var context = new ApplicationContext())
                {
                    context.Clients.Add(
                        new Client
                        {
                            FirstName = textBox1.Text,
                            LastName = textBox4.Text,
                            MiddleName = textBox5.Text,
                            ContactPhone = textBox2.Text,
                            Email = textBox3.Text
                        }
                        );
                    context.SaveChanges();
                    MessageBox.Show($"Client {textBox1.Text} Added", "Succes");
                    Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error");
            }
        }

        private void AddClient_Load(object sender, EventArgs e)
        {

        }
    }
}
