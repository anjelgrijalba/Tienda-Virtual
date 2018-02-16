<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DefaultBasico.aspx.cs" Inherits="PresentacionWebForms.DefaultBasico" EnableViewState="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Tienda Virtual</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>Tienda Virtual</h1>
            <p>Bienvenido <asp:Label ID="lblUsuario" runat="server" Text="Label"></asp:Label></p>
        </div>
        <asp:GridView ID="gvProductos" runat="server" AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField DataField="Precio" HeaderText="Precio" DataFormatString="{0:c}" />
                <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                <asp:HyperLinkField Text="Comprar" 
                    DataNavigateUrlFormatString="~/Ficha.aspx?id={0}"      
                    DataNavigateUrlFields="Id" />
            </Columns>
        </asp:GridView>
    </form>
</body>
</html>
