<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Backend.Master" AutoEventWireup="true" CodeBehind="Usuarios.aspx.cs" Inherits="PresentacionWebForms.admin.Usuarios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="principal" runat="server">
    <section id="usuarios" class="col-xs-12">
        <h2>Usuarios</h2>
        <asp:HyperLink ID="btnAlta" runat="server" CssClass="btn btn-primary" Text="Nuevo Usuario" NavigateUrl="Usuario.aspx?op=alta" />
        <table class="table table-striped table-hover table-condensed">
            <thead>
                <tr>
                    <th>Id</th>
                    <th>Nombre y Apellidos</th>
                    <th>Nick</th>
                    <th>Password</th>
                    <td></td>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="rUsuarios" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td><%# Eval("Id") %></td>
                            <td>Nombre Apellidos</td>
                            <td><%# Eval("Nick") %></td>
                            <td><%# Eval("Password") %></td>
                            <td>
                                <a href="Usuario.aspx?id=<%# Eval("Id") %>&op=editar" class="btn btn-default">Editar</a>                                    
                                <a href="Usuario.aspx?id=<%# Eval("Id") %>&op=borrar" class="btn btn-danger">Borrar</a>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
            <tfoot>
            </tfoot>
        </table>
    </section>
</asp:Content>
