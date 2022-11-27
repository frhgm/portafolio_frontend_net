using classLibrary;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using classLibrary.DTO;
using app.Ventanas.Modales;
using classLibrary.DTOs;

namespace app.Ventanas
{
    /// <summary>
    /// Interaction logic for Pedidos.xaml
    /// </summary>
    public partial class Pedidos
    {
        App _app = ((App)Application.Current);
        private UtilidadesVentanas _utilidadesVentanas = new();
        List<EntradaMenu> menus = new();
        private int rolId;


        public Pedidos()
        {
            InitializeComponent();
            CargarPedidos();
            _utilidadesVentanas.AgregarMenus("Pedidos", menuPrincipal);
        }

        public void MenuSeleccionadoSet(object sender, EventArgs e, string menu)
        {
            UtilidadesVentanas.ObtenerInstanciaVentana(String.Concat("app.Usuarios.", menu));
            this.Close();
        }

        private async void CargarPedidos()
        {
            var pedidos = await _app.pedidoDataService.TraerPedidos();
            PedidosDG.ItemsSource = pedidos.pedidos;
        }

        private async void BorrarUsuario_Click(object sender, RoutedEventArgs e)
        {
            SolicitudPedido data = (sender as FrameworkElement).DataContext as SolicitudPedido;

            var eleccion = MessageBox.Show("Seguro que desea eliminar un usuario?", "Seleccione una opcion",
                MessageBoxButton.YesNo);

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