using app.Data.Implementations;
using app.Usuarios;
using System.Threading.Tasks;
using System.Windows;

namespace app
{
    public partial class Login : Window
    {
        UsuariosDataService UsuariosDataService = new();
        public Login()
        {
            InitializeComponent();
        }

        private async void BtnLogin_OnClickAsync(object sender, RoutedEventArgs e)
        {
            if (TxtRut.Text == string.Empty || TxtPass.Text == string.Empty)
            {
                MessageBox.Show("Debe ingresar sus credenciales");
            }
            else if (!TxtRut.Text.Contains('-'))
            {
                MessageBox.Show("Debe ingresar su Rut con guión (-) antes del DV");
            }
            else
            {
                var usuario = await UsuariosDataService.Login(TxtRut.Text, TxtPass.Text);

                if (usuario == null)
                {
                    MessageBox.Show("Credenciales incorrectas");
                }
                else
                {
                    //MessageBox.Show("Exito!");
                    ListarUsuarios lu = new(usuario.RolId);
                    //{
                    //    Content = new MenuDinamico(usuario.RolId)
                    //};

                    lu.Show();
                    Login l = new Login();
                    l.Close();
                }
            }

        }
    }
}