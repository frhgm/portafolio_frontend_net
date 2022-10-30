using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace app
{
    /// <summary>
    /// Esta clase provee utilidades especificas para las Ventanas/WPF
    /// </summary>
    public class UtilidadesVentanas
    {
        public static void ObtenerInstanciaVentana(string nombre)
        {
            Assembly assemby = Assembly.GetExecutingAssembly();
            System.Type[] types = assemby.GetTypes();
            var varWindows = types.ToList()
                .Where(current => current.BaseType == typeof(Window));
            varWindows.FirstOrDefault(x => x.Name == nombre);
            Type? ventana = Type.GetType(nombre, true);
            try
            {
                Window? v = Activator.CreateInstance(ventana) as Window;
                v.Show();

            }
            catch (Exception e)
            {

            }
        }



    }
}
