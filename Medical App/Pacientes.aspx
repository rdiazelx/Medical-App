<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Pacientes.aspx.cs" Inherits="Medical_App.Pacientes" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Pacientes</title>
    <link href="Styles/Style.css" rel="stylesheet" />
    <script src="Scripts/General.js"></script>
   
</head>
<body>
     <h2>Agregar una Paciente</h2>



     <div class="containerNuevoPaciente">

    <form id="formListaExpedientes" runat="server">


            <asp:TextBox ID="txtNombre" runat="server" CssClass="txtClass" placeholder="Nombre"></asp:TextBox>
            <asp:TextBox ID="txtApellido1" runat="server" CssClass="txtClass"  placeholder="Apellido "></asp:TextBox>
           <asp:DropDownList ID="dptipoIdentificacion" CssClass="txtClass" runat="server"></asp:DropDownList>
            <asp:TextBox ID="txtNumeroIdentificacion" runat="server" CssClass="txtClass"  placeholder="Numero Identificación"></asp:TextBox>
          <asp:DropDownList ID="dpGenero" CssClass="txtClass" runat="server"></asp:DropDownList>
          <asp:DropDownList ID="dpEstadoCivil" CssClass="txtClass" runat="server"></asp:DropDownList>
        <asp:TextBox ID="txtFechaNacimiento" runat="server" CssClass="txtClass" placeholder="Fecha Nacimiento" TextMode="Date"></asp:TextBox>
          <asp:DropDownList ID="dpNacionalidad" CssClass="txtClass" runat="server"></asp:DropDownList>
          <asp:DropDownList ID="dpProvincia" CssClass="txtClass" runat="server"></asp:DropDownList>
         <asp:TextBox ID="txtTelefono" runat="server" CssClass="txtClass" placeholder="Telefono"></asp:TextBox>
         <asp:TextBox ID="txtCorreo" runat="server" CssClass="txtClass" placeholder="Correo"></asp:TextBox>
        
        <asp:Button ID="btnGuardar" runat="server" Text="Guardar Paciente" CssClass="btnClassExpediente" OnClick="btnGuardar_Click" />

        
         <div class="divLeft">
                    <h3>Descarga</h3>
               <asp:Button ID="btnDescargarExcel" runat="server" Text="Archivo de Pacientes" CssClass="btnClass" OnClick="btnDescargarExcel_Click" />
                </div>
       

    </form>
          
    </div>

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

