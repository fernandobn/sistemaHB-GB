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
                    <!-- PRIMERA FILA - 3 columnas -->
                    <div class="col-md-4">
                        <asp:HiddenField ID="hdnPreId" runat="server" />
                        
                        <!-- Grupo 1: Identificación básica -->
                        <div class="form-group">
                            <label>Código Catastral *</label>
                            <asp:TextBox ID="txtCodigoCatastral" runat="server" CssClass="form-control" MaxLength="50" required="true" />
                        </div>
                        
                        <div class="form-group">
                            <label>Fecha Ingreso</label>
                            <asp:TextBox ID="txtFechaIngreso" runat="server" CssClass="form-control" TextMode="DateTimeLocal" />
                        </div>
                        
                        <div class="form-group">
                            <label>Código Anterior</label>
                            <asp:TextBox ID="txtCodigoAnterior" runat="server" CssClass="form-control" MaxLength="50" />
                        </div>
                        
                        <div class="form-group">
                            <label>Número</label>
                            <asp:TextBox ID="txtNumero" runat="server" CssClass="form-control" MaxLength="20" />
                        </div>
                        
                        <div class="form-group">
                            <label>Nombre Predio *</label>
                            <asp:TextBox ID="txtNombrePredio" runat="server" CssClass="form-control" MaxLength="100" required="true" />
                        </div>
                    </div>

                    <div class="col-md-4">
                        <!-- Grupo 2: Dimensiones -->
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>Área Total Terreno (m²)</label>
                                    <asp:TextBox ID="txtAreaTotalTer" runat="server" CssClass="form-control" TextMode="Number" step="0.0001" />
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>Área Total Construcción (m²)</label>
                                    <asp:TextBox ID="txtAreaTotalConst" runat="server" CssClass="form-control" TextMode="Number" step="0.0001" />
                                </div>
                            </div>
                        </div>
                        
                        <div class="form-group">
                            <label>Fondo Relativo</label>
                            <asp:TextBox ID="txtFondoRelativo" runat="server" CssClass="form-control" TextMode="Number" step="0.0001" />
                        </div>

                        <div class="form-group">
                            <label>Frente Fondo</label>
                            <asp:TextBox ID="txtFrenteFondo" runat="server" CssClass="form-control" TextMode="Number" step="0.0001" />
                        </div>
                        
                        <div class="form-group">
                            <label>Propiedad Horizontal</label>
                            <asp:TextBox ID="txtPropiedadHorizontal" runat="server" CssClass="form-control" MaxLength="50" />
                        </div>
                        
                        <div class="form-group">
                            <label>Tipo</label>
                            <asp:DropDownList ID="ddlTipo" runat="server" CssClass="form-control select2">
                                <asp:ListItem Text="Seleccione Tipo" Value="" />
                                <asp:ListItem Text="Tipo 1" Value="1" />
                                <asp:ListItem Text="Tipo 2" Value="2" />
                            </asp:DropDownList>
                        </div>
                    </div>

                    <div class="col-md-4">
                        <!-- Grupo 3: Estado y dominio -->
                        <div class="form-group">
                            <label>Estado *</label>
                            <asp:DropDownList ID="ddlEstado" runat="server" CssClass="form-control select2" required="true">
                                <asp:ListItem Text="Seleccione Estado" Value="" />
                                <asp:ListItem Text="Activo" Value="1" Selected="True" />
                                <asp:ListItem Text="Inactivo" Value="0" />
                            </asp:DropDownList>
                        </div>
                        
                        <div class="form-group">
                            <label>Dominio</label>
                            <asp:DropDownList ID="ddlDominio" runat="server" CssClass="form-control select2" AppendDataBoundItems="true">
                                <asp:ListItem Text="Seleccione Dominio" Value="" />
                            </asp:DropDownList>
                        </div>
                        
                        <div class="form-group">
                            <label>Condición Ocupación</label>
                            <asp:DropDownList ID="ddlCondicionOcupacion" runat="server" CssClass="form-control select2" AppendDataBoundItems="true">
                                <asp:ListItem Text="Seleccione Condición Ocupación" Value="" />
                            </asp:DropDownList>
                        </div>
                        
                        <div class="form-group">
                            <label>Manzana</label>
                            <asp:DropDownList ID="ddlManzana" runat="server" CssClass="form-control select2" AppendDataBoundItems="true">
                                <asp:ListItem Text="Seleccione Manzana" Value="" />
                            </asp:DropDownList>
                        </div>
                        
                        <div class="form-group">
                            <label>Tipo Mixto</label>
                            <asp:DropDownList ID="ddlTipoMixto" runat="server" CssClass="form-control select2">
                                <asp:ListItem Text="Seleccione Tipo Mixto" Value="" />
                                <asp:ListItem Text="Tipo 1" Value="1" />
                                <asp:ListItem Text="Tipo 2" Value="2" />
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>

                <!-- SEGUNDA FILA - 3 columnas -->
                <div class="row g-3 mt-3">
                    <div class="col-md-4">
                        <!-- Grupo 4: Documentación -->
                        <div class="form-group">
                            <label>Dimensión Tomada de Planos</label>
                            <asp:TextBox ID="txtDimTomadoPlanos" runat="server" CssClass="form-control" MaxLength="100" />
                        </div>
                        
                        <div class="form-group">
                            <label>Otra Fuente de Información</label>
                            <asp:TextBox ID="txtOtraFuenteInfo" runat="server" CssClass="form-control" MaxLength="100" />
                        </div>
                        
                        <div class="form-group">
                            <label>Carta Topográfica</label>
                            <asp:TextBox ID="txtCartaTopografica" runat="server" CssClass="form-control" MaxLength="100" />
                        </div>
                        
                        <div class="form-group">
                            <label>Foto Aérea</label>
                            <asp:TextBox ID="txtFotoAerea" runat="server" CssClass="form-control" MaxLength="100" />
                        </div>
                        
                        <div class="form-group">
                            <label>Número Nuevo Bloque</label>
                            <asp:TextBox ID="txtNumNuevoBloque" runat="server" CssClass="form-control" MaxLength="20" />
                        </div>
                    </div>

                    <div class="col-md-4">
                        <!-- Grupo 5: Población -->
                        <div class="form-group">
                            <label>Número de Habitantes</label>
                            <asp:TextBox ID="txtNumHabitantes" runat="server" CssClass="form-control" TextMode="Number" />
                        </div>
                        
                        <div class="form-group">
                            <label>Número de Familias</label>
                            <asp:TextBox ID="txtNumFamilias" runat="server" CssClass="form-control" TextMode="Number" />
                        </div>
                        
                        <div class="form-group">
                            <label>Número de Celulares</label>
                            <asp:TextBox ID="txtNumCelulares" runat="server" CssClass="form-control" TextMode="Number" />
                        </div>
                        
                        <div class="form-group">
                            <label>Propietario Anterior</label>
                            <asp:TextBox ID="txtPropietarioAnterior" runat="server" CssClass="form-control" MaxLength="100" />
                        </div>
                        
                        <div class="form-group">
                            <label>Número Ampliación Bloque</label>
                            <asp:TextBox ID="txtNumAmpliBloque" runat="server" CssClass="form-control" TextMode="Number" />
                        </div>
                    </div>

                    <div class="col-md-4">
                        <!-- Grupo 6: Dominio y ubicación -->
                        <div class="form-group">
                            <label>Porcentaje Dominio (%)</label>
                            <asp:TextBox ID="txtPorcentajeDominio" runat="server" CssClass="form-control" TextMode="Number" step="0.01" min="0" max="100" />
                        </div>
                        
                        <div class="form-group">
                            <label>Detalle Dominio</label>
                            <asp:TextBox ID="txtDetalleDominio" runat="server" CssClass="form-control" MaxLength="200" />
                        </div>
                        
                        <div class="form-group">
                            <label>Valor Tipo Mixto</label>
                            <asp:TextBox ID="txtValorTipoMixto" runat="server" CssClass="form-control" TextMode="Number" step="0.0001" />
                        </div>
                        
                        <div class="form-group">
                            <label>Dirección Principal *</label>
                            <asp:TextBox ID="txtDireccionPrincipal" runat="server" CssClass="form-control" MaxLength="200" required="true" />
                        </div>
                        
                        <div class="form-group">
                            <label>Localización Otros</label>
                            <asp:TextBox ID="txtLocalizacionOtros" runat="server" CssClass="form-control" MaxLength="200" />
                        </div>
                    </div>
                </div>

                <!-- TERCERA FILA - 3 columnas -->
                <div class="row g-3 mt-3">
                    <div class="col-md-4">
                        <!-- Grupo 7: Áreas adicionales -->
                        <div class="form-group">
                            <label>Área Terreno Anterior (m²)</label>
                            <asp:TextBox ID="txtAreaTotalTerrenoAnterior" runat="server" CssClass="form-control" TextMode="Number" step="0.0001" />
                        </div>
                        
                        <div class="form-group">
                            <label>Área Terreno Gráfico (m²)</label>
                            <asp:TextBox ID="txtAreaTotalTerGrafico" runat="server" CssClass="form-control" TextMode="Number" step="0.0001" />
                        </div>
                        
                        <div class="form-group">
                            <label>Área Terreno Alfanumérico (m²)</label>
                            <asp:TextBox ID="txtAreaTotalTerAlfanumerico" runat="server" CssClass="form-control" TextMode="Number" step="0.0001" />
                        </div>
                        
                        <div class="form-group">
                            <label>Área Construcción Alfanumérico (m²)</label>
                            <asp:TextBox ID="txtAreaTotalConstAlfanumerico" runat="server" CssClass="form-control" TextMode="Number" step="0.0001" />
                        </div>
                    </div>

                    <div class="col-md-4">
                        <!-- Grupo 8: Vivienda y estado -->
                        <div class="form-group">
                            <label>Tipo Vivienda</label>
                            <asp:TextBox ID="txtTipoVivienda" runat="server" CssClass="form-control" MaxLength="50" />
                        </div>
                        
                        <div class="form-group">
                            <label>Clasificación Vivienda</label>
                            <asp:DropDownList ID="ddlClasificacionVivienda" runat="server" CssClass="form-control select2" AppendDataBoundItems="true">
                                <asp:ListItem Text="Seleccione Clasificación Vivienda" Value="" />
                            </asp:DropDownList>
                        </div>
                        
                        <div class="form-group">
                            <label>Linderos Definidos</label>
                            <asp:DropDownList ID="ddlLinderosDefinidos" runat="server" CssClass="form-control select2">
                                <asp:ListItem Text="Seleccione" Value="" />
                                <asp:ListItem Text="Sí" Value="1" />
                                <asp:ListItem Text="No" Value="0" />
                            </asp:DropDownList>
                        </div>
                        
                        <div class="form-group">
                            <label>Bien Mostrenco</label>
                            <asp:DropDownList ID="ddlBienMostrenco" runat="server" CssClass="form-control select2">
                                <asp:ListItem Text="Seleccione" Value="" />
                                <asp:ListItem Text="Sí" Value="1" />
                                <asp:ListItem Text="No" Value="0" />
                            </asp:DropDownList>
                        </div>
                    </div>

                    <div class="col-md-4">
                        <!-- Grupo 9: Propiedad horizontal -->
                        <div class="form-group">
                            <label>Dominio Detalle</label>
                            <asp:DropDownList ID="ddlDominioDetalle" runat="server" CssClass="form-control select2">
                                <asp:ListItem Text="Seleccione" Value="" />
                                <asp:ListItem Text="Opción 1" Value="1" />
                                <asp:ListItem Text="Opción 2" Value="2" />
                            </asp:DropDownList>
                        </div>
                        
                        <div class="form-group">
                            <label>En Conflicto</label>
                            <asp:DropDownList ID="ddlEnConflicto" runat="server" CssClass="form-control select2">
                                <asp:ListItem Text="Seleccione" Value="" />
                                <asp:ListItem Text="Sí" Value="1" />
                                <asp:ListItem Text="No" Value="0" />
                            </asp:DropDownList>
                        </div>
                        
                        <div class="form-group">
                            <label>Propietario Desconocido</label>
                            <asp:DropDownList ID="ddlPropietarioDesconocido" runat="server" CssClass="form-control select2">
                                <asp:ListItem Text="Seleccione" Value="" />
                                <asp:ListItem Text="Sí" Value="1" />
                                <asp:ListItem Text="No" Value="0" />
                            </asp:DropDownList>
                        </div>
                        
                        <div class="form-group">
                            <label>Fecha Modificación</label>
                            <asp:TextBox ID="txtFechaModificacion" runat="server" CssClass="form-control" TextMode="DateTimeLocal" Enabled="false" />
                        </div>
                    </div>
                </div>

                <!-- CUARTA FILA - 3 columnas -->
                <div class="row g-3 mt-3">
                    <div class="col-md-4">
                        <!-- Grupo 10: PH - Parte 1 -->
                        <div class="form-group">
                            <label>Modalidad Propiedad Horizontal</label>
                            <asp:DropDownList ID="ddlModalidadPropiedadHorizontal" runat="server" CssClass="form-control select2">
                                <asp:ListItem Text="Seleccione Modalidad PH" Value="" />
                                <asp:ListItem Text="Modalidad 1" Value="1" />
                                <asp:ListItem Text="Modalidad 2" Value="2" />
                            </asp:DropDownList>
                        </div>
                        
                        <div class="form-group">
                            <label>Alícuota Total Declaratoria (%)</label>
                            <asp:TextBox ID="txtAlicuotaTotalDeclaratoria" runat="server" CssClass="form-control" TextMode="Number" step="0.0001" min="0" max="100" />
                        </div>
                    </div>

                    <div class="col-md-4">
                        <!-- Grupo 11: PH - Parte 2 -->
                        <div class="form-group">
                            <label>Tipo Propiedad Horizontal</label>
                            <asp:DropDownList ID="ddlTipoPropiedadHorizontal" runat="server" CssClass="form-control select2">
                                <asp:ListItem Text="Seleccione Tipo PH" Value="" />
                                <asp:ListItem Text="Tipo 1" Value="1" />
                                <asp:ListItem Text="Tipo 2" Value="2" />
                            </asp:DropDownList>
                        </div>
                        
                        <div class="form-group">
                            <label>Hipoteca GAD</label>
                            <asp:DropDownList ID="ddlHipotecaGAD" runat="server" CssClass="form-control select2">
                                <asp:ListItem Text="Seleccione" Value="" />
                                <asp:ListItem Text="Sí" Value="1" />
                                <asp:ListItem Text="No" Value="0" />
                            </asp:DropDownList>
                        </div>
                    </div>

                    <div class="col-md-4">
                        <!-- Grupo 12: PH - Parte 3 -->
                        <div class="form-group">
                            <label>Régimen Propiedad Horizontal</label>
                            <asp:DropDownList ID="ddlRegimenPropiedadHorizontal" runat="server" CssClass="form-control select2">
                                <asp:ListItem Text="Seleccione Régimen PH" Value="" />
                                <asp:ListItem Text="Régimen 1" Value="1" />
                                <asp:ListItem Text="Régimen 2" Value="2" />
                            </asp:DropDownList>
                        </div>
                        
                        <div class="form-group">
                            <label>Prorrateo Título</label>
                            <asp:DropDownList ID="ddlProrrateoTitulo" runat="server" CssClass="form-control select2">
                                <asp:ListItem Text="Seleccione" Value="" />
                                <asp:ListItem Text="Sí" Value="1" />
                                <asp:ListItem Text="No" Value="0" />
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>

                <!-- QUINTA FILA - Observaciones y geometría -->
                <div class="row g-3 mt-3">
                    <div class="col-md-6">
                        <div class="form-group">
                            <label>Observaciones</label>
                            <asp:TextBox ID="txtObservaciones" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="2" MaxLength="500" />
                        </div>
                    </div>
                    
                    <div class="col-md-6">
                        <div class="form-group">
                            <label>Observación PH</label>
                            <asp:TextBox ID="txtObservacionPH" runat="server" CssClass="form-control" MaxLength="500" TextMode="MultiLine" Rows="2" />
                        </div>
                    </div>
                </div>

                <!-- SEXTA FILA - Geometría -->
                <div class="row g-3 mt-3">
                    <div class="col-md-12">
                        <div class="form-group">
                            <label>Geometría (WKT)</label>
                            <asp:TextBox ID="txtGeometria" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3" />
                        </div>
                    </div>
                </div>

                <!-- Botones -->
                <div class="text-center mt-4">
                    <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-primary btn-lg rounded" OnClick="btnGuardar_Click" />
                    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-secondary btn-lg rounded" OnClick="btnCancelar_Click" CausesValidation="false" />
                </div>
            </asp:Panel>
            
            <!-- GridView de Predios -->
            <div class="mt-5">
                <h4 class="mb-3">Listado de Predios</h4>
                <asp:GridView ID="gvPredios" runat="server" CssClass="table table-bordered table-striped table-hover"
                    AllowPaging="true" PageSize="10" AutoGenerateColumns="False"
                    OnPageIndexChanging="gvPredios_PageIndexChanging" OnRowCommand="gvPredios_RowCommand"
                    EmptyDataText="No se encontraron predios registrados">
                    <HeaderStyle CssClass="table-primary" />
                    <Columns>
                        <asp:BoundField DataField="pre_id" HeaderText="ID Predio" SortExpression="pre_id" />
                        <asp:BoundField DataField="pre_codigo_catastral" HeaderText="Código Catastral" SortExpression="pre_codigo_catastral" />
                        <asp:BoundField DataField="pre_nombre_predio" HeaderText="Nombre Predio" SortExpression="pre_nombre_predio" />
                        <asp:BoundField DataField="pre_area_total_ter" HeaderText="Área Terreno (m²)" DataFormatString="{0:N2}" SortExpression="pre_area_total_ter" />
                        <asp:BoundField DataField="pre_direccion_principal" HeaderText="Dirección" SortExpression="pre_direccion_principal" />
                        <asp:TemplateField HeaderText="Estado" SortExpression="pre_estado">
                            <ItemTemplate>
                                <asp:Label Text='<%# (Eval("pre_estado").ToString() == "1") ? "Activo" : "Inactivo" %>' 
                                    CssClass='<%# (Eval("pre_estado").ToString() == "1") ? "badge bg-success" : "badge bg-danger" %>' 
                                    runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Mapa">
                            <ItemTemplate>
                                <a href='MapaPredio.aspx?id=<%# Eval("pre_id") %>&nombre=<%# Eval("pre_nombre_predio") %>&area=<%# Eval("pre_area_total_ter") %>' target="_blank" class="btn btn-sm btn-info">
                                    <i class="fas fa-map-marked-alt"></i> Ver
                                </a>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Acciones">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnEditar" runat="server" CommandName="Editar" CommandArgument='<%# Eval("pre_id") %>' 
                                    CssClass="btn btn-sm btn-warning" ToolTip="Editar">
                                    <i class="fas fa-edit"></i>
                                </asp:LinkButton>
                                <asp:LinkButton ID="btnEliminar" runat="server" CommandName="Eliminar" CommandArgument='<%# Eval("pre_id") %>'
                                    CssClass="btn btn-sm btn-danger" OnClientClick="return confirm('¿Está seguro de eliminar este predio?');" ToolTip="Eliminar">
                                    <i class="fas fa-trash-alt"></i>
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <PagerStyle CssClass="pagination" />
                    <PagerSettings Mode="NumericFirstLast" />
                </asp:GridView>
            </div>
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
            
            // Formatear campos numéricos
            $('input[type="number"]').on('input', function() {
                this.value = this.value.replace(/[^0-9.]/g, '').replace(/(\..*)\./g, '$1');
            });
        });
    </script>
    
    <script type="text/javascript">
        function mostrarMensaje(tipo, mensaje) {
            Swal.fire({
                icon: tipo,
                title: tipo === 'success' ? 'Éxito' : 'Error',
                text: mensaje,
                confirmButtonText: 'Aceptar'
            });
        }
    </script>
</asp:Content>