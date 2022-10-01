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
using classLibrary.DTO;
using System.Data;
using classLibrary.DTOs;

namespace app.Ventanas
{
    /// <summary>
    /// Interaction logic for ListarUsuario.xaml
    /// </summary>
    public partial class Usuarios
    {
        Usuario usuario = new("18392764-7", "Felipe", "Hites", "Ramirez", "framirezhites@maipogrande.cl", 9974303094, 1, "Administrador", DateTime.Today, "663401993");

        List<EntradaMenu> menus = new();
        private readonly UsuariosDataService UsuariosDataService = new();
        private int rolId;


        public void MenuSeleccionadoSet(object sender, EventArgs e, string menu)
        {
            Utils.ObtenerInstanciaVentana(String.Concat("app.Usuarios.", menu));
        }

        public void AgregarMenus()
        {
            menus = Utils.PoblarListaEntradaMenus();
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

                menuPrincipal.Items.Add(acciones);
                menuPrincipal.Items.Add(mantenedores);
            }
            else
            {
                MessageBox.Show("Usted no tiene un rol asignado para acceder al sistema");
            }
        }
       
        public void PoblarCombos()
        {
            List<Rol> roles = new List<Rol>();
            roles.Add(new Rol(1, "Administrador"));
            roles.Add(new Rol(2, "Consultor"));
            roles.Add(new Rol(3, "Transportista"));
            roles.Add(new Rol(4, "Productor"));
            roles.Add(new Rol(5, "Cliente interno"));
            roles.Add(new Rol(6, "Cliente externo"));
            //Cb_EditarRol.ItemsSource = roles;
            //Cb_EditarRol.DisplayMemberPath = "Nombre_Rol";
        }
        public Usuarios()
        {
            InitializeComponent();
            this.rolId = usuario.RolId;
            AgregarMenus();
            CargarUsuarios();
            PoblarCombos();
        }

        private async void CargarUsuarios()
        {
            //TODO Se debe anclar al final de la pagina
            var usuarios = await UsuariosDataService.TraerUsuarios();
            UsuariosDG.ItemsSource = usuarios.usuarios;
        }


        private void Btn_CrearUsuario_OnClick(object sender, RoutedEventArgs e)
        {
            CrearUsuario cU = new CrearUsuario();
            cU.Show();
        }

        private void SeleccionarUsuarioParaEditar_Click(object sender, RoutedEventArgs e)
        {

        }
        private void BorrarUsuario_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AgregarUsuario_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ActualizarUsuario_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UsuariosDG_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dg = (DataGrid)sender;
            Usuario fila = (Usuario)dg.SelectedItem;
            if (fila != null)
            {
                Mod_Rut.Text = fila.Rut;
                Mod_Nombre.Text = fila.Nombre;
                Mod_ApPa.Text = fila.ApellidoPaterno;
                Mod_ApMa.Text = fila.ApellidoMaterno;
                Mod_Email.Text = fila.Email;
                Mod_Telefono.Text = fila.Telefono.ToString();

                Mod_Rol.SelectedIndex = fila.RolId;

            }
        }
    }
}
