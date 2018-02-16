<%@ Page Title="" Language="C#" MasterPageFile="~/TiendaVirtual.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="PresentacionWebForms.Default" %>
<asp:Content ID="Content2" ContentPlaceHolderID="principal" runat="server">
    <section id="index" class="row text-center">
        <h2>Listado de productos</h2>
        <asp:Repeater ID="rProductos" runat="server">
            <ItemTemplate>
                <div class="col-md-4">
                    <div class="thumbnail">
                        <asp:Image runat="server" 
                            ImageUrl='<%# "~/fotos/" + Eval("Id") + ".png" %>'
                            CssClass="thumbnail" Height="150" Width="200"
                            />
                        <div class="caption">
                            <h3><%# Eval("Nombre") %></h3>
                            <h4><%# Eval("Precio", "{0:c}") %></h4>
                            <p><asp:HyperLink runat="server" 
                                NavigateUrl='<%# "~/Ficha.aspx?id=" + Eval("Id") %>' 
                                CssClass="btn btn-primary" role="button">Comprar</asp:HyperLink></p>
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </section>
</asp:Content>
