<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MapaPredio.aspx.cs" Inherits="WebHB_BG.MapaPredio" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Mapa del Predio</title>

    <!-- Leaflet CSS -->
    <link rel="stylesheet" href="https://unpkg.com/leaflet@1.7.1/dist/leaflet.css" />

    <!-- Leaflet JS -->
    <script src="https://unpkg.com/leaflet@1.7.1/dist/leaflet.js"></script>

    <style>
        /* Estilo general */
        body {
            font-family: Arial, sans-serif;
            background-color: #f8f9fa;
            margin: 0;
            padding: 0;
        }
        .container {
            max-width: 1200px;
            margin: 0 auto;
            padding: 20px;
        }

        /* Título */
        h2 {
            text-align: center;
            color: #007bff;
            margin-bottom: 20px;
        }

        /* Mapa */
        #map {
            height: 600px;
            width: 100%;
            border: 1px solid #ccc;
        }

        /* Estilo para el botón */
        .btn-back {
            display: block;
            width: 200px;
            margin: 20px auto;
            padding: 10px;
            text-align: center;
            background-color: #28a745;
            color: white;
            font-size: 16px;
            border: none;
            border-radius: 5px;
            cursor: pointer;
        }
        .btn-back:hover {
            background-color: #218838;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <!-- Título -->
            <h2>Visualización del Mapa del Predio</h2>

            <!-- Mapa -->
            <div id="map"></div>

            <!-- Botón de regreso -->
            <a href="Predio.aspx" class="btn-back">Regresar a Predio</a>

            <script type="text/javascript">
                var nombrePredio = '<%= Request.QueryString["nombre"] %>';
                var areaTerreno = '<%= Request.QueryString["area"] %>';

                var map = L.map('map').setView([0, 0], 15);
                L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png').addTo(map);

                <% if (!string.IsNullOrEmpty(geometriaWkt)) { %>
                    var wktData = '<%= geometriaWkt.Replace("\r", "").Replace("\n", "").Replace("'", "\\'") %>';

                    function parseWKT(wkt) {
                        var coords = [];
                        var cleaned = wkt.replace('MULTIPOLYGON(((', '').replace(')))', '');
                        var points = cleaned.split(',');

                        for (var i = 0; i < points.length; i++) {
                            var point = points[i].trim().split(' ');
                            coords.push([parseFloat(point[1]), parseFloat(point[0])]);
                        }
                        return coords;
                    }

                    var polygonCoords = parseWKT(wktData);
                    var polygon = L.polygon(polygonCoords, { color: 'blue' }).addTo(map);
                    map.fitBounds(polygon.getBounds());

                    // Mostrar detalles al hacer clic en el polígono
                    polygon.on('click', function() {
                        var popupContent = `
                            <strong>Nombre del Predio:</strong> ${nombrePredio} <br/>
                            <strong>Área Terreno (m²):</strong> ${areaTerreno} <br/>
                        `;
                        polygon.bindPopup(popupContent).openPopup();
                    });
                <% } else { %>
                    console.log("No hay geometría disponible.");
                <% } %>
            </script>
        </div>
    </form>
</body>
</html>
