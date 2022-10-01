using app.Data.Implementations;
using classLibrary.DTO;
using System.Threading.Tasks;
using System.Windows;

namespace app.Ventanas
{
    public partial class Login : Window
    {
        UsuariosDataService usuarioDataService = new();

        public Login()
        {
            InitializeComponent();
        }

        private async void BtnLogin_OnClickAsync(object sender, RoutedEventArgs e)
        {
            if (TxtRut.Text == string.Empty || TxtPass.Password == string.Empty)
            {
                MessageBox.Show("Debe ingresar sus credenciales");
            }
            else if (!TxtRut.Text.Contains('-'))
            {
                MessageBox.Show("Debe ingresar su Rut con guión (-) antes del DV");
            }
            else
            {
                var usuario = await usuarioDataService.Login(TxtRut.Text, TxtPass.Password);

                if (usuario == null)
                {
                    MessageBox.Show("Credenciales incorrectas");
                }
                else
                {
                    //MessageBox.Show("Exito!");
                    Usuarios u = new();
                    //{
                    //    Content = new MenuDinamico(usuario.RolId)
                    //};

                    u.Show();
                    Login l = new Login();
                    l.Close();
                }
            }

        }
    }
}