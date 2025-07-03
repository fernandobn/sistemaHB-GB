using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebHB_BG
{
    public partial class PropietarioPredio : System.Web.UI.Page
    {
        string cadenaConexion = ConfigurationManager.ConnectionStrings["conexionGad"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarCatalogos();
                CargarPropietariosPredio();

                if (Request.QueryString["edit"] != null)
                {
                    int idEditar = Convert.ToInt32(Request.QueryString["edit"]);
                    CargarDatosPropietarioPredio(idEditar);
                    btnGuardar.Visible = false;
                    btnActualizar.Visible = true;
                }
                else
                {
                    btnGuardar.Visible = true;
                    btnActualizar.Visible = false;
                }
            }

            string msg = Request.QueryString["msg"];
            if (!string.IsNullOrEmpty(msg))
            {
                string script = "";
                switch (msg)
                {
                    case "guardado":
                        script = "mostrarMensaje('success', 'Propietario-Predio guardado correctamente');";
                        break;
                    case "actualizado":
                        script = "mostrarMensaje('success', 'Propietario-Predio actualizado correctamente');";
                        break;
                    case "eliminado":
                        script = "mostrarMensaje('success', 'Propietario-Predio eliminado correctamente');";
                        break;
                }
                ScriptManager.RegisterStartupScript(this, GetType(), "toastMessage", script, true);
            }
        }

        private void CargarCatalogos()
        {

            CargarPropietarios(ddlPropietario, "-- Seleccionar Propietario --");
            CargarPropietarios(ddlConyuge, "-- Seleccionar Cónyuge --");
            CargarPropietarios(ddlRepresentanteLegal, "-- Seleccionar Representante Legal --");
            CargarPredios();


            CargarCatalogo("ADQUISICION", ddlAdquisicion);
            //CargarCatalogo("SITUACION_ACTUAL", ddlSituacion);
            CargarCatalogo("PARENTESCO", ddlParentesco);
        }

        private void CargarPropietarios(DropDownList ddl, string textoDefault)
        {
            string sql = @"SELECT pro_id, 
                                  CONCAT(pro_nombre, ' ', pro_apellido, ' - ', pro_num_identificacion) as nombre_completo
                           FROM gestion.ges_propietario 
                           ORDER BY pro_nombre, pro_apellido";

            using (var conn = new NpgsqlConnection(cadenaConexion))
            using (var cmd = new NpgsqlCommand(sql, conn))
            {
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    ddl.DataSource = reader;
                    ddl.DataValueField = "pro_id";
                    ddl.DataTextField = "nombre_completo";
                    ddl.DataBind();
                }
            }
            ddl.Items.Insert(0, new ListItem(textoDefault, ""));
        }

        private void CargarPredios()
        {
            string sql = @"SELECT pre_id, 
                                  CONCAT(pre_codigo_catastral, ' - ', pre_nombre_predio) as predio_info
                           FROM catastro.cat_predio 
                           ORDER BY pre_codigo_catastral";

            using (var conn = new NpgsqlConnection(cadenaConexion))
            using (var cmd = new NpgsqlCommand(sql, conn))
            {
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    ddlPredio.DataSource = reader;
                    ddlPredio.DataValueField = "pre_id";
                    ddlPredio.DataTextField = "predio_info";
                    ddlPredio.DataBind();
                }
            }
            ddlPredio.Items.Insert(0, new ListItem("-- Seleccionar Predio --", ""));
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

        private void CargarPropietariosPredio()
        {
            string sql = @"
                SELECT 
                    prp.prp_id,
                    CONCAT(prop.pro_nombre, ' ', prop.pro_apellido) as propietario,
                    prop.pro_num_identificacion,
                    CONCAT(pred.pre_codigo_catastral, ' - ', pred.pre_nombre_predio) as predio_info,
                    prp.prp_alicuota,
                    prp.prp_anios_posesion,
                    CASE 
                        WHEN prp.prp_tiene_escritura = 1 THEN 'Sí'
                        WHEN prp.prp_tiene_escritura = 0 THEN 'No'
                        ELSE 'Sin información'
                    END as tiene_escritura,
                    CASE 
                        WHEN prp.prp_representante = 1 THEN 'Representante'
                        ELSE 'Copropietario'
                    END as tipo_propietario
                FROM catastro.cat_propietario_predio prp
                INNER JOIN gestion.ges_propietario prop ON prp.pro_id = prop.pro_id
                LEFT JOIN catastro.cat_predio pred ON prp.pre_id = pred.pre_id
                ORDER BY prop.pro_nombre, prop.pro_apellido";

            using (var conn = new NpgsqlConnection(cadenaConexion))
            using (var cmd = new NpgsqlCommand(sql, conn))
            {
                conn.Open();
                using (var da = new NpgsqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    gvPropietariosPredio.DataSource = dt;
                    gvPropietariosPredio.DataBind();
                }
            }
        }

        private void CargarDatosPropietarioPredio(int id)
        {
            string sql = @"
                SELECT * FROM catastro.cat_propietario_predio 
                WHERE prp_id = @id";

            using (var conn = new NpgsqlConnection(cadenaConexion))
            using (var cmd = new NpgsqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        hfPropietarioPredioID.Value = reader["prp_id"].ToString();
                        ddlPropietario.SelectedValue = reader["pro_id"]?.ToString() ?? "";
                        ddlConyuge.SelectedValue = reader["pro_id_conyuge"]?.ToString() ?? "";
                        ddlRepresentanteLegal.SelectedValue = reader["pro_id_rep_legal"]?.ToString() ?? "";
                        ddlPredio.SelectedValue = reader["pre_id"]?.ToString() ?? "";
                        txtAlicuota.Text = reader["prp_alicuota"]?.ToString() ?? "";
                        txtAniosPosesion.Text = reader["prp_anios_posesion"]?.ToString() ?? "";
                        txtObservacion.Text = reader["prp_observacion"]?.ToString() ?? "";
                        ddlTieneEscritura.SelectedValue = reader["prp_tiene_escritura"]?.ToString() ?? "";
                        ddlRepresentante.SelectedValue = reader["prp_representante"]?.ToString() ?? "";
                        ddlAdquisicion.SelectedValue = reader["opc_adquisicion"]?.ToString() ?? "";
                        ddlSituacionActual.SelectedValue = reader["opc_situacion_actual"]?.ToString() ?? "";
                        txtCelebradoAnte.Text = reader["prp_celebrado_ante"]?.ToString() ?? "";
                        txtCanton.Text = reader["prp_canton"]?.ToString() ?? "";
                        txtNotaria.Text = reader["prp_notaria"]?.ToString() ?? "";
                        txtFechaInscripcion.Text = reader["prp_fecha_inscripcion"] != DBNull.Value ?
                            Convert.ToDateTime(reader["prp_fecha_inscripcion"]).ToString("yyyy-MM-dd") : "";
                        txtLugarInscripcion.Text = reader["prp_lugar_inscripcion"]?.ToString() ?? "";
                        ddlPerfeccionamiento.SelectedValue = reader["prp_perfeccionamiento"]?.ToString() ?? "";
                        txtLugarRegistro.Text = reader["prp_lugar_registro"]?.ToString() ?? "";
                        txtRegistroPropiedad.Text = reader["prp_registro_propiedad"]?.ToString() ?? "";
                        txtFechaRegistro.Text = reader["prp_fecha_registro"] != DBNull.Value ?
                            Convert.ToDateTime(reader["prp_fecha_registro"]).ToString("yyyy-MM-dd") : "";
                        txtLibro.Text = reader["prp_libro"]?.ToString() ?? "";
                        txtFoja.Text = reader["prp_foja"]?.ToString() ?? "";
                        txtSituacionLegal.Text = reader["prp_situacion_legal"]?.ToString() ?? "";
                        ddlFinanciado.SelectedValue = reader["prp_financiado"]?.ToString() ?? "";
                        txtNombrePueblo.Text = reader["prp_nombre_pueblo"]?.ToString() ?? "";
                        txtAniosPerfeccionamiento.Text = reader["prp_anios_perfeccionamiento"]?.ToString() ?? "";
                        txtAreaEscritura.Text = reader["prp_area_escritura"]?.ToString() ?? "";
                        ddlParentesco.SelectedValue = reader["opc_parentesco"]?.ToString() ?? "";
                    }
                }
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                using (var conn = new NpgsqlConnection(cadenaConexion))
                {
                    conn.Open();

                    string sql = @"
                        INSERT INTO catastro.cat_propietario_predio (
                            pro_id, pro_id_conyuge, pro_id_rep_legal, pre_id, prp_alicuota,
                            prp_anios_posesion, prp_observacion, prp_tiene_escritura, prp_representante,
                            opc_adquisicion, opc_situacion_actual, prp_celebrado_ante, prp_canton,
                            prp_notaria, prp_fecha_inscripcion, prp_lugar_inscripcion, prp_perfeccionamiento,
                            prp_lugar_registro, prp_registro_propiedad, prp_fecha_registro, prp_libro,
                            prp_foja, prp_situacion_legal, prp_financiado, prp_nombre_pueblo,
                            prp_anios_perfeccionamiento, prp_area_escritura, opc_parentesco
                        ) VALUES (
                            @pro_id, @pro_id_conyuge, @pro_id_rep_legal, @pre_id, @prp_alicuota,
                            @prp_anios_posesion, @prp_observacion, @prp_tiene_escritura, @prp_representante,
                            @opc_adquisicion, @opc_situacion_actual, @prp_celebrado_ante, @prp_canton,
                            @prp_notaria, @prp_fecha_inscripcion, @prp_lugar_inscripcion, @prp_perfeccionamiento,
                            @prp_lugar_registro, @prp_registro_propiedad, @prp_fecha_registro, @prp_libro,
                            @prp_foja, @prp_situacion_legal, @prp_financiado, @prp_nombre_pueblo,
                            @prp_anios_perfeccionamiento, @prp_area_escritura, @opc_parentesco
                        )";

                    using (var cmd = new NpgsqlCommand(sql, conn))
                    {
                        AgregarParametros(cmd);
                        cmd.ExecuteNonQuery();
                    }
                }

                Response.Redirect("PropietarioPredio.aspx?msg=guardado");
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "error",
                    $"mostrarMensaje('error', 'Error al guardar: {ex.Message.Replace("'", "\\'")}');", true);
            }
        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(hfPropietarioPredioID.Value))
                return;

            try
            {
                using (var conn = new NpgsqlConnection(cadenaConexion))
                {
                    conn.Open();

                    string sql = @"
                        UPDATE catastro.cat_propietario_predio SET
                            pro_id = @pro_id,
                            pro_id_conyuge = @pro_id_conyuge,
                            pro_id_rep_legal = @pro_id_rep_legal,
                            pre_id = @pre_id,
                            prp_alicuota = @prp_alicuota,
                            prp_anios_posesion = @prp_anios_posesion,
                            prp_observacion = @prp_observacion,
                            prp_tiene_escritura = @prp_tiene_escritura,
                            prp_representante = @prp_representante,
                            opc_adquisicion = @opc_adquisicion,
                            opc_situacion_actual = @opc_situacion_actual,
                            prp_celebrado_ante = @prp_celebrado_ante,
                            prp_canton = @prp_canton,
                            prp_notaria = @prp_notaria,
                            prp_fecha_inscripcion = @prp_fecha_inscripcion,
                            prp_lugar_inscripcion = @prp_lugar_inscripcion,
                            prp_perfeccionamiento = @prp_perfeccionamiento,
                            prp_lugar_registro = @prp_lugar_registro,
                            prp_registro_propiedad = @prp_registro_propiedad,
                            prp_fecha_registro = @prp_fecha_registro,
                            prp_libro = @prp_libro,
                            prp_foja = @prp_foja,
                            prp_situacion_legal = @prp_situacion_legal,
                            prp_financiado = @prp_financiado,
                            prp_nombre_pueblo = @prp_nombre_pueblo,
                            prp_anios_perfeccionamiento = @prp_anios_perfeccionamiento,
                            prp_area_escritura = @prp_area_escritura,
                            opc_parentesco = @opc_parentesco
                        WHERE prp_id = @prp_id";

                    using (var cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@prp_id", Convert.ToInt32(hfPropietarioPredioID.Value));
                        AgregarParametros(cmd);
                        cmd.ExecuteNonQuery();
                    }
                }

                Response.Redirect("PropietarioPredio.aspx?msg=actualizado");
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "error",
                    $"mostrarMensaje('error', 'Error al actualizar: {ex.Message.Replace("'", "\\'")}');", true);
            }
        }

        private void AgregarParametros(NpgsqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@pro_id", Convert.ToInt32(ddlPropietario.SelectedValue));
            cmd.Parameters.AddWithValue("@pro_id_conyuge", GetDbValueInt(ddlConyuge.SelectedValue));
            cmd.Parameters.AddWithValue("@pro_id_rep_legal", GetDbValueInt(ddlRepresentanteLegal.SelectedValue));
            cmd.Parameters.AddWithValue("@pre_id", GetDbValueLong(ddlPredio.SelectedValue));
            cmd.Parameters.AddWithValue("@prp_alicuota", GetDbValueDecimal(txtAlicuota.Text));
            cmd.Parameters.AddWithValue("@prp_anios_posesion", GetDbValueInt(txtAniosPosesion.Text));
            cmd.Parameters.AddWithValue("@prp_observacion", GetDbValue(txtObservacion.Text));
            cmd.Parameters.AddWithValue("@prp_tiene_escritura", GetDbValueSmallInt(ddlTieneEscritura.SelectedValue));
            cmd.Parameters.AddWithValue("@prp_representante", GetDbValueSmallInt(ddlRepresentante.SelectedValue));
            cmd.Parameters.AddWithValue("@opc_adquisicion", GetDbValueInt(ddlAdquisicion.SelectedValue));
            cmd.Parameters.AddWithValue("@opc_situacion_actual", GetDbValueInt(ddlSituacionActual.SelectedValue));
            cmd.Parameters.AddWithValue("@prp_celebrado_ante", GetDbValue(txtCelebradoAnte.Text));
            cmd.Parameters.AddWithValue("@prp_canton", GetDbValue(txtCanton.Text));
            cmd.Parameters.AddWithValue("@prp_notaria", GetDbValue(txtNotaria.Text));
            cmd.Parameters.AddWithValue("@prp_fecha_inscripcion", GetDbValueDate(txtFechaInscripcion.Text));
            cmd.Parameters.AddWithValue("@prp_lugar_inscripcion", GetDbValue(txtLugarInscripcion.Text));
            cmd.Parameters.AddWithValue("@prp_perfeccionamiento", GetDbValueSmallInt(ddlPerfeccionamiento.SelectedValue));
            cmd.Parameters.AddWithValue("@prp_lugar_registro", GetDbValue(txtLugarRegistro.Text));
            cmd.Parameters.AddWithValue("@prp_registro_propiedad", GetDbValue(txtRegistroPropiedad.Text));
            cmd.Parameters.AddWithValue("@prp_fecha_registro", GetDbValueDate(txtFechaRegistro.Text));
            cmd.Parameters.AddWithValue("@prp_libro", GetDbValue(txtLibro.Text));
            cmd.Parameters.AddWithValue("@prp_foja", GetDbValue(txtFoja.Text));
            cmd.Parameters.AddWithValue("@prp_situacion_legal", GetDbValue(txtSituacionLegal.Text));
            cmd.Parameters.AddWithValue("@prp_financiado", GetDbValueSmallInt(ddlFinanciado.SelectedValue));
            cmd.Parameters.AddWithValue("@prp_nombre_pueblo", GetDbValue(txtNombrePueblo.Text));
            cmd.Parameters.AddWithValue("@prp_anios_perfeccionamiento", GetDbValueInt(txtAniosPerfeccionamiento.Text));
            cmd.Parameters.AddWithValue("@prp_area_escritura", GetDbValueDecimal(txtAreaEscritura.Text));
            cmd.Parameters.AddWithValue("@opc_parentesco", GetDbValueInt(ddlParentesco.SelectedValue));
        }

        private object GetDbValue(string value)
        {
            return string.IsNullOrWhiteSpace(value) ? (object)DBNull.Value : value.Trim();
        }

        private object GetDbValueInt(string value)
        {
            return string.IsNullOrWhiteSpace(value) ? (object)DBNull.Value : Convert.ToInt32(value);
        }

        private object GetDbValueLong(string value)
        {
            return string.IsNullOrWhiteSpace(value) ? (object)DBNull.Value : Convert.ToInt64(value);
        }

        private object GetDbValueSmallInt(string value)
        {
            return string.IsNullOrWhiteSpace(value) ? (object)DBNull.Value : Convert.ToInt16(value);
        }

        private object GetDbValueDecimal(string value)
        {
            decimal result;
            return string.IsNullOrWhiteSpace(value) || !decimal.TryParse(value, out result) ?
                (object)DBNull.Value : result;
        }

        private object GetDbValueDate(string value)
        {
            DateTime result;
            return string.IsNullOrWhiteSpace(value) || !DateTime.TryParse(value, out result) ?
                (object)DBNull.Value : result;
        }

        protected void gvPropietariosPredio_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Eliminar")
            {
                int idPropietarioPredio = Convert.ToInt32(e.CommandArgument);

                try
                {
                    using (var conn = new NpgsqlConnection(cadenaConexion))
                    {
                        string sql = "DELETE FROM catastro.cat_propietario_predio WHERE prp_id = @id";
                        using (var cmd = new NpgsqlCommand(sql, conn))
                        {
                            cmd.Parameters.AddWithValue("@id", idPropietarioPredio);
                            conn.Open();
                            cmd.ExecuteNonQuery();
                        }
                    }

                    CargarPropietariosPredio();
                    ScriptManager.RegisterStartupScript(this, GetType(), "msg",
                        "mostrarMensaje('success', 'Registro eliminado correctamente');", true);
                }
                catch (Npgsql.PostgresException ex) when (ex.SqlState == "23503")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "msg",
                        "mostrarMensaje('error', 'No se puede eliminar el registro porque está referenciado por otros datos.');", true);
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "msg",
                        $"mostrarMensaje('error', 'Error al eliminar: {ex.Message.Replace("'", "\\'")}');", true);
                }
            }
        }

        protected void gvPropietariosPredio_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvPropietariosPredio.PageIndex = e.NewPageIndex;
            CargarPropietariosPredio();
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("PropietarioPredio.aspx");
        }
    }
}
