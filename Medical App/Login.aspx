<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Medical_App.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Styles/Style.css" rel="stylesheet" />
</head>
<body>
    <h2>Iniciar sesión</h2>
    <div class="container">
        <form runat="server" id="formLogin">
            <asp:TextBox ID="txtLogin" runat="server" CssClass="txtClass" placeholder="Usuario"></asp:TextBox>
            <asp:TextBox ID="txtPassword" runat="server" CssClass="txtClass" TextMode="Password" placeholder="Password"></asp:TextBox>
            <asp:Button ID="btnLogin" runat="server" Text="Ingresar" CssClass="btnClass"  />
        </form>
    </div>

    <div class="dialog-container" id="divMensaje" style="display: none;" runat="server">
        <div class="message-box">
            <div id="mensajeContenido">
                <span id="mensajeTexto" runat="server"></span>
                <button id="cerrarMensaje" class="btnClass btnMensaje" onclick="cerrarMensaje()">Cerrar</button>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
