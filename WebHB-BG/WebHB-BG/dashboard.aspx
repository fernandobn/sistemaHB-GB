<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="dashboard.aspx.cs" Inherits="WebHB_BG.dashboard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Dashboard</h2>

    <!-- Contenedor con fila para gráficos -->
    <div class="row">
        <!-- Card para el gráfico de barras (Predios por Parroquia) -->
        <div class="col-md-6">
            <div class="card" style="margin-bottom: 20px;">
                <div class="card-body">
                    <h5 class="card-title">Predios por Parroquia</h5>
                    <canvas id="barChart" width="300" height="200"></canvas> <!-- Ajustado tamaño -->
                </div>
            </div>
        </div>

        <!-- Card para el gráfico de pastel (TOP 5 Propietarios con más Predios) -->
        <div class="col-md-6">
            <div class="card" style="margin-bottom: 20px;">
                <div class="card-body">
                    <h5 class="card-title">Top 5 Propietarios con Más Predios</h5>
                    <canvas id="pieChart" width="300" height="200"></canvas> <!-- Ajustado tamaño -->
                </div>
            </div>
        </div>
    </div>



    <script>
        // Datos para el gráfico de barras (Predios por Parroquia)
        var parroquias = <%= Newtonsoft.Json.JsonConvert.SerializeObject(parroquias) %>;
        var prediosPorParroquia = <%= Newtonsoft.Json.JsonConvert.SerializeObject(prediosPorParroquia) %>;

        var barCtx = document.getElementById('barChart').getContext('2d');
        var barChart = new Chart(barCtx, {
            type: 'bar',
            data: {
                labels: parroquias,
                datasets: [{
                    label: 'Total de Predios',
                    data: prediosPorParroquia,
                    backgroundColor: '#FF5733',
                    borderColor: '#C70039',
                    borderWidth: 1
                }]
            },
            options: {
                responsive: true,
                plugins: {
                    legend: {
                        position: 'top',
                    },
                    tooltip: {
                        enabled: true
                    },
                    datalabels: {
                        anchor: 'center',
                        align: 'center',
                        font: {
                            weight: 'bold',
                            size: 16
                        },
                        color: '#FFFFFF',
                        formatter: function (value) {
                            return value;
                        }
                    }
                },
                scales: {
                    y: {
                        beginAtZero: true,
                        ticks: {
                            font: {
                                size: 12
                            }
                        }
                    },
                    x: {
                        ticks: {
                            font: {
                                size: 12
                            }
                        }
                    }
                }
            }
        });

        // Datos para el gráfico de pastel (TOP 5 Propietarios con más Predios)
        var propietarios = <%= Newtonsoft.Json.JsonConvert.SerializeObject(propietarios) %>;
        var prediosPorPropietario = <%= Newtonsoft.Json.JsonConvert.SerializeObject(prediosPorPropietario) %>;

        var pieCtx = document.getElementById('pieChart').getContext('2d');
        var pieChart = new Chart(pieCtx, {
            type: 'pie',
            data: {
                labels: propietarios,
                datasets: [{
                    label: 'Número de Predios',
                    data: prediosPorPropietario,
                    backgroundColor: ['#FF5733', '#33FF57', '#3357FF', '#F2FF33', '#F233FF'],
                    borderColor: '#C70039',
                    borderWidth: 1
                }]
            },
            options: {
                responsive: true,
                plugins: {
                    legend: {
                        position: 'top',
                    },
                    tooltip: {
                        enabled: true
                    },
                    datalabels: {
                        formatter: function (value) {
                            return value;
                        },
                        color: '#FFFFFF',
                        font: {
                            weight: 'bold',
                            size: 16
                        }
                    }
                }
            }
        });
    </script>
</asp:Content>
