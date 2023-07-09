<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Expediente.aspx.cs" Inherits="Medical_App.Expediente" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Styles/Style.css" rel="stylesheet" />
</head>
<body>

    <form id="Expediente" runat="server">
   <div class="table-container">
    <table>
        <thead>
            <tr>
                <th colspan="2">
                    <div class="header-container">
                        <h2>Informacion del paciente</h2>
                    </div>
                </th>
            </tr>
        </thead>
        <tbody>
            <tr>
                <td style="font-weight: bold;">Nombre del paciente:</td>
                <td><asp:Label ID="lblPaciente" runat="server" Text="Label"></asp:Label></td>
            </tr>
            <tr>
                <td style="font-weight: bold;">Fecha de Nacimiento:</td>
                <td><asp:Label ID="lblDOB" runat="server" Text="Label"></asp:Label></td>
            </tr>
        </tbody>
    </table>

    <table>
        <thead>
            <tr>
                <th colspan="3">
                    <div class="header-container">
                        <h2>Informacion del Medico</h2>
                    </div>
                </th>
            </tr>
        </thead>
        <tbody>
            <tr>
                <td style="font-weight: bold;">Nombre del medico:</td>
                <td><asp:Label ID="lblNombreMedico" runat="server" Text="Label"></asp:Label></td>
            </tr>

            <tr>
                <td style="font-weight: bold;">Especialidad:</td>
                <td><asp:Label ID="lblEspecialidad" runat="server" Text="Label"></asp:Label></td>
            </tr>

             <tr>
                <td style="font-weight: bold;">Enfermedad:</td>
                <td><asp:Label ID="lblEnfermedad" runat="server" Text="Label"></asp:Label></td>
            </tr>

            <tr>
                <td style="font-weight: bold;">Fecha de consulta:</td>
                <td><asp:Label ID="lblFechaConsulta" runat="server" Text="Label"></asp:Label></td>
            </tr>
        </tbody>
    </table>

    <table>
        <thead>
            <tr>
                <th colspan="4">
                    <div class="header-container">
                        <h2>Informacion de la consulta</h2>
                    </div>
                </th>
            </tr>
        </thead>
        <tbody>
            <tr>
                <td style="font-weight: bold;">Medicamentos:</td>
                <td><asp:Label ID="lblMedicamentos" runat="server" Text="Label"></asp:Label></td>
            </tr>
            <tr>
                <td style="font-weight: bold;">Indicaciones:</td>
                <td><asp:Label ID="lblIndicaciones" runat="server" Text="Label"></asp:Label></td>
            </tr>
            <tr>
                <td style="font-weight: bold;">Fecha prescripcion:</td>
                <td><asp:Label ID="lblFechaPrescripcion" runat="server" Text="Label"></asp:Label></td>
            </tr>
            <tr>
                <td style="font-weight: bold;">Sucursal:</td>
                <td><asp:Label ID="lblSucursal" runat="server" Text="Label"></asp:Label></td>
            </tr>
        </tbody>
    </table>
</div>



        <asp:Button ID="btnDescargarXML" runat="server" Text="Descargar expediente(XML)" CssClass="btnClassExpediente" OnClick="btnDescargarXML_Click1" />
    </form>


    <div class="dialog-container" id="divMensaje" style="display: none;" runat="server">
        <div class="message-box">
            <div id="mensajeContenido2">
                <span id="mensajeTexto" runat="server"></span>
                <button id="cerrarMensaje2" class="btnClass btnMensaje" onclick="cerrarMensaje()">Cerrar</button>

            </div>

        </div>
    </div>

</body>
</html>
