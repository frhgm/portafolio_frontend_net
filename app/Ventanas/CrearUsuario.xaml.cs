using app.Data.Implementations;
using classLibrary.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
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

namespace app.Ventanas
{
    /// <summary>
    /// Interaction logic for CrearUsuario.xaml
    /// </summary>
    public partial class CrearUsuario : Window
    {
        UsuariosDataService usuarioDataService = new UsuariosDataService();
        public CrearUsuario()
        {
            InitializeComponent();
        }

        private void BtnAgregarUsuario_Click(object sender, EventArgs e)
        {
            //Usuario usuario = new Usuario()
            //{
            //    Rut = TxtRut.Text,
            //    Nombre = TxtNombre.Text,
            //    ApellidoPaterno = TxtApellidoPaterno.Text,
            //    ApellidoMaterno = TxtApellidoMaterno.Text,
            //    Email = TxtEmail.Text,
            //    Telefono = long.Parse(TxtTelefono.Text),
            //    RolId = int.Parse(TxtRol.Text),
            //    FechaCreacion = DateTime.Today
            //};
        }

        private async void Btn_RegistrarUsuario_Click(object sender, RoutedEventArgs e)
        {
            UsuarioSalida usuarioPorRegistrar = new(
                Txt_Rut.Text,
                Txt_Nombre.Text,
                Txt_ApellidoPaterno.Text,
                Txt_ApellidoMaterno.Text,
                Txt_eMail.Text,
                Convert.ToInt64(Txt_Telefono.Text),
                Convert.ToInt32(Txt_Rol.Text),
                "Admin",
                Convert.ToDateTime(Txt_FechaNacimiento.Text),
                Txt_Clave.Text);
            await usuarioDataService.CrearUsuario(usuarioPorRegistrar);
        }
    }
}
