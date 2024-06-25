using Microsoft.Data.SqlClient;
using My_Journal.Models.Ofrenda;
using My_Journal.Properties;
using System.Data;

namespace My_Journal.Models.Pagos
{
    public class MantPago
    {

        public MantPago() { }

        public string Insertar(PagosViewModel pagosViewModel)
        {
            string valstring = string.Empty;
            string cnn = Utilidad.getConexString();
            try
            {
                using (SqlConnection connection = new SqlConnection(cnn))
                {
                    using (SqlCommand sqlCommand = new SqlCommand("[IGLESIA].pcdSetPago", connection))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@IdCategoria", pagosViewModel.PagosCategoria.IdCategoria);
                        sqlCommand.Parameters.AddWithValue("@Descripcion", pagosViewModel.Pagos.Descripcion);
                        sqlCommand.Parameters.AddWithValue("@Cantidad", pagosViewModel.PagosDetalle.Cantidad);
                        sqlCommand.Parameters.AddWithValue("@Fecha", pagosViewModel.Pagos.Fecha);
                        sqlCommand.Parameters.AddWithValue("@IdDivisa", pagosViewModel.Divisa.IdDivisa);
                        sqlCommand.Parameters.AddWithValue("@TasaCambio", pagosViewModel.PagosDetalle.TasaCambio);
                        sqlCommand.Parameters.AddWithValue("@Estado", 1);
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

        public List<PagosViewModel> GetListadoPagos()
        {
            List<PagosViewModel> resultado = new List<PagosViewModel>();
            var cnn = Utilidad.getConexString();
            try
            {
                using (SqlConnection connection = new SqlConnection(cnn))
                {
                    using (SqlCommand sqlCommand = new SqlCommand("[IGLESIA].pcdGetListadosPagos", connection))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        connection.Open();
                        var dt = new DataTable();
                        dt.Load(sqlCommand.ExecuteReader());
                        resultado = (from DataRow dr in dt.Rows
                                     select new PagosViewModel()
                                     {
                                         Pagos = new Pago
                                         {
                                             IdPago = int.Parse(dr["IdPago"].ToString()),
                                             Descripcion = dr["Descripcion"].ToString(),
                                             Fecha = DateTime.Parse(dr["Fecha"].ToString()),
                                         },
                                         PagosCategoria = new PagosCategoria.PagosCategoria
                                         {
                                             IdCategoria = int.Parse(dr["IdCategoria"].ToString()),
                                             Nombre = dr["Nombre"].ToString(),
                                             Descripcion = dr["Descripcion"].ToString(),
                                             Estado = int.Parse(dr["Estado"].ToString()),

                                         },
                                         PagosDetalle = new PagosDetalle.PagosDetalle
                                         {
                                             IdDetalle = int.Parse(dr["IdDetalle"].ToString()),
                                             Cantidad = double.Parse(dr["Cantidad"].ToString()),
                                             TasaCambio = double.Parse(dr["TasaCambio"].ToString()),
                                         },
                                         Divisa = new Divisa.Divisa
                                         {
                                             CodDivisa = dr["CodDivisa"].ToString(),
                                             Descripcion = dr["Descripcion"].ToString()
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
