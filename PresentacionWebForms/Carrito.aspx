<%@ Page Title="" Language="C#" MasterPageFile="~/TiendaVirtual.Master" AutoEventWireup="true" CodeBehind="Carrito.aspx.cs" Inherits="PresentacionWebForms.Carrito" %>

<asp:Content ID="Content2" ContentPlaceHolderID="principal" runat="server">
    <section id="carrito" class="row">
        <h2>Contenido del carrito</h2>
        <table class="table table-striped table-hover table-condensed">
            <thead>
                <tr>
                    <th>Imagen</th>
                    <th>Artículo</th>
                    <th>Cantidad</th>
                    <th>Precio</th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater runat="server" ID="rCarrito">
                    <ItemTemplate>
                        <tr>
                            <td>
                            <asp:Image runat="server" 
                                ImageUrl='<%# "~/fotos/" + Eval("Producto.Id") + ".png" %>'
                                CssClass="thumbnail" Height="30" Width="40"
                            />

                            </td>
                            <td><%# Eval("Producto.Nombre") %>
                            </td>
                            <td><%# Eval("Cantidad") %>
                            </td>
                            <td><%# Eval("Total", "{0:c}") %>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
            <tfoot>
                <tr>
                    <td></td>
                    <td></td>
                    <th>Total</th>
                    <td><asp:Label ID="lblTotal" runat="server" /></td>
                </tr>
                <tr>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td>
                        <asp:Button CssClass="btn btn-primary" Id="btnFactura" runat="server" Text="Factura" OnClick="btnFactura_Click" />
                    </td>
                </tr>
            </tfoot>
        </table>
    </section>

</asp:Content>
