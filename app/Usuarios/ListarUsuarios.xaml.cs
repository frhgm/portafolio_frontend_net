﻿using app.Data;
using app.Data.Implementations;
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
using System.Windows.Shapes;

namespace app.Usuarios
{
    /// <summary>
    /// Interaction logic for ListarUsuario.xaml
    /// </summary>
    public partial class ListarUsuarios : Window
    {
        List<EntradaMenu> menus = new();
        private readonly UsuariosDataService UsuariosDataService = new();
        string rutUsuario = "";
        private int rolId;

        public ListarUsuarios()
        {
            InitializeComponent();
            CargarUsuarios();
        }
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

        public ListarUsuarios(int rolId)
        {
            this.rolId = rolId;
            //MenuDinamico md = new MenuDinamico(rolId);
            menus = PoblarListaEntradaMenus(rolId);

            if (menus.Count != 0)
            {
                MenuItem mantenedores = new()
                {
                    Header = "Mantenedores",
                    VerticalAlignment = VerticalAlignment.Top
                };
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
                this.AddChild(mantenedores);

                //this.Content = mantenedores;
            }
        }

        private async void CargarUsuarios()
        {
            //var usuarios = await UsuariosDataService.GetAllUsuariosAsync();
            //dgUsuarios.ItemsSource = usuarios.usuarios;
            //DataGridViewButtonCell buttonCell = new();
            //dgUsuarios.Columns["Modificar"].DefaultCellStyle = buttonCell;
            //dgUsuarios.Columns.Add(DataGridColumn);
            //dgUsuarios.Columns.Add("Eliminar", "Eliminar");

        }

        private void BtnAgregarUsuario_Click(object sender, EventArgs e)
        {
            CrearUsuario cU = new();
            cU.Show();
        }

        private void dgUsuarios_Enter(object sender, EventArgs e)
        {
            //rutUsuario = dgUsuarios.SelectedRows[0].Cells[0].ToString();
            //Console.Write(rutUsuario);
        }
    }
}
