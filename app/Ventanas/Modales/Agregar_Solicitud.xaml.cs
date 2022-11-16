using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using classLibrary;
using classLibrary.DataServices;
using classLibrary.DTOs;

namespace app.Ventanas.Modales
{
    public partial class Agregar_Solicitud : Window
    {
        private Producto _productoSeleccionado = new();
        private SolicitudPedido? _solicitudPedido { get; set; }

        private readonly SolicitudesDataService dataService = new();

        public Agregar_Solicitud()
        {
            InitializeComponent();
            UtilidadesLogica.PoblarComboProducto(Add_Producto);
            Add_Producto.IsEnabled = false;
            Add_Calidad.IsEnabled = false;
            Add_Cantidad.IsEnabled = false;
        }


        /// <summary>
        /// Este metodo se llamara solo una vez por solicitud, despues recuperando de sus campos los correspondientes datos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        // private void Crear_Solicitud_OnClick(object sender, RoutedEventArgs e)
        // {
        //     _solicitudPedido = new SolicitudPedido(Add_Rut.Text, Add_Direccion.Text, null);
        //    
        //
        //     Add_Rut.IsEnabled = false;
        //     Add_Direccion.IsEnabled = false;
        //     Crear_Solicitud.IsEnabled = false;
        //     // Add_Rut.Text = _solicitudPedido.Rut;
        //     // Add_Direccion.Text = _solicitudPedido.Direccion;
        //
        //     Add_Producto.IsEnabled = true;
        //     Add_Calidad.IsEnabled = true;
        //     Add_Cantidad.IsEnabled = true;
        // }

        /// <summary>
        /// Este metodo puede ser llamado tantas veces como detalles hayan por solicitud
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Agregar_Producto_OnClick(object sender, RoutedEventArgs e)
        {
            // TODO Recuperar inputs para insertar en DataTable
            // 1. Recuperar producto (completo)
            // 2. Recuperar calidad y cantidad
            // 3. Al estar listo agregar a DT
            Crear_ProductoCliente detProd = new Crear_ProductoCliente(
                _productoSeleccionado.Id,
                _productoSeleccionado.NombreProducto,
                int.Parse(Add_Calidad.Text),
                int.Parse(Add_Cantidad.Text)
            );


            DetallesDG.Items.Add(detProd);
            // TODO Encontrar una manera de re-setear a una posicion 0
            // Add_Producto.SelectedIndex = 0;
            Add_Calidad.Text = "";
            Add_Cantidad.Text = "";
        }

        private void Add_Producto_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            if (cb.SelectedIndex != 0)
            {
                Producto fila = (Producto)cb.SelectedItem;

                _productoSeleccionado.Id = fila.Id;
                _productoSeleccionado.NombreProducto = fila.NombreProducto;
                _productoSeleccionado.Descripcion = fila.Descripcion;
            }
        }

        // private void EnviarSolicitud_OnClick(object sender, RoutedEventArgs e)
        // {
        //     List<DetalleSolicitudPedido> lista = DetallesDG.Items.OfType<DetalleSolicitudPedido>().ToList();
        //
        //
        //     SolicitudPedido solicitud = new(Add_Rut.Text, Add_Direccion.Text, lista);
        //     
        //     var resultado = dataService.CrearSolicitud(solicitud);
        //     if (resultado != null)
        //     {
        //         MessageBox.Show("Solicitud enviada exitosamente!");
        //     }
        //     else
        //     {
        //         MessageBox.Show("No se ingreso solicitud, intente nuevamente");
        //     }
        //
        // }
    }
}