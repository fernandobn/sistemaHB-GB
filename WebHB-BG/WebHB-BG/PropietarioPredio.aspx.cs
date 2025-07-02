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
            CargarPredios();
        }
        private void CargarPredios()
        {
            //DataTable dtPredios = new DataTable();

            //using (var conn = new NpgsqlConnection(cadenaConexion))
            //using (var cmd = new NpgsqlCommand("catastro.listar_predios", conn))
            //{
                //cmd.CommandType = CommandType.StoredProcedure;

                //using (var da = new NpgsqlDataAdapter(cmd))
                //{
                    //da.Fill(dtPredios);
                //}
            //}

            //gvPredios.DataSource = dtPredios;
            //gvPredios.DataBind();
        }

        protected void gvPredios_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvPredios.PageIndex = e.NewPageIndex;
            CargarPredios();
        }
    }
}