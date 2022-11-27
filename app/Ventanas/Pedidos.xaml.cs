using System;
using System.Collections.Generic;
using System.Windows;
using classLibrary.DTO;
using app.Ventanas.Modales;

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

        private async void CargarPedidos()
        {
            var pedidos = await _app.pedidoDataService.TraerPedidos();
            PedidosDG.ItemsSource = pedidos.pedidos;
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