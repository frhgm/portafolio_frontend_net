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
using app.Ventanas.Modales;
using classLibrary.DTOs;

namespace app.Ventanas
{
    /// <summary>
    /// Interaction logic for ListarUsuario.xaml
    /// </summary>
    public partial class Pedidos
    {
        List<EntradaMenu> menus = new();
        private readonly SolicitudesDataService dataService = new();
        private int rolId;


        //TODO Esto deberia ser reemplazado por el patron MVVM
        public Pedidos()
        {
            InitializeComponent();

            if (UtilidadesLogica.ComprobarConexionInternet() == false)
            {
                MessageBox.Show("Sin conexion a internet, cerrando");
                return;
            }
            AgregarMenus();
        }

        public void MenuSeleccionadoSet(object sender, EventArgs e, string menu)
        {
            UtilidadesVentanas.ObtenerInstanciaVentana(String.Concat("app.Usuarios.", menu));
            this.Close();
        }

        public void AgregarMenus()
        {
            menus = UtilidadesLogica.PoblarListaEntradaMenus();
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
        private async void CargarSolicitudes()
        {
            var solicitudes = await dataService.TraerSolicitudes();
            SolicitudesDG.ItemsSource = solicitudes.solicitudes_pedidos; 
        }

        private async void BorrarUsuario_Click(object sender, RoutedEventArgs e)
        {
            SolicitudPedido data = (sender as FrameworkElement).DataContext as SolicitudPedido;

            var eleccion = MessageBox.Show("Seguro que desea eliminar un usuario?", "Seleccione una opcion", MessageBoxButton.YesNo);

            if (eleccion == MessageBoxResult.Yes)
            {
                //await dataService.BorrarUsuario(data);
            } 
        }

        //private async void AgregarUsuario_Click(object sender, RoutedEventArgs e)
        //{

        //    RegistrarUsuario usuarioPorRegistrar = new(
        //        Add_Rut.Text,
        //        Add_Nombre.Text,
        //        Add_ApellidoPaterno.Text,
        //        Add_ApellidoMaterno.Text,
        //        Add_Email.Text,
        //        Convert.ToInt64(Add_Telefono.Text),
        //        rolSeleccionadoCrear.Id,
        //        rolSeleccionadoCrear.Nombre_Rol,
        //        DateTime.Today,
        //        Add_Clave.Password);
        //    RegistrarUsuario resultado = await usuarioDataService.CrearUsuario(usuarioPorRegistrar);
        //    if (resultado != null)
        //    {
        //        //TODO Idealmente, simplemente agregar nueva fila https://stackoverflow.com/questions/24095172/how-i-can-add-new-row-into-datagrid-in-wpf
        //        UsuariosDG.ItemsSource = null;
        //        var usuarios = await usuarioDataService.TraerUsuarios();
        //        UsuariosDG.ItemsSource = usuarios.usuarios;
        //        MessageBox.Show("Usuario registrado exitosamente!");
        //    }
        //}

        //private async void ActualizarUsuario_Click(object sender, RoutedEventArgs e)
        //{
        //    ActualizarUsuario usuarioPorActualizar = new(
        //        Mod_Rut.Text,
        //        Mod_Email.Text,
        //        Convert.ToInt64(Mod_Telefono.Text),
        //        Mod_Clave.Password,
        //        "");
        //    await usuarioDataService.ActualizarUsuario(usuarioPorActualizar);
        //}

        //private void UsuariosDG_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    DataGrid dg = (DataGrid)sender;
        //    Usuario fila = (Usuario)dg.SelectedItem;
        //    if (fila != null)
        //    {
        //        Mod_Rut.Text = fila.Rut;
        //        Mod_Nombre.Text = fila.Nombre;
        //        Mod_ApPa.Text = fila.ApellidoPaterno;
        //        Mod_ApMa.Text = fila.ApellidoMaterno;
        //        Mod_Email.Text = fila.Email;
        //        Mod_Telefono.Text = fila.Telefono.ToString();

        //        Mod_Rol.SelectedIndex = fila.RolId;
        //        rolSeleccionadoCrear.Id = fila.RolId;
        //        rolSeleccionadoCrear.Nombre_Rol = fila.NombreRol;
        //        //TODO Se debe arreglar para que muestre Rol correcto
        //        //Quizas iniciar desde 0 los ID?
        //        //Mod_Rol.SelectedIndex -= 1;

        //    }
        //}
        ///// <summary>
        ///// Actualiza el rol seleccionado para crear un usuario
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e">Evento de cambio de seleccion, se parsea como un Rol para asi capturar su id y nombre</param>
        //private void Add_Rol_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    ComboBox cb = (ComboBox)sender;
        //    Rol fila = (Rol)cb.SelectedItem;

        //    rolSeleccionadoCrear.Id = fila.Id;
        //    rolSeleccionadoCrear.Nombre_Rol = fila.Nombre_Rol;
        //}
        //private void Mod_Rol_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    ComboBox cb = (ComboBox)sender;
        //    Rol fila = (Rol)cb.SelectedItem;

        //    rolSeleccionadoModificar.Id = fila.Id;
        //    rolSeleccionadoModificar.Nombre_Rol = fila.Nombre_Rol;
        //}


        private void AgregarPedido_OnClick(object sender, RoutedEventArgs e)
        {
             Agregar_Pedido ap = new Agregar_Pedido();
            try
            {
                ap.ShowDialog();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
    }
}
