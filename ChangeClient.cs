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
    public partial class ChangeClient : Form
    {
        Client _changeClient;
        public ChangeClient(Client client)
        {
            InitializeComponent();
            _changeClient = client;
        }

        private void ChangeClient_Load(object sender, EventArgs e)
        {
            textBox1.Text = _changeClient.FirstName;
            textBox4.Text = _changeClient.LastName;
            textBox5.Text = _changeClient.MiddleName;
            textBox2.Text = _changeClient.ContactPhone;
            textBox3.Text = _changeClient.Email;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                _changeClient.FirstName = textBox1.Text;
                _changeClient.LastName = textBox4.Text;
                _changeClient.MiddleName = textBox5.Text;
                _changeClient.ContactPhone = textBox2.Text;
                _changeClient.Email = textBox3.Text;

                using (var context = new ApplicationContext())
                {
                    context.Update(_changeClient);
                    context.SaveChanges();
                    MessageBox.Show($"Row {_changeClient} Are Updated", "Sucess");
                    Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error");
            }
        }
    }
}
