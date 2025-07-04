using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebHB_BG
{
    public class PropietarioPredioDTO
    {
        public int PrpId { get; set; }
        public string Propietario { get; set; }
        public string ProNumIdentificacion { get; set; }
        public string PredioInfo { get; set; }
        public decimal? PrpAlicuota { get; set; }
        public int? PrpAniosPosesion { get; set; }
        public string TieneEscritura { get; set; }
        public string TipoPropietario { get; set; }
    }
}