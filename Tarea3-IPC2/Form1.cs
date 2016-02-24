using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Tarea3_IPC2
{
    public partial class Form1 : Form
    {
        private Conexion conexion = new Conexion();
        public Form1()
        {
            InitializeComponent();
            conectar_database();
            conexion.llenarcombo_matricula(combobxMatricula);
            conexion.llenarcombo_matricula(combobxMatricula_Eliminar);
        }

        private void conectar_database()
        {
            if (conexion.testear_conexion())
            {
                MessageBox.Show("Conexion Exitosa");
            }
            else
            {
                MessageBox.Show("Conexion Incorrecta");
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMatricula.Text))
            {
                errorProvider1.SetError(txtMatricula, "Debe ingresar matricula");
                txtMatricula.Focus();
                return;
            }
            errorProvider1.SetError(txtMatricula, "");
            if(string.IsNullOrEmpty(txtMarca.Text)){
                errorProvider1.SetError(txtMarca, "Debe ingresar una marca");
                txtMarca.Focus();
                return;
            }
            errorProvider1.SetError(txtMarca, "");
            if (string.IsNullOrEmpty(txtModelo.Text))
            {
                errorProvider1.SetError(txtModelo, "Debe ingresar un modelo");
                txtModelo.Focus();
                return;
            }
            errorProvider1.SetError(txtModelo, "");
            if (string.IsNullOrEmpty(combobxColor.Text))
            {
                errorProvider1.SetError(combobxColor, "Debe seleccionar un color");
                combobxColor.Focus();
                return;
            }
            errorProvider1.SetError(combobxColor, "");
            if (string.IsNullOrEmpty(txtPrecio.Text))
            {
                errorProvider1.SetError(txtPrecio, "Debe colocar un precio");
                txtPrecio.Focus();
                return;
            }
            else
            {
                errorProvider1.SetError(txtPrecio, "");
                string matricula = txtMatricula.Text;
                string marca = txtMarca.Text;
                string modelo = txtModelo.Text;
                string color = combobxColor.Text;
                float precio = System.Convert.ToSingle(txtPrecio.Text);
                string descripcion = richtxtDescripcion.Text;
                if (conexion.insertar_vehiculo(matricula, marca, modelo, color, precio, descripcion))
                {
                    MessageBox.Show("Registro Exitoso");
                }
                else
                {
                    MessageBox.Show("Error En Registro");
                }
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            string matricula = combobxMatricula.Text;
            string campo = combobxCampo.Text;
            string nuevo_valor = txtNuevo.Text;

            if (conexion.actualizar_vehiculo(matricula, campo, nuevo_valor))
            {
                MessageBox.Show("Actualizacion Exitosa");
            }
            else
            {
                MessageBox.Show("Actualizacion Erronea");
            }
        }

        private void btnAgregar_Cliente_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNIT.Text))
            {
                errorProvider2.SetError(txtNIT, "Debe ingresar un NIT");
                txtNIT.Focus();
                return;
            }
            errorProvider2.SetError(txtNIT, "");

            if (string.IsNullOrEmpty(txtNombre_Cliente.Text))
            {
                errorProvider2.SetError(txtNombre_Cliente, "Nombre obligatorio");
                txtNombre_Cliente.Focus();
                return;
            }
            errorProvider2.SetError(txtNombre_Cliente, "");

            if (string.IsNullOrEmpty(txtApellido.Text))
            {
                errorProvider2.SetError(txtApellido, "Apellido obligatorio");
                txtApellido.Focus();
                return;
            }
            errorProvider2.SetError(txtApellido, "");

            if (string.IsNullOrEmpty(txtDireccion.Text))
            {
                errorProvider2.SetError(txtDireccion, "Debe ingresar una Direccion");
                txtDireccion.Focus();
                return;
            }
            else
            {
                errorProvider2.SetError(txtDireccion, "");
                string nit = txtNIT.Text;
                string nombre = txtNombre_Cliente.Text;
                string apellido = txtApellido.Text;
                string direccion = txtDireccion.Text;
                string telefono = txtTelefono.Text;
                if (conexion.Agregar_Cliente(nit, nombre, apellido, direccion, telefono))
                {
                    MessageBox.Show("Cliente Agregado");
                }
                else
                {
                    MessageBox.Show("Error al agregar");
                }
            }
            


        }

        
        private void combobxMatricula_Eliminar_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            SqlConnection cnn = new SqlConnection("Data Source=Edu-PC;Initial Catalog=venta_vehiculos;Integrated Security=True");
            string script = "SELECT marca, modelo, precio FROM vehiculo WHERE matricula = '" + combobxMatricula_Eliminar.Text + "'";
            SqlCommand cm = new SqlCommand(script, cnn);
            cnn.Open();
            SqlDataReader dr = cm.ExecuteReader();
            if (dr.Read() == true)
            {
                lblmarca.Text = "Marca: "+dr["marca"].ToString();
                lblmodelo.Text = "Modelo: "+dr["modelo"].ToString();
                lblprecio.Text = "Precio: "+dr["precio"].ToString();
            }
            else
            {
                MessageBox.Show("ksd");
            }
            cnn.Close();
        }

    }
}
