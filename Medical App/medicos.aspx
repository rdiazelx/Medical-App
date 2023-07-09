<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="medicos.aspx.cs" Inherits="Medical_App.medicos" EnableEventValidation="false" %>



<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head>
    <script src="Scripts/General.js"></script>
    <!-- basic -->
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <!-- mobile metas -->
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <meta name="viewport" content="initial-scale=1, maximum-scale=1">
    <!-- site metas -->
    <title>About</title>
    <meta name="keywords" content="">
    <meta name="description" content="">
    <meta name="author" content="">
    <link href="Styles/Style.css" rel="stylesheet" />
    <!-- bootstrap css -->
    <link rel="stylesheet" href="css/bootstrap.min.css">
    <!-- style css -->
    <link rel="stylesheet" href="css/style.css">
    <!-- Responsive-->
    <link rel="stylesheet" href="css/responsive.css">
    <!-- fevicon -->
    <link rel="icon" href="images/fevicon.png" type="image/gif" />
    <!-- Scrollbar Custom CSS -->
    <link rel="stylesheet" href="css/jquery.mCustomScrollbar.min.css">
    <!-- Tweaks for older IEs-->
    <link rel="stylesheet" href="https://netdna.bootstrapcdn.com/font-awesome/4.0.3/css/font-awesome.css">
    <!-- owl stylesheets -->
    <link rel="stylesheet" href="css/owl.carousel.min.css">
    <link rel="stylesheet" href="css/owl.theme.default.min.css">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/fancybox/2.1.5/jquery.fancybox.min.css" media="screen">
