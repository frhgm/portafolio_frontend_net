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
        App _app = ((App)Application.Current);
        private classLibrary.DTOs.Pedidos _pedidosSeleccionado;

        public Agregar_Subasta()
        {
            InitializeComponent();
            UtilidadesLogica.PoblarComboPedidos(AddPedido, _app.pedidoDataService);
        }


        private async void AgregarSubasta_OnClick(object sender, RoutedEventArgs e)
        {
            CrearSubasta subasta = new(_pedidosSeleccionado.PedidoId, Convert.ToInt32(AddMontoMinimo.Text));
            var subastaCreada = await _app.subastaDataService.CrearSubasta(subasta);
            // var subastaCreada = await _dataService.CrearSubasta(subasta);
            if (subastaCreada)
            {
                MessageBox.Show("Subasta ingresada!");
            }
        }

        private void AddPedido_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            _pedidosSeleccionado = (classLibrary.DTOs.Pedidos)cb.SelectedItem;
        }
    }
}