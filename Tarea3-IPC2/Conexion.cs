using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Tarea3_IPC2
{
    class Conexion
    {
        private string cadena_conexion;
        SqlDataReader dr;

        public Conexion()
        {
            cadena_conexion = "Data Source=Edu-PC;Initial Catalog=venta_vehiculos;Integrated Security=True";
        }

        public bool testear_conexion()
        {
            SqlConnection conn = get_conexion();
            if (conn != null)
            {
                conn.Close();
                return true;
            }
            else
            {
                return false;
            }
        }

        private SqlConnection get_conexion()
        {
            try
            {
                SqlConnection conn = new SqlConnection(cadena_conexion);
                conn.Open();
                return conn;
            }
            catch (Exception exc)
            {
                return null;
            }
        }

        public bool insertar_vehiculo(string matricula, string marca, string modelo, string color, float precio, string descripcion)
        {
            SqlConnection conn;
            try
            {
                using (conn = get_conexion())
                {
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = conn;
                        command.CommandType = System.Data.CommandType.Text;
                        command.CommandText = "INSERT INTO vehiculo"
                            + "(matricula, marca, modelo, color, precio, descripcion)"
                            + "values (@matricula, @marca, @modelo, @color, @precio, @descripcion)";

                        command.Parameters.Add("@matricula", System.Data.SqlDbType.VarChar).Value = matricula;
                        command.Parameters.Add("@marca", System.Data.SqlDbType.VarChar).Value = marca;
                        command.Parameters.Add("@modelo", System.Data.SqlDbType.VarChar).Value = modelo;
                        command.Parameters.Add("@color", System.Data.SqlDbType.VarChar).Value = color;
                        command.Parameters.Add("@precio", System.Data.SqlDbType.Float).Value = precio;
                        command.Parameters.Add("@descripcion", System.Data.SqlDbType.VarChar).Value = descripcion;

                        return (command.ExecuteNonQuery() == 1);

                    }
                }
            }
            catch (Exception exc)
            {
                return false;
            }
        }

        public bool actualizar_vehiculo(string matricula, string campo, string valor)
        {
            SqlConnection conn;
            try
            {
                using (conn = get_conexion())
                {
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = conn;
                        command.CommandType = System.Data.CommandType.Text;
                        command.CommandText = "UPDATE vehiculo"
                            + " SET " + campo + "=@nuevo_valor"
                            + " WHERE matricula=@matricula";

                        command.Parameters.Add("@matricula", System.Data.SqlDbType.VarChar).Value = matricula;

                        if (campo.CompareTo("precio") == 0)
                        {
                            float nuevo_precio = System.Convert.ToSingle(valor);
                            command.Parameters.Add("@nuevo_valor", System.Data.SqlDbType.Float).Value = nuevo_precio;
                        }
                        else
                        {
                            command.Parameters.Add("@nuevo_valor", System.Data.SqlDbType.VarChar).Value = valor;
                        }

                        return (command.ExecuteNonQuery() == 1);

                    }
                }
            }
            catch (Exception exc)
            {
                return false;
            }
        }

        public bool Agregar_Cliente(string nit, string nombre, string apellido, string direccion, string telefono)
        {
            SqlConnection conn;
            try
            {
                using (conn = get_conexion())
                {
                    using (SqlCommand command = new SqlCommand())
                    {
                        if (string.IsNullOrEmpty(telefono))
                        {
                            command.Connection = conn;
                            command.CommandType = System.Data.CommandType.Text;
                            command.CommandText = "INSERT INTO cliente"
                                + "(nit, nombre, apellido, direccion)"
                                + " VALUES (@nit, @nombre, @apellido, @direccion)";

                            command.Parameters.Add("@nit", System.Data.SqlDbType.VarChar).Value = nit;
                            command.Parameters.Add("@nombre", System.Data.SqlDbType.VarChar).Value = nombre;
                            command.Parameters.Add("@apellido", System.Data.SqlDbType.VarChar).Value = apellido;
                            command.Parameters.Add("@direccion", System.Data.SqlDbType.VarChar).Value = direccion;

                            return (command.ExecuteNonQuery() == 1);
                        }
                        else
                        {
                            command.Connection = conn;
                            command.CommandType = System.Data.CommandType.Text;
                            command.CommandText = "INSERT INTO cliente"
                                + "(nit, nombre, apellido, direccion, telefono)"
                                + " VALUES (@nit, @nombre, @apellido, @direccion, @telefono)";

                            command.Parameters.Add("@nit", System.Data.SqlDbType.VarChar).Value = nit;
                            command.Parameters.Add("@nombre", System.Data.SqlDbType.VarChar).Value = nombre;
                            command.Parameters.Add("@apellido", System.Data.SqlDbType.VarChar).Value = apellido;
                            command.Parameters.Add("@direccion", System.Data.SqlDbType.VarChar).Value = direccion;
                            command.Parameters.Add("@telefono", System.Data.SqlDbType.VarChar).Value = telefono;

                            return (command.ExecuteNonQuery() == 1);
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                return false;
            }
        }

        public void llenarcombo_matricula(ComboBox cb)
        {
            SqlConnection conn;
            try
            {
                using (conn = get_conexion())
                {
                    using (SqlCommand com = new SqlCommand())
                    {
                        com.Connection = conn;
                        com.CommandType = System.Data.CommandType.Text;
                        com.CommandText = "SELECT matricula FROM vehiculo";
                        dr = com.ExecuteReader();
                        while (dr.Read())
                        {
                            cb.Items.Add(dr["matricula"].ToString());
                        }
                        dr.Close();
                    }
                }
            }catch(Exception ex){
                MessageBox.Show("no se lleno el combo " + ex.ToString());
            }
        }

        public void llenarcombo_cliente(ComboBox cb)
        {
            SqlConnection conn;
            try
            {
                using (conn = get_conexion())
                {
                    using (SqlCommand com = new SqlCommand())
                    {
                        com.Connection = conn;
                        com.CommandType = System.Data.CommandType.Text;
                        com.CommandText = "SELECT cod_cliente FROM cliente";
                        dr = com.ExecuteReader();
                        while (dr.Read())
                        {
                            cb.Items.Add(dr["cod_cliente"].ToString());
                        }
                        dr.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("no se lleno el combo " + ex.ToString());
            }
        }

        public bool eliminar_registro(string tabla, string condicion)
        {
            SqlConnection conn;
            
                using (conn = get_conexion())
                {
                    using (SqlCommand com = new SqlCommand())
                    {
                        com.Connection = conn;
                        com.CommandType = System.Data.CommandType.Text;
                        com.CommandText = "DELETE FROM " + tabla + " WHERE " + condicion;
                        int i = com.ExecuteNonQuery();
                        conn.Close();
                        if (i > 0)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                        
                    }
                }
            
        }

        public bool actualizar_cliente(string codigo, string campo, string nuevo)
        {
            SqlConnection conn;
            try
            {
                using (conn = get_conexion())
                {
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = conn;
                        command.CommandType = System.Data.CommandType.Text;
                        command.CommandText = "UPDATE cliente"
                            + " SET " + campo + "=@nuevo_valor"
                            + " WHERE cod_cliente=@codigo";

                        command.Parameters.Add("@codigo", System.Data.SqlDbType.VarChar).Value = codigo;
                        command.Parameters.Add("@nuevo_valor", System.Data.SqlDbType.VarChar).Value = nuevo;
                        

                        return (command.ExecuteNonQuery() == 1);

                    }
                }
            }
            catch (Exception exc)
            {
                return false;
            }
        }
    }
}
