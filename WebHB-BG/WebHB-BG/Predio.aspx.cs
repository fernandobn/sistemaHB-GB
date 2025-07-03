using Npgsql;
using NetTopologySuite.Geometries;
using System;
using System.Configuration;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebHB_BG
{
    public partial class Predio : Page
    {
        string cadenaConexion = ConfigurationManager.ConnectionStrings["conexionGad"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Configurar el mapeo de tipos para PostGIS (solo una vez)
            NpgsqlConnection.GlobalTypeMapper.UseNetTopologySuite();

            if (!IsPostBack)
            {
                CargarCatalogos();
                CargarPredios();

                if (Request.QueryString["id"] != null)
                {
                    int idPredio = Convert.ToInt32(Request.QueryString["id"]);
                    CargarDatosPredio(idPredio);
                }
            }
        }

        private void CargarCatalogos()
        {
            CargarCatalogo("DOMINIO", ddlDominio);
            CargarCatalogo("CONDICION_OCUPACION", ddlCondicionOcupacion);
            CargarCatalogo("CLASIFICACION_VIVIENDA", ddlClasificacionVivienda);
            CargarManzanas();
        }

        private void CargarCatalogo(string tipo, DropDownList ddl)
        {
            string sql = "SELECT cat_id, cat_nombre FROM gestion.ges_catalogo WHERE cat_tag = @tipo ORDER BY cat_nombre";

            using (var conn = new NpgsqlConnection(cadenaConexion))
            using (var cmd = new NpgsqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@tipo", tipo);
                conn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    ddl.DataSource = reader;
                    ddl.DataValueField = "cat_id";
                    ddl.DataTextField = "cat_nombre";
                    ddl.DataBind();
                }
            }

            ddl.Items.Insert(0, new ListItem("-- Seleccionar --", ""));
        }

        private void CargarManzanas()
        {
            string sql = "SELECT man_id, man_clave_catastral FROM catastro.cat_manzana ORDER BY man_clave_catastral";

            using (var conn = new NpgsqlConnection(cadenaConexion))
            using (var cmd = new NpgsqlCommand(sql, conn))
            {
                conn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    ddlManzana.DataSource = reader;
                    ddlManzana.DataValueField = "man_id";
                    ddlManzana.DataTextField = "man_clave_catastral";
                    ddlManzana.DataBind();
                }
            }

            ddlManzana.Items.Insert(0, new ListItem("-- Seleccionar --", ""));
        }

        private void CargarPredios()
        {
            using (var conn = new NpgsqlConnection(cadenaConexion))
            using (var cmd = new NpgsqlCommand("catastro.listar_predios", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    DataTable dt = new DataTable();
                    dt.Load(reader);
                    gvPredios.DataSource = dt;
                    gvPredios.DataBind();
                }
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                bool esEdicion = hdnPreId.Value != "";

                using (var conn = new NpgsqlConnection(cadenaConexion))
                {
                    conn.Open();

                    if (esEdicion)
                    {
                        ActualizarPredio(conn);
                    }
                    else
                    {
                        InsertarPredio(conn);
                    }
                }

                ScriptManager.RegisterStartupScript(this, GetType(), "success",
                    "mostrarMensaje('success', 'Predio " + (esEdicion ? "actualizado" : "registrado") + " correctamente');", true);

                LimpiarFormulario();
                CargarPredios();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "error",
                    "mostrarMensaje('error', 'Error: " + ex.Message.Replace("'", "") + "');", true);
            }
        }

        private void InsertarPredio(NpgsqlConnection conn)
        {
            using (var cmd = new NpgsqlCommand("catastro.insertar_predio", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                // Agregar todos los parámetros
                cmd.Parameters.AddWithValue("p_pre_codigo_catastral", txtCodigoCatastral.Text.Trim());
                cmd.Parameters.AddWithValue("p_pre_fecha_ingreso", string.IsNullOrEmpty(txtFechaIngreso.Text) ? (object)DBNull.Value : DateTime.Parse(txtFechaIngreso.Text));
                cmd.Parameters.AddWithValue("p_pre_codigo_anterior", txtCodigoAnterior.Text.Trim());
                cmd.Parameters.AddWithValue("p_pre_numero", txtNumero.Text.Trim());
                cmd.Parameters.AddWithValue("p_pre_nombre_predio", txtNombrePredio.Text.Trim());
                cmd.Parameters.AddWithValue("p_pre_area_total_ter", string.IsNullOrEmpty(txtAreaTotalTer.Text) ? (object)DBNull.Value : decimal.Parse(txtAreaTotalTer.Text));
                cmd.Parameters.AddWithValue("p_pre_area_total_const", string.IsNullOrEmpty(txtAreaTotalConst.Text) ? (object)DBNull.Value : decimal.Parse(txtAreaTotalConst.Text));
                cmd.Parameters.AddWithValue("p_pre_fondo_relativo", string.IsNullOrEmpty(txtFondoRelativo.Text) ? (object)DBNull.Value : decimal.Parse(txtFondoRelativo.Text));
                cmd.Parameters.AddWithValue("p_pre_frente_fondo", string.IsNullOrEmpty(txtFrenteFondo.Text) ? (object)DBNull.Value : decimal.Parse(txtFrenteFondo.Text));
                cmd.Parameters.AddWithValue("p_pre_observaciones", txtObservaciones.Text.Trim());
                cmd.Parameters.AddWithValue("p_pre_dim_tomado_planos", txtDimTomadoPlanos.Text.Trim());
                cmd.Parameters.AddWithValue("p_pre_otra_fuente_info", txtOtraFuenteInfo.Text.Trim());
                cmd.Parameters.AddWithValue("p_pre_num_nuevo_bloque", txtNumNuevoBloque.Text.Trim());
                cmd.Parameters.AddWithValue("p_pre_num_ampli_bloque", string.IsNullOrEmpty(txtNumAmpliBloque.Text) ? (object)DBNull.Value : short.Parse(txtNumAmpliBloque.Text));
                cmd.Parameters.AddWithValue("p_pre_tipo", string.IsNullOrEmpty(ddlTipo.SelectedValue) ? (object)DBNull.Value : short.Parse(ddlTipo.SelectedValue));
                cmd.Parameters.AddWithValue("p_pre_propiedad_horizontal", txtPropiedadHorizontal.Text.Trim());
                cmd.Parameters.AddWithValue("p_pre_estado", string.IsNullOrEmpty(ddlEstado.SelectedValue) ? (object)DBNull.Value : short.Parse(ddlEstado.SelectedValue));
                cmd.Parameters.AddWithValue("p_pre_dominio", string.IsNullOrEmpty(ddlDominio.SelectedValue) ? (object)DBNull.Value : int.Parse(ddlDominio.SelectedValue));

                // Manejo de geometría como texto WKT
                if (!string.IsNullOrEmpty(txtGeometria.Text))
                {
                    cmd.Parameters.AddWithValue("p_geometria", NpgsqlTypes.NpgsqlDbType.Geometry, txtGeometria.Text);
                }
                else
                {
                    cmd.Parameters.AddWithValue("p_geometria", DBNull.Value);
                }

                cmd.Parameters.AddWithValue("p_opc_condicion_ocupacion", string.IsNullOrEmpty(ddlCondicionOcupacion.SelectedValue) ? (object)DBNull.Value : int.Parse(ddlCondicionOcupacion.SelectedValue));
                cmd.Parameters.AddWithValue("p_pre_num_habitantes", string.IsNullOrEmpty(txtNumHabitantes.Text) ? (object)DBNull.Value : int.Parse(txtNumHabitantes.Text));
                cmd.Parameters.AddWithValue("p_pre_propietario_anterior", txtPropietarioAnterior.Text.Trim());
                cmd.Parameters.AddWithValue("p_pre_carta_topografica", txtCartaTopografica.Text.Trim());
                cmd.Parameters.AddWithValue("p_pre_foto_aerea", txtFotoAerea.Text.Trim());
                cmd.Parameters.AddWithValue("p_man_id", string.IsNullOrEmpty(ddlManzana.SelectedValue) ? (object)DBNull.Value : short.Parse(ddlManzana.SelectedValue));
                cmd.Parameters.AddWithValue("p_pre_num_familias", string.IsNullOrEmpty(txtNumFamilias.Text) ? (object)DBNull.Value : short.Parse(txtNumFamilias.Text));
                cmd.Parameters.AddWithValue("p_pre_porcentaje_dominio", string.IsNullOrEmpty(txtPorcentajeDominio.Text) ? (object)DBNull.Value : decimal.Parse(txtPorcentajeDominio.Text));
                cmd.Parameters.AddWithValue("p_pre_detalle_dominio", txtDetalleDominio.Text.Trim());
                cmd.Parameters.AddWithValue("p_pre_tipo_mixto", string.IsNullOrEmpty(ddlTipoMixto.SelectedValue) ? (object)DBNull.Value : short.Parse(ddlTipoMixto.SelectedValue));
                cmd.Parameters.AddWithValue("p_pre_valor_tipo_mixto", string.IsNullOrEmpty(txtValorTipoMixto.Text) ? (object)DBNull.Value : decimal.Parse(txtValorTipoMixto.Text));
                cmd.Parameters.AddWithValue("p_pre_linderos_definidos", string.IsNullOrEmpty(ddlLinderosDefinidos.SelectedValue) ? (object)DBNull.Value : short.Parse(ddlLinderosDefinidos.SelectedValue));
                cmd.Parameters.AddWithValue("p_pre_area_total_terreno_anterior", string.IsNullOrEmpty(txtAreaTotalTerrenoAnterior.Text) ? (object)DBNull.Value : decimal.Parse(txtAreaTotalTerrenoAnterior.Text));
                cmd.Parameters.AddWithValue("p_pre_localizacion_otros", txtLocalizacionOtros.Text.Trim());
                cmd.Parameters.AddWithValue("p_pre_bien_mostrenco", string.IsNullOrEmpty(ddlBienMostrenco.SelectedValue) ? (object)DBNull.Value : short.Parse(ddlBienMostrenco.SelectedValue));
                cmd.Parameters.AddWithValue("p_pre_en_conflicto", string.IsNullOrEmpty(ddlEnConflicto.SelectedValue) ? (object)DBNull.Value : short.Parse(ddlEnConflicto.SelectedValue));
                cmd.Parameters.AddWithValue("p_pre_area_total_ter_grafico", string.IsNullOrEmpty(txtAreaTotalTerGrafico.Text) ? (object)DBNull.Value : decimal.Parse(txtAreaTotalTerGrafico.Text));
                cmd.Parameters.AddWithValue("p_pre_propietario_desconocido", string.IsNullOrEmpty(ddlPropietarioDesconocido.SelectedValue) ? (object)DBNull.Value : short.Parse(ddlPropietarioDesconocido.SelectedValue));
                cmd.Parameters.AddWithValue("p_pre_area_total_ter_alfanumerico", string.IsNullOrEmpty(txtAreaTotalTerAlfanumerico.Text) ? (object)DBNull.Value : decimal.Parse(txtAreaTotalTerAlfanumerico.Text));
                cmd.Parameters.AddWithValue("p_pre_dominio_detalle", string.IsNullOrEmpty(ddlDominioDetalle.SelectedValue) ? (object)DBNull.Value : short.Parse(ddlDominioDetalle.SelectedValue));
                cmd.Parameters.AddWithValue("p_pre_direccion_principal", txtDireccionPrincipal.Text.Trim());
                cmd.Parameters.AddWithValue("p_pre_area_total_const_alfanumerico", string.IsNullOrEmpty(txtAreaTotalConstAlfanumerico.Text) ? (object)DBNull.Value : decimal.Parse(txtAreaTotalConstAlfanumerico.Text));
                cmd.Parameters.AddWithValue("p_pre_tipo_vivienda", txtTipoVivienda.Text.Trim());
                cmd.Parameters.AddWithValue("p_opc_clasificacion_vivienda", string.IsNullOrEmpty(ddlClasificacionVivienda.SelectedValue) ? (object)DBNull.Value : int.Parse(ddlClasificacionVivienda.SelectedValue));
                cmd.Parameters.AddWithValue("p_pre_fecha_modificacion", string.IsNullOrEmpty(txtFechaModificacion.Text) ? (object)DBNull.Value : DateTime.Parse(txtFechaModificacion.Text));
                cmd.Parameters.AddWithValue("p_pre_num_celulares", string.IsNullOrEmpty(txtNumCelulares.Text) ? (object)DBNull.Value : short.Parse(txtNumCelulares.Text));
                cmd.Parameters.AddWithValue("p_pre_modalidad_propiedad_horizontal", string.IsNullOrEmpty(ddlModalidadPropiedadHorizontal.SelectedValue) ? (object)DBNull.Value : short.Parse(ddlModalidadPropiedadHorizontal.SelectedValue));
                cmd.Parameters.AddWithValue("p_pre_alicuota_total_declaratoria", string.IsNullOrEmpty(txtAlicuotaTotalDeclaratoria.Text) ? (object)DBNull.Value : decimal.Parse(txtAlicuotaTotalDeclaratoria.Text));
                cmd.Parameters.AddWithValue("p_pre_tipo_propiedad_horizontal", string.IsNullOrEmpty(ddlTipoPropiedadHorizontal.SelectedValue) ? (object)DBNull.Value : short.Parse(ddlTipoPropiedadHorizontal.SelectedValue));
                cmd.Parameters.AddWithValue("p_pre_observacion_ph", txtObservacionPH.Text.Trim());
                cmd.Parameters.AddWithValue("p_pre_hipoteca_gad", string.IsNullOrEmpty(ddlHipotecaGAD.SelectedValue) ? (object)DBNull.Value : short.Parse(ddlHipotecaGAD.SelectedValue));
                cmd.Parameters.AddWithValue("p_pre_regimen_propiedad_horizontal", string.IsNullOrEmpty(ddlRegimenPropiedadHorizontal.SelectedValue) ? (object)DBNull.Value : short.Parse(ddlRegimenPropiedadHorizontal.SelectedValue));
                cmd.Parameters.AddWithValue("p_pre_prorrateo_titulo", string.IsNullOrEmpty(ddlProrrateoTitulo.SelectedValue) ? (object)DBNull.Value : short.Parse(ddlProrrateoTitulo.SelectedValue));

                cmd.ExecuteNonQuery();
            }
        }

        private void ActualizarPredio(NpgsqlConnection conn)
        {
            using (var cmd = new NpgsqlCommand("catastro.actualizar_predio", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("p_pre_id", long.Parse(hdnPreId.Value));
                cmd.Parameters.AddWithValue("p_pre_codigo_catastral", txtCodigoCatastral.Text.Trim());
                cmd.Parameters.AddWithValue("p_pre_fecha_ingreso", string.IsNullOrEmpty(txtFechaIngreso.Text) ? (object)DBNull.Value : DateTime.Parse(txtFechaIngreso.Text));
                cmd.Parameters.AddWithValue("p_pre_codigo_anterior", txtCodigoAnterior.Text.Trim());
                cmd.Parameters.AddWithValue("p_pre_numero", txtNumero.Text.Trim());
                cmd.Parameters.AddWithValue("p_pre_nombre_predio", txtNombrePredio.Text.Trim());
                cmd.Parameters.AddWithValue("p_pre_area_total_ter", string.IsNullOrEmpty(txtAreaTotalTer.Text) ? (object)DBNull.Value : decimal.Parse(txtAreaTotalTer.Text));
                cmd.Parameters.AddWithValue("p_pre_area_total_const", string.IsNullOrEmpty(txtAreaTotalConst.Text) ? (object)DBNull.Value : decimal.Parse(txtAreaTotalConst.Text));
                cmd.Parameters.AddWithValue("p_pre_fondo_relativo", string.IsNullOrEmpty(txtFondoRelativo.Text) ? (object)DBNull.Value : decimal.Parse(txtFondoRelativo.Text));
                cmd.Parameters.AddWithValue("p_pre_frente_fondo", string.IsNullOrEmpty(txtFrenteFondo.Text) ? (object)DBNull.Value : decimal.Parse(txtFrenteFondo.Text));
                cmd.Parameters.AddWithValue("p_pre_observaciones", txtObservaciones.Text.Trim());
                cmd.Parameters.AddWithValue("p_pre_dim_tomado_planos", txtDimTomadoPlanos.Text.Trim());
                cmd.Parameters.AddWithValue("p_pre_otra_fuente_info", txtOtraFuenteInfo.Text.Trim());
                cmd.Parameters.AddWithValue("p_pre_num_nuevo_bloque", txtNumNuevoBloque.Text.Trim());
                cmd.Parameters.AddWithValue("p_pre_num_ampli_bloque", string.IsNullOrEmpty(txtNumAmpliBloque.Text) ? (object)DBNull.Value : short.Parse(txtNumAmpliBloque.Text));
                cmd.Parameters.AddWithValue("p_pre_tipo", string.IsNullOrEmpty(ddlTipo.SelectedValue) ? (object)DBNull.Value : short.Parse(ddlTipo.SelectedValue));
                cmd.Parameters.AddWithValue("p_pre_propiedad_horizontal", txtPropiedadHorizontal.Text.Trim());
                cmd.Parameters.AddWithValue("p_pre_estado", string.IsNullOrEmpty(ddlEstado.SelectedValue) ? (object)DBNull.Value : short.Parse(ddlEstado.SelectedValue));
                cmd.Parameters.AddWithValue("p_pre_dominio", string.IsNullOrEmpty(ddlDominio.SelectedValue) ? (object)DBNull.Value : int.Parse(ddlDominio.SelectedValue));

                // Manejo de geometría como texto WKT
                if (!string.IsNullOrEmpty(txtGeometria.Text))
                {
                    cmd.Parameters.AddWithValue("p_geometria", NpgsqlTypes.NpgsqlDbType.Geometry, txtGeometria.Text);
                }
                else
                {
                    cmd.Parameters.AddWithValue("p_geometria", DBNull.Value);
                }

                cmd.Parameters.AddWithValue("p_opc_condicion_ocupacion", string.IsNullOrEmpty(ddlCondicionOcupacion.SelectedValue) ? (object)DBNull.Value : int.Parse(ddlCondicionOcupacion.SelectedValue));
                cmd.Parameters.AddWithValue("p_pre_num_habitantes", string.IsNullOrEmpty(txtNumHabitantes.Text) ? (object)DBNull.Value : int.Parse(txtNumHabitantes.Text));
                cmd.Parameters.AddWithValue("p_pre_propietario_anterior", txtPropietarioAnterior.Text.Trim());
                cmd.Parameters.AddWithValue("p_pre_carta_topografica", txtCartaTopografica.Text.Trim());
                cmd.Parameters.AddWithValue("p_pre_foto_aerea", txtFotoAerea.Text.Trim());
                cmd.Parameters.AddWithValue("p_man_id", string.IsNullOrEmpty(ddlManzana.SelectedValue) ? (object)DBNull.Value : short.Parse(ddlManzana.SelectedValue));
                cmd.Parameters.AddWithValue("p_pre_num_familias", string.IsNullOrEmpty(txtNumFamilias.Text) ? (object)DBNull.Value : short.Parse(txtNumFamilias.Text));
                cmd.Parameters.AddWithValue("p_pre_porcentaje_dominio", string.IsNullOrEmpty(txtPorcentajeDominio.Text) ? (object)DBNull.Value : decimal.Parse(txtPorcentajeDominio.Text));
                cmd.Parameters.AddWithValue("p_pre_detalle_dominio", txtDetalleDominio.Text.Trim());
                cmd.Parameters.AddWithValue("p_pre_tipo_mixto", string.IsNullOrEmpty(ddlTipoMixto.SelectedValue) ? (object)DBNull.Value : short.Parse(ddlTipoMixto.SelectedValue));
                cmd.Parameters.AddWithValue("p_pre_valor_tipo_mixto", string.IsNullOrEmpty(txtValorTipoMixto.Text) ? (object)DBNull.Value : decimal.Parse(txtValorTipoMixto.Text));
                cmd.Parameters.AddWithValue("p_pre_linderos_definidos", string.IsNullOrEmpty(ddlLinderosDefinidos.SelectedValue) ? (object)DBNull.Value : short.Parse(ddlLinderosDefinidos.SelectedValue));
                cmd.Parameters.AddWithValue("p_pre_area_total_terreno_anterior", string.IsNullOrEmpty(txtAreaTotalTerrenoAnterior.Text) ? (object)DBNull.Value : decimal.Parse(txtAreaTotalTerrenoAnterior.Text));
                cmd.Parameters.AddWithValue("p_pre_localizacion_otros", txtLocalizacionOtros.Text.Trim());
                cmd.Parameters.AddWithValue("p_pre_bien_mostrenco", string.IsNullOrEmpty(ddlBienMostrenco.SelectedValue) ? (object)DBNull.Value : short.Parse(ddlBienMostrenco.SelectedValue));
                cmd.Parameters.AddWithValue("p_pre_en_conflicto", string.IsNullOrEmpty(ddlEnConflicto.SelectedValue) ? (object)DBNull.Value : short.Parse(ddlEnConflicto.SelectedValue));
                cmd.Parameters.AddWithValue("p_pre_area_total_ter_grafico", string.IsNullOrEmpty(txtAreaTotalTerGrafico.Text) ? (object)DBNull.Value : decimal.Parse(txtAreaTotalTerGrafico.Text));
                cmd.Parameters.AddWithValue("p_pre_propietario_desconocido", string.IsNullOrEmpty(ddlPropietarioDesconocido.SelectedValue) ? (object)DBNull.Value : short.Parse(ddlPropietarioDesconocido.SelectedValue));
                cmd.Parameters.AddWithValue("p_pre_area_total_ter_alfanumerico", string.IsNullOrEmpty(txtAreaTotalTerAlfanumerico.Text) ? (object)DBNull.Value : decimal.Parse(txtAreaTotalTerAlfanumerico.Text));
                cmd.Parameters.AddWithValue("p_pre_dominio_detalle", string.IsNullOrEmpty(ddlDominioDetalle.SelectedValue) ? (object)DBNull.Value : short.Parse(ddlDominioDetalle.SelectedValue));
                cmd.Parameters.AddWithValue("p_pre_direccion_principal", txtDireccionPrincipal.Text.Trim());
                cmd.Parameters.AddWithValue("p_pre_area_total_const_alfanumerico", string.IsNullOrEmpty(txtAreaTotalConstAlfanumerico.Text) ? (object)DBNull.Value : decimal.Parse(txtAreaTotalConstAlfanumerico.Text));
                cmd.Parameters.AddWithValue("p_pre_tipo_vivienda", txtTipoVivienda.Text.Trim());
                cmd.Parameters.AddWithValue("p_opc_clasificacion_vivienda", string.IsNullOrEmpty(ddlClasificacionVivienda.SelectedValue) ? (object)DBNull.Value : int.Parse(ddlClasificacionVivienda.SelectedValue));
                cmd.Parameters.AddWithValue("p_pre_fecha_modificacion", string.IsNullOrEmpty(txtFechaModificacion.Text) ? (object)DBNull.Value : DateTime.Parse(txtFechaModificacion.Text));
                cmd.Parameters.AddWithValue("p_pre_num_celulares", string.IsNullOrEmpty(txtNumCelulares.Text) ? (object)DBNull.Value : short.Parse(txtNumCelulares.Text));
                cmd.Parameters.AddWithValue("p_pre_modalidad_propiedad_horizontal", string.IsNullOrEmpty(ddlModalidadPropiedadHorizontal.SelectedValue) ? (object)DBNull.Value : short.Parse(ddlModalidadPropiedadHorizontal.SelectedValue));
                cmd.Parameters.AddWithValue("p_pre_alicuota_total_declaratoria", string.IsNullOrEmpty(txtAlicuotaTotalDeclaratoria.Text) ? (object)DBNull.Value : decimal.Parse(txtAlicuotaTotalDeclaratoria.Text));
                cmd.Parameters.AddWithValue("p_pre_tipo_propiedad_horizontal", string.IsNullOrEmpty(ddlTipoPropiedadHorizontal.SelectedValue) ? (object)DBNull.Value : short.Parse(ddlTipoPropiedadHorizontal.SelectedValue));
                cmd.Parameters.AddWithValue("p_pre_observacion_ph", txtObservacionPH.Text.Trim());
                cmd.Parameters.AddWithValue("p_pre_hipoteca_gad", string.IsNullOrEmpty(ddlHipotecaGAD.SelectedValue) ? (object)DBNull.Value : short.Parse(ddlHipotecaGAD.SelectedValue));
                cmd.Parameters.AddWithValue("p_pre_regimen_propiedad_horizontal", string.IsNullOrEmpty(ddlRegimenPropiedadHorizontal.SelectedValue) ? (object)DBNull.Value : short.Parse(ddlRegimenPropiedadHorizontal.SelectedValue));
                cmd.Parameters.AddWithValue("p_pre_prorrateo_titulo", string.IsNullOrEmpty(ddlProrrateoTitulo.SelectedValue) ? (object)DBNull.Value : short.Parse(ddlProrrateoTitulo.SelectedValue));

                cmd.ExecuteNonQuery();
            }
        }

        private void CargarDatosPredio(int idPredio)
        {
            // Modificamos la consulta para obtener la geometría como texto WKT
            string sql = @"SELECT *, ST_AsText(geometria) as geometria_texto 
                         FROM catastro.cat_predio WHERE pre_id = @id";

            using (var conn = new NpgsqlConnection(cadenaConexion))
            using (var cmd = new NpgsqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@id", idPredio);
                conn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        hdnPreId.Value = idPredio.ToString();
                        txtCodigoCatastral.Text = reader["pre_codigo_catastral"].ToString();
                        txtFechaIngreso.Text = reader["pre_fecha_ingreso"] != DBNull.Value ?
                            ((DateTime)reader["pre_fecha_ingreso"]).ToString("yyyy-MM-ddTHH:mm") : "";
                        txtCodigoAnterior.Text = reader["pre_codigo_anterior"].ToString();
                        txtNumero.Text = reader["pre_numero"].ToString();
                        txtNombrePredio.Text = reader["pre_nombre_predio"].ToString();
                        txtAreaTotalTer.Text = reader["pre_area_total_ter"] != DBNull.Value ?
                            reader["pre_area_total_ter"].ToString() : "";
                        txtAreaTotalConst.Text = reader["pre_area_total_const"] != DBNull.Value ?
                            reader["pre_area_total_const"].ToString() : "";
                        txtFondoRelativo.Text = reader["pre_fondo_relativo"] != DBNull.Value ?
                            reader["pre_fondo_relativo"].ToString() : "";
                        txtFrenteFondo.Text = reader["pre_frente_fondo"] != DBNull.Value ?
                            reader["pre_frente_fondo"].ToString() : "";
                        txtObservaciones.Text = reader["pre_observaciones"].ToString();
                        txtDimTomadoPlanos.Text = reader["pre_dim_tomado_planos"].ToString();
                        txtOtraFuenteInfo.Text = reader["pre_otra_fuente_info"].ToString();
                        txtNumNuevoBloque.Text = reader["pre_num_nuevo_bloque"].ToString();
                        txtNumAmpliBloque.Text = reader["pre_num_ampli_bloque"] != DBNull.Value ?
                            reader["pre_num_ampli_bloque"].ToString() : "";
                        ddlTipo.SelectedValue = reader["pre_tipo"] != DBNull.Value ?
                            reader["pre_tipo"].ToString() : "";
                        txtPropiedadHorizontal.Text = reader["pre_propiedad_horizontal"].ToString();
                        ddlEstado.SelectedValue = reader["pre_estado"] != DBNull.Value ?
                            reader["pre_estado"].ToString() : "";
                        ddlDominio.SelectedValue = reader["pre_dominio"] != DBNull.Value ?
                            reader["pre_dominio"].ToString() : "";

                        // Obtenemos la geometría como texto WKT
                        txtGeometria.Text = reader["geometria_texto"] != DBNull.Value ?
                            reader["geometria_texto"].ToString() : "";

                        ddlCondicionOcupacion.SelectedValue = reader["opc_condicion_ocupacion"] != DBNull.Value ?
                            reader["opc_condicion_ocupacion"].ToString() : "";
                        txtNumHabitantes.Text = reader["pre_num_habitantes"] != DBNull.Value ?
                            reader["pre_num_habitantes"].ToString() : "";
                        txtPropietarioAnterior.Text = reader["pre_propietario_anterior"].ToString();
                        txtCartaTopografica.Text = reader["pre_carta_topografica"].ToString();
                        txtFotoAerea.Text = reader["pre_foto_aerea"].ToString();
                        ddlManzana.SelectedValue = reader["man_id"] != DBNull.Value ?
                            reader["man_id"].ToString() : "";
                        txtNumFamilias.Text = reader["pre_num_familias"] != DBNull.Value ?
                            reader["pre_num_familias"].ToString() : "";
                        txtPorcentajeDominio.Text = reader["pre_porcentaje_dominio"] != DBNull.Value ?
                            reader["pre_porcentaje_dominio"].ToString() : "";
                        txtDetalleDominio.Text = reader["pre_detalle_dominio"].ToString();
                        ddlTipoMixto.SelectedValue = reader["pre_tipo_mixto"] != DBNull.Value ?
                            reader["pre_tipo_mixto"].ToString() : "";
                        txtValorTipoMixto.Text = reader["pre_valor_tipo_mixto"] != DBNull.Value ?
                            reader["pre_valor_tipo_mixto"].ToString() : "";
                        ddlLinderosDefinidos.SelectedValue = reader["pre_linderos_definidos"] != DBNull.Value ?
                            reader["pre_linderos_definidos"].ToString() : "";
                        txtAreaTotalTerrenoAnterior.Text = reader["pre_area_total_terreno_anterior"] != DBNull.Value ?
                            reader["pre_area_total_terreno_anterior"].ToString() : "";
                        txtLocalizacionOtros.Text = reader["pre_localizacion_otros"].ToString();
                        ddlBienMostrenco.SelectedValue = reader["pre_bien_mostrenco"] != DBNull.Value ?
                            reader["pre_bien_mostrenco"].ToString() : "";
                        ddlEnConflicto.SelectedValue = reader["pre_en_conflicto"] != DBNull.Value ?
                            reader["pre_en_conflicto"].ToString() : "";
                        txtAreaTotalTerGrafico.Text = reader["pre_area_total_ter_grafico"] != DBNull.Value ?
                            reader["pre_area_total_ter_grafico"].ToString() : "";
                        ddlPropietarioDesconocido.SelectedValue = reader["pre_propietario_desconocido"] != DBNull.Value ?
                            reader["pre_propietario_desconocido"].ToString() : "";
                        txtAreaTotalTerAlfanumerico.Text = reader["pre_area_total_ter_alfanumerico"] != DBNull.Value ?
                            reader["pre_area_total_ter_alfanumerico"].ToString() : "";
                        ddlDominioDetalle.SelectedValue = reader["pre_dominio_detalle"] != DBNull.Value ?
                            reader["pre_dominio_detalle"].ToString() : "";
                        txtDireccionPrincipal.Text = reader["pre_direccion_principal"].ToString();
                        txtAreaTotalConstAlfanumerico.Text = reader["pre_area_total_const_alfanumerico"] != DBNull.Value ?
                            reader["pre_area_total_const_alfanumerico"].ToString() : "";
                        txtTipoVivienda.Text = reader["pre_tipo_vivienda"].ToString();
                        ddlClasificacionVivienda.SelectedValue = reader["opc_clasificacion_vivienda"] != DBNull.Value ?
                            reader["opc_clasificacion_vivienda"].ToString() : "";
                        txtFechaModificacion.Text = reader["pre_fecha_modificacion"] != DBNull.Value ?
                            ((DateTime)reader["pre_fecha_modificacion"]).ToString("yyyy-MM-ddTHH:mm") : "";
                        txtNumCelulares.Text = reader["pre_num_celulares"] != DBNull.Value ?
                            reader["pre_num_celulares"].ToString() : "";
                        ddlModalidadPropiedadHorizontal.SelectedValue = reader["pre_modalidad_propiedad_horizontal"] != DBNull.Value ?
                            reader["pre_modalidad_propiedad_horizontal"].ToString() : "";
                        txtAlicuotaTotalDeclaratoria.Text = reader["pre_alicuota_total_declaratoria"] != DBNull.Value ?
                            reader["pre_alicuota_total_declaratoria"].ToString() : "";
                        ddlTipoPropiedadHorizontal.SelectedValue = reader["pre_tipo_propiedad_horizontal"] != DBNull.Value ?
                            reader["pre_tipo_propiedad_horizontal"].ToString() : "";
                        txtObservacionPH.Text = reader["pre_observacion_ph"].ToString();
                        ddlHipotecaGAD.SelectedValue = reader["pre_hipoteca_gad"] != DBNull.Value ?
                            reader["pre_hipoteca_gad"].ToString() : "";
                        ddlRegimenPropiedadHorizontal.SelectedValue = reader["pre_regimen_propiedad_horizontal"] != DBNull.Value ?
                            reader["pre_regimen_propiedad_horizontal"].ToString() : "";
                        ddlProrrateoTitulo.SelectedValue = reader["pre_prorrateo_titulo"] != DBNull.Value ?
                            reader["pre_prorrateo_titulo"].ToString() : "";
                    }
                }
            }
        }

        protected void gvPredios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Editar")
            {
                Response.Redirect($"Predio.aspx?id={e.CommandArgument}");
            }
            else if (e.CommandName == "Eliminar")
            {
                try
                {
                    int idPredio = Convert.ToInt32(e.CommandArgument);
                    EliminarPredio(idPredio);
                    CargarPredios();

                    ScriptManager.RegisterStartupScript(this, GetType(), "success",
                        "mostrarMensaje('success', 'Predio eliminado correctamente');", true);
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "error",
                        "mostrarMensaje('error', 'Error al eliminar: " + ex.Message.Replace("'", "") + "');", true);
                }
            }
        }

        private void EliminarPredio(int id)
        {
            using (var conn = new NpgsqlConnection(cadenaConexion))
            using (var cmd = new NpgsqlCommand("catastro.eliminar_predio", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_id", id);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
        }

        private void LimpiarFormulario()
        {
            hdnPreId.Value = "";
            // Limpiar todos los campos del formulario
            txtCodigoCatastral.Text = "";
            txtFechaIngreso.Text = "";
            txtCodigoAnterior.Text = "";
            txtNumero.Text = "";
            txtNombrePredio.Text = "";
            txtAreaTotalTer.Text = "";
            txtAreaTotalConst.Text = "";
            txtFondoRelativo.Text = "";
            txtFrenteFondo.Text = "";
            txtObservaciones.Text = "";
            txtDimTomadoPlanos.Text = "";
            txtOtraFuenteInfo.Text = "";
            txtNumNuevoBloque.Text = "";
            txtNumAmpliBloque.Text = "";
            ddlTipo.SelectedIndex = 0;
            txtPropiedadHorizontal.Text = "";
            ddlEstado.SelectedIndex = 0;
            ddlDominio.SelectedIndex = 0;
            txtGeometria.Text = "";
            ddlCondicionOcupacion.SelectedIndex = 0;
            txtNumHabitantes.Text = "";
            txtPropietarioAnterior.Text = "";
            txtCartaTopografica.Text = "";
            txtFotoAerea.Text = "";
            ddlManzana.SelectedIndex = 0;
            txtNumFamilias.Text = "";
            txtPorcentajeDominio.Text = "";
            txtDetalleDominio.Text = "";
            ddlTipoMixto.SelectedIndex = 0;
            txtValorTipoMixto.Text = "";
            ddlLinderosDefinidos.SelectedIndex = 0;
            txtAreaTotalTerrenoAnterior.Text = "";
            txtLocalizacionOtros.Text = "";
            ddlBienMostrenco.SelectedIndex = 0;
            ddlEnConflicto.SelectedIndex = 0;
            txtAreaTotalTerGrafico.Text = "";
            ddlPropietarioDesconocido.SelectedIndex = 0;
            txtAreaTotalTerAlfanumerico.Text = "";
            ddlDominioDetalle.SelectedIndex = 0;
            txtDireccionPrincipal.Text = "";
            txtAreaTotalConstAlfanumerico.Text = "";
            txtTipoVivienda.Text = "";
            ddlClasificacionVivienda.SelectedIndex = 0;
            txtFechaModificacion.Text = "";
            txtNumCelulares.Text = "";
            ddlModalidadPropiedadHorizontal.SelectedIndex = 0;
            txtAlicuotaTotalDeclaratoria.Text = "";
            ddlTipoPropiedadHorizontal.SelectedIndex = 0;
            txtObservacionPH.Text = "";
            ddlHipotecaGAD.SelectedIndex = 0;
            ddlRegimenPropiedadHorizontal.SelectedIndex = 0;
            ddlProrrateoTitulo.SelectedIndex = 0;

            Response.Redirect("Predio.aspx");
        }

        protected void gvPredios_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvPredios.PageIndex = e.NewPageIndex;
            CargarPredios();
        }
    }
}