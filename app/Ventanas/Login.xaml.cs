using classLibrary;
using classLibrary.DataServices;
using classLibrary.DTOs;
using System.Windows;

namespace app.Ventanas
{
    public partial class Login : Window
    {
        App _app = ((App)Application.Current);
        public Login()
        {
            InitializeComponent();
            TxtRut.Text = "18392764-7";
            TxtPass.Password = "framirez";
        }
        

        private async void BtnLogin_OnClickAsync(object sender, RoutedEventArgs e)
        {
            if (!Rut.RutValido(TxtRut.Text))
            {
                MessageBox.Show("Debe ingresar un rut valido");
                return;
            }
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
                var usuario = await _app.usuarioDataService.Login(TxtRut.Text, TxtPass.Password);

                if (usuario.Rut == "")
                {
                    MessageBox.Show("Credenciales incorrectas");
                }
                else
                {
                    if (usuario.RolId != 1)
                    {
                        MessageBox.Show("Debe ingresar por el sitio web, cerrando...");
                        this.Close();
                    }
                    //TODO Enviar usuario recuperado a Lista
                    Usuarios u = new Usuarios();
                    u.SetUsuario(usuario);

                    u.Show();
                }
            }

        }
    }
}