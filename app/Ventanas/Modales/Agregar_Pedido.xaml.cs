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
            DetallesPedidoDG.ItemsSource = null;
            UtilidadesLogica.PoblarComboSolicitudes(Add_Solicitud);

        }

        private void Add_Solicitud_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            solicitudSeleccionada = (SolicitudPedido)cb.SelectedItem;
            Debug.Write(solicitudSeleccionada.Id);
        }
    }
}