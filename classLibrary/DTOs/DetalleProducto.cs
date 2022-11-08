namespace classLibrary.DTOs
{
    public class DetalleProducto
    {
        public int ProductoId { get; set; }
        public string NombreProducto { get; set; }
        public int Calidad { get; set; }
        public int Cantidad { get; set; }

        public DetalleProducto(int productoId, string nombreProducto, int calidad, int cantidad)
        {
            ProductoId = productoId;
            NombreProducto = nombreProducto;
            Calidad = calidad;
            Cantidad = cantidad;
        }
    }
}