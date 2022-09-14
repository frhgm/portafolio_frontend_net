using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app.Data
{
    public class EntradaMenu
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        public EntradaMenu(int Id, string Nombre)
        {
            this.Id = Id;
            this.Nombre = $"{Nombre}.xaml";
        }
    }
}
