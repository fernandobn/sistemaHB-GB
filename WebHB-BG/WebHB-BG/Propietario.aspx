<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Propietario.aspx.cs" Inherits="WebHB_BG.Propietario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div
        class="container-fluid bg-primary py-5 bg-header"
        style="margin-bottom: 90px">
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
                <h1 class="mb-0">Registrar Nuevo Propietario</h1>
            </div>

            <!-- FORMULARIO -->
            <asp:Panel ID="pnlFormulario" runat="server">
                <div class="row g-3">
                    <!-- COLUMNA 1 -->
                    <div class="col-md-6">
                        <label class="form-label">Tipo de Identificación</label>
                        <asp:DropDownList ID="ddlTipoIdentificacion" runat="server" CssClass="form-select select2" AppendDataBoundItems="true">
                            <asp:ListItem Text="Seleccione" Value="" />
                        </asp:DropDownList>

                        <label class="form-label mt-3">Número de Identificación</label>
                        <asp:TextBox ID="txtIdentificacion" runat="server" CssClass="form-control" MaxLength="50" />
                        <asp:RequiredFieldValidator ControlToValidate="txtIdentificacion" ErrorMessage="Ingrese la identificación" CssClass="text-danger mt-1" Display="Dynamic" runat="server" />

                        <label class="form-label mt-3">Nombre</label>
                        <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" MaxLength="100" />
                        <asp:RequiredFieldValidator ControlToValidate="txtNombre" ErrorMessage="Ingrese el nombre" CssClass="text-danger mt-1" Display="Dynamic" runat="server" />

                        <label class="form-label mt-3">Apellido</label>
                        <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control" MaxLength="100" />
                        <asp:RequiredFieldValidator ControlToValidate="txtApellido" ErrorMessage="Ingrese el apellido" CssClass="text-danger mt-1" Display="Dynamic" runat="server" />

                        <label class="form-label mt-3">Ciudad</label>
                        <asp:TextBox ID="txtCiudad" runat="server" CssClass="form-control" MaxLength="100" />

                        <label class="form-label mt-3">Dirección Domicilio</label>
                        <asp:TextBox ID="txtDomicilio" runat="server" CssClass="form-control" MaxLength="255" />

                        <label class="form-label mt-3">Referencia</label>
                        <asp:TextBox ID="txtReferencia" runat="server" CssClass="form-control" MaxLength="255" />

                        <label class="form-label mt-3">Fecha de Nacimiento</label>
                        <asp:TextBox ID="txtFechaNacimiento" runat="server" TextMode="Date" CssClass="form-control" />

                        <label class="form-label mt-3">Estado Civil</label>
                        <asp:DropDownList ID="ddlEstadoCivil" runat="server" CssClass="form-select select2" AppendDataBoundItems="true">
                            <asp:ListItem Text="Seleccione" Value="" />
                        </asp:DropDownList>

                        <label class="form-label mt-3">Sexo</label>
                        <asp:DropDownList ID="ddlSexo" runat="server" CssClass="form-select">
                            <asp:ListItem Text="Seleccione" Value="" />
                            <asp:ListItem Text="Masculino" Value="1" />
                            <asp:ListItem Text="Femenino" Value="2" />
                        </asp:DropDownList>

                        <label class="form-label mt-3">Correo Electrónico</label>
                        <asp:TextBox ID="txtCorreo" runat="server" CssClass="form-control" MaxLength="100" TextMode="Email" />

                        <label class="form-label mt-3">Teléfono 1</label>
                        <asp:TextBox ID="txtTelefono1" runat="server" CssClass="form-control" MaxLength="20" />

                        <label class="form-label mt-3">Teléfono 2</label>
                        <asp:TextBox ID="txtTelefono2" runat="server" CssClass="form-control" MaxLength="20" />
                    </div>

                    <!-- COLUMNA 2 -->
                    <div class="col-md-6">
                        <label class="form-label mt-3">Código Postal</label>
                        <asp:TextBox ID="txtCodigoPostal" runat="server" CssClass="form-control" MaxLength="20" />

                        <label class="form-label mt-3">Nro. Conadis</label>
                        <asp:TextBox ID="txtNroConadis" runat="server" CssClass="form-control" MaxLength="50" />

                        <label class="form-label mt-3">% Discapacidad</label>
                        <asp:TextBox ID="txtPorcentajeConadis" runat="server" CssClass="form-control" TextMode="Number" />

                        <label class="form-label mt-3">Tipo Persona</label>
                        <asp:DropDownList ID="ddlTipoPersona" runat="server" CssClass="form-select select2" AppendDataBoundItems="true">
                            <asp:ListItem Text="Seleccione" Value="" />
                        </asp:DropDownList>

                        <label class="form-label mt-3">Número de Registro</label>
                        <asp:TextBox ID="txtNumeroRegistro" runat="server" CssClass="form-control" MaxLength="50" />

                        <label class="form-label mt-3">Inscrito en</label>
                        <asp:TextBox ID="txtInscritoEn" runat="server" CssClass="form-control" MaxLength="100" />

                        <label class="form-label mt-3">Lugar de Inscripción</label>
                        <asp:TextBox ID="txtLugarInscripcion" runat="server" CssClass="form-control" MaxLength="100" />

                        <label class="form-label mt-3">RUC</label>
                        <asp:TextBox ID="txtRuc" runat="server" CssClass="form-control" MaxLength="13" />

                        <label class="form-label mt-3">Razón Social (Persona Natural)</label>
                        <asp:TextBox ID="txtRazonSocial" runat="server" CssClass="form-control" MaxLength="255" />

                        <label class="form-label mt-3">Fecha Fallecido</label>
                        <asp:TextBox ID="txtFechaFallecido" runat="server" TextMode="Date" CssClass="form-control" />
                    </div>
                </div>

                <!-- Hidden para ID -->
                <asp:HiddenField ID="hfPropietarioID" runat="server" />

                <!-- Botones -->
                <div class="text-center mt-4">
                    <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-primary btn-lg rounded" />
                    <asp:Button ID="btnActualizar" runat="server" Text="Actualizar" CssClass="btn btn-warning btn-lg rounded ms-3" />
                </div>
            </asp:Panel>
        </div>
    </div>

    <!-- Script select2 init -->
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
