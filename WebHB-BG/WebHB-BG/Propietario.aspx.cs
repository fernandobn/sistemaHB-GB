using Npgsql;
using System;
using System.Configuration;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebHB_BG
{
    public partial class Propietario : Page
    {
        string cadenaConexion = ConfigurationManager.ConnectionStrings["conexionGad"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarCatalogosSeparados();
                CargarPropietarios();

                if (Request.QueryString["edit"] != null)
                {
                    int idEditar = Convert.ToInt32(Request.QueryString["edit"]);
                    hfPropietarioID.Value = idEditar.ToString();
                    CargarDatosPropietario(idEditar);
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
                        script = "iziToast.success({ title: 'Éxito', message: 'Propietario guardado correctamente', position: 'topRight' });";
                        break;
                    case "actualizado":
                        script = "iziToast.success({ title: 'Éxito', message: 'Propietario actualizado correctamente', position: 'topRight' });";
                        break;
                }

                ScriptManager.RegisterStartupScript(this, GetType(), "toastMessage", script, true);
            }
        }

        // Nuevo método que carga cada catálogo llamando a funciones PostgreSQL separadas
        private void CargarCatalogosSeparados()
        {
            CargarCatalogoFuncion("gestion.listar_tipos_identificacion", ddlTipoIdentificacion);
            CargarCatalogoFuncion("gestion.listar_estados_civiles", ddlEstadoCivil);
            CargarCatalogoFuncion("gestion.listar_tipos_persona", ddlTipoPersona);
        }

        private void CargarCatalogoFuncion(string funcionNombre, DropDownList ddl)
        {
            string sql = $"SELECT * FROM {funcionNombre}()";

            using (var conn = new NpgsqlConnection(cadenaConexion))
            using (var cmd = new NpgsqlCommand(sql, conn))
            {
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

        // El resto de métodos permanece igual
        private void CargarDatosPropietario(int id)
        {
            string sql = @"
        SELECT 
            pro_id, opc_tipoidentificacion, pro_num_identificacion, pro_nombre, pro_apellido,
            pro_direccion_ciudad, pro_direccion_domicilio, pro_direccion_referencia,
            pro_fecha_nacimiento, opc_estado_civil, pro_sexo, pro_correo_electronico,
            pro_telefono1, pro_telefono2, pro_codigo_postal, pro_nro_conadis, pro_porcentaje_conadis,
            opc_tipo_conadis, pro_validado, opc_tipo_entidad, pro_tipo_persona, pro_numero_registro,
            pro_genero, pro_inscrito_en, pro_lugar_inscripcion, pro_id_cliente, pro_tiene_ruc,
            pro_ruc, pro_razon_social_pn, pro_fecha_fallecido
        FROM gestion.ges_propietario
        WHERE pro_id = @id";

            using (var conn = new NpgsqlConnection(cadenaConexion))
            using (var cmd = new NpgsqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        ddlTipoIdentificacion.SelectedValue = reader["opc_tipoidentificacion"]?.ToString() ?? "";
                        txtIdentificacion.Text = reader["pro_num_identificacion"]?.ToString() ?? "";
                        txtNombre.Text = reader["pro_nombre"]?.ToString() ?? "";
                        txtApellido.Text = reader["pro_apellido"]?.ToString() ?? "";
                        txtCiudad.Text = reader["pro_direccion_ciudad"]?.ToString() ?? "";
                        txtDomicilio.Text = reader["pro_direccion_domicilio"]?.ToString() ?? "";
                        txtReferencia.Text = reader["pro_direccion_referencia"]?.ToString() ?? "";
                        txtFechaNacimiento.Text = reader["pro_fecha_nacimiento"] != DBNull.Value ? Convert.ToDateTime(reader["pro_fecha_nacimiento"]).ToString("yyyy-MM-dd") : "";
                        ddlEstadoCivil.SelectedValue = reader["opc_estado_civil"]?.ToString() ?? "";
                        ddlSexo.SelectedValue = reader["pro_sexo"]?.ToString() ?? "";
                        txtCorreo.Text = reader["pro_correo_electronico"]?.ToString() ?? "";
                        txtTelefono1.Text = reader["pro_telefono1"]?.ToString() ?? "";
                        txtTelefono2.Text = reader["pro_telefono2"]?.ToString() ?? "";
                        txtCodigoPostal.Text = reader["pro_codigo_postal"]?.ToString() ?? "";
                        txtNroConadis.Text = reader["pro_nro_conadis"]?.ToString() ?? "";
                        txtPorcentajeConadis.Text = reader["pro_porcentaje_conadis"]?.ToString() ?? "";
                        ddlTipoPersona.SelectedValue = reader["pro_tipo_persona"]?.ToString() ?? "";
                        txtNumeroRegistro.Text = reader["pro_numero_registro"]?.ToString() ?? "";
                        txtInscritoEn.Text = reader["pro_inscrito_en"]?.ToString() ?? "";
                        txtLugarInscripcion.Text = reader["pro_lugar_inscripcion"]?.ToString() ?? "";
                        txtRuc.Text = reader["pro_ruc"]?.ToString() ?? "";
                        txtRazonSocial.Text = reader["pro_razon_social_pn"]?.ToString() ?? "";
                        txtFechaFallecido.Text = reader["pro_fecha_fallecido"] != DBNull.Value ? Convert.ToDateTime(reader["pro_fecha_fallecido"]).ToString("yyyy-MM-dd") : "";
                    }
                }
            }
        }


        private object GetDbValue(string value)
        {
            return string.IsNullOrWhiteSpace(value) ? (object)DBNull.Value : value.Trim();
        }

        private object GetDbValue(int? value)
        {
            return value.HasValue ? (object)value.Value : DBNull.Value;
        }

        private object GetDbValue(decimal? value)
        {
            return value.HasValue ? (object)value.Value : DBNull.Value;
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                using (var conn = new NpgsqlConnection(cadenaConexion))
                {
                    conn.Open();

                    string sql = @"INSERT INTO gestion.ges_propietario (
                opc_tipoidentificacion,
                pro_num_identificacion,
                pro_nombre,
                pro_apellido,
                pro_direccion_ciudad,
                pro_direccion_domicilio,
                pro_direccion_referencia,
                pro_fecha_nacimiento,
                opc_estado_civil,
                pro_sexo,
                pro_correo_electronico,
                pro_telefono1,
                pro_telefono2,
                pro_codigo_postal,
                pro_nro_conadis,
                pro_porcentaje_conadis,
                opc_tipo_conadis,
                pro_validado,
                opc_tipo_entidad,
                pro_tipo_persona,
                pro_numero_registro,
                pro_genero,
                pro_inscrito_en,
                pro_lugar_inscripcion,
                pro_id_cliente,
                pro_tiene_ruc,
                pro_ruc,
                pro_razon_social_pn,
                pro_fecha_fallecido
            ) VALUES (
                @p_opc_tipoidentificacion,
                @p_pro_num_identificacion,
                @p_pro_nombre,
                @p_pro_apellido,
                @p_pro_direccion_ciudad,
                @p_pro_direccion_domicilio,
                @p_pro_direccion_referencia,
                @p_pro_fecha_nacimiento,
                @p_opc_estado_civil,
                @p_pro_sexo,
                @p_pro_correo_electronico,
                @p_pro_telefono1,
                @p_pro_telefono2,
                @p_pro_codigo_postal,
                @p_pro_nro_conadis,
                @p_pro_porcentaje_conadis,
                @p_opc_tipo_conadis,
                @p_pro_validado,
                @p_opc_tipo_entidad,
                @p_pro_tipo_persona,
                @p_pro_numero_registro,
                @p_pro_genero,
                @p_pro_inscrito_en,
                @p_pro_lugar_inscripcion,
                @p_pro_id_cliente,
                @p_pro_tiene_ruc,
                @p_pro_ruc,
                @p_pro_razon_social_pn,
                @p_pro_fecha_fallecido
            );";

                    using (var cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("p_opc_tipoidentificacion",
                            string.IsNullOrEmpty(ddlTipoIdentificacion.SelectedValue)
                            ? DBNull.Value
                            : (object)Convert.ToInt32(ddlTipoIdentificacion.SelectedValue));

                        cmd.Parameters.AddWithValue("p_pro_num_identificacion", GetDbValue(txtIdentificacion.Text));
                        cmd.Parameters.AddWithValue("p_pro_nombre", GetDbValue(txtNombre.Text));
                        cmd.Parameters.AddWithValue("p_pro_apellido", GetDbValue(txtApellido.Text));
                        cmd.Parameters.AddWithValue("p_pro_direccion_ciudad", GetDbValue(txtCiudad.Text));
                        cmd.Parameters.AddWithValue("p_pro_direccion_domicilio", GetDbValue(txtDomicilio.Text));
                        cmd.Parameters.AddWithValue("p_pro_direccion_referencia", GetDbValue(txtReferencia.Text));

                        DateTime fNac;
                        if (DateTime.TryParse(txtFechaNacimiento.Text, out fNac))
                            cmd.Parameters.AddWithValue("p_pro_fecha_nacimiento", fNac);
                        else
                            cmd.Parameters.AddWithValue("p_pro_fecha_nacimiento", DBNull.Value);

                        cmd.Parameters.AddWithValue("p_opc_estado_civil",
                            string.IsNullOrEmpty(ddlEstadoCivil.SelectedValue)
                            ? DBNull.Value
                            : (object)Convert.ToInt32(ddlEstadoCivil.SelectedValue));

                        cmd.Parameters.AddWithValue("p_pro_sexo",
                            string.IsNullOrEmpty(ddlSexo.SelectedValue)
                            ? DBNull.Value
                            : (object)Convert.ToInt32(ddlSexo.SelectedValue));

                        cmd.Parameters.AddWithValue("p_pro_correo_electronico", GetDbValue(txtCorreo.Text));
                        cmd.Parameters.AddWithValue("p_pro_telefono1", GetDbValue(txtTelefono1.Text));
                        cmd.Parameters.AddWithValue("p_pro_telefono2", GetDbValue(txtTelefono2.Text));
                        cmd.Parameters.AddWithValue("p_pro_codigo_postal", GetDbValue(txtCodigoPostal.Text));
                        cmd.Parameters.AddWithValue("p_pro_nro_conadis", GetDbValue(txtNroConadis.Text));

                        decimal porc;
                        if (decimal.TryParse(txtPorcentajeConadis.Text, out porc))
                            cmd.Parameters.AddWithValue("p_pro_porcentaje_conadis", porc);
                        else
                            cmd.Parameters.AddWithValue("p_pro_porcentaje_conadis", DBNull.Value);

                        cmd.Parameters.AddWithValue("p_opc_tipo_conadis", DBNull.Value);
                        cmd.Parameters.AddWithValue("p_pro_validado", DBNull.Value);
                        cmd.Parameters.AddWithValue("p_opc_tipo_entidad", DBNull.Value);

                        cmd.Parameters.AddWithValue("p_pro_tipo_persona",
                            string.IsNullOrEmpty(ddlTipoPersona.SelectedValue)
                            ? DBNull.Value
                            : (object)Convert.ToInt32(ddlTipoPersona.SelectedValue));

                        cmd.Parameters.AddWithValue("p_pro_numero_registro", GetDbValue(txtNumeroRegistro.Text));
                        cmd.Parameters.AddWithValue("p_pro_genero", DBNull.Value);
                        cmd.Parameters.AddWithValue("p_pro_inscrito_en", GetDbValue(txtInscritoEn.Text));
                        cmd.Parameters.AddWithValue("p_pro_lugar_inscripcion", GetDbValue(txtLugarInscripcion.Text));
                        cmd.Parameters.AddWithValue("p_pro_id_cliente", DBNull.Value);
                        cmd.Parameters.AddWithValue("p_pro_tiene_ruc", 0);  // smallint, no boolean

                        cmd.Parameters.AddWithValue("p_pro_ruc", GetDbValue(txtRuc.Text)); // Asumo que tienes txtRuc, si no, agrega o cambia
                        cmd.Parameters.AddWithValue("p_pro_razon_social_pn", GetDbValue(txtRazonSocial.Text));

                        DateTime fFal;
                        if (DateTime.TryParse(txtFechaFallecido.Text, out fFal))
                            cmd.Parameters.AddWithValue("p_pro_fecha_fallecido", fFal);
                        else
                            cmd.Parameters.AddWithValue("p_pro_fecha_fallecido", DBNull.Value);

                        cmd.ExecuteNonQuery();
                    }
                }
                Response.Redirect("Propietario.aspx?msg=guardado");
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al guardar: " + ex.Message;
            }
        }










        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(hfPropietarioID.Value))
                return;

            try
            {
                using (var conn = new NpgsqlConnection(cadenaConexion))
                {
                    conn.Open();

                    string sql = @"
                UPDATE gestion.ges_propietario SET
                    opc_tipoidentificacion = @p_opc_tipoidentificacion,
                    pro_num_identificacion = @p_pro_num_identificacion,
                    pro_nombre = @p_pro_nombre,
                    pro_apellido = @p_pro_apellido,
                    pro_direccion_ciudad = @p_pro_direccion_ciudad,
                    pro_direccion_domicilio = @p_pro_direccion_domicilio,
                    pro_direccion_referencia = @p_pro_direccion_referencia,
                    pro_fecha_nacimiento = @p_pro_fecha_nacimiento,
                    opc_estado_civil = @p_opc_estado_civil,
                    pro_sexo = @p_pro_sexo,
                    pro_correo_electronico = @p_pro_correo_electronico,
                    pro_telefono1 = @p_pro_telefono1,
                    pro_telefono2 = @p_pro_telefono2,
                    pro_codigo_postal = @p_pro_codigo_postal,
                    pro_nro_conadis = @p_pro_nro_conadis,
                    pro_porcentaje_conadis = @p_pro_porcentaje_conadis,
                    pro_tipo_persona = @p_pro_tipo_persona,
                    pro_numero_registro = @p_pro_numero_registro,
                    pro_inscrito_en = @p_pro_inscrito_en,
                    pro_lugar_inscripcion = @p_pro_lugar_inscripcion,
                    pro_ruc = @p_pro_ruc,
                    pro_razon_social_pn = @p_pro_razon_social_pn,
                    pro_fecha_fallecido = @p_pro_fecha_fallecido
                WHERE pro_id = @p_pro_id;
            ";

                    using (var cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("p_pro_id", Convert.ToInt32(hfPropietarioID.Value));

                        cmd.Parameters.AddWithValue("p_opc_tipoidentificacion",
                            string.IsNullOrEmpty(ddlTipoIdentificacion.SelectedValue)
                            ? DBNull.Value
                            : (object)Convert.ToInt32(ddlTipoIdentificacion.SelectedValue));

                        cmd.Parameters.AddWithValue("p_pro_num_identificacion", GetDbValue(txtIdentificacion.Text));
                        cmd.Parameters.AddWithValue("p_pro_nombre", GetDbValue(txtNombre.Text));
                        cmd.Parameters.AddWithValue("p_pro_apellido", GetDbValue(txtApellido.Text));
                        cmd.Parameters.AddWithValue("p_pro_direccion_ciudad", GetDbValue(txtCiudad.Text));
                        cmd.Parameters.AddWithValue("p_pro_direccion_domicilio", GetDbValue(txtDomicilio.Text));
                        cmd.Parameters.AddWithValue("p_pro_direccion_referencia", GetDbValue(txtReferencia.Text));

                        DateTime fNac;
                        if (DateTime.TryParse(txtFechaNacimiento.Text, out fNac))
                            cmd.Parameters.AddWithValue("p_pro_fecha_nacimiento", fNac);
                        else
                            cmd.Parameters.AddWithValue("p_pro_fecha_nacimiento", DBNull.Value);

                        cmd.Parameters.AddWithValue("p_opc_estado_civil",
                            string.IsNullOrEmpty(ddlEstadoCivil.SelectedValue)
                            ? DBNull.Value
                            : (object)Convert.ToInt32(ddlEstadoCivil.SelectedValue));

                        cmd.Parameters.AddWithValue("p_pro_sexo",
                            string.IsNullOrEmpty(ddlSexo.SelectedValue)
                            ? DBNull.Value
                            : (object)Convert.ToInt32(ddlSexo.SelectedValue));

                        cmd.Parameters.AddWithValue("p_pro_correo_electronico", GetDbValue(txtCorreo.Text));
                        cmd.Parameters.AddWithValue("p_pro_telefono1", GetDbValue(txtTelefono1.Text));
                        cmd.Parameters.AddWithValue("p_pro_telefono2", GetDbValue(txtTelefono2.Text));
                        cmd.Parameters.AddWithValue("p_pro_codigo_postal", GetDbValue(txtCodigoPostal.Text));
                        cmd.Parameters.AddWithValue("p_pro_nro_conadis", GetDbValue(txtNroConadis.Text));

                        decimal porc;
                        if (decimal.TryParse(txtPorcentajeConadis.Text, out porc))
                            cmd.Parameters.AddWithValue("p_pro_porcentaje_conadis", porc);
                        else
                            cmd.Parameters.AddWithValue("p_pro_porcentaje_conadis", DBNull.Value);

                        cmd.Parameters.AddWithValue("p_pro_tipo_persona",
                            string.IsNullOrEmpty(ddlTipoPersona.SelectedValue)
                            ? DBNull.Value
                            : (object)Convert.ToInt32(ddlTipoPersona.SelectedValue));

                        cmd.Parameters.AddWithValue("p_pro_numero_registro", GetDbValue(txtNumeroRegistro.Text));
                        cmd.Parameters.AddWithValue("p_pro_inscrito_en", GetDbValue(txtInscritoEn.Text));
                        cmd.Parameters.AddWithValue("p_pro_lugar_inscripcion", GetDbValue(txtLugarInscripcion.Text));
                        cmd.Parameters.AddWithValue("p_pro_ruc", GetDbValue(txtRuc.Text));
                        cmd.Parameters.AddWithValue("p_pro_razon_social_pn", GetDbValue(txtRazonSocial.Text));

                        DateTime fFal;
                        if (DateTime.TryParse(txtFechaFallecido.Text, out fFal))
                            cmd.Parameters.AddWithValue("p_pro_fecha_fallecido", fFal);
                        else
                            cmd.Parameters.AddWithValue("p_pro_fecha_fallecido", DBNull.Value);

                        cmd.ExecuteNonQuery();
                    }
                }

                Response.Redirect("Propietario.aspx?msg=actualizado");
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al actualizar: " + ex.Message;
            }
        }

        private void CargarPropietarios()
        {
            DataTable dtPropietarios = new DataTable();

            using (var conn = new NpgsqlConnection(cadenaConexion))
            using (var cmd = new NpgsqlCommand("gestion.list_propietarios", conn))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                using (var da = new NpgsqlDataAdapter(cmd))
                {
                    da.Fill(dtPropietarios);
                }
            }

            gvPropietarios.DataSource = dtPropietarios;
            gvPropietarios.DataBind();
        }

        protected void gvPropietarios_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvPropietarios.PageIndex = e.NewPageIndex;
            CargarPropietarios();
        }
    }
}
