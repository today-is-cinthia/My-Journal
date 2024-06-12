using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using My_Journal.Properties;
using System.Data;

namespace My_Journal.Models.Ofrenda
{ 
    public class MantOfrenda
    {
        public MantOfrenda()
        {
        }
        public string Insertar(Ofrenda ofrenda)
        {
            string valstring = string.Empty;
            string cnn = Utilidad.getConexString();
            try
            {
                using (SqlConnection connection = new SqlConnection(cnn))
                {
                    using (SqlCommand sqlCommand = new SqlCommand("[IGLESIA].pcdSetOfrenda", connection))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@IdCategoria", ofrenda.IdCatOfrenda);
                        sqlCommand.Parameters.AddWithValue("@Descripcion", ofrenda.Descripcion);
                        sqlCommand.Parameters.AddWithValue("@Cantidad", ofrenda.Cantidad);
                        sqlCommand.Parameters.AddWithValue("@Fecha", ofrenda.Fecha);
                        sqlCommand.Parameters.AddWithValue("@IdDivisa", ofrenda.Divisa);
                        sqlCommand.Parameters.AddWithValue("@TasaCambio", ofrenda.TasaCambio);
                        sqlCommand.Parameters.AddWithValue("@IdUsuario", 1);

                        connection.Open();
                        sqlCommand.ExecuteNonQuery();
                        connection.Close();
                        //if (Cod.Value.ToString() != string.Empty)
                        //{
                        //    var _resource = new ResourceManager(typeof(strings));
                        //    string res = _resource.GetString(Cod.Value.ToString());
                        //    valstring = res;
                        //}
                    }
                }
            }
            catch (Exception ex)
            {
                valstring = "ERROR";
                //Utilidad.Ubicacion = clase + System.Reflection.MethodBase.GetCurrentMethod().Name;
                //Utilidad.GuardarLog(ex.Message, Utilidad.objectToString(registro));
            }
            return valstring;
        }

        public List<OfrendaViewModel> GetListadoOfrendas()
        {
            List<OfrendaViewModel> resultado = new List<OfrendaViewModel>();
            var cnn = Utilidad.getConexString();
            try
            {
                using (SqlConnection connection = new SqlConnection(cnn))
                {
                    using (SqlCommand sqlCommand = new SqlCommand("[IGLESIA].pcdGetListadoOfrendas", connection))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        connection.Open();
                        var dt = new DataTable();
                        dt.Load(sqlCommand.ExecuteReader());
                        resultado = (from DataRow dr in dt.Rows
                                     select new OfrendaViewModel()
                                     {
                                         Ofrenda = new Ofrenda
                                         {
                                             IdOfrenda = int.Parse(dr["IdOfrenda"].ToString()),
                                             IdCatOfrenda = int.Parse(dr["IdCatOfrenda"].ToString()),
                                             Cantidad = double.Parse(dr["Cantidad"].ToString()),
                                             Descripcion = dr["Descripcion"].ToString(),
                                             Fecha = DateTime.Parse(dr["Fecha"].ToString()),
                                             Divisa = int.Parse(dr["Divisa"].ToString()),
                                             TasaCambio = double.Parse(dr["TasaCambio"].ToString()),
                                             UsuarioCreacion = dr["UsuarioCreacion"] != DBNull.Value ? int.Parse(dr["UsuarioCreacion"].ToString()) : (int?)null,
                                             FechaCreacion = dr["FechaCreacion"] != DBNull.Value ? DateTime.Parse(dr["FechaCreacion"].ToString()) : (DateTime?)null,
                                             UsuarioModifica = dr["UsuarioModifica"] != DBNull.Value ? int.Parse(dr["UsuarioModifica"].ToString()) : (int?)null,
                                             FechaModifica = dr["FechaModifica"] != DBNull.Value ? DateTime.Parse(dr["FechaModifica"].ToString()) : (DateTime?)null,
                                         },
                                         OfrendaCategoria = new OfrendaCategoria.OfrendasCategoria
                                         {
                                             Nombre = dr["CategoriaNombre"].ToString(),
                                             Descripcion = dr["CategoriaDescripcion"].ToString()
                                         },
                                         Divisa = new Divisa.Divisa
                                         {
                                             CodDivisa = dr["CodDivisa"].ToString(),
                                             Descripcion = dr["Divisa"].ToString()
                                         }
                                     }).ToList();
                        connection.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejar la excepción según sea necesario
            }
            return resultado;
        }

    }
}
