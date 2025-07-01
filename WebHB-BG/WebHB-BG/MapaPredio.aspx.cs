using Npgsql;
using System;
using System.Configuration;

namespace WebHB_BG
{
    public partial class MapaPredio : System.Web.UI.Page
    {
        // Propiedad pública para acceder desde el .aspx
        public string geometriaWkt = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string idPredio = Request.QueryString["id"];

                if (!string.IsNullOrEmpty(idPredio))
                {
                    ObtenerGeometriaWKT(Convert.ToInt32(idPredio));
                }
            }
        }

        private void ObtenerGeometriaWKT(int id)
        {
            string cadenaConexion = ConfigurationManager.ConnectionStrings["conexionGad"].ConnectionString;

            using (var conn = new NpgsqlConnection(cadenaConexion))
            {
                string sql = "SELECT ST_AsText(ST_Transform(geometria, 4326)) AS geometria_text FROM catastro.cat_predio WHERE pre_id = @id";

                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    conn.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read() && reader["geometria_text"] != DBNull.Value)
                        {
                            geometriaWkt = reader["geometria_text"].ToString();
                        }
                    }
                }
            }
        }
    }
}
