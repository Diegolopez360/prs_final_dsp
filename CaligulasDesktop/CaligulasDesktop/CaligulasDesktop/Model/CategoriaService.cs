using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace CaligulasDesktop.Model
{
    class CategoriaService : ApplicationDbContext
    {
        public CategoriaService() : base()
        {

        }

        public ICollection<Entity.CategoriaEntity> GetAll()
        {
            List<Entity.CategoriaEntity> lista = new List<Entity.CategoriaEntity>();
            try
            {
                using (SqlCommand command = Open().CreateCommand())
                {
                    command.CommandText = "select * from Categoria";
                    command.CommandType = System.Data.CommandType.Text;
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                lista.Add(new Entity.CategoriaEntity { CategoriaId = reader.GetString(0), Nombre = reader.GetString(1) });
                            }
                        }
                    }
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
            return lista;
        }
    }
}
