<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Predio.aspx.cs" Inherits="WebHB_BG.Predio" EnableEventValidation="false" %>

<%@ Import Namespace="System.Data" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid bg-primary py-5 bg-header" style="margin-bottom: 90px">
        <div class="row py-5">
            <div class="col-12 pt-lg-5 mt-lg-5 text-center">
                <h1 class="display-4 text-white animated zoomIn">Gestión de Predios</h1>
                <a href="/" class="h5 text-white">Inicio</a>
                <i class="far fa-circle text-white px-2"></i>
                <a href="#" class="h5 text-white">Catastro</a>
            </div>
        </div>
    </div>

    <div class="container-fluid py-5">
        <div class="container py-5">
            <div class="section-title text-center pb-3 mb-5 mx-auto" style="max-width: 600px">
                <h5 class="fw-bold text-primary text-uppercase">Formulario de Predio</h5>
                <h1 class="mb-0">Registrar / Editar Predio</h1>
            </div>

            <asp:Panel ID="pnlFormulario" runat="server">
                <div class="row g-3">
                    <!-- COLUMNA 1 -->
                    <div class="col-md-6">
                        <asp:TextBox ID="txtCodigo" runat="server" CssClass="form-control mb-3" Placeholder="Código Catastral" />
                        <asp:TextBox ID="txtFechaIngreso" runat="server" CssClass="form-control mb-3" Placeholder="Fecha Ingreso" TextMode="Date" />
                        <asp:TextBox ID="txtCodigoAnterior" runat="server" CssClass="form-control mb-3" Placeholder="Código Anterior" />
                        <asp:TextBox ID="txtNumero" runat="server" CssClass="form-control mb-3" Placeholder="Número" />
                        <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control mb-3" Placeholder="Nombre Predio" />
                        <asp:TextBox ID="txtAreaTotalTerreno" runat="server" CssClass="form-control mb-3" Placeholder="Área Total Terreno" />
                        <asp:TextBox ID="txtAreaTotalConstruccion" runat="server" CssClass="form-control mb-3" Placeholder="Área Total Construcción" />
                        <asp:TextBox ID="txtFondoRelativo" runat="server" CssClass="form-control mb-3" Placeholder="Fondo Relativo" />
                        <asp:TextBox ID="txtFrenteFondo" runat="server" CssClass="form-control mb-3" Placeholder="Frente Fondo" />
                        <asp:DropDownList ID="ddlEstado" runat="server" CssClass="form-select select2 mb-3" AppendDataBoundItems="true">
                            <asp:ListItem Text="Seleccione Estado" Value="" />
                            <asp:ListItem Text="Activo" Value="1" />
                            <asp:ListItem Text="Inactivo" Value="0" />
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddlDominio" runat="server" CssClass="form-select select2 mb-3" AppendDataBoundItems="true">
                            <asp:ListItem Text="Seleccione Dominio" Value="" />
                        </asp:DropDownList>
                        <asp:TextBox ID="txtGeometria" runat="server" CssClass="form-control mb-3" Placeholder="Geometría (WKT)" />
                        <asp:DropDownList ID="ddlCondicionOcupacion" runat="server" CssClass="form-select select2 mb-3" AppendDataBoundItems="true">
                            <asp:ListItem Text="Seleccione Condición Ocupación" Value="" />
                        </asp:DropDownList>
                        <asp:TextBox ID="txtNumHabitantes" runat="server" CssClass="form-control mb-3" Placeholder="Número de Habitantes" />
                        <asp:TextBox ID="txtPropietarioAnterior" runat="server" CssClass="form-control mb-3" Placeholder="Propietario Anterior" />
                        <asp:TextBox ID="txtCartaTopografica" runat="server" CssClass="form-control mb-3" Placeholder="Carta Topográfica" />
                    </div>

                    <!-- COLUMNA 2 -->
                    <div class="col-md-6">
                        <asp:TextBox ID="txtFotoAerea" runat="server" CssClass="form-control mb-3" Placeholder="Foto Aérea" />
                        <asp:DropDownList ID="ddlManzana" runat="server" CssClass="form-select select2 mb-3" AppendDataBoundItems="true">
                            <asp:ListItem Text="Seleccione Manzana" Value="" />
                        </asp:DropDownList>
                        <asp:TextBox ID="txtNumFamilias" runat="server" CssClass="form-control mb-3" Placeholder="Número de Familias" />
                        <asp:TextBox ID="txtPorcentajeDominio" runat="server" CssClass="form-control mb-3" Placeholder="Porcentaje Dominio" />
                        <asp:TextBox ID="txtDetalleDominio" runat="server" CssClass="form-control mb-3" Placeholder="Detalle Dominio" />
                        <asp:TextBox ID="txtTipoMixto" runat="server" CssClass="form-control mb-3" Placeholder="Tipo Mixto" />
                        <asp:TextBox ID="txtValorTipoMixto" runat="server" CssClass="form-control mb-3" Placeholder="Valor Tipo Mixto" />
                        <asp:TextBox ID="txtLinderosDefinidos" runat="server" CssClass="form-control mb-3" Placeholder="Linderos Definidos" />
                        <asp:TextBox ID="txtAreaTotalTerrenoAnterior" runat="server" CssClass="form-control mb-3" Placeholder="Área Terreno Anterior" />
                        <asp:TextBox ID="txtLocalizacionOtros" runat="server" CssClass="form-control mb-3" Placeholder="Localización Otros" />
                        <asp:TextBox ID="txtBienMostrenco" runat="server" CssClass="form-control mb-3" Placeholder="Bien Mostrenco" />
                        <asp:TextBox ID="txtEnConflicto" runat="server" CssClass="form-control mb-3" Placeholder="En Conflicto" />
                        <asp:TextBox ID="txtAreaTotalTerrGrafico" runat="server" CssClass="form-control mb-3" Placeholder="Área Terreno Gráfico" />
                        <asp:TextBox ID="txtPropietarioDesconocido" runat="server" CssClass="form-control mb-3" Placeholder="Propietario Desconocido" />
                        <asp:TextBox ID="txtAreaTotalTerrAlfanum" runat="server" CssClass="form-control mb-3" Placeholder="Área Terreno Alfanumérico" />
                        <asp:TextBox ID="txtDominioDetalle" runat="server" CssClass="form-control mb-3" Placeholder="Dominio Detalle" />
                        <asp:TextBox ID="txtDireccionPrincipal" runat="server" CssClass="form-control mb-3" Placeholder="Dirección Principal" />
                    </div>
                </div>

                <!-- Segunda fila de campos -->
                <div class="row g-3 mt-4">
                    <div class="col-md-6">
                        <asp:TextBox ID="txtAreaTotalConstAlfanum" runat="server" CssClass="form-control mb-3" Placeholder="Área Construcción Alfanumérico" />
                        <asp:TextBox ID="txtTipoVivienda" runat="server" CssClass="form-control mb-3" Placeholder="Tipo Vivienda" />
                        <asp:DropDownList ID="ddlClasificacionVivienda" runat="server" CssClass="form-select select2 mb-3" AppendDataBoundItems="true">
                            <asp:ListItem Text="Seleccione Clasificación Vivienda" Value="" />
                        </asp:DropDownList>
                        <asp:TextBox ID="txtFechaModificacion" runat="server" CssClass="form-control mb-3" Placeholder="Fecha Modificación" TextMode="Date" />
                        <asp:TextBox ID="txtNumCelulares" runat="server" CssClass="form-control mb-3" Placeholder="Número de Celulares" />
                        <asp:TextBox ID="txtModalidadPH" runat="server" CssClass="form-control mb-3" Placeholder="Modalidad PH" />
                        <asp:TextBox ID="txtAlicuotaTotalDeclaratoria" runat="server" CssClass="form-control mb-3" Placeholder="Alícuota Total Declaratoria" />
                        <asp:TextBox ID="txtTipoPropiedadHorizontal" runat="server" CssClass="form-control mb-3" Placeholder="Tipo Propiedad Horizontal" />
                        <asp:TextBox ID="txtObservacionPH" runat="server" CssClass="form-control mb-3" Placeholder="Observación PH" />
                        <asp:TextBox ID="txtHipotecaGAD" runat="server" CssClass="form-control mb-3" Placeholder="Hipoteca GAD" />
                        <asp:TextBox ID="txtRegimenPH" runat="server" CssClass="form-control mb-3" Placeholder="Régimen PH" />
                        <asp:TextBox ID="txtProrrateoTitulo" runat="server" CssClass="form-control mb-3" Placeholder="Prorrateo Título" />
                    </div>
                </div>

                <!-- Botón -->
                <div class="text-center mt-4">
                    <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-primary btn-lg rounded" OnClick="btnGuardar_Click" />
                </div>
            </asp:Panel>
            <br />
            <br />
            <asp:GridView ID="gvPredios" runat="server" CssClass="table table-bordered table-striped"
                AllowPaging="true" PageSize="10" AutoGenerateColumns="False"
                OnPageIndexChanging="gvPredios_PageIndexChanging" OnRowCommand="gvPredios_RowCommand">
                <HeaderStyle CssClass="table-primary" />
                <Columns>
                    <asp:BoundField DataField="pre_id" HeaderText="ID" />
                    <asp:BoundField DataField="pre_codigo_catastral" HeaderText="Código Catastral" />
                    <asp:BoundField DataField="pre_nombre_predio" HeaderText="Nombre Predio" />
                    <asp:TemplateField HeaderText="Mapa">
                        <ItemTemplate>
                            <a href='MapaPredio.aspx?id=<%# Eval("pre_id") %>' target="_blank">🗺️</a>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Acciones">
                        <ItemTemplate>
                            <a href='Predio.aspx?edit=<%# Eval("pre_id") %>' class="btn btn-sm btn-warning">Editar</a>
                            <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CommandName="Eliminar" CommandArgument='<%# Eval("pre_id") %>'
                                CssClass="btn btn-danger btn-sm" OnClientClick='<%# "return confirmarEliminacionPredio(event, " + Eval("pre_id") + ");" %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>

        </div>
    </div>

    <!-- Init Select2 -->
    <script>
        $(document).ready(function () {
            $(".select2").select2({
                placeholder: "Seleccione una opción",
                allowClear: true,
                width: "100%"
            });
        });
    </script>
    <script type="text/javascript">
        function mostrarMensaje(tipo, mensaje) {
            iziToast[tipo]({
                title: tipo === 'success' ? 'Éxito' : 'Error',
                message: mensaje,
                position: 'topRight'
            });
        }
    </script>
    <script>
        function validarFormularioPredio() {
            let errores = [];

            if ($("#<%= txtCodigo.ClientID %>").val().trim() === "")
                errores.push("Debe ingresar el Código Catastral.");
            if ($("#<%= txtNombre.ClientID %>").val().trim() === "")
                errores.push("Debe ingresar el Nombre del Predio.");
            if ($("#<%= ddlDominio.ClientID %>").val() === "")
                errores.push("Debe seleccionar el Dominio.");
            if ($("#<%= ddlCondicionOcupacion.ClientID %>").val() === "")
                errores.push("Debe seleccionar la Condición de Ocupación.");
            if ($("#<%= ddlClasificacionVivienda.ClientID %>").val() === "")
                errores.push("Debe seleccionar la Clasificación de Vivienda.");
            if ($("#<%= ddlManzana.ClientID %>").val() === "")
                errores.push("Debe seleccionar una Manzana.");

            if (errores.length > 0) {
                errores.forEach(function (mensaje) {
                    iziToast.error({
                        title: 'Error',
                        message: mensaje,
                        position: 'topRight'
                    });
                });
                return false; // Evita el postback si hay errores
            }

            return true; // Permite el postback si no hay errores
        }

        $(document).ready(function () {
            $("#<%= btnGuardar.ClientID %>").click(function () {
                return validarFormularioPredio();
            });
        });
    </script>
    <script>
        function confirmarEliminacionPredio(event, id) {
            event.preventDefault();
            iziToast.question({
                timeout: 20000,
                close: false,
                overlay: true,
                displayMode: 'once',
                title: '¿Confirmar?',
                message: '¿Deseas eliminar este predio?',
                position: 'center',
                buttons: [
                    ['<button><b>SÍ</b></button>', function (instance, toast) {
                        __doPostBack('<%= gvPredios.UniqueID %>', 'Eliminar$' + id);
                        instance.hide({}, toast, 'button');
                    }],
                    ['<button>NO</button>', function (instance, toast) {
                        instance.hide({}, toast, 'button');
                    }]
                ]
            });
            return false;
        }
    </script>


</asp:Content>
