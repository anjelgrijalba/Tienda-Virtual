<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DemoObjectDataSource.aspx.cs" Inherits="PresentacionWebForms.DemoObjectDataSource" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" DataObjectTypeName="TiendaVirtual.Entidades.Usuario" DeleteMethod="Baja" InsertMethod="Alta" SelectMethod="BuscarTodos" TypeName="TiendaVirtual.AccesoDatos.DaoUsuarioColecciones" UpdateMethod="Modificacion">
                <DeleteParameters>
                    <asp:Parameter Name="id" Type="Int32" />
                </DeleteParameters>
            </asp:ObjectDataSource>
            <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False" DataSourceID="ObjectDataSource1">
                <Columns>
                    <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
                    <asp:BoundField DataField="Id" HeaderText="Id" SortExpression="Id" />
                    <asp:BoundField DataField="Nick" HeaderText="Nick" SortExpression="Nick" />
                    <asp:BoundField DataField="Password" HeaderText="Password" SortExpression="Password" />
                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>
