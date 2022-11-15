using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using classLibrary;
using classLibrary.DataServices;
using classLibrary.DTOs;

namespace app.Ventanas.Modales
{
    public partial class Agregar_Pedido : Window
    {
        private readonly SolicitudesDataService _solicitudDataService = new();
        private readonly PedidosDataService _dataService = new();
        private SolicitudPedido solicitudSeleccionada = null;

        public Agregar_Pedido()
        {
            InitializeComponent();
            UtilidadesLogica.PoblarComboSolicitudes(Add_Solicitud);
            DetallesPedidoDG.Items.Add(new
            {
                Nombre = "Felipe", Calidad = "1000", Rut = "18392764-7", Precio = 1000, Cantidad = 500, Ganancia = 500,
                Total = 1500000
            });
        }

        private void Add_Solicitud_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            solicitudSeleccionada = (SolicitudPedido)cb.SelectedItem;
        }

        private async void BuscarProductos_Productor_OnClick(object sender, RoutedEventArgs e)
        {
            // TODO SI NO SELECCIONO UNA SOLICITUD DENEGAR
            // TODO Lograr poblar (o no) DataGrid con Productos_Productores coincidentes con Productos_Clientes
            // 1. Traer los producto_cliente de la solcitud seleccionada
            // TRAIGO LOS DETALLES DE UNA SOLICITUD EN PARTICULAR
            var detalles = await _solicitudDataService.TraerDetallesSolcitudPedido(solicitudSeleccionada.Id);
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
                var pp = await _dataService.TraerProductosProductorPedido(pc.Id);
                if (pp == null)
                {
                    puedeCrearPedido = false;
                }
                else
                {
                    var detalle = await _dataService.PoblarDetallePedido(pp.Id, pc.Id);
                    // TODO Controlar que no intente agregar si es null, o vacio
                    filasPorAgregar.Add(detalle);
                }
            }

            // SI ES QUE TODOS LOS PRODUCTOS_CLIENTES PUEDEN SER VENDIDOS, SE AGREGAN LOS PRODUCTOS PRODUCTORES A LA TABLA PARA VISUALAZAR
            if (puedeCrearPedido)
            {
                DetallesPedidoDG.Items.Add(filasPorAgregar);
            }
            // 2.1 Si no, (SP_GET_PRODUCTO_PRODUCTOR_PEDIDO), detener creacion de pedido  
            // MessageBox.Show("No puede generar pedido, ya que no hay productos suficientes");
            // return;

            // if (puedeCrearPedido)
            // {
            //     // RECUPERAR EL ID DE TODOS LOS PRODUCTOS_PRODUCTORES 
            //     // 3. Realizar llamada a SP_INSERT_PEDIDO_Y_DETALLE
            // }
        }
    }
}