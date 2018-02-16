using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TiendaVirtual.Entidades;
using TiendaVirtual.LogicaNegocio;

namespace PresentacionWebForms.admin
{
    public partial class Producto : System.Web.UI.Page
    {
        ILogicaNegocio ln;
        string op;
        int id;

        protected void Page_Load(object sender, EventArgs e)
        {
            ln = (ILogicaNegocio)Application["logicaNegocio"];

            op = Request["op"];

            if (Request["id"] != null)
                id = int.Parse(Request["id"]);

            if (!IsPostBack && op != null)
            {
                if (op == "editar" || op == "borrar")
                {
                    IProducto producto = ln.BuscarProductoPorId(id);

                    txtNombre.Text = producto.Nombre;
                    txtPrecio.Text = producto.Precio.ToString();
                }

                switch (op)
                {
                    case "editar":
                        txtNombre.Enabled = true;
                        txtPrecio.Enabled = true;

                        break;
                    case "borrar":
                        txtNombre.Enabled = false;
                        txtPrecio.Enabled = false;

                        btn.Text = "Borrar";
                        btn.CssClass = btn.CssClass + " btn-danger";

                        break;
                    case "alta":
                        break;
                    default:
                        break;
                }

            }
        }

        protected void btn_Click(object sender, EventArgs e)
        {
            switch (op)
            {
                case "alta":
                    ln.AltaProducto(new global::TiendaVirtual.Entidades.Producto(
                        id, txtNombre.Text, decimal.Parse(txtPrecio.Text)));
                    break;
                case "editar":
                    ln.ModificarProducto(new global::TiendaVirtual.Entidades.Producto(
                        id, txtNombre.Text, decimal.Parse(txtPrecio.Text)));
                    break;
                case "borrar":
                    ln.BajaProducto(id);
                    break;
                default:
                    break;
            }

            Response.Redirect("Productos.aspx");
        }
    }
}
