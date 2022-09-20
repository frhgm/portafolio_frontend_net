﻿using classLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using classLibrary.DTO;
namespace app
{
    public static class Utils
    {
        public static List<EntradaMenu> PoblarListaEntradaMenus()
        {
            string[] ventanas = { "Transporte", "Pedido", "Detalle Pedidos", "Solicitud Pedido", "Detalle Solicitud Pedido", "Producto", "Producto Cliente", "Producto Productor", "Contrato", "Usuario", "Rol", "Subasta", "Oferta Subasta", "Solicitud Pedido", "Reportes", "Pagar Pedidos", "Activar Seguros" };
            List<EntradaMenu> menus = new();
            for (int i = 1; i < ventanas.Length; i++)
            {
                menus.Add(new EntradaMenu(i, ventanas[i]));
            }
            return menus;
        }
        public static void ObtenerInstanciaVentana(string nombre)
        {
            Assembly assemby = Assembly.GetExecutingAssembly();
            System.Type[] types = assemby.GetTypes();
            var varWindows = types.ToList()
                .Where(current => current.BaseType == typeof(Window));
            varWindows.FirstOrDefault(x => x.Name == nombre);
            Type? ventana = Type.GetType(nombre, true);
            try
            {
                Window? v = Activator.CreateInstance(ventana) as Window;
                v.Show();

            }
            catch (Exception e)
            {

            }
        }


    }
}
