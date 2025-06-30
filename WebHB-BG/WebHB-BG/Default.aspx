<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebHB_BG._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container-fluid p-0 m-0">
        <div id="header-carousel" class="carousel slide carousel-fade" data-bs-ride="carousel">
            <div class="carousel-inner">
                <div class="carousel-item active">
                    <img class="w-100" src="plantilla/img/carousel-1.jpg" alt="Catálogo Municipal" style="height: 100vh; object-fit: cover;" />
                    <div class="carousel-caption d-flex flex-column align-items-center justify-content-center">
                        <div class="p-3" style="max-width: 900px">
                            <h5 class="text-white text-uppercase mb-3 animated slideInDown">Gad Municipal</h5>
                            <h1 class="display-1 text-white mb-md-4 animated zoomIn">Catálogo de Servicios Municipales</h1>
                            <a href="#" class="btn btn-primary py-md-3 px-md-5 me-3 animated slideInLeft">Agregar Nuevo Catálogo</a>
                            <a href="#" class="btn btn-outline-light py-md-3 px-md-5 animated slideInRight">Ver Catálogos</a>
                        </div>
                    </div>
                </div>
                <div class="carousel-item">
                    <img class="w-100" src="plantilla/img/carousel-2.jpg" alt="Servicios Municipales" style="height: 100vh; object-fit: cover;" />
                    <div class="carousel-caption d-flex flex-column align-items-center justify-content-center">
                        <div class="p-3" style="max-width: 900px">
                            <h5 class="text-white text-uppercase mb-3 animated slideInDown">Gad Municipal</h5>
                            <h1 class="display-1 text-white mb-md-4 animated zoomIn">Servicios para la Comunidad</h1>
                            <a href="/servicios.aspx" class="btn btn-primary py-md-3 px-md-5 me-3 animated slideInLeft">Servicios</a>
                        </div>
                    </div>
                </div>
            </div>
            <button class="carousel-control-prev" type="button" data-bs-target="#header-carousel" data-bs-slide="prev">
                <span class="carousel-control-prev-icon" aria-hidden="true"></span>
                <span class="visually-hidden">Anterior</span>
            </button>
            <button class="carousel-control-next" type="button" data-bs-target="#header-carousel" data-bs-slide="next">
                <span class="carousel-control-next-icon" aria-hidden="true"></span>
                <span class="visually-hidden">Siguiente</span>
            </button>
        </div>
    </div>

</asp:Content>

