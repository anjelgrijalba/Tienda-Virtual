<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Backend.Master" AutoEventWireup="true" CodeBehind="Productos.aspx.cs" Inherits="PresentacionWebForms.admin.Productos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="principal" runat="server">
    <section id="productos" class="col-xs-12">
        <h2>Productos</h2>
        <asp:HyperLink ID="btnAlta" runat="server" CssClass="btn btn-primary" Text="Nuevo Producto" NavigateUrl="Producto.aspx?op=alta" />
        <asp:GridView ID="gvProductos" CssClass="table" runat="server" AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField DataField="Id" HeaderText="Id" />
                <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                <asp:BoundField DataField="Precio" HeaderText="Precio" DataFormatString="{0:c}" />
                <%--<asp:TemplateField HeaderText="Opciones" SortExpression="Id">
                    <ItemTemplate>
                        <asp:HyperLink runat="server" Text="Editar" CssClass="btn btn-default"
                            NavigateUrl='Producto.aspx?id=<%# Eval("Id") %>&op=editar'
                             />

                        <asp:HyperLink runat="server" Text="Borrar" CssClass="btn btn-danger"
                            DataNavigateUrlFormatString="Producto.aspx?id={0}&op=borrar"
                            DataNavigateUrlFields="Id" />
                    </ItemTemplate>
                </asp:TemplateField>--%>
                <asp:HyperLinkField ItemStyle-CssClass="text-center" Text="Editar"
                    DataNavigateUrlFormatString="Producto.aspx?id={0}&op=editar"
                    DataNavigateUrlFields="Id">
                    <ControlStyle CssClass="btn btn-default"></ControlStyle>
                </asp:HyperLinkField>
                <asp:HyperLinkField Text="Borrar" ItemStyle-CssClass="text-center"
                    DataNavigateUrlFormatString="Producto.aspx?id={0}&op=borrar"
                    DataNavigateUrlFields="Id">
                    <ControlStyle CssClass="btn btn-danger"></ControlStyle>
                </asp:HyperLinkField>
            </Columns>
        </asp:GridView>
    </section>
</asp:Content>
