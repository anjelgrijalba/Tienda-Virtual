using System;
using System.Collections.Generic;

namespace TiendaVirtual.Entidades
{
    public class Factura : Compra, IFactura
    {
        public Factura(IUsuario usuario) : base(usuario)
        {
        }
        
       
        public string Numero { get; set; }
        public DateTime Fecha { get; set; }

        public void ImportarLineas(IEnumerable<ILineaFactura> lineas)
        {
            lineasFactura.Clear();

            foreach (ILineaFactura linea in lineas)
                lineasFactura.Add(linea.Producto.Id, linea);
        }
        //public IEnumerable<ILineaFactura> MostrarLineas(IFactura factura)
        //{
           
        //    foreach (ILineaFactura linea in factura)
        //        lineasFactura.Add(linea.Producto.Id, linea);
        //    return 
        //}

        public override string ToString()
        {
           
            return $"{Numero}\n{Fecha}\n{Usuario}\n{string.Join("\n",LineasFactura)}\n{ImporteSinIva}\n{Iva}\n{Total}";
        }
    }
}
