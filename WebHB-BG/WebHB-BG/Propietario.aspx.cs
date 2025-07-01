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
            string sql = @"SELECT * FROM gestion.cat_propietario WHERE pro_id = @id";

            using (var conn = new NpgsqlConnection(cadenaConexion))
            using (var cmd = new NpgsqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        ddlTipoIdentificacion.SelectedValue = reader["pro_tipo_identificacion"]?.ToString() ?? "";
                        txtIdentificacion.Text = reader["pro_identificacion"]?.ToString() ?? "";
                        txtNombre.Text = reader["pro_nombre"]?.ToString() ?? "";
                        txtApellido.Text = reader["pro_apellido"]?.ToString() ?? "";
                        txtCiudad.Text = reader["pro_ciudad"]?.ToString() ?? "";
                        txtDomicilio.Text = reader["pro_domicilio"]?.ToString() ?? "";
                        txtReferencia.Text = reader["pro_referencia"]?.ToString() ?? "";
                        txtFechaNacimiento.Text = reader["pro_fecha_nacimiento"] != DBNull.Value ? Convert.ToDateTime(reader["pro_fecha_nacimiento"]).ToString("yyyy-MM-dd") : "";
                        ddlEstadoCivil.SelectedValue = reader["pro_estado_civil"]?.ToString() ?? "";
                        ddlSexo.SelectedValue = reader["pro_sexo"]?.ToString() ?? "";
                        txtCorreo.Text = reader["pro_correo"]?.ToString() ?? "";
                        txtTelefono1.Text = reader["pro_telefono1"]?.ToString() ?? "";
                        txtTelefono2.Text = reader["pro_telefono2"]?.ToString() ?? "";
                        txtCodigoPostal.Text = reader["pro_codigo_postal"]?.ToString() ?? "";
                        txtNroConadis.Text = reader["pro_nro_conadis"]?.ToString() ?? "";
                        txtPorcentajeConadis.Text = reader["pro_porcentaje_discapacidad"]?.ToString() ?? "";
                        ddlTipoPersona.SelectedValue = reader["pro_tipo_persona"]?.ToString() ?? "";
                        txtNumeroRegistro.Text = reader["pro_numero_registro"]?.ToString() ?? "";
                        txtInscritoEn.Text = reader["pro_inscrito_en"]?.ToString() ?? "";
                        txtLugarInscripcion.Text = reader["pro_lugar_inscripcion"]?.ToString() ?? "";
                        txtRuc.Text = reader["pro_ruc"]?.ToString() ?? "";
                        txtRazonSocial.Text = reader["pro_razon_social"]?.ToString() ?? "";
                        txtFechaFallecido.Text = reader["pro_fecha_fallecido"] != DBNull.Value ? Convert.ToDateTime(reader["pro_fecha_fallecido"]).ToString("yyyy-MM-dd") : "";
                    }
                }
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            using (var conn = new NpgsqlConnection(cadenaConexion))
            using (var cmd = new NpgsqlCommand("gestion.insertar_propietario", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("p_tipo_identificacion", string.IsNullOrEmpty(ddlTipoIdentificacion.SelectedValue) ? (object)DBNull.Value : Convert.ToInt32(ddlTipoIdentificacion.SelectedValue));
                cmd.Parameters.AddWithValue("p_identificacion", txtIdentificacion.Text.Trim());
                cmd.Parameters.AddWithValue("p_nombre", txtNombre.Text.Trim());
                cmd.Parameters.AddWithValue("p_apellido", txtApellido.Text.Trim());
                cmd.Parameters.AddWithValue("p_ciudad", txtCiudad.Text.Trim());
                cmd.Parameters.AddWithValue("p_domicilio", txtDomicilio.Text.Trim());
                cmd.Parameters.AddWithValue("p_referencia", txtReferencia.Text.Trim());
                cmd.Parameters.AddWithValue("p_fecha_nacimiento", string.IsNullOrEmpty(txtFechaNacimiento.Text) ? (object)DBNull.Value : DateTime.Parse(txtFechaNacimiento.Text));
                cmd.Parameters.AddWithValue("p_estado_civil", string.IsNullOrEmpty(ddlEstadoCivil.SelectedValue) ? (object)DBNull.Value : Convert.ToInt32(ddlEstadoCivil.SelectedValue));
                cmd.Parameters.AddWithValue("p_sexo", string.IsNullOrEmpty(ddlSexo.SelectedValue) ? (object)DBNull.Value : Convert.ToInt32(ddlSexo.SelectedValue));
                cmd.Parameters.AddWithValue("p_correo", txtCorreo.Text.Trim());
                cmd.Parameters.AddWithValue("p_telefono1", txtTelefono1.Text.Trim());
                cmd.Parameters.AddWithValue("p_telefono2", txtTelefono2.Text.Trim());
                cmd.Parameters.AddWithValue("p_codigo_postal", txtCodigoPostal.Text.Trim());
                cmd.Parameters.AddWithValue("p_nro_conadis", txtNroConadis.Text.Trim());
                cmd.Parameters.AddWithValue("p_porcentaje_discapacidad", string.IsNullOrEmpty(txtPorcentajeConadis.Text) ? (object)DBNull.Value : Convert.ToDecimal(txtPorcentajeConadis.Text));
                cmd.Parameters.AddWithValue("p_tipo_persona", string.IsNullOrEmpty(ddlTipoPersona.SelectedValue) ? (object)DBNull.Value : Convert.ToInt32(ddlTipoPersona.SelectedValue));
                cmd.Parameters.AddWithValue("p_numero_registro", txtNumeroRegistro.Text.Trim());
                cmd.Parameters.AddWithValue("p_inscrito_en", txtInscritoEn.Text.Trim());
                cmd.Parameters.AddWithValue("p_lugar_inscripcion", txtLugarInscripcion.Text.Trim());
                cmd.Parameters.AddWithValue("p_ruc", txtRuc.Text.Trim());
                cmd.Parameters.AddWithValue("p_razon_social", txtRazonSocial.Text.Trim());
                cmd.Parameters.AddWithValue("p_fecha_fallecido", string.IsNullOrEmpty(txtFechaFallecido.Text) ? (object)DBNull.Value : DateTime.Parse(txtFechaFallecido.Text));

                conn.Open();
                cmd.ExecuteNonQuery();
            }
            Response.Redirect("Propietario.aspx?msg=guardado");
        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(hfPropietarioID.Value))
                return;

            using (var conn = new NpgsqlConnection(cadenaConexion))
            using (var cmd = new NpgsqlCommand("gestion.actualizar_propietario", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("p_id", Convert.ToInt32(hfPropietarioID.Value));
                cmd.Parameters.AddWithValue("p_tipo_identificacion", string.IsNullOrEmpty(ddlTipoIdentificacion.SelectedValue) ? (object)DBNull.Value : Convert.ToInt32(ddlTipoIdentificacion.SelectedValue));
                cmd.Parameters.AddWithValue("p_identificacion", txtIdentificacion.Text.Trim());
                cmd.Parameters.AddWithValue("p_nombre", txtNombre.Text.Trim());
                cmd.Parameters.AddWithValue("p_apellido", txtApellido.Text.Trim());
                cmd.Parameters.AddWithValue("p_ciudad", txtCiudad.Text.Trim());
                cmd.Parameters.AddWithValue("p_domicilio", txtDomicilio.Text.Trim());
                cmd.Parameters.AddWithValue("p_referencia", txtReferencia.Text.Trim());
                cmd.Parameters.AddWithValue("p_fecha_nacimiento", string.IsNullOrEmpty(txtFechaNacimiento.Text) ? (object)DBNull.Value : DateTime.Parse(txtFechaNacimiento.Text));
                cmd.Parameters.AddWithValue("p_estado_civil", string.IsNullOrEmpty(ddlEstadoCivil.SelectedValue) ? (object)DBNull.Value : Convert.ToInt32(ddlEstadoCivil.SelectedValue));
                cmd.Parameters.AddWithValue("p_sexo", string.IsNullOrEmpty(ddlSexo.SelectedValue) ? (object)DBNull.Value : Convert.ToInt32(ddlSexo.SelectedValue));
                cmd.Parameters.AddWithValue("p_correo", txtCorreo.Text.Trim());
                cmd.Parameters.AddWithValue("p_telefono1", txtTelefono1.Text.Trim());
                cmd.Parameters.AddWithValue("p_telefono2", txtTelefono2.Text.Trim());
                cmd.Parameters.AddWithValue("p_codigo_postal", txtCodigoPostal.Text.Trim());
                cmd.Parameters.AddWithValue("p_nro_conadis", txtNroConadis.Text.Trim());
                cmd.Parameters.AddWithValue("p_porcentaje_discapacidad", string.IsNullOrEmpty(txtPorcentajeConadis.Text) ? (object)DBNull.Value : Convert.ToDecimal(txtPorcentajeConadis.Text));
                cmd.Parameters.AddWithValue("p_tipo_persona", string.IsNullOrEmpty(ddlTipoPersona.SelectedValue) ? (object)DBNull.Value : Convert.ToInt32(ddlTipoPersona.SelectedValue));
                cmd.Parameters.AddWithValue("p_numero_registro", txtNumeroRegistro.Text.Trim());
                cmd.Parameters.AddWithValue("p_inscrito_en", txtInscritoEn.Text.Trim());
                cmd.Parameters.AddWithValue("p_lugar_inscripcion", txtLugarInscripcion.Text.Trim());
                cmd.Parameters.AddWithValue("p_ruc", txtRuc.Text.Trim());
                cmd.Parameters.AddWithValue("p_razon_social", txtRazonSocial.Text.Trim());
                cmd.Parameters.AddWithValue("p_fecha_fallecido", string.IsNullOrEmpty(txtFechaFallecido.Text) ? (object)DBNull.Value : DateTime.Parse(txtFechaFallecido.Text));

                conn.Open();
                cmd.ExecuteNonQuery();
            }
            Response.Redirect("Propietario.aspx?msg=actualizado");
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
