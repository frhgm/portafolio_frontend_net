﻿using ShowMeTheXAML;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using classLibrary.DataServices;

namespace app
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public OfertasSubastaDataService ofertaDataService;
        public PedidosDataService pedidoDataService;
        public SolicitudesDataService solicitudDataService;
        public SubastaDataService subastaDataService;
        protected override void OnStartup(StartupEventArgs e)
        {
            XamlDisplay.Init();
            base.OnStartup(e);
            ofertaDataService = new();
            pedidoDataService = new();
            solicitudDataService = new();
            subastaDataService = new();
        }
    }
}