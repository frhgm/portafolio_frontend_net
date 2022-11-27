using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using classLibrary.DTO;
using classLibrary.DTOs;
using System.Diagnostics;
using System.IO;
using classLibrary.DataServices;

namespace app.Ventanas
{
    /// <summary>
    /// Interaction logic for Productos.xaml
    /// </summary>
    public partial class Productos
    {
        UtilidadesVentanas utilidadesVentanas = new UtilidadesVentanas();
        string urlImagenGuardada = "";
        string urlFilaImagenSeleccionada = "";
        int idSeleccionado = 0;
        List<EntradaMenu> menus = new();
        private readonly ProductosDataService dataService = new();
        private int rolId;


        public Productos()
        {
            InitializeComponent();

            // TODO Recuperar nombre de ventana actual, asi si cambia el nombre no se caera al invocarla
            var menus = utilidadesVentanas.AgregarMenus("Productos");
            menuPrincipal.Items.Add(menus.Item1);
            menuPrincipal.Items.Add(menus.Item2);

            CargarProductos();
        }

        /// <summary>
        /// Se llama al metodo TraerProductos, que va a buscar al servidor sp_get_all_productos, y pobla el DataGrid con esta lista
        /// </summary>
        private async void CargarProductos()
        {
            ProductosDG.ItemsSource = null;
            var productos = await dataService.TraerProductos();
            ProductosDG.ItemsSource = productos.productos;
        }



        private async void AddImagen_Click(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog 
            OpenFileDialog dlg = new()
            {
                // Set filter for file extension and default file extension 
                DefaultExt = ".jpg",
                Filter = "JPEG Files (*.jpg)|*.jpg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif"
            };


            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();


            // Get the selected file name and display in a TextBox 
            string filename = "No se guardo imagen";
            if (result.HasValue && result.Value)
            {
                // Open document 
                filename = dlg.FileName;
                Byte[] bytes = File.ReadAllBytes(filename);
                string base64 = Convert.ToBase64String(bytes);

                ExternalAPIsDataService external = new();
                // Envia imagen como base64 y nombre de archivo
                string nombreSinExtension = Path.GetFileNameWithoutExtension(filename);
                var respuestaAPI = await external.SubirImagen(base64, nombreSinExtension);

                if (respuestaAPI is null)
                {
                    MessageBox.Show("No se pudo subir imagen a API, intente nuevamente por favor");
                    return;
                }
                urlImagenGuardada = respuestaAPI.Data.Url;

                Add_Imagen.Content = nombreSinExtension + " subida";
                Debug.WriteLine(urlImagenGuardada);
            }
            Debug.WriteLine("aosa");
        }
        private async void AgregarProducto_Click(object sender, RoutedEventArgs e)
        {

            if (urlImagenGuardada == "")
            {
                MessageBox.Show("Debe haber seleccionado una imagen para agregar un producto");
                return;
            }
            RegistrarProducto productoPorRegistrar = new(
                Add_NombreProducto.Text,
                Add_Descripcion.Text,
               urlImagenGuardada
            );
            bool productoCreado = await dataService.CrearProducto(productoPorRegistrar);
            if (productoCreado)
            {
                //TODO Idealmente, simplemente agregar nueva fila https://stackoverflow.com/questions/24095172/how-i-can-add-new-row-into-datagrid-in-wpf
                CargarProductos();
                MessageBox.Show("Producto registrado exitosamente!");
            }
            else
            {
                MessageBox.Show("No se creó producto, intente nuevamente");
            }
        }

        private async void BorrarProducto_Click(object sender, RoutedEventArgs e)
        {
            Producto data = (sender as FrameworkElement).DataContext as Producto;

            var eleccion = MessageBox.Show("Seguro que desea eliminar el producto?", "Seleccione una opcion", MessageBoxButton.YesNo);

            if (eleccion == MessageBoxResult.Yes)
            {
                bool respuesta = await dataService.BorrarProducto(data.Id);
                if (respuesta)
                {
                    MessageBox.Show("Producto eliminado correctamente");
                    CargarProductos();
                }
                else
                {
                    MessageBox.Show("Ocurrio un error, intentar nuevamente por favor");
                }
            }
        }

        private void RefrescarTabla_Click(object sender, RoutedEventArgs e)
        {
            CargarProductos();
        }
        private async void ActualizarProducto_Click(object sender, RoutedEventArgs e)
        {
            ActualizarProducto productoPorActualizar = new(
                idSeleccionado,
                Mod_NombreProducto.Text,
                Mod_Descripcion.Text,
                urlFilaImagenSeleccionada);
            bool productoActualizado = await dataService.ActualizarProducto(productoPorActualizar);
            if (productoActualizado)
            {
                //TODO Idealmente, simplemente agregar nueva fila https://stackoverflow.com/questions/24095172/how-i-can-add-new-row-into-datagrid-in-wpf
                MessageBox.Show("Producto actualizado exitosamente!");
                CargarProductos();
            }
            else
            {
                MessageBox.Show("No se actualizó producto, intente nuevamente");
            }
        }

        private void ProductosDG_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dg = (DataGrid)sender;
            Producto fila = (Producto)dg.SelectedItem;
            if (fila != null)
            {
                Mod_NombreProducto.Text = fila.NombreProducto;
                Mod_Descripcion.Text = fila.Descripcion;
                idSeleccionado = fila.Id;
                urlFilaImagenSeleccionada = fila.Imagen;
            }
        }
    }
}
