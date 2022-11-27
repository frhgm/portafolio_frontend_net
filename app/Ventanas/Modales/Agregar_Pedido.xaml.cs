using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using classLibrary;
using classLibrary.DTOs;

namespace app.Ventanas.Modales
{
    public partial class Agregar_Pedido : Window
    {
        private SolicitudPedido_Recibida solicitudSeleccionada = null;
        App _app = ((App)Application.Current);
        private CrearPedido _pedidoPorCrear = new CrearPedido();

        public Agregar_Pedido()
        {
            InitializeComponent();
            UtilidadesLogica.PoblarComboSolicitudes(Add_Solicitud);
            CrearPedido.IsEnabled = false;
        }

        private void Add_Solicitud_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            solicitudSeleccionada = (SolicitudPedido_Recibida)cb.SelectedItem;
        }

        private async void BuscarProductos_Productor_OnClick(object sender, RoutedEventArgs e)
        {
            // TODO SI NO SELECCIONO UNA SOLICITUD DENEGAR
            // TODO Lograr poblar (o no) DataGrid con Productos_Productores coincidentes con Productos_Clientes
            // 1. Traer los producto_cliente de la solcitud seleccionada
            // TRAIGO LOS DETALLES DE UNA SOLICITUD EN PARTICULAR
            var detalles = await _app.solicitudDataService.TraerDetallesSolcitudPedido(solicitudSeleccionada.Id);
            List<ProductoCliente> productosClientes = new List<ProductoCliente>();
            foreach (var detalle in detalles.DetallesSolicitudPedido)
            {
                productosClientes.Add(new ProductoCliente(detalle.ProductoClienteId, detalle.ProductoId,
                    detalle.NombreProducto, detalle.Calidad, detalle.Cantidad));
            }

            // 2. Llamar a SP_PRODUCTO_PRODUCTOR_PEDIDO con cada producto_cliente.id para saber si sirve
            bool puedeCrearPedido = true;
            List<FilaProductoProductor> filasPorAgregar = new List<FilaProductoProductor>();
            // LLAMO LOS PRODUCTOS PRODUCTORES COINCIDENTES CON PRODUCTOS CLIENTES
            foreach (var pc in productosClientes)
            {
                var pp = await _app.pedidoDataService.TraerProductosProductorPedido(pc.Id);
                if (pp == null)
                {
                    puedeCrearPedido = false;
                }
                else
                {
                    var detalle = await _app.pedidoDataService.PoblarDetallePedido(pp.Id, pc.Id);
                    // TODO Controlar que no intente agregar si es null, o vacio
                    filasPorAgregar.Add(detalle);
                }
            }

            // SI ES QUE TODOS LOS PRODUCTOS_CLIENTES PUEDEN SER VENDIDOS, SE AGREGAN LOS PRODUCTOS PRODUCTORES A LA TABLA PARA VISUALAZAR
            if (puedeCrearPedido)
            {
                int totalAPagar = 0;
                List<CrearDetallePedido> detallesPedido = new List<CrearDetallePedido>();
                foreach (var fila in filasPorAgregar)
                {
                    totalAPagar += fila.Total;
                    DetallesPedidoDG.Items.Add(fila);
                    CrearDetallePedido crearDetalle = new CrearDetallePedido();
                    crearDetalle.Calidad = fila.Calidad;
                    crearDetalle.Cantidad = fila.CantidadProductoCliente;
                    crearDetalle.Precio = fila.Precio;
                    crearDetalle.Productor_Id = fila.ProductorRut;
                    crearDetalle.ProductoId = fila.ProductoId;
                    detallesPedido.Add(crearDetalle);
                }

                LblTotal.Content = $"Total: {totalAPagar.ToString("C", CultureInfo.CurrentCulture)}";
                CrearPedido crearPedido = new CrearPedido();
                // crearPedido.Total = totalAPagar;
                crearPedido.SolicitudId = solicitudSeleccionada.Id;
                crearPedido.DetallePedido = detallesPedido;

                _pedidoPorCrear = crearPedido;
                CrearPedido.IsEnabled = true;
            }
            else
            {
                MessageBox.Show("No puede generar pedido, ya que no hay productos suficientes");
            }
        }

        private async void CrearPedido_OnClick(object sender, RoutedEventArgs e)
        {
            bool pedidoCreado = await _app.pedidoDataService.CrearPedido(_pedidoPorCrear);

            if (pedidoCreado)
            {
                MessageBox.Show("Pedido creado exitosamente!");
            }
            else
            {
                MessageBox.Show("No se creo pedido, revise datos");
            }
        }
    }
}