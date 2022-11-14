using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using app.Data.Implementations;
using classLibrary;
using classLibrary.DTOs;

namespace app.Ventanas.Modales
{
    public partial class Agregar_Pedido : Window
    {
        private readonly SolicitudesDataService _solicitudDataService = new();
        private SolicitudPedido solicitudSeleccionada = null;
        public Agregar_Pedido()
        {
            InitializeComponent();
            UtilidadesLogica.PoblarComboSolicitudes(Add_Solicitud);
            DetallesPedidoDG.Items.Add(new {Nombre = "Felipe", Calidad = "1000", Rut="18392764-7", Precio=1000, Cantidad=500, Ganancia=500, Total=1500000});

        }

        private void Add_Solicitud_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            solicitudSeleccionada = (SolicitudPedido)cb.SelectedItem;
            Debug.Write(solicitudSeleccionada.Id);
        }

        private void BuscarProductos_Productor_OnClick(object sender, RoutedEventArgs e)
        {
            //TODO Lograr poblar (o no) DataGrid con Productos_Productores coincidentes con Productos_Clientes
            Producto_Cliente pc
            // 1. Traer los producto_cliente de la solcitud seleccionada
            // 2. Llamar a SP_PRODUCTO_PRODUCTOR_PEDIDO con cada producto_cliente.id para saber si sirve
            // 2.1 Si no, (SP_GET_PRODUCTO_PRODUCTOR_LOOKUP), detener creacion de pedido  
            // 2.2 Si existen, proceder
            // 3. Realizar llamada a SP_INSERT_PEDIDO_Y_DETALLE
        }
    }
}