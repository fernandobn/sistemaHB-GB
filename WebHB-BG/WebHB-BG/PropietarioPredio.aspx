<%@ Page Title="Propietario Predio" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PropietarioPredio.aspx.cs" Inherits="WebHB_BG.PropietarioPredio" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid bg-primary py-5 bg-header" style="margin-bottom: 90px">
        <div class="row py-5">
            <div class="col-12 pt-lg-5 mt-lg-5 text-center">
                <h1 class="display-4 text-white animated zoomIn">Guardar Propietario</h1>
                <a href="/" class="h5 text-white">Inicio</a>
                <i class="far fa-circle text-white px-2"></i>
                <a href="#" class="h5 text-white">Propietario Predio</a>
            </div>
        </div>
    </div>

    <div class="container py-5">
    <div class="text-center mb-5">
        <h5 class="text-primary text-uppercase fw-bold">Gestión de Propietarios</h5>
        <h1 class="display-6">Propietario del Predio</h1>
        <p class="text-muted">Complete o actualice la información correspondiente al propietario del predio.</p>
    </div>

    <asp:Panel ID="pnlFormulario" runat="server" CssClass="bg-white border rounded-4 shadow-sm p-4">
        <asp:HiddenField ID="hfPropietarioPredioID" runat="server" />

        <div class="row g-4">
            <div class="col-md-6">
                <h5 class="text-primary mb-3 border-bottom pb-2">Datos del Propietario</h5>

                <div class="mb-3">
                    <label class="form-label">Propietario</label>
                    <asp:DropDownList ID="ddlPropietario" runat="server" CssClass="form-select select2" AppendDataBoundItems="true" />
                </div>

                <div class="mb-3">
                    <label class="form-label">Cónyuge</label>
                    <asp:DropDownList ID="ddlConyuge" runat="server" CssClass="form-select select2" AppendDataBoundItems="true" />
                </div>

                <div class="mb-3">
                    <label class="form-label">Representante Legal</label>
                    <asp:DropDownList ID="ddlRepresentanteLegal" runat="server" CssClass="form-select select2" AppendDataBoundItems="true" />
                </div>

                <div class="mb-3">
                    <label class="form-label">Predio</label>
                    <asp:DropDownList ID="ddlPredio" runat="server" CssClass="form-select select2" AppendDataBoundItems="true" />
                </div>

                <div class="row">
                    <div class="col-md-6 mb-3">
                        <label class="form-label">Alicuota</label>
                        <asp:TextBox ID="txtAlicuota" runat="server" CssClass="form-control" TextMode="Number" step="0.01" />
                    </div>
                    <div class="col-md-6 mb-3">
                        <label class="form-label">Años de Posesión</label>
                        <asp:TextBox ID="txtAniosPosesion" runat="server" CssClass="form-control" TextMode="Number" />
                    </div>
                </div>

                <div class="mb-3">
                    <label class="form-label">Observación</label>
                    <asp:TextBox ID="txtObservacion" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3" />
                </div>

                <div class="row">
                    <div class="col-md-6 mb-3">
                        <label class="form-label">Tiene Escritura</label>
                        <asp:DropDownList ID="ddlTieneEscritura" runat="server" CssClass="form-select">
                            <asp:ListItem Value="" Text="-- Seleccionar --" />
                            <asp:ListItem Value="1" Text="Sí" />
                            <asp:ListItem Value="0" Text="No" />
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-6 mb-3">
                        <label class="form-label">Representante</label>
                        <asp:DropDownList ID="ddlRepresentante" runat="server" CssClass="form-select">
                            <asp:ListItem Value="" Text="-- Seleccionar --" />
                            <asp:ListItem Value="1" Text="Sí" />
                            <asp:ListItem Value="0" Text="No" />
                        </asp:DropDownList>
                    </div>
                </div>

                <div class="mb-3">
                    <label class="form-label">Tipo Adquisición</label>
                    <asp:DropDownList ID="ddlAdquisicion" runat="server" CssClass="form-select select2" AppendDataBoundItems="true" />
                </div>

                <div class="mb-3">
                    <label class="form-label">Situación Actual</label>
                    <asp:DropDownList ID="ddlSituacionActual" runat="server" CssClass="form-select select2" AppendDataBoundItems="true" />
                </div>

                <div class="mb-3">
                    <label class="form-label">Celebrado Ante</label>
                    <asp:TextBox ID="txtCelebradoAnte" runat="server" CssClass="form-control" />
                </div>
            </div>

            <div class="col-md-6">
                <h5 class="text-primary mb-3 border-bottom pb-2">Datos Legales</h5>

                <div class="row">
                    <div class="col-md-6 mb-3">
                        <label class="form-label">Cantón</label>
                        <asp:TextBox ID="txtCanton" runat="server" CssClass="form-control" />
                    </div>
                    <div class="col-md-6 mb-3">
                        <label class="form-label">Notaría</label>
                        <asp:TextBox ID="txtNotaria" runat="server" CssClass="form-control" />
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-6 mb-3">
                        <label class="form-label">Fecha Inscripción</label>
                        <asp:TextBox ID="txtFechaInscripcion" runat="server" CssClass="form-control" TextMode="Date" />
                    </div>
                    <div class="col-md-6 mb-3">
                        <label class="form-label">Lugar Inscripción</label>
                        <asp:TextBox ID="txtLugarInscripcion" runat="server" CssClass="form-control" />
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-6 mb-3">
                        <label class="form-label">Perfeccionamiento</label>
                        <asp:DropDownList ID="ddlPerfeccionamiento" runat="server" CssClass="form-select">
                            <asp:ListItem Value="" Text="-- Seleccionar --" />
                            <asp:ListItem Value="1" Text="Sí" />
                            <asp:ListItem Value="0" Text="No" />
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-6 mb-3">
                        <label class="form-label">Lugar Registro</label>
                        <asp:TextBox ID="txtLugarRegistro" runat="server" CssClass="form-control" />
                    </div>
                </div>

                <div class="mb-3">
                    <label class="form-label">Registro de Propiedad</label>
                    <asp:TextBox ID="txtRegistroPropiedad" runat="server" CssClass="form-control" />
                </div>

                <div class="row">
                    <div class="col-md-6 mb-3">
                        <label class="form-label">Fecha Registro</label>
                        <asp:TextBox ID="txtFechaRegistro" runat="server" CssClass="form-control" TextMode="Date" />
                    </div>
                    <div class="col-md-6 mb-3">
                        <label class="form-label">Libro</label>
                        <asp:TextBox ID="txtLibro" runat="server" CssClass="form-control" />
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-6 mb-3">
                        <label class="form-label">Foja</label>
                        <asp:TextBox ID="txtFoja" runat="server" CssClass="form-control" />
                    </div>
                    <div class="col-md-6 mb-3">
                        <label class="form-label">Situación Legal</label>
                        <asp:TextBox ID="txtSituacionLegal" runat="server" CssClass="form-control" />
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-6 mb-3">
                        <label class="form-label">¿Está Financiado?</label>
                        <asp:DropDownList ID="ddlFinanciado" runat="server" CssClass="form-select">
                            <asp:ListItem Value="" Text="-- Seleccionar --" />
                            <asp:ListItem Value="1" Text="Sí" />
                            <asp:ListItem Value="0" Text="No" />
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-6 mb-3">
                        <label class="form-label">Nombre del Pueblo</label>
                        <asp:TextBox ID="txtNombrePueblo" runat="server" CssClass="form-control" />
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-6 mb-3">
                        <label class="form-label">Años Perfeccionamiento</label>
                        <asp:TextBox ID="txtAniosPerfeccionamiento" runat="server" CssClass="form-control" TextMode="Number" />
                    </div>
                    <div class="col-md-6 mb-3">
                        <label class="form-label">Área Escritura (m²)</label>
                        <asp:TextBox ID="txtAreaEscritura" runat="server" CssClass="form-control" TextMode="Number" step="0.01" />
                    </div>
                </div>

                <div class="mb-3">
                    <label class="form-label">Parentesco</label>
                    <asp:DropDownList ID="ddlParentesco" runat="server" CssClass="form-select select2" AppendDataBoundItems="true" />
                </div>
            </div>
        </div>

        <div class="text-center mt-4">
            <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-success btn-lg rounded-pill px-4" OnClick="btnGuardar_Click" />
            <asp:Button ID="btnActualizar" runat="server" Text="Actualizar" CssClass="btn btn-warning btn-lg rounded-pill px-4 ms-2" Visible="false" OnClick="btnActualizar_Click" />
            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-secondary btn-lg rounded-pill px-4 ms-2" OnClick="btnCancelar_Click" />
        </div>
    </asp:Panel>

    <asp:ScriptManager ID="ScriptManager1" runat="server" />

