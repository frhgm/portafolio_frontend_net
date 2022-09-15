using classLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace app
{
    public static class Utils
    {
        public static List<EntradaMenu> PoblarListaEntradaMenus(int id)
        {
            List<EntradaMenu> menus = new List<EntradaMenu>();

            switch (id)
            {
                //Administrador
                case 1:
                    menus.Add(new EntradaMenu(1, "Transporte"));
                    menus.Add(new EntradaMenu(2, "Pedido"));
                    menus.Add(new EntradaMenu(3, "DetallePedidos"));
                    menus.Add(new EntradaMenu(4, "SolicitudPedido"));
                    menus.Add(new EntradaMenu(5, "DetalleSolicitudPedido"));
                    menus.Add(new EntradaMenu(6, "Producto"));
                    menus.Add(new EntradaMenu(7, "ProductoCliente"));
                    menus.Add(new EntradaMenu(8, "ProductoProductor"));
                    menus.Add(new EntradaMenu(9, "Contrato"));
                    menus.Add(new EntradaMenu(10, "Usuario"));
                    menus.Add(new EntradaMenu(11, "Rol"));
                    menus.Add(new EntradaMenu(12, "Subasta"));
                    menus.Add(new EntradaMenu(13, "OfertaSubasta"));
                    menus.Add(new EntradaMenu(14, "SolicitudPedido"));
                    menus.Add(new EntradaMenu(15, "Reportes"));
                    menus.Add(new EntradaMenu(16, "Pagar pedidos"));
                    menus.Add(new EntradaMenu(17, "ActivarSeguro"));
                    break;
                //Consultor
                case 2:
                    menus.Add(new EntradaMenu(15, "Reportes"));
                    break;
                //Transportista
                case 3:
                    menus.Add(new EntradaMenu(1, "Transporte"));
                    menus.Add(new EntradaMenu(3, "DetallePedidos"));
                    menus.Add(new EntradaMenu(9, "Contrato"));
                    menus.Add(new EntradaMenu(13, "OfertaSubasta"));
                    break;
                case 4:
                    menus.Add(new EntradaMenu(1, "Transporte"));
                    menus.Add(new EntradaMenu(8, "ProductoProductor"));

                    break;
                case 5:

                    break;
                case 6:
                    menus.Add(new EntradaMenu(14, "SolicitudPedido"));
                    break;
                default:
                    break;
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
            Type ventana = Type.GetType(nombre, true);
            try
            {
                Window v = ((Window)Activator.CreateInstance(ventana));
                v.Show();

            }
            catch (Exception e)
            {

            }
        }


    }
}
