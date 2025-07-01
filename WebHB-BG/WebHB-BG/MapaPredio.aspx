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
        #map { height: 600px; width: 100%; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <h2>Visualización del Mapa del Predio</h2>
        <div id="map"></div>

        <script type="text/javascript">
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
            <% } else { %>
                console.log("No hay geometría disponible.");
            <% } %>
        </script>
    </form>
</body>
</html>
