<%@ Page Language="C#" MasterPageFile="~/TiendaVirtual.Master" AutoEventWireup="true" CodeBehind="Ficha.aspx.cs" Inherits="PresentacionWebForms.Ficha" %>

<asp:Content runat="server" ContentPlaceHolderID="principal">
    <section id="ficha" class="row">
        <h2>Ficha de producto</h2>
        <div class="col-xs-12 col-sm-6">
            <asp:Image ID="imgProducto" runat="server" Height="300" width="400" />
        </div>
        <div class="col-xs-12 col-sm-6">
            <h2><asp:Label ID="lblNombre" runat="server" Text=""></asp:Label></h2>
            <h3><asp:Label ID="lblPrecio" runat="server" Text="" ClientIDMode="Static"></asp:Label></h3>
            <h3><asp:Label ID="lblTotal" runat="server" Text="" ClientIDMode="Static"></asp:Label></h3>
            <div id="frmCarrito" class="form-inline">
                <div class="form-group">
                    <asp:TextBox runat="server" CssClass="form-control" TextMode="Number" 
                        name="cantidad" id="cantidad" value="1" ClientIDMode="Static" />                   
                    <button class="btn btn-default" id="btnAumentarCantidad"><span class="glyphicon glyphicon-plus"></span></button>
                    <button class="btn btn-default" id="btnReducirCantidad"><span class="glyphicon glyphicon-minus"></span></button>
                    <asp:LinkButton runat="server" CssClass="btn btn-primary" 
                        ID="btnCarrito"
                        Text='Carrito <span class="glyphicon glyphicon-shopping-cart"></span>'
                        OnCommand="btnCarrito_Command"
                        />
                </div>
            </div>
        </div>
    </section>
</asp:Content>
