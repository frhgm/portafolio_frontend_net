using System;
using System.Windows;
using classLibrary.DTOs;

namespace app.Ventanas.Modales
{
    public partial class Agregar_Solicitud : Window
    {
        private SolicitudPedido _solicitudPedido { get; set; }

        public Agregar_Solicitud()
        {
            InitializeComponent();
            Add_Producto.IsEnabled = false;
            Add_Calidad.IsEnabled = false;
            Add_Cantidad.IsEnabled = false;
        }


        private void Crear_Solicitud_OnClick(object sender, RoutedEventArgs e)
        {
            _solicitudPedido = new SolicitudPedido()
            {
                Rut = Add_Rut.Text,
                Fecha = DateTime.Today,
                Direccion = Add_Direccion.Text,
                Estado_solicitud = "1" // TODO: Reemplazar por enumerador de estados 
            };

            Add_Rut.IsEnabled = false;
            Add_Direccion.IsEnabled = false;
            Crear_Solicitud.IsEnabled = false;
            Add_Rut.Text = _solicitudPedido.Rut;
            Add_Direccion.Text = _solicitudPedido.Direccion;

            Add_Producto.IsEnabled = true;
            Add_Calidad.IsEnabled = true;
            Add_Cantidad.IsEnabled = true;
        }

        private void Agregar_Producto_OnClick(object sender, RoutedEventArgs e)
        {
            // TODO Recuperar inputs para insertar en DataTable
            // 1. Recuperar producto (completo)
            Producto prodUno = new Producto(1, "Sandia", "Una sandia grande", "url");
            // 2. Recuperar calidad y cantidad
            // 3. Al estar listo agregar a DT
            DetalleProducto detProd = new DetalleProducto(prodUno.Id, prodUno.NombreProducto, int.Parse(Add_Calidad.Text),int.Parse(Add_Cantidad.Text));


            // DetallesDG.Items.Add(detProd);
        }
    }
}