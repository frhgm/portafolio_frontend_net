using System;
using System.Windows;
using System.Windows.Controls;
using classLibrary;
using classLibrary.DataServices;
using classLibrary.DTOs;

namespace app.Ventanas.Modales
{
    public partial class Agregar_Subasta : Window
    {
        private readonly SubastaDataService _dataService = new();
        private readonly PedidosDataService _pedidoDataService = new();
        private Pedido pedidoSeleccionado;

        public Agregar_Subasta()
        {
            InitializeComponent();
            UtilidadesLogica.PoblarComboPedidos(AddPedido, _pedidoDataService);
        }


        private async void AgregarSubasta_OnClick(object sender, RoutedEventArgs e)
        {
            CrearSubasta subasta = new(pedidoSeleccionado.PedidoId, Convert.ToInt32(AddMontoMinimo.Text));
            var subastaCreada = await _dataService.CrearSubasta(subasta);
            if (subastaCreada)
            {
                MessageBox.Show("Subasta ingresada!");
            }
            else
            {
                MessageBox.Show("No se ingreso subasta, vuelva a intentar");
            }
        }

        private void AddPedido_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            pedidoSeleccionado = (Pedido)cb.SelectedItem;
        }
    }
}