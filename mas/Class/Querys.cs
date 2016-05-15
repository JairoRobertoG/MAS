using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    class Querys
    {
        SqlParameter param;

        public string datosConexion()
        {
            string result = "Data Source = 127.0.0.1;"
            + "Initial Catalog = MAS; Integrated Security = true;";

            return result;
        }

        public DataTable Datos()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(datosConexion());//cadena conexion

                string consulta = "SELECT UPPER(Colonia) as Colonia FROM CT_Colonias"; 
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
                                               string Tel, string Colonia, string Calle, string NoCalle, int CP)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(datosConexion()))
                {
                    con.Open();

                    string textoCmd = "insert into CT_Informacion ( " +
                                        "Nombre, " +
                                        "ApellidoPaterno, " +
                                        "ApellidoMaterno, " +
                                        "Tel, " +
                                        "Colonia, " +
                                        "Calle, " +
                                        "NoCalle, " +
                                        "CP, " +
                                        "Fecha " +
                                      ") " +
                                      "values ( " +
                                        "@Nombre, " +
                                        "@ApellidoPaterno, " +
                                        "@ApellidoMaterno, " +
                                        "@Tel, " +
                                        "@Colonia, " +
                                        "@Calle, " +
                                        "@NoCalle, " +
                                        "@CP, " +
                                        "GETDATE() " +
                                      ") " ;

                    SqlCommand cmd = new SqlCommand(textoCmd, con);

                    cmd.Parameters.AddWithValue("@Nombre", Nombre);
                    cmd.Parameters.AddWithValue("@ApellidoPaterno", ApellidoPaterno);
                    cmd.Parameters.AddWithValue("@ApellidoMaterno", ApellidoMaterno);
                    cmd.Parameters.AddWithValue("@Tel", Tel);
                    cmd.Parameters.AddWithValue("@Colonia", Colonia);
                    cmd.Parameters.AddWithValue("@Calle", Calle);
                    cmd.Parameters.AddWithValue("@NoCalle", NoCalle);
                    cmd.Parameters.AddWithValue("@CP", CP);
                    
                    try
                    {
                        cmd.ExecuteNonQuery();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        MessageBox.Show("La información ha sido guardada con exito");
                    }

                    catch (SqlException e)
                    {
                        MessageBox.Show(e.ToString());
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
                                        
                    string textCmd = "select CP " +
                                      "from CT_Colonias " +
                                      "where Colonia = @Colonia ";

                    
                    param.ParameterName = "@Colonia";
                    param.SqlDbType = SqlDbType.VarChar;
                    param.Value = Colonia;

                    SqlCommand cmd = new SqlCommand(textCmd, con);

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
        
        public AutoCompleteStringCollection Autocomplete()
        {
            DataTable dt;
            AutoCompleteStringCollection coleccion = new AutoCompleteStringCollection();
            try
            {
                dt = Datos();
                foreach (DataRow row in dt.Rows)
                {
                    coleccion.Add(row["Colonia"].ToString().Trim());
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return coleccion;
        }





        
    }
}
