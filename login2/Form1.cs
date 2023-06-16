using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static login2.form_login;

namespace login2
{
    public partial class form_login : Form
    {
        public form_login()
        {
            InitializeComponent();
            txt_clave.KeyDown += txt_clave_KeyDown;
            CargarUsuarios();
            CargarProductos();
        }

        public class Usuarios
        {
            private static int contador_usuarios = 1;
            public int ID { get; }
            public string Usuario { get; set; }
            public string Nombre { get; set; }
            public string Apellido { get; set; }
            public string Clave { get; set; }
            public string Tipo_usuario { get; set; }

            public Usuarios(string usuario, string nombre, string apellido, string clave, string tipo_usuario)
            {
                ID = contador_usuarios++;
                Usuario = usuario;
                Nombre = nombre;
                Apellido = apellido;
                Clave = clave;
                Tipo_usuario = tipo_usuario;
            }
        }


        List<Usuarios> listaUsuarios = new List<Usuarios>();

        private void CargarUsuarios()
        {
            // Crear instancias de usuarios admin, vendedor, repositor y supervisor
            Usuarios admin = new Usuarios("admin", "Admin", "Admin", "admin", "admin");
            Usuarios vendedor = new Usuarios("vendedor", "Juan", "Pérez", "vendedor", "vendedor");
            Usuarios repositor = new Usuarios("repositor", "María", "Gómez", "repositor", "repositor");
            Usuarios supervisor = new Usuarios("supervisor", "Pedro", "López", "supervisor", "supervisor");

            // Agregar los usuarios a la lista
            listaUsuarios.Add(admin);
            listaUsuarios.Add(vendedor);
            listaUsuarios.Add(repositor);
            listaUsuarios.Add(supervisor);


            combobox_usuarios.Items.Clear();
            // Iterar sobre la lista de usuarios y agregar los tipos a la ComboBox
            foreach (Usuarios usuario in listaUsuarios)
            {
                combobox_usuarios.Items.Add(usuario.Tipo_usuario);
            }
        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public class Stock
        {
            private static int contador_productos = 1;
            public int ID_p { get; }
            public string Nombre_p { get; set; }
            public string Descripcion_p { get; set; }
            public double Precio_p { get; set; }
            public int Stock_p { get; set; }

            public Stock(string nombre_p, string descripcion_p, double precio_p, int stock_p)
            {
                ID_p = contador_productos++;
                Nombre_p = nombre_p;
                Descripcion_p = descripcion_p;
                Precio_p = precio_p;
                Stock_p = stock_p;
            }
        }

        // Crear una lista de productos
        List<Stock> listaProductos = new List<Stock>();

        private void CargarProductos()
        {
            // Crear instancias de productos
            Stock producto1 = new Stock("Producto 1", "Descripción 1", 11.99, 1);
            Stock producto2 = new Stock("Producto 2", "Descripción 2", 22.99, 2);
            Stock producto3 = new Stock("Producto 3", "Descripción 3", 33.99, 3);
            Stock producto4 = new Stock("Producto 4", "Descripción 4", 44.99, 4);

            // Agregar los stock a la lista
            listaProductos.Add(producto1);
            listaProductos.Add(producto2);
            listaProductos.Add(producto3);
            listaProductos.Add(producto4);

        }




        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



        private void txt_clave_TextChanged(object sender, EventArgs e)
        {
            if (txt_clave.Text.Length > 0)
            {
                ojo_invisible.BringToFront();
                ojo_visible.Visible = true;
                ojo_invisible.Visible = true;
            }
            else
            {
                txt_clave.PasswordChar = '*';
                ojo_visible.Visible = false;
                ojo_invisible.Visible = false;
            }
        }

        private void btn_ingresar_Click(object sender, EventArgs e)
        {
            if (txt_usuario.Text.Length > 0 && txt_clave.Text.Length > 0)
            {
                string usuario = txt_usuario.Text;
                string clave = txt_clave.Text;
                string tipo_usuario = "";

                // Verificar si el usuario y la clave son correctos
                bool inicio_sesion = false;
                foreach (Usuarios u in listaUsuarios)
                {
                    if (u.Usuario == usuario && u.Clave == clave)
                    {
                        inicio_sesion = true;
                        tipo_usuario = u.Tipo_usuario;
                        break;
                    }
                }

                if (inicio_sesion)
                {
                    switch (tipo_usuario)
                    {
                        case "vendedor":
                            grupo_base.Text = "vendedor";
                            btn_cargar.Visible = false;
                            btn_vender.Visible = true;
                            btn_cerrar_caja.Visible = true;
                            btn_config.Visible = false;
                            btn_supervisor.Visible = false;
                            break;
                        case "repositor":
                            grupo_base.Text = "repositor";
                            btn_cargar.Visible = true;
                            btn_vender.Visible = false;
                            btn_cerrar_caja.Visible = true;
                            btn_config.Visible = false;
                            btn_supervisor.Visible = false;
                            break;
                        case "supervisor":
                            grupo_base.Text = "supervisor";
                            btn_cargar.Visible = false;
                            btn_vender.Visible = false;
                            btn_cerrar_caja.Visible = false;
                            btn_config.Visible = false;
                            btn_supervisor.Visible = true;
                            break;
                        case "admin":
                            grupo_base.Text = "admin";
                            btn_cargar.Visible = true;
                            btn_vender.Visible = true;
                            btn_cerrar_caja.Visible = true;
                            btn_config.Visible = true;
                            btn_supervisor.Visible = true;
                            break;
                    }
                    grupo_base.Visible = true;
                    grupo_menu_usuario.Visible = true;
                    grupo_login.Visible = false;
                    btn_cerrar_sesion.Visible = true;
                    LimpiarCampos();
                    txt_usuario.Focus();
                }
                else
                {
                    MessageBox.Show("Credenciales incorrectas");
                    LimpiarCampos();
                    txt_usuario.Focus();
                    ojo_invisible.BringToFront();
                    ojo_visible.Visible = false;
                    ojo_invisible.Visible = false;
                    txt_clave.PasswordChar = '*';
                }
            }
            else
            {
                MessageBox.Show("Complete todos los campos");
            }
        }

        private void LimpiarCampos()
        {
            txt_usuario.Clear();
            txt_clave.Clear();
            txt_user.Clear();
            txt_name.Clear();
            txt_apellido.Clear();
            txt_pass.Clear();
            txt_pass2.Clear();
            combobox_usuarios.Text = null;

        }

        private void ojo_visible_Click(object sender, EventArgs e)
        {
            ojo_invisible.BringToFront();
            txt_clave.PasswordChar = '*';
        }

        private void ojo_invisible_Click(object sender, EventArgs e)
        {
            ojo_visible.BringToFront();
            txt_clave.PasswordChar = '\0';
        }

        private void btn_config_Click(object sender, EventArgs e)
        {
            grupo_menu_configuracion.Visible = true;
        }
        private void btn_supervisor_Click(object sender, EventArgs e)
        {
        }

        private void btn_atras_Click(object sender, EventArgs e)
        {
            grupo_listaUsuarios.Visible = false;
            grupo_nuevo_usuario.Visible = false;
            grupo_menu_configuracion.Visible = false;
            LimpiarCampos();
        }

        private void btn_cerrar_sesion_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("¿Estas seguro?", "CERRANDO SESION", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                grupo_base.Visible = false;
                grupo_login.Visible = true;
                grupo_base.Text = "";
                grupo_menu_usuario.Visible = false;
                grupo_menu_configuracion.Visible = false;
                grupo_nuevo_usuario.Visible = false;
                btn_cerrar_sesion.Visible = false;
                LimpiarCampos();
            }

        }

