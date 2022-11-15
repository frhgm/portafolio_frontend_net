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
using classLibrary.DataServices;

namespace app.Ventanas
{
    /// <summary>
    /// Interaction logic for ListarUsuario.xaml
    /// </summary>
    public partial class Usuarios
    {
        Rol rolSeleccionadoCrear = new Rol();
        Rol rolSeleccionadoModificar = new Rol();
        UtilidadesVentanas utilidadesVentanas = new UtilidadesVentanas();
        public Usuario usuario { get; set; } = new("18392764-7", "Felipe", "Ramirez", "Hites", "framirezhites@maipogrande.cl", 9974303094, 1, "Admin", DateTime.Today, "663401993");
        List<EntradaMenu> menus = new();
        private readonly UsuariosDataService usuarioDataService = new();
        private int rolId;

        public void SetUsuario(Usuario usuario)
        {
            this.usuario = usuario;
        }
        //TODO Esto deberia ser reemplazado por el patron MVVM
        public Usuarios()
        {
            InitializeComponent();

            if (UtilidadesLogica.ComprobarConexionInternet() == false)
            {
                MessageBox.Show("Sin conexion a internet, cerrando");
                return;
            }

            this.rolId = usuario.RolId;
            var menus = utilidadesVentanas.AgregarMenus("Usuarios");
            menuPrincipal.Items.Add(menus.Item1);
            menuPrincipal.Items.Add(menus.Item2);

            CargarUsuarios();
            UtilidadesLogica.PoblarCombosRoles(Add_Rol);
            UtilidadesLogica.PoblarCombosRoles(Mod_Rol);
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
        private async void BorrarUsuario_Click(object sender, RoutedEventArgs e)
        {
            Usuario data = (sender as FrameworkElement).DataContext as Usuario;

            var eleccion = MessageBox.Show("Seguro que desea eliminar un usuario?", "Seleccione una opcion", MessageBoxButton.YesNo);

            if (eleccion == MessageBoxResult.Yes)
            {
                await usuarioDataService.BorrarUsuario(data);
            }
        }

        private async void AgregarUsuario_Click(object sender, RoutedEventArgs e)
        {

            if (!Rut.RutValido(Add_Rut.Text))
            {
                MessageBox.Show("Debe ingresar un rut valido");
                return;
            }
 
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
                Add_Clave.Password);
            RegistrarUsuario resultado = await usuarioDataService.CrearUsuario(usuarioPorRegistrar);
            if (resultado != null)
            {
                //TODO Idealmente, simplemente agregar nueva fila https://stackoverflow.com/questions/24095172/how-i-can-add-new-row-into-datagrid-in-wpf
                UsuariosDG.ItemsSource = null;
                var usuarios = await usuarioDataService.TraerUsuarios();
                UsuariosDG.ItemsSource = usuarios.usuarios;
                MessageBox.Show("Usuario registrado exitosamente!");
            }
        }

        private async void ActualizarUsuario_Click(object sender, RoutedEventArgs e)
        {
            ActualizarUsuario usuarioPorActualizar = new(
                Mod_Rut.Text,
                Mod_Email.Text,
                Convert.ToInt64(Mod_Telefono.Text),
                Mod_Clave.Password,
                "");
            var usuario = await usuarioDataService.ActualizarUsuario(usuarioPorActualizar);
            if (usuario != null)
            {
                UsuariosDG.ItemsSource = null;
                var usuarios = await usuarioDataService.TraerUsuarios();
                UsuariosDG.ItemsSource = usuarios.usuarios;
                MessageBox.Show("Usuario actualizado exitosamente!");
            }
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

                Mod_Rol.SelectedIndex = fila.RolId - 1; //TODO Esto esta asi porque la lista empieza en 1, y el arreglo en 0
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