</head>
<body>
    <form runat="server">
        <!-- header section start -->
        <div class="header_section">
            <nav class="destop_header navbar navbar-expand-lg navbar-light bg-light">
                <div class="logo"></div>
                <div class="collapse navbar-collapse" id="navbarSupportedContent">
                    <ul class="navbar-nav mr-auto">
                        <li class="nav-item">
                            <asp:Button ID="bSucursal" runat="server" class="button button5" Text="Sucursales" OnClick="bSucursal_Click" />
                        </li>
                        <li class="nav-item">
                            <asp:Button ID="bMedicos" runat="server" class="button button5" Text="Médicos" OnClick="bMedicos_Click" />
                        </li>
                        <li class="nav-item">
                            <asp:Button ID="bPaciente" runat="server" class="button button5" Text="Pacientes" OnClick="bPaciente_Click" />
                        </li>
                        <li class="nav-item">
                            <a class="logo_main" href="#">
                                <img src="images/logo.png"></a>
                        </li>
                        <li class="nav-item">
                            <asp:Button ID="bMedicamentos" runat="server" class="button button5" Text="Medicamentos" OnClick="bMedicamentos_Click" />
                        </li>
                        <li class="nav-item">
                            <asp:Button ID="bEnfermedades" runat="server" class="button button5" Text="Enfermedades" OnClick="bEnfermedades_Click" />
                        </li>
                     
                      <li class="nav-item">
                        <asp:Button ID="bCitas" runat="server" class="button button5" Text="Agendar cita" OnClick="bCitas_Click" />
                        <h2 class="hover-message">Para agendar una cita debe registrar primero al paciente</h2>
                    </li>

                    </ul>
                </div>
            </nav>
            <nav class="mobile_header navbar navbar-expand-lg navbar-light bg-light">
                <div class="logo"><a href="index.html">
                    <img src="images/logo.png"></a></div>
                <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarSupportedContent2" aria-controls="navbarSupportedContent2" aria-expanded="false" aria-label="Toggle navigation">
                    <span class="navbar-toggler-icon"></span>
                </button>
                <div class="collapse navbar-collapse" id="navbarSupportedContent2">
                    <ul class="navbar-nav mr-auto">
                        <li class="nav-item">
                            <a class="nav-link" href="index.html">Home</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link" href="about.html">About</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link" href="doctor.html">Doctor</a>
                        </li>
                        <li class="nav-item">
                            <a class="logo_main" href="index.html">
                                <img src="images/logo.png"></a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link" href="depatments.html">Depatments</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link" href="blog.html">Blog</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link" href="contact.html">Contact</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link" href="#">
                                <img src="images/search-icon.png"></a>
                        </li>
                        <li class="nav-item active">
                            <a href="login.aspx">LOGIN</a>
                        </li>
                    </ul>

                </div>
            </nav>
        </div>
        <!-- header section end -->
        <!-- about section start -->
        <div class="about_section layout_padding">
            <div class="container-fluid">
                <div class="row">
                    <div class="col-md-6">
                        <div class="about_taital">
                            <h4 class="about_text">Tabla de información</h4>

                            <asp:Label ID="lblMessage" runat="server" Visible="false" Text="" CssClass="message1"></asp:Label>

                            
                            <asp:GridView ID="gridLista" runat="server" class="gridview" OnRowCommand="gridLista_RowCommand"></asp:GridView>
                            <columns>
                                <asp:ButtonField ButtonType="Button" CommandName="Select" Text="Seleccionar" ControlStyle-CssClass="button-select" />

                                <div>
                                   <asp:Button ID="btnNuevoPaciente" runat="server" Text="Nuevo Paciente" CssClass="btnClassExpediente" OnClick="btnNuevoPaciente_Click" Style="display: none" />

                                </div>

                            </columns>
                        </div>
                    </div>


                </div>
            </div>
        </div>
        <!-- about section end -->
        <!-- Javascript files-->
        <script src="js/jquery.min.js"></script>
        <script src="js/popper.min.js"></script>
        <script src="js/bootstrap.bundle.min.js"></script>
        <script src="js/jquery-3.0.0.min.js"></script>
        <script src="js/plugin.js"></script>
        <!-- sidebar -->
        <script src="js/jquery.mCustomScrollbar.concat.min.js"></script>
        <script src="js/custom.js"></script>
        <!-- javascript -->
        <script src="js/owl.carousel.js"></script>
        <script src="https:cdnjs.cloudflare.com/ajax/libs/fancybox/2.1.5/jquery.fancybox.min.js"></script>




        <div>
            <asp:Button ID="btnAñadirUsu" runat="server" Text="Insertar Usuario" CssClass="btnClass" OnClick="btnMostrarUsu_Click" Height="59px" Width="246px" Style="display: none" /></div>

        <div>
            <asp:Button ID="btnAñadirMedico" runat="server" Text="Insertar Médico" CssClass="btnClass" OnClick="btnMostrarMedic_Click" Height="59px" Width="246px" Style="display: none" /></div>
        <div>
            <asp:Button ID="btnAñadirMeds" runat="server" Text="Insertar Medicamentos" CssClass="btnClass" OnClick="btnMostrarMeds_Click" Height="59px" Width="246px" Style="display: none" /></div>
        <div>
            <asp:Button ID="btnAñadirSucu" runat="server" Text="Insertar Sucursal" CssClass="btnClass" OnClick="btnMostrarSucu_Click" Height="59px" Width="246px" Style="display: none" /></div>
        <div>
            <asp:Button ID="btnAñadirPac" runat="server" Text="Insertar Persona" CssClass="btnClass" OnClick="btnMostrarUsu_Click" Height="59px" Width="246px" Style="display: none" /></div>
        <div>
            <asp:Button ID="btnAñadirEnfe" runat="server" Text="Insertar Enfermedad" CssClass="btnClass" OnClick="btnMostrarEnfe_Click" Height="59px" Width="246px" Style="display: none" /></div>

       
        
        
          <!-- Funciones de insertar -->
        
        
        <div class="dialog-container" id="divAgregarUsu" style="display: none;" runat="server">
            <div class="container">
                <div id="mensajeContenido">
                    <asp:TextBox ID="txtCorreo" runat="server" CssClass="txtClass" placeholder="Correo"></asp:TextBox>
                    <asp:TextBox ID="txtPass" runat="server" CssClass="txtClass" placeholder="Contraseña"></asp:TextBox>
                    <asp:TextBox ID="txtRol" runat="server" CssClass="txtClass" placeholder="Rol"></asp:TextBox>

                    <asp:Button ID="BtnCerrar" runat="server" Text="Cerrar" CssClass="btnClass" OnClick="btnCerrar_Click" />
                   

                </div>
            </div>
        </div>


        <div class="dialog-container" id="divAgregarMedico" style="display: none;" runat="server">
            <div class="container">
                <div id="mensajeContenidoMedic">

                    <asp:TextBox ID="txtNom_Medic" runat="server" CssClass="txtClass" placeholder="Nombre"></asp:TextBox>
                    <asp:TextBox ID="txtApell_Medic" runat="server" CssClass="txtClass" placeholder="Apellido"></asp:TextBox>
                    <asp:TextBox ID="txtTipoIdent" runat="server" CssClass="txtClass" placeholder="Tipo de Identificación"></asp:TextBox>
                    <asp:TextBox ID="txtIdenti" runat="server" CssClass="txtClass" placeholder="Identificación"></asp:TextBox>

                    <asp:TextBox ID="txtGenero" runat="server" CssClass="txtClass" placeholder="Género"></asp:TextBox>
                    <asp:TextBox ID="txtEstadoCivil" runat="server" CssClass="txtClass" placeholder="Estado Civil"></asp:TextBox>
                    <asp:TextBox ID="txtFechaNacimiento" runat="server" CssClass="txtClass" placeholder="Fecha de Nacimiento" type="datetime-local"></asp:TextBox>
                    <asp:TextBox ID="txtEspec" runat="server" CssClass="txtClass" placeholder="Especialidad"></asp:TextBox>
                    <asp:TextBox ID="txtTelef" runat="server" CssClass="txtClass" placeholder="Teléfono"></asp:TextBox>
                    <asp:TextBox ID="txtCorreo_Med" runat="server" CssClass="txtClass" placeholder="Correo"></asp:TextBox>
                    <asp:Button ID="Button1" runat="server" Text="Cerrar" CssClass="btnClass" OnClick="btnCerrarMedic_Click" />
                    <asp:Button ID="Button2" runat="server" Text="Agregar" CssClass="btnClass" OnClick="bAgregarMedic_Click" />

                </div>
            </div>
        </div>

        <div class="dialog-container" id="divAgregarMeds" style="display: none;" runat="server">
            <div class="container">
                <div id="mensajeContenidoMedicamen">

                    <asp:TextBox ID="txtNombre_Meds" runat="server" CssClass="txtClass" placeholder="Nombre del medicamento"></asp:TextBox>
                    <asp:TextBox ID="txt_CasaFarma" runat="server" CssClass="txtClass" placeholder="Casa farmaceútica"></asp:TextBox>
                    <asp:TextBox ID="txtCantidad" runat="server" CssClass="txtClass" placeholder="Cantidad"></asp:TextBox>
                    <asp:Button ID="bCerrarMeds" runat="server" Text="Cerrar" CssClass="btnClass" OnClick="btnCerrarMeds_Click" />
                    <asp:Button ID="bGuardarMeds" runat="server" Text="Agregar" CssClass="btnClass" OnClick="bAgregarMeds_Click" />

                </div>
            </div>
        </div>

        <div class="dialog-container" id="divAgregarSucu" style="display: none;" runat="server">
            <div class="container">
                <div id="mensajeContenidoSucu">

                    <asp:TextBox ID="txtLugar_Sucu" runat="server" CssClass="txtClass" placeholder="Provincia de la sucursal"></asp:TextBox>
                    <asp:TextBox ID="txtDireccion" runat="server" CssClass="txtClass" placeholder="Dirección"></asp:TextBox>
                    <asp:TextBox ID="txtTelefono" runat="server" CssClass="txtClass" placeholder="Teléfono"></asp:TextBox>
                    <asp:TextBox ID="txtCorreo_Sucu" runat="server" CssClass="txtClass" placeholder="Correo"></asp:TextBox>
                    <asp:Button ID="bCerrar_Sucu" runat="server" Text="Cerrar" CssClass="btnClass" OnClick="btnCerrarSucu_Click" />
                    <asp:Button ID="bGuardar_Sucu" runat="server" Text="Agregar" CssClass="btnClass" OnClick="bAgregarSucu_Click" />

                </div>
            </div>
        </div>

        <div class="dialog-container" id="divAgregarEnfe" style="display: none;" runat="server">
            <div class="container">
                <div id="mensajeContenidoEnfe">

                    <asp:TextBox ID="txtNom_Enfe" runat="server" CssClass="txtClass" placeholder="Nombre de la enfermedad"></asp:TextBox>
                    <asp:TextBox ID="txtDescri_Enfe" runat="server" CssClass="txtClassBig" placeholder="Descripción de la enfermedad"></asp:TextBox>
                    <asp:Button ID="bCerrar_Enfe" runat="server" Text="Cerrar" CssClass="btnClass" OnClick="btnCerrarEnfe_Click" />
                    <asp:Button ID="bGuardar_Enfe" runat="server" Text="Agregar" CssClass="btnClass" OnClick="bAgregarEnfe_Click" />

                </div>
            </div>
        </div>




        <!-- Mensajes en pantalla -->

        <div class="dialog-container" id="divMensaje" style="display: none;" runat="server">
            <div class="message-box">
                <div id="mensajeContenido2">
                    <span id="mensajeTexto" runat="server"></span>
                    <button id="cerrarMensaje2" class="btnClass btnMensaje" onclick="cerrarMensaje()">Cerrar</button>

                </div>

            </div>
        </div>


          <div class="dialog-container" id="divMensaje2" style="display: none;" runat="server">
            <div class="message-box">
                <div id="mensajeContenido2">
                    <span id="mensajeTexto2" runat="server"></span>
                    
                </div>

            </div>
        </div>


        <div class="col-md-6">
            <div class="image_2" style="text-align: center;">
                <img src="images/img-2.png" style="max-width: 50%; height: auto;">
            </div>
        </div>

    </form>
</body>

</html>
