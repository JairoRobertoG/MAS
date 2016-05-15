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
        clParent cl = new clParent();
        Querys obj = new Querys();
        DataSet ds;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Start();
            lblFechaActual.Text = cl.CurrentyDate();
            Autocompletados();
        }

        public void Autocompletados()
        {
            txtColonia.AutoCompleteCustomSource = obj.Autocomplete();
            txtColonia.AutoCompleteMode = AutoCompleteMode.Suggest;
            txtColonia.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }
        
        private void timer1_Tick(object sender, EventArgs e)
        {
            lblHora.Text = DateTime.Now.ToLongTimeString();
        }
        
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            SaveInformation();
        }

        public void SaveInformation()
        {
            if (cl.ValidateInformation(txtNombre.Text, txtApellidoPaterno.Text, txtTel.Text,
                txtColonia.Text, txtCalle.Text, txtNoCalle.Text, txtCP.Text))
            {
                obj.insertarInformacionCliente(txtNombre.Text, txtApellidoPaterno.Text, txtApellidoMaterno.Text,
                    txtTel.Text, txtColonia.Text, txtCalle.Text, txtNoCalle.Text, Convert.ToInt32(txtCP.Text));
            }
                                    
        }

        private void txtColonia_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == Convert.ToChar(Keys.Enter))
            {
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

        private void txtColonia_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                try
                {
                    ds = obj.CP(txtColonia.Text);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        txtCP.Text = ds.Tables[0].Rows[0]["CP"].ToString().Trim();
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

   } 
                                        
}
