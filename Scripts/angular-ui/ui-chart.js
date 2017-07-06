angular.module('ui.chart', [])
    .service('chartService', function () {
        this.drawChart = function (chartDivId, title, data, options) {
            if (options == undefined) {
                options = {
                    title: title,
                    seriesDefaults: {
                        renderer: jQuery.jqplot.PieRenderer,
                        rendererOptions: {
                            showDataLabels: true
                        }
                    },
                    legend: { show: true, location: 'e' }
                };
            }

            $.jqplot(chartDivId, data, options);
        }
    });