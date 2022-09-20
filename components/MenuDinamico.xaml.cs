using classLibrary;
using classLibrary.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace components
{
    /// <summary>
    /// Interaction logic for EntradaMenu.xaml
    /// </summary>
    public partial class MenuDinamico : UserControl
    {
        private int rolId;

        //TODO Crear lista de ventanas a acceder por Rol
        List<EntradaMenu> menus = new();

        public List<EntradaMenu> PoblarListaEntradaMenus(int id)
        {
            menus = new List<EntradaMenu>();

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
        public MenuDinamico(int rolId)
        {
            InitializeComponent();
            this.rolId = rolId;
            menus = PoblarListaEntradaMenus(rolId);

            if (menus.Count != 0)
            {
                Menu mantenedores = new();
                foreach (EntradaMenu menu in menus)
                {
                    //if (menu.Id > 13)
                    //{

                    //}
                    MenuItem iterador = new()
                    {
                        Header = menu.Nombre
                    };
                    mantenedores.Items.Add(iterador);
                }
                mantenedores.VerticalAlignment = VerticalAlignment.Top;
                //this.AddChild(mantenedores);
                this.Content = mantenedores;
            }
            else
            {
                MessageBox.Show("Usted no tiene un rol asignado para acceder al sistema");
            }
        }


    }
}
