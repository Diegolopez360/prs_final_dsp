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
    public partial class CreateArticuloForm : Form
    {
        private readonly Model.MarcaService marcaService = new Model.MarcaService();
        private readonly Model.CategoriaService categoriaService = new Model.CategoriaService();
        private readonly Model.ArticuloService articuloService = new Model.ArticuloService();

        public CreateArticuloForm()
        {
            InitializeComponent();
        }

        private void CreateArticuloForm_Load(object sender, EventArgs e)
        {
            cmbMarca.DataSource = marcaService.GetAll();
            cmbMarca.DisplayMember = "Nombre";
            cmbMarca.ValueMember = "MarcaId";

            cmbCategoria.DataSource = categoriaService.GetAll();
            cmbCategoria.DisplayMember = "Nombre";
            cmbCategoria.ValueMember = "CategoriaId";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtSku.Text))
            {
                MessageBox.Show("SKU no debe estar vacio", "Error Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSku.Focus();
            }
            else if (string.IsNullOrEmpty(txtNombre.Text))
            {
                MessageBox.Show("Nombre no debe estar vacio", "Error Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombre.Focus();
            }
            else if (string.IsNullOrEmpty(txtPrecioUnitario.Text))
            {
                MessageBox.Show("Precio Unitario no debe estar vacio", "Error Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPrecioUnitario.Focus();
            }
            else if (string.IsNullOrEmpty(txtPrecioCompra.Text))
            {
                MessageBox.Show("Precio Compra no debe estar vacio", "Error Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPrecioCompra.Focus();
            }
            else if (string.IsNullOrEmpty(txtStock.Text))
            {
                MessageBox.Show("Stock no debe estar vacio", "Error Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtStock.Focus();
            }
            else if (string.IsNullOrEmpty(txtUnidadesCompradas.Text))
            {
                MessageBox.Show("Unidades Compradas no debe estar vacio", "Error Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUnidadesCompradas.Focus();
            }
            else
            {
                if (cmbMarca.SelectedValue == null)
                {
                    MessageBox.Show("Control de selección para marcas no se ha establecido", "Error Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (cmbCategoria.SelectedValue == null)
                {
                    MessageBox.Show("Control de selección para marcas no se ha establecido", "Error Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    Model.Entity.ArticuloEntity articulo = new Model.Entity.ArticuloEntity
                    {
                        ArticuloId = Guid.NewGuid().ToString(),
                        Nombre = txtNombre.Text,
                        Sku = txtSku.Text,
                        Descripcion = txtDescripcion.Text,
                        PrecioUnitario = double.Parse(txtPrecioUnitario.Text),
                        PrecioCompra = double.Parse(txtPrecioCompra.Text),
                        Stock = int.Parse(txtStock.Text),
                        UnidadesCompradas = int.Parse(txtUnidadesCompradas.Text),
                        CategoriaId = cmbCategoria.SelectedValue.ToString(),
                        MarcaId = cmbMarca.SelectedValue.ToString(),
                        FechaCreacion = DateTime.Now,
                        ImageUrl = "NotFound.jpg"
                    };
                    articuloService.Save(articulo);
                    MessageBox.Show("Se ha agregado un nuevo regsitro", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}
