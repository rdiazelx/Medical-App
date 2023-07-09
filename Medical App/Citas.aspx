<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Citas.aspx.cs" Inherits="Medical_App.Citas"  EnableEventValidation="false"%>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Citas</title>
    <link href="Styles/Style.css" rel="stylesheet" />
    <script src="Scripts/General.js"></script>
</head>
<body>


    
    <div class="containerCitas">
        <form id="form1" runat="server">

            <h2>Agendar Cita</h2>

            <h4>Informacion del paciente:</h4>

            <h4>ID Paciente</h4>
            <asp:Label ID="lblId" runat="server" ></asp:Label>

            <h4>Fecha de Nacimiento</h4>
            <asp:Label ID="lblFechaNacimiento" runat="server" ></asp:Label>

           <asp:DropDownList ID="dpPacientes" runat="server" CssClass="txtClass" OnSelectedIndexChanged="dpPacientes_SelectedIndexChanged" AutoPostBack="True">
    <asp:ListItem Text="Seleccione" Value=""></asp:ListItem>
</asp:DropDownList>


             <h4>Fecha de cita:</h4>
              <asp:TextBox ID="txtFechaCita" runat="server" CssClass="txtClass" placeholder="Fecha de Cita" TextMode="Date"></asp:TextBox>

            <h4>Medico:</h4>
            <asp:DropDownList ID="dpMedicos" runat="server" CssClass="txtClass"  AutoPostBack="true" OnSelectedIndexChanged="dpMedicos_SelectedIndexChanged">
                <asp:ListItem Text="Seleccione" Value=""></asp:ListItem>
            </asp:DropDownList>



              <h4>Especialidad:</h4>
             <asp:TextBox ID="txtEspecialidad" runat="server" CssClass="txtClass" ReadOnly="true"></asp:TextBox>


            <h4>Enfermedad o Sintomas:</h4>
             <asp:TextBox ID="txtEnfermedad" runat="server" CssClass="txtClass"></asp:TextBox>


             <h4>Medicamentos:</h4>
             <asp:TextBox ID="txtMedicamentos" runat="server" CssClass="txtClass"></asp:TextBox>


             <h4>Indicaciones:</h4>
             <asp:TextBox ID="txtIndicaciones" runat="server" CssClass="txtClass"></asp:TextBox>


            <h4>Fecha Prescripción:</h4>
              <asp:TextBox ID="txtFecha" runat="server" CssClass="txtClass" placeholder="Fecha Nacimiento" TextMode="Date"></asp:TextBox>

            <h4>Sucursal:</h4>
           <asp:DropDownList ID="dpSucursal" CssClass="txtClass" runat="server">
                <asp:ListItem Text="Seleccione" Value=""></asp:ListItem>
                <asp:ListItem Text="San José" Value="1"></asp:ListItem>
                <asp:ListItem Text="Heredia" Value="2"></asp:ListItem>
                <asp:ListItem Text="Puntarenas" Value="3"></asp:ListItem>
            </asp:DropDownList>


            <asp:Button ID="Button1" runat="server" Text="Guardar Cita" CssClass="btnClass" OnClick="GuardarCita"/>

             <asp:Button ID="Button3" runat="server" Text="Cancelar" CssClass="btnClass" OnClick="Button3_Click" />

         <div class="divLeft">
               <asp:Button ID="Button2" runat="server" Text="Ver Citas" CssClass="btnClass" OnClick="VerCitas" />
                </div>
       
        <br />

         <div class="about_section layout_padding">
        <div class="container-fluid">
          <div class="row">
              <h4>Citas</h4>
            <div class="col-md-12">
              <div class="about_taital" style="  width: 100%;">
                <asp:GridView ID="gridLista" runat="server" class="gridview" OnRowCommand="gridLista_RowCommand"></asp:GridView>
                   <Columns>
                     <asp:ButtonField ButtonType="Button" CommandName="Select" Text="Seleccionar" ControlStyle-CssClass="button-select" />
                     <!-- Other columns... -->
                 </columns>
              </div>
            </div>
           
         
          </div>
        </div>
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
       <asp:Label ID="lblError" runat="server" CssClass="error-message" Visible="false" BorderColor="Red"></asp:Label>
</body>
</html>