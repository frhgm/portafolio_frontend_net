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
    /// Interaction logic for Subastas.xaml
    /// </summary>
    public partial class Subastas
    {
        App _app = ((App)Application.Current);
        List<EntradaMenu> menus = new();
        private int rolId;
        private Subasta subastaSeleccionada { get; set; } = null;
        private UtilidadesVentanas _utilidadesVentanas = new();
        private int ofertaGanadora = 0;


        public Subastas()
        {
            InitializeComponent();
            CargarSubastas();
            _utilidadesVentanas.AgregarMenus("Subastas", menuPrincipal);
        }

        public void MenuSeleccionadoSet(object sender, EventArgs e, string menu)
        {
            UtilidadesVentanas.ObtenerInstanciaVentana(String.Concat("app.Subastas.", menu));
            this.Close();
        }

        private async void CargarSubastas()
        {
            var subastas = await _app.subastaDataService.TraerSubastas();
            foreach (var subasta in subastas.subastas)
            {
                if (subasta.OfertaSubastaId == null)
                {
                    subasta.OfertaSubastaId = 0;
                }
            }

            SubastasDG.ItemsSource = subastas.subastas;
        }

        private async void CargarOfertasDeSubasta()
        {
            var ofertas = await _app.ofertaDataService.TraerOfertasDeSubasta(subastaSeleccionada.Id);
            if (ofertas.OfertasSubasta.Count == 0)
            {
                // TODO Mostrar de mejor manera?
                MessageBox.Show("No se encontraron ofertas para esta subasta. \nAbajo puede crear");
            }

            OfertasDG.ItemsSource = null;
            OfertasDG.ItemsSource = ofertas.OfertasSubasta;
        }

        private void AgregarSubasta_OnClick(object sender, RoutedEventArgs e)
        {
            Agregar_Subasta agregarSubasta = new Agregar_Subasta();
            try
            {
                agregarSubasta.ShowDialog();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        private void VerOfertas_Click(object sender, RoutedEventArgs e)
        {
            Subasta data = (sender as FrameworkElement).DataContext as Subasta;
            subastaSeleccionada = data;
            CargarOfertasDeSubasta();
            SubastarOfertas.IsEnabled = true;
        }

        private async void SubastarOfertas_Click(object sender, RoutedEventArgs e)
        {
            if (subastaSeleccionada.PedidoId != 0)
            {
                ofertaGanadora = await _app.ofertaDataService.Subastar(subastaSeleccionada.PedidoId);
            }

            if (ofertaGanadora != 0)
            {
                MessageBox.Show($"Gano la oferta N°{ofertaGanadora}!");
            }
            else
            {
                MessageBox.Show("Ocurrio un error, por favor intentar nuevamente");
            }
        }
    }
}