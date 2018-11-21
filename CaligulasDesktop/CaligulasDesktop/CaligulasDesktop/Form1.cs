using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CaligulasDesktop
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Username no debe estar vacio", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (string.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("Contraseña no debe estar vacia", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if(textBox1.Text.Equals("administrador") && textBox2.Text.Equals("Pa$$w0rd"))
                {
                    Hide();
                    Dashboard dashboard = new Dashboard();
                    dashboard.Show();
                }
                else
                {
                    MessageBox.Show("Error de autenticación, Username ó Contraseña incorrectos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
