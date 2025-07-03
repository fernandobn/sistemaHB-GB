using System;
using System.Collections.Generic;
using System.Web.UI;
using Npgsql;  // Asegúrate de tener Npgsql añadido a tu proyecto

namespace WebHB_BG
{
    public partial class dashboard : System.Web.UI.Page
    {
        public List<string> parroquias { get; set; }
        public List<int> prediosPorParroquia { get; set; }
        public List<string> propietarios { get; set; }
        public List<int> prediosPorPropietario { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
        
            parroquias = new List<string>();
            prediosPorParroquia = new List<int>();

            string connectionString = "Host=localhost;Port=5432;Database=minicipio;Username=postgres;Password=F3rtipan77;Pooling=true;SSL Mode=Prefer;";
            string queryParroquias = @"
                SELECT pr.par_nombre, COUNT(vp.pre_id) AS total_predios
                FROM catastro.vst_predio vp
                JOIN catastro.cat_parroquias pr ON vp.par_id = pr.par_id
                GROUP BY pr.par_nombre
                ORDER BY total_predios DESC";

            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                NpgsqlCommand cmd = new NpgsqlCommand(queryParroquias, conn);
                conn.Open();
                NpgsqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    parroquias.Add(reader["par_nombre"].ToString());
                    prediosPorParroquia.Add(Convert.ToInt32(reader["total_predios"]));
                }
            }

            propietarios = new List<string>();
            prediosPorPropietario = new List<int>();


            string queryPropietarios = @"
                SELECT 
                    CONCAT(gp.pro_nombre, ' ', gp.pro_apellido) AS nombre_completo,
                    COUNT(pp.pre_id) AS numero_predios
                FROM catastro.cat_propietario_predio pp
                JOIN gestion.ges_propietario gp ON pp.pro_id = gp.pro_id
                GROUP BY gp.pro_nombre, gp.pro_apellido
                ORDER BY numero_predios DESC
                LIMIT 5";

            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                NpgsqlCommand cmd = new NpgsqlCommand(queryPropietarios, conn);
                conn.Open();
                NpgsqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    propietarios.Add(reader["nombre_completo"].ToString());
                    prediosPorPropietario.Add(Convert.ToInt32(reader["numero_predios"]));
                }
            }
        }
    }
}