        private void btn_nuevo_usuario_Click(object sender, EventArgs e)
        {
            grupo_nuevo_usuario.Visible = true;
        }

        private void btn_guardar_Click(object sender, EventArgs e)
        {
            if (combobox_usuarios.SelectedItem == null || txt_user.Text == "" || txt_name.Text == "" || txt_apellido.Text == "" || txt_pass.Text == "" || txt_pass2.Text == "")
            {
                MessageBox.Show("Complete todos los campos");
            }
            else
            {
                if (txt_pass.Text == txt_pass2.Text)
                {
                    if (grupo_nuevo_usuario.Visible == true)
                    {
                        // Obtener el usuario seleccionado en el ListBox
                        Usuarios usuarioSeleccionado = listaUsuarios[listBox_usuarios.SelectedIndex];

                        // Actualizar los datos del usuario con los valores de los controles de texto y combobox
                        usuarioSeleccionado.Usuario = txt_user.Text;
                        usuarioSeleccionado.Nombre = txt_name.Text;
                        usuarioSeleccionado.Apellido = txt_apellido.Text;
                        usuarioSeleccionado.Clave = txt_pass.Text;
                        usuarioSeleccionado.Tipo_usuario = combobox_usuarios.Text;

                        // Actualizar el ListBox con los nuevos valores del usuario
                        listBox_usuarios.Items[listBox_usuarios.SelectedIndex] = ObtenerTextoUsuario(usuarioSeleccionado);

                        // Limpiar los controles de texto y combobox, y ocultar el grupo grupo_edit_usuario
                        LimpiarCampos();
                        btn_lista_usuarios_Click(null, EventArgs.Empty);
                        grupo_nuevo_usuario.Visible = false;
                    }
                    else
                    {
                        Usuarios userList = new Usuarios(txt_user.Text, txt_name.Text, txt_apellido.Text, txt_pass.Text, combobox_usuarios.Text);
                        listaUsuarios.Add(userList);
                        LimpiarCampos();
                    }

                }
                else
                {
                    MessageBox.Show("Las contraseñas no coinciden");
                    txt_pass.Focus();
                }
            }
        }


