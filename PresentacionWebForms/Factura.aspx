<%@ Page Title="" Language="C#" MasterPageFile="~/TiendaVirtual.Master" AutoEventWireup="true" CodeBehind="Factura.aspx.cs" Inherits="PresentacionWebForms.Factura" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="principal" runat="server">
    <section id="factura" class="row">
        <h2>Factura</h2>
        <div id="datosvendedor" class="col-xs-6 col-sm-8">
            <p>Vendedor Vendedorez (CopyPasteShop)</p>
            <p>Su dirección</p>
            <p>44444</p>
            <p>Su Localidad</p>
            <p>12.345.678Z</p>
            <p>12/12/17</p>
            <p>Factura: CPS/0045/2017</p>
        </div>
        <div id="datoscliente" class="col-xs-6 col-sm-4">
            <p>
                <asp:Label ID="lblUsuario" runat="server" /></p>
            <p>Su dirección</p>
            <p>44444</p>
            <p>Su Localidad</p>
            <p>12.345.678Z</p>
        </div>
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
                <asp:Repeater runat="server" ID="rFactura">
                    <ItemTemplate>
                        <tr>
                            <td>
                                <asp:Image runat="server"
                                    ImageUrl='<%# "~/fotos/" + Eval("Producto.Id") + ".png" %>'
                                    CssClass="thumbnail" Height="30" Width="40" />
                            </td>
                            <td><%# Eval("Producto.Nombre") %>
                            </td>
                            <td><%# Eval("Cantidad") %>
                            </td>
                            <td><%# Eval("Total", "{0:c}") %>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
            <tfoot>
                <tr>
                    <td></td>
                    <td></td>
                    <th>Base Imponible</th>
                    <th><asp:Label ID="lblBaseImponible" runat="server" /></th>
                </tr>
                <tr>
                    <td></td>
                    <td></td>
                    <th>IVA</th>
                    <th><asp:Label ID="lblIva" runat="server" />
                    </th>
                </tr>
                <tr>
                    <td></td>
                    <td></td>
                    <th>Total</th>
                    <th><asp:Label ID="lblTotal" runat="server" />
                    </th>
                </tr>
            </tfoot>
        </table>
    </section>
</asp:Content>
