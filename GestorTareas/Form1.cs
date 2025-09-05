using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GestorTareas
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            dgvTareas.CellClick += new DataGridViewCellEventHandler(dgvTareas_CellClick);
        }

        public class Tarea
        {
            public string Codigo { get; set; }
            public string Nombre { get; set; }
            public string Descripcion { get; set; }
            public DateTime Fecha { get; set; }
            public string Lugar { get; set; }
            public string Estado { get; set; }
        }

        List<Tarea> listaTareas = new List<Tarea>();

        private void ActualizarGrid(List<Tarea> lista)
        {
            dgvTareas.DataSource = null; 
            dgvTareas.DataSource = lista; 
        }

        private void CargarListaCompleta()
        {
            ActualizarGrid(listaTareas);
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCodigo.Text) || string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("El código y el nombre son campos obligatorios.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;

            }

            Tarea nueva = new Tarea()
            {
                Codigo = txtCodigo.Text,
                Nombre = txtNombre.Text,
                Descripcion = txtDescripcion.Text,
                Fecha = dtpFecha.Value,
                Lugar = txtLugar.Text,
                Estado = cmbEstado.Text 
            };

            listaTareas.Add(nueva);
            ActualizarGrid(listaTareas);
            MessageBox.Show("Tarea agregada correctamente.");
            txtCodigo.Text = "";
            txtNombre.Text = "";
            txtDescripcion.Text = "";
            
            txtLugar.Text = "";
            cmbEstado.Text = "";
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvTareas.SelectedRows.Count > 0)
            {
                string codigoSeleccionado = dgvTareas.SelectedRows[0].Cells["Codigo"].Value.ToString();
                Tarea tareaAEditar = listaTareas.FirstOrDefault(t => t.Codigo == codigoSeleccionado);

                if (tareaAEditar != null)
                {
                    tareaAEditar.Codigo = txtCodigo.Text;
                    tareaAEditar.Nombre = txtNombre.Text;
                    tareaAEditar.Descripcion = txtDescripcion.Text;
                    tareaAEditar.Fecha = dtpFecha.Value;
                    tareaAEditar.Lugar = txtLugar.Text;
                    tareaAEditar.Estado = cmbEstado.Text;

                    ActualizarGrid(listaTareas);
                    MessageBox.Show("Tarea editada correctamente.");
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecciona una fila para editar.");
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvTareas.SelectedRows.Count > 0)
            {
                string codigoSeleccionado = dgvTareas.SelectedRows[0].Cells["Codigo"].Value.ToString();
                Tarea tareaAEliminar = listaTareas.FirstOrDefault(t => t.Codigo == codigoSeleccionado);

                if (tareaAEliminar != null)
                {
                    listaTareas.Remove(tareaAEliminar);
                    ActualizarGrid(listaTareas);
                    MessageBox.Show("Tarea eliminada correctamente.");
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecciona una fila para eliminar.");
            }
        }

        private void dgvTareas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvTareas.Rows[e.RowIndex];
                txtCodigo.Text = row.Cells["Codigo"].Value?.ToString() ?? "";
                txtNombre.Text = row.Cells["Nombre"].Value?.ToString() ?? "";
                txtDescripcion.Text = row.Cells["Descripcion"].Value?.ToString() ?? "";
                dtpFecha.Value = (DateTime)row.Cells["Fecha"].Value;
                txtLugar.Text = row.Cells["Lugar"].Value?.ToString() ?? "";
                cmbEstado.Text = row.Cells["Estado"].Value?.ToString() ?? "";
            }
        }

        private void btnBuscarCodigo_Click(object sender, EventArgs e)
        {
            string codigoABuscar = txtBuscarCodigo.Text.Trim();
            if (string.IsNullOrWhiteSpace(codigoABuscar))
            {
                MessageBox.Show("Ingresa un código para buscar.");
                CargarListaCompleta();
                return;
            }

            var resultados = listaTareas.Where(t => t.Codigo.Equals(codigoABuscar, StringComparison.OrdinalIgnoreCase)).ToList();

            if (resultados.Any())
            {
                ActualizarGrid(resultados);
                MessageBox.Show($"Se encontró {resultados.Count} tarea(s) con el código '{codigoABuscar}'.");
            }
            else
            {
                ActualizarGrid(new List<Tarea>());
                MessageBox.Show($"No se encontraron tareas con el código '{codigoABuscar}'.");
            }
        }
        private void btnBuscarEstado_Click(object sender, EventArgs e)
        {
            if (cmbBuscarEstado.SelectedItem == null)
            {
                MessageBox.Show("Selecciona un estado para buscar.");
                CargarListaCompleta();
                return;
            }

            string estadoABuscar = cmbBuscarEstado.SelectedItem.ToString();
            var resultados = listaTareas.Where(t => t.Estado.Equals(estadoABuscar, StringComparison.OrdinalIgnoreCase)).ToList();

            if (resultados.Any())
            {
                ActualizarGrid(resultados);
                MessageBox.Show($"Se encontraron {resultados.Count} tareas en estado '{estadoABuscar}'.");
            }
            else
            {
                ActualizarGrid(new List<Tarea>());
                MessageBox.Show($"No se encontraron tareas en estado '{estadoABuscar}'.");
            }
        }

        
        private void btnBuscarFecha_Click(object sender, EventArgs e)
        {
            DateTime fechaInicio = dtpFechaInicio.Value.Date;
            DateTime fechaFin = dtpFechaFin.Value.Date;

            if (fechaInicio > fechaFin)
            {
                MessageBox.Show("La fecha de inicio no puede ser posterior a la fecha de fin.");
                CargarListaCompleta();
                return;
            }

            var resultados = listaTareas.Where(t => t.Fecha.Date >= fechaInicio && t.Fecha.Date <= fechaFin).ToList();

            if (resultados.Any())
            {
                ActualizarGrid(resultados);
                MessageBox.Show($"Se encontraron {resultados.Count} tareas en el rango de fechas seleccionado.");
            }
            else
            {
                ActualizarGrid(new List<Tarea>());
                MessageBox.Show($"No se encontraron tareas en el rango de fechas seleccionado.");
            }
        }
    }
}