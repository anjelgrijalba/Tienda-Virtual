using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TiendaVirtual.Entidades;

namespace PresentacionWebForms
{
    public partial class Factura : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            IFactura factura = (IFactura)Session["factura"];

            if(factura == null)
            {
                Response.Redirect("~/Default.aspx");
                return;
            }
            else
            {
                Session["carrito"] = new global::TiendaVirtual.Entidades.Carrito(factura.Usuario);
            }

            lblUsuario.Text = factura.Usuario.Nick;
            lblBaseImponible.Text = string.Format("{0:c}", factura.ImporteSinIva);
            lblIva.Text = string.Format("{0:c}", factura.Iva);
            lblTotal.Text = string.Format("{0:c}", factura.Total);

            rFactura.DataSource = factura.LineasFactura;
            rFactura.DataBind();
        }
    }
}