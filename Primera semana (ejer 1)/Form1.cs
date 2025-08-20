using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HolaMundo
{
    public partial class Form1 : Form
    {
        int intentos = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void butIniciar_Click(object sender, EventArgs e)
        {
            string usuario, clave;
            usuario = ctUsuario.Text;
            clave = ctClave.Text;

            if(usuario =="" || clave== "")
            {
                MessageBox.Show("Complete todos los campos.", "Error de inicio de sesion");
            }
            if (usuario == "admin" && clave == "admin123")
            {
                MessageBox.Show("Bienvenido al sistema "  + usuario);
                ctUsuario.Clear();
                ctClave.Clear();

            }
            else
            {
                MessageBox.Show("Usuario o clave incorrectos,numero de intentos restantes: " + (3-intentos),"Error de inicio de sesion", MessageBoxButtons.OK,MessageBoxIcon.Error);
                intentos= intentos+1;
                if (intentos>3)
                {
                    MessageBox.Show("Has superado el numero de intentos", "Error de inicio de sesion", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Close();
                }
            }
        }
    }
}