        private void form_login_Load(object sender, EventArgs e)
        {
            txt_usuario.Select();
            grupo_base.Visible = false;



        }

        private void btn_lista_usuarios_Click(object sender, EventArgs e)
        {
            // Limpiar el ListBox antes de agregar los elementos
            grupo_listaUsuarios.Visible = true;
            listBox_usuarios.Items.Clear();

            // Recorrer la lista de usuarios
            foreach (Usuarios usuario in listaUsuarios)
            {
                // Crear una cadena con las propiedades del usuario
                string usuarioInfo = $"ID: {usuario.ID}, Usuario: {usuario.Usuario}, Nombre: {usuario.Nombre}, Apellido: {usuario.Apellido}, Clave: {usuario.Clave}, Tipo de usuario: {usuario.Tipo_usuario}";

                // Agregar la cadena al ListBox
                listBox_usuarios.Items.Add(usuarioInfo);
            }

        }

        private void btn_editar_Click(object sender, EventArgs e)
        {
            // Verificar si se ha seleccionado un usuario en el ListBox
            if (listBox_usuarios.Text != null)
            {
                // Obtener el usuario seleccionado en el ListBox
                Usuarios usuarioSeleccionado = listaUsuarios[listBox_usuarios.SelectedIndex];

                // Llevar los datos del usuario al grupo grupo_edit_usuario
                txt_user.Text = usuarioSeleccionado.Usuario;
                txt_name.Text = usuarioSeleccionado.Nombre;
                txt_apellido.Text = usuarioSeleccionado.Apellido;
                txt_pass.Text = usuarioSeleccionado.Clave;
                txt_pass2.Text = usuarioSeleccionado.Clave;
                combobox_usuarios.Text = usuarioSeleccionado.Tipo_usuario;

                // Mostrar el grupo grupo_edit_usuario
                grupo_nuevo_usuario.Visible = true;
            }
            else
            {
                MessageBox.Show("Seleccione un usuario para editar.");
            }
        }

        private void btn_eliminar_Click(object sender, EventArgs e)
        {
            // Verificar si se ha seleccionado un usuario en el ListBox
            if (listBox_usuarios.SelectedIndex != -1)
            {
                // Obtener el usuario seleccionado en el ListBox
                Usuarios usuarioSeleccionado = listaUsuarios[listBox_usuarios.SelectedIndex];

                // Mostrar un mensaje de confirmación antes de eliminar el usuario
                DialogResult result = MessageBox.Show("¿Está seguro de eliminar el usuario?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // Eliminar el usuario de la lista
                    listaUsuarios.Remove(usuarioSeleccionado);

                    // Actualizar el ListBox
                    listBox_usuarios.Items.RemoveAt(listBox_usuarios.SelectedIndex);

                    // Limpiar los controles de texto y ocultar el grupo grupo_edit_usuario
                    LimpiarCampos();
                    grupo_nuevo_usuario.Visible = false;
                }
            }
            else
            {
                MessageBox.Show("Seleccione un usuario para eliminar.");
            }
        }


        private string ObtenerTextoUsuario(Usuarios usuario)
        {
            return $"{usuario.ID} - {usuario.Usuario} ({usuario.Nombre} {usuario.Apellido})";
        }

        private void btn_cerrar_grupo_listaUsuarios_Click(object sender, EventArgs e)
        {
            grupo_listaUsuarios.Visible = false;
            LimpiarCampos();
        }

        private void btn_cargar_Click(object sender, EventArgs e)
        {
            // Limpiar el ListBox antes de agregar los elementos
            grupo_stock.Visible = true;
            actualizar_lista_stock();
        }

        private void actualizar_lista_stock()
        {
            listBox_stock.Items.Clear();

            foreach (Stock producto in listaProductos)
            {
                // Crear una cadena con las propiedades del producto
                string productoInfo = $"ID: {producto.ID_p}, Nombre: {producto.Nombre_p}, Descripción: {producto.Descripcion_p}, Precio: {producto.Precio_p}, Stock: {producto.Stock_p}";

                // Agregar la cadena al ListBox
                listBox_stock.Items.Add(productoInfo);
            }
        }



