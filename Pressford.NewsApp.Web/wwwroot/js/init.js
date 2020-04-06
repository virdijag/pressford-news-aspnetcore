
$(document).ready(function () {
    getChartData();
});

function getChartData() {
    $.ajax({

        url: "http://localhost:51576/api/article",
        type: "GET",
        success: function (result) {
            var obj = JSON.parse(JSON.stringify(result));

            var likes = [];
            var titles = [];

            $.each(obj, function (i, $val) {
                likes.push($val.numberOfLikes);
                titles.push($val.title);
            });

            var data = likes;
            var labels = titles;
            renderChart(data, labels);
        },
        error: function (err) {
            alert(err);
        }
    });
}

function renderChart(data, labels) {
    new Chart(document.getElementById("horizontalBar"), {
        "type": "horizontalBar",
        "data": {
            "labels": labels,
            "datasets": [{
                "label": "Number Of Likes",
                "data": data,
                "fill": false,
                "backgroundColor": ["rgba(255, 99, 132, 0.2)", "rgba(255, 159, 64, 0.2)",
                    "rgba(255, 205, 86, 0.2)", "rgba(75, 192, 192, 0.2)", "rgba(54, 162, 235, 0.2)",
                    "rgba(153, 102, 255, 0.2)", "rgba(201, 203, 207, 0.2)"
                ],
                "borderColor": ["rgb(255, 99, 132)", "rgb(255, 159, 64)", "rgb(255, 205, 86)",
                    "rgb(75, 192, 192)", "rgb(54, 162, 235)", "rgb(153, 102, 255)", "rgb(201, 203, 207)"
                ],
                "borderWidth": 1
            }]
        },
        "options": {
            "scales": {
                "xAxes": [{
                    "ticks": {
                        "beginAtZero": true
                    }
                }],
                yAxes: [{ ticks: { mirror: true, padding: -10 } }]
            }
        }
    });

}