<asp:UpdatePanel ID="updGrid" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <asp:GridView ID="gvPropietariosPredio" runat="server" AutoGenerateColumns="false" AllowPaging="true" PageSize="20"
            OnPageIndexChanging="gvPropietariosPredio_PageIndexChanging" OnRowCommand="gvPropietariosPredio_RowCommand"
            CssClass="table table-bordered table-hover mt-5 shadow-sm">
            <Columns>
                <asp:BoundField DataField="prp_id" HeaderText="ID" ReadOnly="true" />
                <asp:BoundField DataField="propietario" HeaderText="Propietario" />
                <asp:BoundField DataField="pro_num_identificacion" HeaderText="Identificación" />
                <asp:BoundField DataField="predio_info" HeaderText="Predio" />
                <asp:BoundField DataField="prp_alicuota" HeaderText="Alicuota" DataFormatString="{0:N2}" />
                <asp:BoundField DataField="prp_anios_posesion" HeaderText="Años Posesión" />
                <asp:BoundField DataField="tiene_escritura" HeaderText="Tiene Escritura" />
                <asp:BoundField DataField="tipo_propietario" HeaderText="Tipo" />
                <asp:TemplateField HeaderText="Acciones">
                    <ItemTemplate>
                        <asp:Button ID="btnEditar" runat="server" CommandName="Editar" CommandArgument='<%# Eval("prp_id") %>' Text="Editar" CssClass="btn btn-sm btn-outline-primary me-2" />
                        <asp:Button ID="btnEliminar" runat="server" CommandName="Eliminar" CommandArgument='<%# Eval("prp_id") %>' Text="Eliminar" CssClass="btn btn-sm btn-outline-danger"
                            OnClientClick='<%# "return confirmarEliminacion(event, \"" + Eval("prp_id") + "\");" %>' />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="gvPropietariosPredio" EventName="PageIndexChanging" />
        <asp:AsyncPostBackTrigger ControlID="gvPropietariosPredio" EventName="RowCommand" />
    </Triggers>
