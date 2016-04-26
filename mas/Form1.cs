using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;

namespace WindowsFormsApplication1
{

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Stop();
            fechaActual();

            Autocompletados();
        }

        public void Autocompletados()
        {
            txtColonia.AutoCompleteCustomSource = Autocomplete();
            txtColonia.AutoCompleteMode = AutoCompleteMode.Suggest;
            txtColonia.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }

        public AutoCompleteStringCollection Autocomplete()
        {
            MAS_COM.Class1 obj = new MAS_COM.Class1();
            DataTable dt;
            AutoCompleteStringCollection coleccion = new AutoCompleteStringCollection();
            try
            {
                dt = obj.Datos();
                foreach (DataRow row in dt.Rows)
                {
                    coleccion.Add(row["Colonia"].ToString().Trim());
                }                              
            }
            catch (Exception e)
            {
                lblError.Text = "No Se pudieron cargar las colonias favor de contactar al administrador de la aplicacion";
            }
            
            return coleccion;
        }

        public void fechaActual()
        {
            lblFechaActual.Text = DateTime.Now.ToString("dd/MMM/yyyy");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblHora.Text = DateTime.Now.ToLongTimeString();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Prueba en github");
            GuardarInformacion();
        }

        public void GuardarInformacion()
        {
            MAS_COM.Class1 obj = new MAS_COM.Class1();
            try
            {
                obj.insertarInformacionCliente(txtNombre.Text, txtApellidoPaterno.Text, txtApellidoMaterno.Text,
                    txtTel.Text, txtColonia.Text, txtCalle.Text, 
                    Convert.ToInt32(txtNoCalle.Text), Convert.ToInt32(txtCP.Text));
            }
            catch (Exception ex)
            {
                lblError.Text = "No se pudo guardar la información favor de contactar al administrador de la aplicación";
            }
        }
        
        private void txtColonia_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == Convert.ToChar(Keys.Enter))
            {
                MAS_COM.Class1 obj = new MAS_COM.Class1();
                DataSet ds;

                try
                {
                    ds = obj.CP(txtColonia.Text);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        txtCP.Text = ds.Tables[0].Rows[0]["CP"].ToString().Trim();
                        txtColonia.Focus();
                    }
                    else
                    {
                        MessageBox.Show("No se encontro un C.P. para esta Colonia: " + txtColonia.Text);
                    }
                }
                catch (Exception ex)
                {
                    lblError.Text = "No se pudo cargar el CP favor de contactar al administrador de la aplicación";
                }
            }
            
        }

        //private void txtColonia_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    if (e.KeyChar == Convert.ToChar(Keys.Enter))
        //    {
        //        MAS_COM.Class1 obj = new MAS_COM.Class1();
        //        DataSet ds;

        //        try
        //        {
        //            ds = obj.CP(txtColonia.Text);
        //            if (ds.Tables[0].Rows.Count > 0)
        //            {
        //                txtCP.Text = ds.Tables[0].Rows[0]["CP"].ToString().Trim();
        //            }
        //            else
        //            {
        //                MessageBox.Show("No se encontro un C.P. para esta Colonia: " + txtColonia.Text);
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            lblError.Text = "No se pudo cargar el CP favor de contactar al administrador de la aplicación";
        //        }
        //    }
        }

        
                                        
    }
