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
        private readonly UsuariosDataService UsuariosDataService = new();
        string rutUsuario = "";
        public ListarUsuarios()
        {
            InitializeComponent();
            CargarUsuarios();
        }
        private async void CargarUsuarios()
        {
            var usuarios = await UsuariosDataService.GetAllUsuariosAsync();
            dgUsuarios.ItemsSource = usuarios.usuarios;
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
