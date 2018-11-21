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
    public partial class ArticuloForm : Form
    {
        private readonly Model.ArticuloService articuloService = new Model.ArticuloService();

        public ArticuloForm()
        {
            InitializeComponent();
        }

        private void ArticuloForm_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = articuloService.GetAll();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CreateArticuloForm createArticuloForm = new CreateArticuloForm();
            createArticuloForm.Owner = this;
            createArticuloForm.ShowDialog();
        }
    }
}
