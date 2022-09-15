using app.Data.Implementations;
using classLibrary;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
        private string menuSeleccionado;

        
        public void MenuSeleccionadoSet(object sender, EventArgs e, string menu)
        {
            Utils.ObtenerInstanciaVentana(String.Concat("app.Usuarios.",menu));

        }
        public ListarUsuarios()
        {
            InitializeComponent();
        }
        public ListarUsuarios(int rolId)
        {
            InitializeComponent();
            this.rolId = rolId;
            menus = Utils.PoblarListaEntradaMenus(rolId);
            if (menus.Count != 0)
            {
                MenuItem mantenedores = new()
                {
                    Header = "Mantenedores",
                    VerticalAlignment = VerticalAlignment.Top
                };
                MenuItem acciones = new()
                {
                    Header = "Acciones",
                    VerticalAlignment = VerticalAlignment.Top
                };
                foreach (EntradaMenu menu in menus)
                {
                    MenuItem iterador = new()
                    {
                        Header = menu.Nombre
                    };
                    iterador.Click += (sender, e) => MenuSeleccionadoSet(sender, e, menu.Nombre);

                    if (menu.Id > 13)
                    {
                        acciones.Items.Add(iterador);
                    }
                    else
                    {
                        mantenedores.Items.Add(iterador);
                    }
                }
                //this.AddChild(mantenedores);
                
                menuPrincipal.Items.Add(acciones);
                menuPrincipal.Items.Add(mantenedores);
            }
            else
            {
                MessageBox.Show("Usted no tiene un rol asignado para acceder al sistema");
            }
            CargarUsuarios();
        }


        



        private async void CargarUsuarios()
        {
            //TODO Se debe anclar al final de la pagina
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
    }
}
