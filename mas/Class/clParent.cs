using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    class clParent
    {
        public string CurrentyDate()
        {
            return DateTime.Now.ToString("dd/MMM/yyyy");
        }

        public bool ValidateInformation(string name, string lastName, string telephone,
                    string district, string street, string numberStreet, string zipCode)
        {
            if (name == string.Empty)
            {
                MessageBox.Show("Debes de escribir un Nombre");
                return false;
            }
            else if (lastName == string.Empty)
            {
                MessageBox.Show("Debes de escribir un Apellido Paterno");
                return false;
            }
            else if (telephone == string.Empty)
            {
                MessageBox.Show("Debes de escribir un El Numero Telefonico");
                return false;
            }
            else if (IsNumeric(telephone) == false)
            {
                MessageBox.Show("Debes de escribir Solo Numeros en el Telefono");
                return false;
            }
            else if (district == string.Empty)
            {
                MessageBox.Show("Debes de escribir una Colonia");
                return false;
            }
            else if (street == string.Empty)
            {
                MessageBox.Show("Debes de escribir un la Calle");
                return false;
            }
            else if (numberStreet == string.Empty)
            {
                MessageBox.Show("Debes de escribir el Numero de Casa");
                return false;
            }
            else if (zipCode == string.Empty)
            {
                MessageBox.Show("Debes de escribir un el Código Postal");
                return false;
            }
            else if (IsNumeric(zipCode) == false)
            {
                MessageBox.Show("Debes de escribir numeros en el Código Postal");
                return false;
            }
            
            return true;
        }

        private bool IsNumeric(string num)
        {
            try
            {
                double x = Convert.ToDouble(num);
                return true;
            }

            catch (Exception)
            {
                return false;
            }
        }
    }
}
