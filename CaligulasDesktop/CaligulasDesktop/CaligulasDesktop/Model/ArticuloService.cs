using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace CaligulasDesktop.Model
{
    class ArticuloService : ApplicationDbContext
    {
        public ArticuloService() : base()
        {

        }

        public void Save(Entity.ArticuloEntity entity)
        {
            try
            {
                using (SqlCommand command = Open().CreateCommand())
                {
                    command.CommandType = System.Data.CommandType.Text;
                    command.CommandText = "insert into Articulo(ArticuloId,SKU,Nombre,Descripcion,ImageUrl,PrecioUnitario,PrecioCompra,Stock,UnidadesCompradas,FechaCreacion,Categoria_CategoriaId,Marca_MarcaId) values(@articuloid,@sku,@nombre,@descripcion,@imageurl,@preciounitario,@preciocompra,@stock,@unidadescompradas,@fechacreacion,@categoria,@marca)";
                    command.Parameters.AddWithValue("@articuloid", entity.ArticuloId);
                    command.Parameters.AddWithValue("@sku", entity.Sku);
                    command.Parameters.AddWithValue("@nombre", entity.Nombre);
                    command.Parameters.AddWithValue("@descripcion", entity.Descripcion);
                    command.Parameters.AddWithValue("@imageurl", entity.ImageUrl);
                    command.Parameters.AddWithValue("@preciounitario", entity.PrecioUnitario);
                    command.Parameters.AddWithValue("@preciocompra", entity.PrecioCompra);
                    command.Parameters.AddWithValue("@stock", entity.Stock);
                    command.Parameters.AddWithValue("@unidadescompradas", entity.UnidadesCompradas);
                    command.Parameters.AddWithValue("@fechacreacion", entity.FechaCreacion);
                    command.Parameters.AddWithValue("@categoria", entity.CategoriaId);
                    command.Parameters.AddWithValue("@marca", entity.MarcaId);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception error)
            {
                System.Diagnostics.Debug.WriteLine(error.Message);
            }
            finally
            {
                CloseConnection();
            }

        }

        public DataTable GetAll()
        {
            DataTable datatable = new DataTable();
            try
            {
                string sqllines = "select a.ArticuloId,a.SKU,a.Nombre,a.PrecioUnitario,a.PrecioCompra,a.Stock,m.Nombre as Marca,c.Nombre as Categoria from Articulo a inner join Categoria c on a.Categoria_CategoriaId = c.CategoriaId inner join Marca m on a.Marca_MarcaId = m.MarcaId";
                using (SqlDataAdapter adapter = new SqlDataAdapter(sqllines, Open()))
                {
                    adapter.Fill(datatable);
                }
            }
            catch (Exception error)
            {
                System.Diagnostics.Debug.WriteLine(error.Message);
            }
            finally
            {
                CloseConnection();
            }
            return datatable;
        }
    }
}
