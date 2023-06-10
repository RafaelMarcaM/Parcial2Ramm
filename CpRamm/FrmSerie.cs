using CadRamm;
using ClnRamm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CpRamm
{
    public partial class FrmSerie : Form
    {
        bool esNuevo = false;
        public FrmSerie()
        {
            InitializeComponent();
        }

        private void FrmSerie_Load(object sender, EventArgs e)
        {
            Size = new Size(792, 386);
            listar();
        }

        private void listar()
        {
            var series = SerieCln.listarPa(txtParametro.Text.Trim());
            dgvLista.DataSource = series;
            dgvLista.Columns["id"].Visible = false;
            dgvLista.Columns["titulo"].HeaderText = "Título";
            dgvLista.Columns["sinopsis"].HeaderText = "Sinopsis";
            dgvLista.Columns["director"].HeaderText = "Director";
            dgvLista.Columns["duracion"].HeaderText = "Duración";
            dgvLista.Columns["fechaEstreno"].HeaderText = "Fecha de Estreno";
            btnEditar.Enabled = series.Count > 0;
            btnEliminar.Enabled = series.Count > 0;
            if (series.Count > 0) dgvLista.Rows[0].Cells["titulo"].Selected = true;
        }
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            esNuevo = true;
            Size = new Size(792, 550);
            txtTitulo.Focus();
            limpiar();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            esNuevo = false;
            Size = new Size(792, 550);

            int index = dgvLista.CurrentCell.RowIndex;
            int id = Convert.ToInt32(dgvLista.Rows[index].Cells["id"].Value);
            var serie = SerieCln.get(id);
            txtTitulo.Text = serie.titulo;
            txtSinopsis.Text = serie.sinopsis;
            txtDirector.Text = serie.director;
            txtDuracion.Text = Convert.ToString(serie.duracion);
            dtpFechaEstreno.Value = serie.fechaEstreno;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Size = new Size(792, 386);
            limpiar();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            listar();
        }

        private void txtParametro_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)Keys.Enter) listar();
        }
        private bool validar()
        {
            bool esValido = true;
            erpTitulo.SetError(txtTitulo, "");
            erpSinopsis.SetError(txtSinopsis, "");
            erpDirector.SetError(txtDirector, "");
            erpDuracion.SetError(txtDuracion, "");

            if (string.IsNullOrEmpty(txtTitulo.Text))
            {
                erpTitulo.SetError(txtTitulo, "El campo Titulo es obligatorio");
                esValido = false;
            }
            if (string.IsNullOrEmpty(txtSinopsis.Text))
            {
                erpSinopsis.SetError(txtSinopsis, "El campo Sinopsis es obligatorio");
                esValido = false;
            }
            if (string.IsNullOrEmpty(txtDirector.Text))
            {
                erpDirector.SetError(txtDirector, "El campo Director es obligatorio");
                esValido = false;
            }
            if (string.IsNullOrEmpty(txtDuracion.Text))
            {
                erpDuracion.SetError(txtDuracion, "El campo Saldo es obligatorio");
                esValido = false;
            }            
            return esValido;
        }
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (validar())
            {
                var serie = new Serie();
                serie.titulo = txtTitulo.Text.Trim();
                serie.sinopsis = txtSinopsis.Text.Trim();
                serie.director = txtDirector.Text;
                serie.duracion = Convert.ToInt32(txtDuracion.Text);
                serie.fechaEstreno = dtpFechaEstreno.Value;

                if (esNuevo)
                {
                    serie.registroActivo = true;
                    SerieCln.insertar(serie);
                }
                else
                {
                    int index = dgvLista.CurrentCell.RowIndex;
                    serie.id = Convert.ToInt32(dgvLista.Rows[index].Cells["id"].Value);
                    SerieCln.actualizar(serie);
                }
                listar();
                btnCancelar.PerformClick();
                MessageBox.Show("Serie guardado correctamente", "::: Parcial - Mensaje :::",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void limpiar()
        {
            txtTitulo.Text = string.Empty;
            txtSinopsis.Text = string.Empty;
            txtDirector.Text = string.Empty;
            txtDuracion.Text = string.Empty;
            dtpFechaEstreno.Text = string.Empty;
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            int index = dgvLista.CurrentCell.RowIndex;
            int id = Convert.ToInt32(dgvLista.Rows[index].Cells["id"].Value);
            string titulo = dgvLista.Rows[index].Cells["titulo"].Value.ToString();
            DialogResult dialog = MessageBox.Show($"¿Está seguro que desea dar de baja " +
                $"el producto {titulo}?", "::: Parcial - Mensaje :::",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dialog == DialogResult.OK)
            {
                SerieCln.eliminar(id);
                listar();
                MessageBox.Show($"Producto dado de baja correctamente", "::: Parcial - Mensaje :::",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
