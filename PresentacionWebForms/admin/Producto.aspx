<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Backend.Master" AutoEventWireup="true" CodeBehind="Producto.aspx.cs" Inherits="PresentacionWebForms.admin.Producto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="principal" runat="server">
    <section id="producto" class="col-xs-12">
        <h2>Producto</h2>
        <div class="form-horizontal">
            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="txtNombre" CssClass="col-sm-3 control-label">Nombre</asp:Label>
                <div class="col-sm-9">
                    <asp:TextBox runat="server" CssClass="form-control" ID="txtNombre" />
                </div>
            </div>
            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="txtPrecio" CssClass="col-sm-3 control-label">Precio</asp:Label>
                <div class="col-sm-9">
                    <asp:TextBox runat="server" CssClass="form-control" ID="txtPrecio" />
                </div>
            </div>
            <div class="form-group">
                <div class="col-sm-offset-3 col-sm-9">
                    <asp:Button ID="btn" runat="server" CssClass="btn btn-primary" Text="Guardar" OnClick="btn_Click" />
                    <asp:Label ID="lblResultado" runat="server">&nbsp;</asp:Label>
                </div>
            </div>
        </div>
    </section>
</asp:Content>

