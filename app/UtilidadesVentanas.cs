using classLibrary;
using classLibrary.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace app
{
    /// <summary>
    /// Esta clase provee utilidades especificas para las Ventanas/WPF
    /// </summary>
    public class UtilidadesVentanas
    {
        public static Window ObtenerInstanciaVentana(string nombre)
        {
            Assembly assemby = Assembly.GetExecutingAssembly();
            System.Type[] types = assemby.GetTypes();
            var varWindows = types.ToList()
                .Where(current => current.BaseType == typeof(Window));
            Type? ventana = Type.GetType(String.Concat("app.Ventanas.", nombre), true);
            try
            {
                Window? v = Activator.CreateInstance(ventana) as Window;
                return v;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public void MenuSeleccionadoSet(object sender, EventArgs e, string menuOriginal, string menuDestino)
        {
            var ventanaDestino = ObtenerInstanciaVentana(menuDestino);
            var ventanaOriginal = ObtenerInstanciaVentana(menuOriginal);
            ventanaOriginal.Close();
            // TODO Por alguna razon, no quiere cerrar la ventana original. Agradezco ideas!
            ventanaDestino.Show();
        }

        /// <summary>
        /// Este metodo pobla MenuItems con las repectivas acciones y mantenedores
        /// </summary>
        /// <returns>Devuelve una tupla, donde la primera posicion son los menus de acciones,<br>y la segunda los menus de mantenedores</br></returns>
        public Tuple<MenuItem, MenuItem> AgregarMenus(string menuOriginal)
        {
            var menus = UtilidadesLogica.PoblarListaEntradaMenus();
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
                    iterador.Click += (sender, e) => MenuSeleccionadoSet(sender, e, menuOriginal, menu.Nombre);

                    if (menu.Id > 11)
                    {
                        acciones.Items.Add(iterador);
                    }
                    else
                    {
                        mantenedores.Items.Add(iterador);
                    }
                }

                Tuple<MenuItem, MenuItem> tuplaMenus = new Tuple<MenuItem, MenuItem>(acciones, mantenedores);

                return tuplaMenus;
            }
            else
            {
                MessageBox.Show("Usted no tiene un rol asignado para acceder al sistema");
                return null;
            }
        }




    }
}
