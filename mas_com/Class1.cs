using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;

namespace MAS_COM
{
    public class Class1
    {
        SqlDataReader Rs;
        System.Data.SqlClient.SqlParameter param;
        
        public string datosConexion()
        {
            string result = "Data Source = 127.1.1.0;"
            + "Initial Catalog = MAS; Integrated Security = true;";

            return result;
        }

        public DataTable Datos()
        {
            DataTable dt = new DataTable();

            try
            {
                SqlConnection conexion = new SqlConnection(datosConexion());//cadena conexion

                string consulta = "SELECT UPPER(Colonia) as Colonia FROM CT_Colonias"; //consulta a la tabla paises
                SqlCommand comando = new SqlCommand(consulta, conexion);

                SqlDataAdapter adap = new SqlDataAdapter(comando);

                adap.Fill(dt);
                return dt;
            }

            catch (SqlException e)
            {
                throw e;
            }
            
        }

        public void insertarInformacionCliente(string Nombre, string ApellidoPaterno, string ApellidoMaterno, 
                                               string Tel, string Colonia, string Calle, int NoCalle, int CP)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(datosConexion()))
                {
                    con.Open();

                    string textoCmd = "insert into CT_Informacion ( " +
                                        "Nombre, " +
                                        "ApellidoPaterno, " +
                                        "Tel, " +
                                        "Colonia, " +
                                        "Calle, " +
                                        "NoCalle, " +
                                        "CP " +
                                      ") " +
                                      "values ( " +
                                        "@Nombre, " +
                                        "@ApellidoPaterno, " +
                                        "@Tel, " +
                                        "@Colonia, " +
                                        "@Calle, " +
                                        "@NoCalle, " +
                                        "@CP " +
                                      ") " ;

                    SqlCommand cmd = new SqlCommand(textoCmd, con);

                    cmd.Parameters.AddWithValue("@Nombre", Nombre);
                    cmd.Parameters.AddWithValue("@ApellidoPaterno", ApellidoPaterno);
                    cmd.Parameters.AddWithValue("@Tel", Tel);
                    cmd.Parameters.AddWithValue("@Colonia", Colonia);
                    cmd.Parameters.AddWithValue("@Calle", Calle);
                    cmd.Parameters.AddWithValue("@NoCalle", NoCalle);
                    cmd.Parameters.AddWithValue("@CP", CP);
                    
                    try
                    {
                        cmd.ExecuteNonQuery();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                    }

                    catch (SqlException e)
                    {
                        throw e;
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
                                               
        }

        public DataSet CP(string Colonia)
        {
            DataSet ds = new DataSet();
            param = new System.Data.SqlClient.SqlParameter();
            try
            {
                using (SqlConnection con = new SqlConnection(datosConexion()))
                {
                    con.Open();
                                        
                    string textoCmd = "select CP " +
                                      "from CT_Colonias " +
                                      "where Colonia = @Colonia ";

                    
                    param.ParameterName = "@Colonia";
                    param.SqlDbType = SqlDbType.VarChar;
                    param.Value = Colonia;

                    SqlCommand cmd = new SqlCommand(textoCmd, con);

                    try
                    {
                        cmd.Parameters.Add(param);
                        cmd.ExecuteNonQuery();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(ds);
                        
                    }

                    catch (SqlException e)
                    {
                        throw e;
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return ds;
        }

                        
    }
}
