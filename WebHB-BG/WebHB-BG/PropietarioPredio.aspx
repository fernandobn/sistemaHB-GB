<%@ Page Title="Propietario Predio" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PropietarioPredio.aspx.cs" Inherits="WebHB_BG.PropietarioPredio" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid bg-primary py-5 bg-header" style="margin-bottom: 90px">
        <div class="row py-5">
            <div class="col-12 pt-lg-5 mt-lg-5 text-center">
                <h1 class="display-4 text-white animated zoomIn">Guardar Propietario</h1>
                <a href="/" class="h5 text-white">Inicio</a>
                <i class="far fa-circle text-white px-2"></i>
                <a href="#" class="h5 text-white">Catálogo</a>
            </div>
        </div>
    </div>

    <div class="container-fluid py-5">
        <div class="container py-5">
            <div class="section-title text-center pb-3 mb-5 mx-auto" style="max-width: 600px">
                <h5 class="fw-bold text-primary text-uppercase">Gestión de Propietarios</h5>
                <h1 class="mb-0">Propietario del Predio</h1>
            </div>

           <asp:Panel ID="pnlFormulario" runat="server" CssClass="bg-light shadow rounded p-4">
    <!-- HiddenFields -->
    <asp:HiddenField ID="hfPrpId" runat="server" />
    <asp:HiddenField ID="hfProId" runat="server" />
    <asp:HiddenField ID="hfProIdConyuge" runat="server" />
    <asp:HiddenField ID="hfProIdRepLegal" runat="server" />
    <asp:HiddenField ID="hfPreId" runat="server" />

    <div class="row g-4">
        <!-- Columna Izquierda -->
        <div class="col-md-6">
            <div class="mb-3">
                <label class="form-label">Alicuota</label>
                <asp:TextBox ID="txtAlicuota" runat="server" CssClass="form-control" TextMode="Number" />
            </div>

            <div class="mb-3">
                <label class="form-label">Años de Posesión</label>
                <asp:TextBox ID="txtAniosPosesion" runat="server" CssClass="form-control" TextMode="Number" />
            </div>

            <div class="mb-3">
                <label class="form-label">Observación</label>
                <asp:TextBox ID="txtObservacion" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3" />
            </div>

            <div class="form-check mb-3">
                <asp:CheckBox ID="chkTieneEscritura" runat="server" CssClass="form-check-input" />
                <label class="form-check-label" for="chkTieneEscritura">Tiene Escritura</label>
            </div>

            <div class="mb-3">
                <label class="form-label">Representante</label>
                <asp:TextBox ID="txtRepresentante" runat="server" CssClass="form-control" />
            </div>

            <div class="mb-3">
                <label class="form-label">Tipo Adquisición</label>
                <asp:DropDownList ID="ddlAdquisicion" runat="server" CssClass="form-select select2" AppendDataBoundItems="true">
                    <asp:ListItem Text="Seleccione" Value="" />
                </asp:DropDownList>
            </div>

            <div class="mb-3">
                <label class="form-label">Situación Actual</label>
                <asp:DropDownList ID="ddlSituacionActual" runat="server" CssClass="form-select select2" AppendDataBoundItems="true">
                    <asp:ListItem Text="Seleccione" Value="" />
                </asp:DropDownList>
            </div>

            <div class="mb-3">
                <label class="form-label">Celebrado ante</label>
                <asp:TextBox ID="txtCelebradoAnte" runat="server" CssClass="form-control" />
            </div>
        </div>

        <!-- Columna Derecha -->
        <div class="col-md-6">
            <div class="mb-3">
                <label class="form-label">Cantón</label>
                <asp:TextBox ID="txtCanton" runat="server" CssClass="form-control" />
            </div>

            <div class="mb-3">
                <label class="form-label">Notaría</label>
                <asp:TextBox ID="txtNotaria" runat="server" CssClass="form-control" />
            </div>

            <div class="mb-3">
                <label class="form-label">Fecha Inscripción</label>
                <asp:TextBox ID="txtFechaInscripcion" runat="server" CssClass="form-control" TextMode="Date" />
            </div>

            <div class="mb-3">
                <label class="form-label">Lugar Inscripción</label>
                <asp:TextBox ID="txtLugarInscripcion" runat="server" CssClass="form-control" />
            </div>

            <div class="mb-3">
                <label class="form-label">Perfeccionamiento</label>
                <asp:TextBox ID="txtPerfeccionamiento" runat="server" CssClass="form-control" />
            </div>

            <div class="mb-3">
                <label class="form-label">Lugar Registro</label>
                <asp:TextBox ID="txtLugarRegistro" runat="server" CssClass="form-control" />
            </div>

            <div class="mb-3">
                <label class="form-label">Registro de Propiedad</label>
                <asp:TextBox ID="txtRegistroPropiedad" runat="server" CssClass="form-control" />
            </div>

            <div class="mb-3">
                <label class="form-label">Fecha Registro</label>
                <asp:TextBox ID="txtFechaRegistro" runat="server" CssClass="form-control" TextMode="Date" />
            </div>

            <div class="mb-3">
                <label class="form-label">Libro</label>
                <asp:TextBox ID="txtLibro" runat="server" CssClass="form-control" />
            </div>

            <div class="mb-3">
                <label class="form-label">Foja</label>
                <asp:TextBox ID="txtFoja" runat="server" CssClass="form-control" />
            </div>

            <div class="mb-3">
                <label class="form-label">Situación Legal</label>
                <asp:TextBox ID="txtSituacionLegal" runat="server" CssClass="form-control" />
            </div>

            <div class="form-check mb-3">
                <asp:CheckBox ID="chkFinanciado" runat="server" CssClass="form-check-input" />
                <label class="form-check-label" for="chkFinanciado">¿Está Financiado?</label>
            </div>

            <div class="mb-3">
                <label class="form-label">Nombre del Pueblo</label>
                <asp:TextBox ID="txtNombrePueblo" runat="server" CssClass="form-control" />
            </div>

            <div class="mb-3">
                <label class="form-label">Años de Perfeccionamiento</label>
                <asp:TextBox ID="txtAniosPerfeccionamiento" runat="server" CssClass="form-control" TextMode="Number" />
            </div>

            <div class="mb-3">
                <label class="form-label">Área de Escritura (m²)</label>
                <asp:TextBox ID="txtAreaEscritura" runat="server" CssClass="form-control" TextMode="Number" />
            </div>

            <div class="mb-3">
                <label class="form-label">Parentesco</label>
                <asp:DropDownList ID="ddlParentesco" runat="server" CssClass="form-select select2" AppendDataBoundItems="true">
                    <asp:ListItem Text="Seleccione" Value="" />
                </asp:DropDownList>
            </div>
        </div>
    </div>

    <!-- Botones -->
    <div class="text-center mt-5">
        <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-primary btn-lg rounded" />
        <asp:Button ID="btnActualizar" runat="server" Text="Actualizar" CssClass="btn btn-warning btn-lg rounded ms-3" Visible="false" />
    </div>
</asp:Panel>

        </div>
    </div>

    <!-- JS Select2 -->
    <script>
        $(document).ready(function () {
            $(".select2").select2({
                placeholder: "Seleccione una opción",
                allowClear: true,
                width: "100%"
            });
        });
    </script>
</asp:Content>