</asp:UpdatePanel>

</div>


    <!-- JS Select2 e IziToast -->
    <script>
        $(document).ready(function () {
            $(".select2").select2({
                placeholder: "Seleccione una opción",
                allowClear: true,
                width: "100%"
            });

            $("#<%= btnGuardar.ClientID %>").click(function () {
                return validarFormulario();
            });

            $("#<%= btnActualizar.ClientID %>").click(function () {
                return validarFormulario();
            });
        });

        function validarFormulario() {
            let errores = [];

            if ($("#<%= ddlPropietario.ClientID %>").val() === "")
                errores.push("Debe seleccionar un propietario.");
            if ($("#<%= ddlPredio.ClientID %>").val() === "")
                errores.push("Debe seleccionar un predio.");
            if ($("#<%= ddlAdquisicion.ClientID %>").val() === "")
                errores.push("Debe seleccionar el tipo de adquisición.");
            if ($("#<%= ddlSituacionActual.ClientID %>").val() === "")
                errores.push("Debe seleccionar la situación actual.");
            if ($("#<%= ddlParentesco.ClientID %>").val() === "")
                errores.push("Debe seleccionar el parentesco.");

            if (errores.length > 0) {
                errores.forEach(function (msg) {
                    iziToast.error({
                        title: 'Error',
                        message: msg,
                        position: 'topRight'
                    });
                });
                return false;
            }
            return true;
        }

        function confirmarEliminacion(event, id) {
            event.preventDefault();
            iziToast.question({
                timeout: 20000,
                close: false,
                overlay: true,
                displayMode: 'once',
                title: '¿Confirmar?',
                message: '¿Deseas eliminar este registro?',
                position: 'center',
                buttons: [
                    ['<button><b>SÍ</b></button>', function (instance, toast) {
                        __doPostBack('<%= gvPropietariosPredio.UniqueID %>', 'Eliminar$' + id);
                        instance.hide({}, toast, 'button');
                    }],
                    ['<button>NO</button>', function (instance, toast) {
                        instance.hide({}, toast, 'button');
                    }]
                ]
            });
            return false;
        }

        function mostrarMensaje(tipo, mensaje) {
            if (tipo === 'success') {
                iziToast.success({
                    title: 'Éxito',
                    message: mensaje,
                    position: 'topRight'
                });
            } else if (tipo === 'error') {
                iziToast.error({
                    title: 'Error',
                    message: mensaje,
                    position: 'topRight'
                });
            }
        }
    </script>
</asp:Content>
