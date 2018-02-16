<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Backend.Master" AutoEventWireup="true" CodeBehind="Usuario.aspx.cs" Inherits="PresentacionWebForms.admin.Usuario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="principal" runat="server">
    <section id="usuario" class="col-xs-12">
        <h2>Usuario</h2>
        <div class="form-horizontal">
            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="txtUsuario" CssClass="col-sm-3 control-label">Usuario</asp:Label>
                <div class="col-sm-9">
                    <asp:TextBox runat="server" CssClass="form-control" ID="txtUsuario" />
                </div>
            </div>
            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="txtPassword" CssClass="col-sm-3 control-label">Contraseña</asp:Label>
                <div class="col-sm-9">
                    <asp:TextBox runat="server" CssClass="form-control" ID="txtPassword" TextMode="Password" />
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
