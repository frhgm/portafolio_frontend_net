using classLibrary.DTO;
using classLibrary.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using classLibrary.DataServices;

namespace classLibrary
{
    /// <summary>
    /// Esta clase provee logica que puede ser reutilizada en toda la solucion
    /// </summary>
    public class UtilidadesLogica
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
        
        public static async void PoblarComboProducto(ComboBox combo)
        {
            var productoDataService = new ProductosDataService();
            var productos = await productoDataService.TraerProductos();
            combo.ItemsSource = productos.productos;
            combo.DisplayMemberPath = "NombreProducto";
        }

        public static async void PoblarComboSolicitudes(ComboBox combo)
        {
            var solicitudDataService = new SolicitudesDataService();
            var solicitudesClientes = await solicitudDataService.TraerSolicitudes();
            // combo.ItemsSource = solicitudesClientes.solicitudes_pedidos;
            // combo.DisplayMemberPath = "Usuario_id";

            //TODO Terminar para que muestre id-rutcliente
            foreach (var solicitud in solicitudesClientes.solicitudes_pedidos)
            {
                combo.Items.Add(solicitud);
                combo.DisplayMemberPath = "Mostrar_Solicitud";
                // combo.DisplayMemberPath = $"{solicitud.Id}-{solicitud.Usuario_id}";
            }


        }
        public static List<EntradaMenu> PoblarListaEntradaMenus()
        {
            string[] ventanas =
            {
                // "Contrato",
                // "Detalle Pedidos",
                // "Detalle Solicitud Pedido",
                "Pedidos",
                "Productos",
                // "Producto Cliente",
                // "Producto Productor",
                // "Rol",
                // "Solicitudes",
                // "Subasta",
                // "Transporte",
                "Usuarios",
                // "Activar Seguros",
                // "Ofertar Subasta",
                // "Pagar Pedidos",
                // "Reportes",
            };
            List<EntradaMenu> menus = new();
            for (int i = 0; i < ventanas.Length; i++)
            {
                menus.Add(new EntradaMenu(i, ventanas[i]));
            }
            
            return menus;
        }
        
        
        //public static string Encriptar(string dato)
        //{
        //    try
        //    {
        //        byte[] encData_byte = new byte[dato.Length];
        //        encData_byte = System.Text.Encoding.UTF8.GetBytes(dato);
        //        string encodedData = Convert.ToBase64String(encData_byte);
        //        return encodedData;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Error in base64Encode" + ex.Message);
        //    }
        //}

        //public static string Decriptar(string encodedData)
        //{
        //    System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
        //    System.Text.Decoder utf8Decode = encoder.GetDecoder();
        //    byte[] todecode_byte = Convert.FromBase64String(encodedData);
        //    int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
        //    char[] decoded_char = new char[charCount];
        //    utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
        //    string result = new String(decoded_char);
        //    return result;
        //}

    }
}
