<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FichaAnterior.aspx.cs" Inherits="FichaAnterior" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Ficha</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>Ficha</h1>

        </div>
        <asp:Label ID="lblId" runat="server"></asp:Label>
        <br />
        <asp:Label ID="lblNombre" runat="server" Text=""></asp:Label>
        <br />
        <asp:Label ID="lblPrecio" runat="server" Text=""></asp:Label>
        <br />
        <asp:Image ID="imgProducto" runat="server" />
    </form>
</body>
</html>
