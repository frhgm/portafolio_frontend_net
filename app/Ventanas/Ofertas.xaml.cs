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
    public partial class Ofertas
    {
        App _app = ((App)Application.Current);
        private Subasta subastaSeleccionada { get; set; } = null;
        List<EntradaMenu> menus = new();
        private int rolId;


        //TODO Esto deberia ser reemplazado por el patron MVVM
        public Ofertas()
        {
            InitializeComponent();

            if (UtilidadesLogica.ComprobarConexionInternet() == false)
            {
                MessageBox.Show("Sin conexion a internet, cerrando");
                return;
            }

            AgregarMenus();
        }

        public void SetSubasta(Subasta subasta)
        {
            this.subastaSeleccionada = subasta;
            CargarOfertasDeSubasta();
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
        private async void CargarOfertasDeSubasta()
        {
            var ofertas = await _app.ofertaDataService.TraerOfertasDeSubasta(subastaSeleccionada.Id);
            if (ofertas is null)
            {
                // TODO Mostrar de mejor manera?
                MessageBox.Show("No se encontraron ofertas para esta subasta. \nAbajo puede crear");
            }
            OfertasDG.ItemsSource = ofertas.OfertasSubasta;
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