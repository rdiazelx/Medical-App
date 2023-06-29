<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="admin.aspx.cs" Inherits="Medical_App.admin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<form runat="server">
<head>
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
      <!-- header section start -->
      <div class="header_section">
        <nav class="destop_header navbar navbar-expand-lg navbar-light bg-light">
          <div class="logo"></div>
          <div class="collapse navbar-collapse" id="navbarSupportedContent">
            <ul class="navbar-nav mr-auto">
              <li class="nav-item">
                     <asp:Button ID="bSucursal" runat="server" class="button button5" Text="Sucursales" OnClick="bSucursal_Click"  />
              </li>
              <li class="nav-item">
                  <asp:Button ID="bMedicos" runat="server" class="button button5" Text="Médicos"  />
              </li>
              <li class="nav-item">
                  <asp:Button ID="bPaciente" runat="server" class="button button5" Text="Pacientes" OnClick="bPaciente_Click" />
              </li>
              <li class="nav-item">
                <a class="logo_main" href="#"><img src="images/logo.png"></a>
              </li>
              <li class="nav-item">
                <asp:Button ID="bMedicamentos" runat="server" class="button button5" Text="Enfermedades"  />
              </li>
              <li class="nav-item">
                <asp:Button ID="bEnfermedades" runat="server" class="button button5" Text="Medicamentos" />
              </li>
              <li class="nav-item">
                  <asp:Button ID="bUsu" runat="server" class="button button5" Text="Usuarios"  /> 
              </li>             
            </ul>
          </div>
        </nav>
        <nav class="mobile_header navbar navbar-expand-lg navbar-light bg-light">
          <div class="logo"><a href="index.html"><img src="images/logo.png"></a></div>
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
                <a class="logo_main" href="index.html"><img src="images/logo.png"></a>
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
                <a class="nav-link" href="#"><img src="images/search-icon.png"></a>
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
                <h4 class="about_text">About</h4>
                <asp:GridView ID="gridLista" runat="server" class="gridview">
                </asp:GridView>
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
  <div class="col-md-6">
    <div class="image_2" style="text-align: center;">
        <img src="images/img-2.png" style="max-width: 50%; height: auto;">
    </div>
</div>


   </body>
</form>
</html>
