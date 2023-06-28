<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Catalogos.aspx.cs" Inherits="Medical_App.Catalogos" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Catalogos de consultas</title>
    <link href="Styles/Style.css" rel="stylesheet" />
    <script src="General/JavaScript.js"></script>
</head>
<body>
    <h1>Consultas</h1>
    <div class="containerTable">
        <form id="formCatalogo" runat="server">
            <div class="container">
                <div>
                    <h3>Informacion de pacientes</h3>
                    <div>
                        <asp:Button ID="btnPacientes" runat="server" Text="Mostrar" OnClick="btnPacientes_Click" CssClass="btnClass" />
                    </div>
                </div>
                  <div class="grid-container">
                    <asp:GridView ID="gridListaPacientes" runat="server" CssClass="gridListaPacientes" OnRowCommand="gridListaPacientes_RowCommand"></asp:GridView>
                </div>
                <div class="divider"></div>
                <div>
                    <h3>Sucursales</h3>
                    <div>
                        <asp:Button ID="btnSucursales" runat="server" Text="Mostrar" OnClick="btnSucursales_Click" CssClass="btnClass" />
                    </div>
                     </div>
                      <div class="grid-container">
                    <asp:GridView ID="gridSucursales" runat="server" CssClass="gridSucursales"></asp:GridView>
                </div>
                <div class="divider"></div>
                <div>
                    <h3>Enfermedades</h3>
                    <div>
                        <asp:Button ID="btnEnfermedades" runat="server" Text="Mostrar" OnClick="btnEnfermedades_Click" CssClass="btnClass" />
                    </div>
                     </div>
                    <div class="grid-container">
                    <asp:GridView ID="gridListaEnfermedades" runat="server" CssClass="gridListaEnfermedadess"></asp:GridView>
                </div>
              
                <div class="divider"></div>
                <div>
                    <h3>Medicamentos</h3>
                    <div>
                        <asp:Button ID="btnMedicamentos" runat="server" Text="Mostrar" OnClick="btnMedicamentos_Click" CssClass="btnClass" />
                    </div>
                </div>
                <div class="grid-container">
                    <asp:GridView ID="gridMedicamentos" runat="server" CssClass="gridMedicamentos"></asp:GridView>
                </div>
                <div class="clear"></div>
                <label id="labelInfoCargada" runat="server"></label>
            </div>
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
</body>
</html>
