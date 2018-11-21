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
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
        }

        private void Dashboard_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Dashboard_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(MessageBox.Show("¿Esta seguro de cerrar la aplicación?","Confirmar Acción", MessageBoxButtons.OKCancel, MessageBoxIcon.Question).Equals(DialogResult.Cancel))
            {
                e.Cancel = true;
            }
        }

        private void artículosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ArticuloForm articuloForm = new ArticuloForm();
            articuloForm.Owner = this;
            articuloForm.ShowDialog();
        }
    }
}