        private void btn_nuevo_p_Click(object sender, EventArgs e)
        {
            grupo_nuevo_p.Visible = true;
            txt_stock_p.Enabled = true;
        }

        //detectar enter en login
        private void txt_clave_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btn_ingresar.PerformClick();
            }
        }

        private void btn_x_nuevo_p_Click(object sender, EventArgs e)
        {
            grupo_nuevo_p_Clear();
        }

        private void grupo_nuevo_p_Clear()
        {
            grupo_nuevo_p.Visible = false;
            txt_nombre_p.Clear();
            txt_descripcion_p.Clear();
            txt_precio_p.Clear();
            txt_stock_p.Clear();
            txt_stock_p.Enabled = false;


        }

        private void btn_cerrar_grupo_stock_Click(object sender, EventArgs e)
        {
            grupo_stock.Visible = false;
            grupo_nuevo_p.Visible = false;
        }

        private void btn_guardar_p_Click(object sender, EventArgs e)
        {
            // Obtener los valores de los TextBox
            string nombre = txt_nombre_p.Text;
            string descripcion = txt_descripcion_p.Text;

            double precio;
            if (!double.TryParse(txt_precio_p.Text, out precio))
            {
                MessageBox.Show("El precio debe ser un valor numérico válido.");
                return;
            }

            int cant_stock;
            if (!int.TryParse(txt_stock_p.Text, out cant_stock))
            {
                MessageBox.Show("La cantidad de stock debe ser un valor numérico válido.");
                return;
            }

            // Crear una nueva instancia de Stock
            Stock nuevoStock = new Stock(nombre, descripcion, precio, cant_stock);

            // Agregar el nuevoStock a la lista listaProductos
            listaProductos.Add(nuevoStock);
            actualizar_lista_stock();
            grupo_nuevo_p_Clear();
        }


        private void btn_reponer_p_Click(object sender, EventArgs e)
        {
            if (listBox_stock.SelectedItem != null)
            {
                // Obtener el producto seleccionado en el ListBox
                Stock productoSeleccionado = listaProductos[listBox_stock.SelectedIndex];

                // Mostrar el nombre del producto en el TextBox
                txt_nombre_reponer_p.Text = productoSeleccionado.Nombre_p;
                grupo_reponer_p.Visible = true;
            }
            else
            {
                MessageBox.Show("Seleccione un producto para reponer.");
            }
        }

        private void btn_guardar_reponer_p_Click(object sender, EventArgs e)
        {
            // Verificar si se ha seleccionado un producto en el ListBox
            if (listBox_stock.SelectedIndex != -1)
            {
                // Obtener el producto seleccionado en el ListBox
                Stock productoSeleccionado = listaProductos[listBox_stock.SelectedIndex];

                // Obtener la cantidad a reponer ingresada en el TextBox txt_reponer_p
                int cantidadAReponer = Convert.ToInt32(txt_reponer_p.Text);

                // Incrementar el stock del producto seleccionado
                productoSeleccionado.Stock_p += cantidadAReponer;

                // Guardar la cantidad reponida en una base de datos o en una estructura de datos

                // Actualizar la lista del ListBox
                actualizar_lista_stock();                               
                cerrar_grupo_reponer_p();
                // Mostrar un mensaje de éxito
                MessageBox.Show("Cantidad reponida guardada correctamente.");
            }
            else
            {
                MessageBox.Show("Seleccione un producto para reponer stock.");
            }
        }

        private void btn_x_reponer_p_Click(object sender, EventArgs e)
        {
            cerrar_grupo_reponer_p();
        }

        private void cerrar_grupo_reponer_p()
        {
            txt_reponer_p.Clear();
            txt_nombre_reponer_p.Clear();
            grupo_reponer_p.Visible = false;            
        }

        private void btn_eliminar_p_Click(object sender, EventArgs e)
        {
            // Verificar si se ha seleccionado un producto en el listBox_stock
            if (listBox_stock.SelectedIndex != -1)
            {
                // Obtener el producto seleccionado en el listBox_stock
                Stock productoSeleccionado = listaProductos[listBox_stock.SelectedIndex];

                // Mostrar un mensaje de confirmación antes de eliminar el producto
                DialogResult result = MessageBox.Show("¿Está seguro de eliminar el producto?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // Eliminar el producto de la lista
                    listaProductos.Remove(productoSeleccionado);

                    // Actualizar el listBox_stock
                    listBox_stock.Items.RemoveAt(listBox_stock.SelectedIndex);
                }
            }
            else
            {
                MessageBox.Show("Seleccione un producto para eliminar.");
            }
        }

        private void btn_editar_p_Click(object sender, EventArgs e)
        {

        }
    }
}