using Npgsql;
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
        public DataTable dtPredios = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarCatalogos();
                CargarPredios();

                if (Request.QueryString["delete"] != null)
                {
                    int idEliminar = Convert.ToInt32(Request.QueryString["delete"]);
                    EliminarPredio(idEliminar);
                    Response.Redirect("Predio.aspx");
                }

                if (Request.QueryString["edit"] != null)
                {
                    int idEditar = Convert.ToInt32(Request.QueryString["edit"]);
                    CargarDatosPredio(idEditar);
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
                using (var da = new NpgsqlDataAdapter(cmd))
                {
                    dtPredios = new DataTable();
                    da.Fill(dtPredios);
                }
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            using (var conn = new NpgsqlConnection(cadenaConexion))
            {
                conn.Open();

                if (Request.QueryString["edit"] != null)
                {
                    using (var cmd = new NpgsqlCommand("catastro.actualizar_predio", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("p_pre_id", Convert.ToInt32(Request.QueryString["edit"]));
                        cmd.Parameters.AddWithValue("p_pre_codigo_catastral", txtCodigo.Text.Trim());
                        cmd.Parameters.AddWithValue("p_pre_nombre_predio", txtNombre.Text.Trim());
                        cmd.Parameters.AddWithValue("p_pre_dominio", string.IsNullOrEmpty(ddlDominio.SelectedValue) ? (object)DBNull.Value : Convert.ToInt32(ddlDominio.SelectedValue));
                        cmd.Parameters.AddWithValue("p_opc_condicion_ocupacion", string.IsNullOrEmpty(ddlCondicionOcupacion.SelectedValue) ? (object)DBNull.Value : Convert.ToInt32(ddlCondicionOcupacion.SelectedValue));
                        cmd.Parameters.AddWithValue("p_opc_clasificacion_vivienda", string.IsNullOrEmpty(ddlClasificacionVivienda.SelectedValue) ? (object)DBNull.Value : Convert.ToInt32(ddlClasificacionVivienda.SelectedValue));
                        cmd.Parameters.AddWithValue("p_man_id", string.IsNullOrEmpty(ddlManzana.SelectedValue) ? (object)DBNull.Value : Convert.ToInt32(ddlManzana.SelectedValue));

                        cmd.ExecuteNonQuery();
                    }
                }
                else
                {
                    using (var cmd = new NpgsqlCommand("catastro.insertar_predio", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("p_pre_codigo_catastral", txtCodigo.Text.Trim());
                        cmd.Parameters.AddWithValue("p_pre_nombre_predio", txtNombre.Text.Trim());
                        cmd.Parameters.AddWithValue("p_pre_dominio", string.IsNullOrEmpty(ddlDominio.SelectedValue) ? (object)DBNull.Value : Convert.ToInt32(ddlDominio.SelectedValue));
                        cmd.Parameters.AddWithValue("p_opc_condicion_ocupacion", string.IsNullOrEmpty(ddlCondicionOcupacion.SelectedValue) ? (object)DBNull.Value : Convert.ToInt32(ddlCondicionOcupacion.SelectedValue));
                        cmd.Parameters.AddWithValue("p_opc_clasificacion_vivienda", string.IsNullOrEmpty(ddlClasificacionVivienda.SelectedValue) ? (object)DBNull.Value : Convert.ToInt32(ddlClasificacionVivienda.SelectedValue));
                        cmd.Parameters.AddWithValue("p_man_id", string.IsNullOrEmpty(ddlManzana.SelectedValue) ? (object)DBNull.Value : Convert.ToInt32(ddlManzana.SelectedValue));

                        cmd.ExecuteNonQuery();
                    }
                }
            }

            Response.Redirect("Predio.aspx");
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

        private void CargarDatosPredio(int id)
        {
            string sql = @"SELECT pre_codigo_catastral, pre_nombre_predio, pre_dominio, 
                                  opc_condicion_ocupacion, opc_clasificacion_vivienda, man_id
                           FROM catastro.cat_predio 
                           WHERE pre_id = @id";

            using (var conn = new NpgsqlConnection(cadenaConexion))
            using (var cmd = new NpgsqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        txtCodigo.Text = reader["pre_codigo_catastral"].ToString();
                        txtNombre.Text = reader["pre_nombre_predio"].ToString();
                        ddlDominio.SelectedValue = reader["pre_dominio"] != DBNull.Value ? reader["pre_dominio"].ToString() : "";
                        ddlCondicionOcupacion.SelectedValue = reader["opc_condicion_ocupacion"] != DBNull.Value ? reader["opc_condicion_ocupacion"].ToString() : "";
                        ddlClasificacionVivienda.SelectedValue = reader["opc_clasificacion_vivienda"] != DBNull.Value ? reader["opc_clasificacion_vivienda"].ToString() : "";
                        ddlManzana.SelectedValue = reader["man_id"] != DBNull.Value ? reader["man_id"].ToString() : "";
                    }
                }
            }
        }
    }
}
