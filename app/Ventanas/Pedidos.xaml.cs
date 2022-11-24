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
using System.Globalization;
using app.Ventanas.Modales;
using classLibrary.DataServices;
using classLibrary.DTOs;

namespace app.Ventanas
{
    /// <summary>
    /// Interaction logic for ListarUsuario.xaml
    /// </summary>
    public partial class Pedidos
    {
        App _app = ((App)Application.Current);
        List<EntradaMenu> menus = new();
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
            CargarPedidos();
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
        private async void CargarPedidos()
        {
            var pedidos = await _app.pedidoDataService.TraerPedidos();
            PedidosDG.ItemsSource = pedidos.pedidos;
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
