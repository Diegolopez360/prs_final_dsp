using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace CaligulasDesktop.Model
{
    class MarcaService : ApplicationDbContext
    {
        public MarcaService() : base()
        {

        }

        public ICollection<Entity.MarcaEntity> GetAll()
        {
            List<Entity.MarcaEntity> lista = new List<Entity.MarcaEntity>();
            try
            {
                using (SqlCommand command = Open().CreateCommand())
                {
                    command.CommandText = "select * from Marca";
                    command.CommandType = System.Data.CommandType.Text;
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                lista.Add(new Entity.MarcaEntity { MarcaId = reader.GetString(0), Nombre = reader.GetString(1) });
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
