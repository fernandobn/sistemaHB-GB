<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Propietario.aspx.cs" Inherits="WebHB_BG.Propietario" %>

<%@ Import Namespace="System.Data" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid bg-primary py-5 bg-header" style="margin-bottom: 90px">
        <div class="row py-5">
            <div class="col-12 pt-lg-5 mt-lg-5 text-center">
                <h1 class="display-4 text-white animated zoomIn">Gestión de Propietarios</h1>
                <a href="/" class="h5 text-white">Inicio</a>
                <i class="far fa-circle text-white px-2"></i>
                <a href="#" class="h5 text-white">Catálogo</a>
            </div>
        </div>
    </div>

    <div class="container-fluid py-5">
        <div class="container py-5">
            <div class="section-title text-center pb-3 mb-5 mx-auto" style="max-width: 600px">
                <h5 class="fw-bold text-primary text-uppercase">Formulario de Propietario</h5>
                <h1 class="mb-0">Registrar / Editar Propietario</h1>
            </div>

            <asp:Panel ID="pnlFormulario" runat="server">
                <div class="row g-3">
                    <!-- COLUMNA 1 -->
                    <div class="col-md-6">
                        <asp:DropDownList ID="ddlTipoIdentificacion" runat="server" CssClass="form-select select2 mb-3" AppendDataBoundItems="true">
                            <asp:ListItem Text="Seleccione Tipo Identificación" Value="" />
                        </asp:DropDownList>
                        <br />
                        <asp:TextBox ID="txtIdentificacion" runat="server" CssClass="form-control mb-3" Placeholder="Número de Identificación" MaxLength="50" />
                        <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control mb-3" Placeholder="Nombre" MaxLength="100" />
                        <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control mb-3" Placeholder="Apellido" MaxLength="100" />

                        <asp:TextBox ID="txtCiudad" runat="server" CssClass="form-control mb-3" Placeholder="Ciudad" MaxLength="100" />
                        <asp:TextBox ID="txtDomicilio" runat="server" CssClass="form-control mb-3" Placeholder="Dirección Domicilio" MaxLength="255" />
                        <asp:TextBox ID="txtReferencia" runat="server" CssClass="form-control mb-3" Placeholder="Referencia" MaxLength="255" />

                        <asp:TextBox ID="txtFechaNacimiento" runat="server" CssClass="form-control mb-3" Placeholder="Fecha de Nacimiento" TextMode="Date" />
                        <br />
                        <asp:DropDownList ID="ddlEstadoCivil" runat="server" CssClass="form-select select2 mb-3" AppendDataBoundItems="true">
                            <asp:ListItem Text="Seleccione Estado Civil" Value="" />
                        </asp:DropDownList>
                        <br />
                        <asp:DropDownList ID="ddlSexo" runat="server" CssClass="form-select mb-3">
                            <asp:ListItem Text="Seleccione Sexo" Value="" />
                            <asp:ListItem Text="Masculino" Value="1" />
                            <asp:ListItem Text="Femenino" Value="2" />
                        </asp:DropDownList>

                        <asp:TextBox ID="txtCorreo" runat="server" CssClass="form-control mb-3" Placeholder="Correo Electrónico" MaxLength="100" TextMode="Email" />
                        <asp:TextBox ID="txtTelefono1" runat="server" CssClass="form-control mb-3" Placeholder="Teléfono 1" MaxLength="20" />
                        <asp:TextBox ID="txtTelefono2" runat="server" CssClass="form-control mb-3" Placeholder="Teléfono 2" MaxLength="20" />
                    </div>

                    <!-- COLUMNA 2 -->
                    <div class="col-md-6">
                        <asp:TextBox ID="txtCodigoPostal" runat="server" CssClass="form-control mb-3" Placeholder="Código Postal" MaxLength="20" />
                        <asp:TextBox ID="txtNroConadis" runat="server" CssClass="form-control mb-3" Placeholder="Nro. Conadis" MaxLength="50" />
                        <asp:TextBox ID="txtPorcentajeConadis" runat="server" CssClass="form-control mb-3" Placeholder="% Discapacidad" TextMode="Number" />

                        <asp:DropDownList ID="ddlTipoPersona" runat="server" CssClass="form-select select2 mb-3" AppendDataBoundItems="true">
                            <asp:ListItem Text="Seleccione Tipo Persona" Value="" />
                        </asp:DropDownList>
                        <br />
                        <asp:TextBox ID="txtNumeroRegistro" runat="server" CssClass="form-control mb-3" Placeholder="Número de Registro" MaxLength="50" />
                        <asp:TextBox ID="txtInscritoEn" runat="server" CssClass="form-control mb-3" Placeholder="Inscrito en" MaxLength="100" />
                        <asp:TextBox ID="txtLugarInscripcion" runat="server" CssClass="form-control mb-3" Placeholder="Lugar de Inscripción" MaxLength="100" />

                        <asp:TextBox ID="txtRuc" runat="server" CssClass="form-control mb-3" Placeholder="RUC" MaxLength="13" />
                        <asp:TextBox ID="txtRazonSocial" runat="server" CssClass="form-control mb-3" Placeholder="Razón Social (Persona Natural)" MaxLength="255" />

                        <asp:TextBox ID="txtFechaFallecido" runat="server" CssClass="form-control mb-3" Placeholder="Fecha Fallecido" TextMode="Date" />
                    </div>
                </div>

                <asp:HiddenField ID="hfPropietarioID" runat="server" />

                <div class="text-center mt-4">
                    <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-primary btn-lg rounded" OnClick="btnGuardar_Click" />
                    <asp:Button ID="btnActualizar" runat="server" Text="Actualizar" CssClass="btn btn-warning btn-lg rounded ms-3" OnClick="btnActualizar_Click" />
                </div>
            </asp:Panel>

            <asp:GridView ID="gvPropietarios" runat="server" AutoGenerateColumns="False"
                AllowPaging="True" PageSize="10" OnPageIndexChanging="gvPropietarios_PageIndexChanging"
                CssClass="table table-bordered table-striped" PagerStyle-CssClass="pagination">

                <Columns>
                    <asp:BoundField DataField="pro_id" HeaderText="ID" />
                    <asp:BoundField DataField="opc_tipoidentificacion" HeaderText="Tipo Identificación" />
                    <asp:BoundField DataField="pro_nombre" HeaderText="Nombre" />
                    <asp:BoundField DataField="pro_direccion_ciudad" HeaderText="Ciudad" />

                    <asp:TemplateField HeaderText="Acciones">
                        <ItemTemplate>
                            <a href='Propietario.aspx?edit=<%# Eval("pro_id") %>' class="btn btn-sm btn-warning">Editar</a>
                            <a href='Propietario.aspx?delete=<%# Eval("pro_id") %>'
                                class="btn btn-sm btn-danger ms-2"
                                onclick="return confirm('¿Eliminar este registro?');">Eliminar</a>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>

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
