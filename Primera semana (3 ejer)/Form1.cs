using System;
using System.Windows.Forms;

namespace ProyectoIMC
{
    public partial class Form1 : Form
    {
        int contador = 0;
        bool bloqueado = false; 

        public Form1()
        {
            InitializeComponent();
        }

        
        private void btnCalcularIMC_Click(object sender, EventArgs e)
        {
            if (double.TryParse(txtAltura.Text, out double altura) &&
                double.TryParse(txtPeso.Text, out double peso))
            {
                if (altura <= 0 || peso <= 0)
                {
                    MessageBox.Show("Los valores deben ser mayores que 0.");
                    return;
                }

                double imc = peso / (altura * altura);
                lblResultadoIMC.Text = $"IMC: {imc:F2}";

                if (imc < 16)
                    lblDiagnostico.Text = "Delgadez Severa";
                else if (imc >= 16 && imc < 18.5)
                    lblDiagnostico.Text = "Delgadez Moderada";
                else if (imc >= 18.5 && imc < 25)
                    lblDiagnostico.Text = "Peso Normal";
                else if (imc >= 25 && imc < 30)
                    lblDiagnostico.Text = "Sobrepeso";
                else if (imc >= 30 && imc < 35)
                    lblDiagnostico.Text = "Obesidad Grado 1";
                else if (imc >= 35 && imc < 40)
                    lblDiagnostico.Text = "Obesidad Grado 2";
                else
                    lblDiagnostico.Text = "Obesidad Grado 3";
            }
            else
            {
                MessageBox.Show("Ingrese valores numéricos válidos.");
            }
        }

        private void txtNumeros_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        
        private void lblContador_Click(object sender, EventArgs e)
        {
            contador++;
            lblContador.Text = contador.ToString();
        }

      
        private void txtCelsius_TextChanged(object sender, EventArgs e)
        {
            if (bloqueado) return;
            if (double.TryParse(txtCelsius.Text, out double celsius))
            {
                bloqueado = true;
                txtFahrenheit.Text = ((celsius * 9 / 5) + 32).ToString("F2");
                bloqueado = false;
            }
            else if (string.IsNullOrWhiteSpace(txtCelsius.Text))
            {
                bloqueado = true;
                txtFahrenheit.Clear();
                bloqueado = false;
            }
        }

        private void txtFahrenheit_TextChanged(object sender, EventArgs e)
        {
            if (bloqueado) return;
            if (double.TryParse(txtFahrenheit.Text, out double fahrenheit))
            {
                bloqueado = true;
                txtCelsius.Text = ((fahrenheit - 32) * 5 / 9).ToString("F2");
                bloqueado = false;
            }
            else if (string.IsNullOrWhiteSpace(txtFahrenheit.Text))
            {
                bloqueado = true;
                txtCelsius.Clear();
                bloqueado = false;
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtCelsius.Clear();
            txtFahrenheit.Clear();
        }
    }
}
