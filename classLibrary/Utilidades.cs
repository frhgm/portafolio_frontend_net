using classLibrary.DTO;
using classLibrary.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace classLibrary
{
    public class Utilidades
    {
        public static bool ComprobarConexionInternet()
        {
            try
            {
                using (var client = new WebClient())
                using (var stream = client.OpenRead("http://www.google.com"))
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
        public static void PoblarCombosRoles(ComboBox combo)
        {
            List<Rol> roles = new List<Rol>();
            roles.Add(new Rol(1, "Administrador"));
            roles.Add(new Rol(2, "Consultor"));
            roles.Add(new Rol(3, "Transportista"));
            roles.Add(new Rol(4, "Productor"));
            roles.Add(new Rol(5, "Cliente interno"));
            roles.Add(new Rol(6, "Cliente externo"));
            combo.ItemsSource = roles;
            combo.DisplayMemberPath = "Nombre_Rol";
        }
        public static List<EntradaMenu> PoblarListaEntradaMenus()
        {
            string[] ventanas = { "Transporte", "Pedido", "Detalle Pedidos", "Solicitud Pedido", "Detalle Solicitud Pedido", "Producto", "Producto Cliente", "Producto Productor", "Contrato", "Usuarios", "Rol", "Subasta", "Oferta Subasta", "Solicitud Pedido", "Reportes", "Pagar Pedidos", "Activar Seguros" };
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
