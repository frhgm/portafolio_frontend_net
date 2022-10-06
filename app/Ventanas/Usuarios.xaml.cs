using app.Data.Implementations;
using classLibrary;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using classLibrary.DTO;
using System.Data;
using classLibrary.DTOs;

namespace app.Ventanas
{
    /// <summary>
    /// Interaction logic for ListarUsuario.xaml
    /// </summary>
    public partial class Usuarios
    {
        Rol rolSeleccionadoCrear = new Rol();
        Rol rolSeleccionadoModificar = new Rol();
        private Usuario usuario = null;

        List<EntradaMenu> menus = new();
        private readonly UsuariosDataService usuarioDataService = new();
        private int rolId;

        //TODO Esto deberia ser reemplazado por el patron MVVM
        public Usuarios(Login login)
        {
            InitializeComponent();
            usuario = login.usuarioInput;
            if (Utilidades.ComprobarConexionInternet() == false)
            {
                MessageBox.Show("Sin conexion a internet, cerrando");
                return;
            }
            this.rolId = usuario.RolId;
            AgregarMenus();
            CargarUsuarios();
            Utilidades.PoblarCombosRoles(Add_Rol);
            Utilidades.PoblarCombosRoles(Mod_Rol);
        }

        public void MenuSeleccionadoSet(object sender, EventArgs e, string menu)
        {
            Utilidades.ObtenerInstanciaVentana(String.Concat("app.Usuarios.", menu));
        }

        public void AgregarMenus()
        {
            menus = Utilidades.PoblarListaEntradaMenus();
            if (menus.Count != 0)
            {
                MenuItem mantenedores = new()
                {
                    Header = "Mantenedores",
                    VerticalAlignment = VerticalAlignment.Top
                };
                MenuItem acciones = new()
                {
                    Header = "Acciones",
                    VerticalAlignment = VerticalAlignment.Top
                };
                foreach (EntradaMenu menu in menus)
                {
                    MenuItem iterador = new()
                    {
                        Header = menu.Nombre
                    };
                    iterador.Click += (sender, e) => MenuSeleccionadoSet(sender, e, menu.Nombre);

                    if (menu.Id > 13)
                    {
                        acciones.Items.Add(iterador);
                    }
                    else
                    {
                        mantenedores.Items.Add(iterador);
                    }
                }

                menuPrincipal.Items.Add(acciones);
                menuPrincipal.Items.Add(mantenedores);
            }
            else
            {
                MessageBox.Show("Usted no tiene un rol asignado para acceder al sistema");
            }
        }


        
        /// <summary>
        /// Se llama al metodo TraerUsuarios, que va a buscar al servidor sp_get_all_users, y pobla el DataGrid con esta lista
        /// </summary>
        private async void CargarUsuarios()
        {
            //TODO Se debe anclar al final de la pagina
            var usuarios = await usuarioDataService.TraerUsuarios();
            UsuariosDG.ItemsSource = usuarios.usuarios;
        }

        private void SeleccionarUsuarioParaEditar_Click(object sender, RoutedEventArgs e)
        {

        }
        private void BorrarUsuario_Click(object sender, RoutedEventArgs e)
        {
            //var usuarioSeleccionado = (Usuario)sender;

            MessageBox.Show("Seguro que desea eliminar un usuario?", "Seleccione una opcion", MessageBoxButton.YesNo);
        }

        private async void AgregarUsuario_Click(object sender, RoutedEventArgs e)
        {

            RegistrarUsuario usuarioPorRegistrar = new(
                Add_Rut.Text,
                Add_Nombre.Text,
                Add_ApellidoPaterno.Text,
                Add_ApellidoMaterno.Text,
                Add_Email.Text,
                Convert.ToInt64(Add_Telefono.Text),
                rolSeleccionadoCrear.Id,
                rolSeleccionadoCrear.Nombre_Rol,
                DateTime.Today,
                Add_Clave.Text);
            RegistrarUsuario resultado = await usuarioDataService.CrearUsuario(usuarioPorRegistrar);
            if (resultado != null)
            {
                UsuariosDG.Items.Add(resultado);
            }
        }

        private async void ActualizarUsuario_Click(object sender, RoutedEventArgs e)
        {
            ActualizarUsuario usuarioPorActualizar = new(
                Mod_Rut.Text,
                Mod_Email.Text,
                Convert.ToInt64(Mod_Telefono.Text),
                Mod_Clave.Text,
                "");
            await usuarioDataService.ActualizarUsuario(usuarioPorActualizar);
        }

        private void UsuariosDG_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dg = (DataGrid)sender;
            Usuario fila = (Usuario)dg.SelectedItem;
            if (fila != null)
            {
                Mod_Rut.Text = fila.Rut;
                Mod_Nombre.Text = fila.Nombre;
                Mod_ApPa.Text = fila.ApellidoPaterno;
                Mod_ApMa.Text = fila.ApellidoMaterno;
                Mod_Email.Text = fila.Email;
                Mod_Telefono.Text = fila.Telefono.ToString();

                Mod_Rol.SelectedIndex = fila.RolId;
                rolSeleccionadoCrear.Id = fila.RolId;
                rolSeleccionadoCrear.Nombre_Rol = fila.NombreRol;
                //TODO Se debe arreglar para que muestre Rol correcto
                //Quizas iniciar desde 0 los ID?
                //Mod_Rol.SelectedIndex -= 1;

            }
        }
        /// <summary>
        /// Actualiza el rol seleccionado para crear un usuario
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">Evento de cambio de seleccion, se parsea como un Rol para asi capturar su id y nombre</param>
        private void Add_Rol_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            Rol fila = (Rol)cb.SelectedItem;

            rolSeleccionadoCrear.Id = fila.Id;
            rolSeleccionadoCrear.Nombre_Rol = fila.Nombre_Rol;
        }
        private void Mod_Rol_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            Rol fila = (Rol)cb.SelectedItem;

            rolSeleccionadoModificar.Id = fila.Id;
            rolSeleccionadoModificar.Nombre_Rol = fila.Nombre_Rol;
        }


    }
}
