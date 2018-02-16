using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaVirtual.Entidades
{
    public class Compra : ICompra
    {
        private const decimal IVA = 0.21m;

        protected IDictionary<int, ILineaFactura> lineasFactura = new Dictionary<int, ILineaFactura>();

        public Compra(IUsuario usuario) => Usuario = usuario;

        public IUsuario Usuario { get; set; }

        public IEnumerable<ILineaFactura> LineasFactura =>
            lineasFactura.Values.ToArray<ILineaFactura>();

        public decimal ImporteSinIva
        {
            get
            {
                decimal importeSinIva = 0.0m;

                foreach (ILineaFactura linea in lineasFactura.Values)
                    importeSinIva += linea.ImporteSinIva;

                return importeSinIva;
            }
        }

        public decimal Iva => ImporteSinIva * IVA;

        public decimal Total => ImporteSinIva + Iva;

        public void Add(IProducto producto)
        {
            Add(producto, 1);

            //if (lineasFactura.ContainsKey(producto.Id))
            //    lineasFactura[producto.Id].Cantidad++;
            //else
            //    lineasFactura.Add(producto.Id, new LineaFactura(producto, 1));

        }

        public void Add(IProducto producto, int cantidad)
        {
            if (lineasFactura.ContainsKey(producto.Id))
                lineasFactura[producto.Id].Cantidad += cantidad;
            else
                lineasFactura.Add(producto.Id, new LineaFactura(producto, cantidad));
        }

        public (decimal ImporteSinIva, decimal Iva, decimal Total) CalcularTotales()
        {
            var res =  (ImporteSinIva: 0.0m, Iva: 0.0m, Total: 0.0m);

            foreach (ILineaFactura linea in lineasFactura.Values)
                res.ImporteSinIva += linea.ImporteSinIva;

            res.Iva = ImporteSinIva * IVA;
            res.Total = res.ImporteSinIva + res.Iva;

            return res;
        }
    }
}
