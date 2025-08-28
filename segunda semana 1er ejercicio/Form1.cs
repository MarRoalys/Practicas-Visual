
using System;
using System.Windows.Forms;

namespace CalculadoraBasica
{
    public partial class Form1 : Form
    {
      
        double firstNum;
        
        double memoryValue = 0;
      
        string op;
      
        bool isOperatorUsed = false;
       
        bool newNumberReady = true;

        public Form1()
        {
            InitializeComponent();
        }

      
        private void btnNum_Click(object sender, EventArgs e)
        {
            if (newNumberReady)
            {
                txtDisplay.Text = "";
                newNumberReady = false;
            }

            Button btn = (Button)sender;
           
            if (btn.Text == "." && txtDisplay.Text.Contains("."))
            {
                return;
            }

            txtDisplay.Text = txtDisplay.Text + btn.Text;
            isOperatorUsed = false;
        }

        
        private void btnC_Click(object sender, EventArgs e)
        {
          
            txtDisplay.Text = "0";
            firstNum = 0;
            op = "";
            isOperatorUsed = false;
            newNumberReady = true;
        }

        
        private void btnOp_Click(object sender, EventArgs e)
        {
            if (!isOperatorUsed)
            {
                firstNum = double.Parse(txtDisplay.Text);
                isOperatorUsed = true;
            }

            Button btn = (Button)sender;
            op = btn.Text;
            newNumberReady = true;
        }

        
        private void btnEqual_Click(object sender, EventArgs e)
        {
            double secondNum = double.Parse(txtDisplay.Text);
            double result = 0;

            switch (op)
            {
                case "+":
                    result = firstNum + secondNum;
                    break;
                case "-":
                    result = firstNum - secondNum;
                    break;
                case "*":
                    result = firstNum * secondNum;
                    break;
                case "/":
                    if (secondNum == 0)
                    {
                        MessageBox.Show("No se puede dividir por cero", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    result = firstNum / secondNum;
                    break;
                case "^":
                    result = Math.Pow(firstNum, secondNum);
                    break;
                default:
                    break;
            }
            
            txtDisplay.Text = result.ToString();
            firstNum = result; 
            isOperatorUsed = false;
            newNumberReady = true;
        }

        
        private void btnSqrt_Click(object sender, EventArgs e)
        {
            double num = double.Parse(txtDisplay.Text);
            if (num < 0)
            {
                MessageBox.Show("No se puede calcular la raíz cuadrada de un número negativo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            txtDisplay.Text = Math.Sqrt(num).ToString();
            newNumberReady = true;
        }

        
        private void btnPi_Click(object sender, EventArgs e)
        {
            txtDisplay.Text = Math.PI.ToString();
            newNumberReady = true;
        }

        
        private void btnMPlus_Click(object sender, EventArgs e)
        {
            memoryValue += double.Parse(txtDisplay.Text);
            newNumberReady = true;
        }

        private void btnMMinus_Click(object sender, EventArgs e)
        {
            memoryValue -= double.Parse(txtDisplay.Text);
            newNumberReady = true;
        }

        private void btnMR_Click(object sender, EventArgs e)
        {
            txtDisplay.Text = memoryValue.ToString();
            newNumberReady = true;
        }

        private void btnMC_Click(object sender, EventArgs e)
        {
            memoryValue = 0;
        }

        
        private void btnCos_Click(object sender, EventArgs e)
        {
           
            double degrees = double.Parse(txtDisplay.Text);
            double radians = degrees * (Math.PI / 180);
            txtDisplay.Text = Math.Cos(radians).ToString();
            newNumberReady = true;
        }

        private void btnSin_Click(object sender, EventArgs e)
        {
            double degrees = double.Parse(txtDisplay.Text);
            double radians = degrees * (Math.PI / 180);
            txtDisplay.Text = Math.Sin(radians).ToString();
            newNumberReady = true;
        }

        private void btnTan_Click(object sender, EventArgs e)
        {
            double degrees = double.Parse(txtDisplay.Text);
            double radians = degrees * (Math.PI / 180);
            txtDisplay.Text = Math.Tan(radians).ToString();
            newNumberReady = true;
        }

       
        private void btnLog_Click(object sender, EventArgs e)
        {
            double num = double.Parse(txtDisplay.Text);
            if (num <= 0)
            {
                MessageBox.Show("El logaritmo solo se puede calcular para números positivos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            txtDisplay.Text = Math.Log(num).ToString();
            newNumberReady = true;
        }

        private void btnToFraction_Click(object sender, EventArgs e)
        {
            double num = double.Parse(txtDisplay.Text);
            string fraction = ConvertToFraction(num);
            txtDisplay.Text = fraction;
            newNumberReady = true;
        }

        
        private string ConvertToFraction(double num)
        {
         
            if (num == (int)num)
            {
                return num.ToString() + "/1";
            }

            int precision = 10000;
            int numerator = (int)(num * precision);
            int denominator = precision;

            int gcd = GetGCD(numerator, denominator);
            numerator /= gcd;
            denominator /= gcd;

            return numerator + "/" + denominator;
        }

       
        private int GetGCD(int a, int b)
        {
            while (b != 0)
            {
                int temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }
    }
}

