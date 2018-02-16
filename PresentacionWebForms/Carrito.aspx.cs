using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TiendaVirtual.Entidades;
using TiendaVirtual.LogicaNegocio;

namespace PresentacionWebForms
{
    public partial class Carrito : System.Web.UI.Page
    {
        ILogicaNegocio ln;
        ICarrito carrito;

        protected void Page_Load(object sender, EventArgs e)
        {
            ln = (ILogicaNegocio)Application["logicaNegocio"];
            carrito = (ICarrito)Session["carrito"];

            if(carrito == null)
            {
                Response.Redirect("~/Default.aspx");
                return;
            }

            int id, cantidad;
            
            if(!IsPostBack && Request["id"] != null && Request["cantidad"] != null)
            {
                id = int.Parse(Request["id"]);
                cantidad = int.Parse(Request["cantidad"]);

                //HACK: Bucle para añadir una cantidad de productos
                var producto = ln.BuscarProductoPorId(id);

                for (int i = 0; i < cantidad; i++)
                    ln.AgregarProductoACarrito(producto, carrito);
                    //carrito.Add(producto);
            }

            rCarrito.DataSource = carrito.LineasFactura;
            rCarrito.DataBind();

            lblTotal.Text = string.Format("{0:c}", carrito.Total);
        }

        protected void btnFactura_Click(object sender, EventArgs e)
        {
            IFactura factura = ln.FacturarCarrito(carrito);

            Session["factura"] = factura;

            Response.Redirect("~/Factura.aspx");
        }
    }
}