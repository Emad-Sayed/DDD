google.charts.load("current", { packages: ["corechart"] });
google.charts.setOnLoadCallback(drawChart);
function drawChart() {
    var data = google.visualization.arrayToDataTable([
        ['Task', 'Hours per Day'],
        ['جديدة', 2],
        ['غير معينة', 3],
        ['مفتوحة', 7],
        ['معلقة', 5],
        ['مغلقة', 4]
    ]);

    var options = {
        title: 'البلاغات بالحالة',
        is3D: true,
    };

    var chart = new google.visualization.PieChart(document.getElementById('piechart_3d'));
    chart.draw(data, options);
}



google.charts.load('current', { packages: ['corechart', 'bar'] });
google.charts.setOnLoadCallback(drawStacked);

function drawStacked() {
    var data = new google.visualization.DataTable();
    data.addColumn('timeofday', 'Time of Day');
    data.addColumn('number', 'Am');
    data.addColumn('number', 'Pm');

    data.addRows([

        [{ v: [1], f: 'السبت' }, 4, 5],
        [{ v: [2], f: 'الأحد' }, 5, 5],
        [{ v: [3], f: 'الاثنين' }, 6, 6.25],
        [{ v: [4], f: 'الثلاثاء' }, 8, 8.25],
        [{ v: [5], f: 'الاربعاء' }, 9, 10],
        [{ v: [6], f: 'الخميس' }, 8, 7],
        [{ v: [7], f: 'الجمعة' }, 6, 5.25],


    ]);




    var options = {
        title: 'البلاغات صباحا و مساءا فى اسبوع',
        isStacked: true,
        hAxis: {
            format: " ",
            viewWindow: {
                min: [0],
                max: [8]
            }
        },
        vAxis: {
            title: 'عدد البلاغات'
        }
    };

    var chart = new google.visualization.ColumnChart(document.getElementById('chart_div'));
    chart.draw(data, options);
}



var table = document.getElementById("myTable"); //Table parent

for (var i = 1; i < table.rows.length; i++) {
    //Loop through the table rows

    for (var j = 0; j < table.rows[i].cells.length; j++) {
        //Loop each cell of each row

        var td_val = parseInt(table.rows[i].cells[j].innerHTML); //Get the TD content and make it a number

        // Check the value against a number range

        if (td_val >= 18) {
            table.rows[i].cells[j].classList.add("m90");
        } else if (td_val < 18 && td_val >= 16) {
            table.rows[i].cells[j].classList.add("m80");
        } else if (td_val < 16 && td_val >= 14) {
            table.rows[i].cells[j].classList.add("m70");
        } else if (td_val < 14 && td_val >= 12) {
            table.rows[i].cells[j].classList.add("m60");
        } else if (td_val < 12 && td_val >= 10) {
            table.rows[i].cells[j].classList.add("m50");
        } else if (td_val < 10 && td_val >= 8) {
            table.rows[i].cells[j].classList.add("m40");
        } else if (td_val < 8 && td_val >= 6) {
            table.rows[i].cells[j].classList.add("m30");
        } else if (td_val < 6 && td_val >= 4) {
            table.rows[i].cells[j].classList.add("m20");
        } else if (td_val < 4 && td_val >= 2) {
            table.rows[i].cells[j].classList.add("m10");
        } else if (td_val < 2) {
            table.rows[i].cells[j].classList.add("m0");
        }
    }
}



Highcharts.chart('container', {

    chart: {
        styledMode: true
    },

    title: {
        text: 'البلاغات بالنوع'
    },

    xAxis: {
        categories: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec']
    },

    series: [{
        type: 'pie',
        allowPointSelect: true,
        keys: ['name', 'y', 'selected', 'sliced'],
        data: [
            ['صيانة - أبواب', 71.5, false],
            ['صيانة - سبكاة', 45.4, false],
            ['صيانة - كهرباء', 66.2, false],
            ['صيانة - جرافيتى', 176.0, false],
            ['مساعدة ساكن', 148.5, false],
            ['مساعدة موظف', 48.5, false],
            ['مساعدة الإدارة', 48.5, false],
            ['شخص غير مرخص بالدخول', 48.5, false],
            ['تحرش', 48.5, false],
            ['شخص مشتبه به', 48.5, false],
            ['إتلاف ممتلكات', 48.5, false],
            ['باركنج', 48.5, false],
            ['تعدى على ممتلكات الغير', 48.5, false],
            ['عثور على شىء', 48.5, false],
            ['باب مفتوح', 48.5, false],
            ['حادث', 48.5, false],
            ['حادث سير', 48.5, false],
            ['حريق', 48.5, false],
            ['ضبط ممنوعات', 48.5, false],
            ['سرقة', 48.5, false],
        ],
        showInLegend: true
    }]
});





google.charts.load('current', {packages: ['corechart', 'line']});
google.charts.setOnLoadCallback(drawBackgroundColor);

function drawBackgroundColor() {
      var data = new google.visualization.DataTable();
      data.addColumn('number', 'X');
      data.addColumn('number', 'Days');

      data.addRows([
      [0,0], [1 , 10], [2 , 15], [3, 20], [4, 30], [5 , 40] , [6 , 60], [7 , 70], [8 , 60] , [9 , 50] , [10 ,45]
       
      ]);

      var options = {
        hAxis: {
          title: ' فى ايام من 1/12/2019 الى 10/12/2019'
        },
        vAxis: {
          title: 'عدد البلاغات'
        },
        backgroundColor: '#fff'
      };

      var chart = new google.visualization.LineChart(document.getElementById('chart_div1'));
      chart.draw(data, options);
    }



    // Themes begin
am4core.useTheme(am4themes_kelly);
am4core.useTheme(am4themes_animated);
am4core.options.commercialLicense = true;
// Themes end

// Create chart instance
var chart = am4core.create("chartdiv", am4charts.XYChart);

var data = [

    
    {
        "sector": "السبت",
        "عادى": 8,
        "هام": 5
    },
    {
        "sector": "الأحد",
        "عادى": 7,
        "هام": 4
    },
    {
        "sector": "الإثنين",
        "عادى": 5,
        "هام": 6,
        "بالغ الاهمية": 11
    },
    {
        "sector": "الثلاثاء",
        "عادى": 8,
        "هام": 17,
        "بالغ الاهمية": 6
    },
    {
        "sector": "الأربعاء",
        "عادى": 11,
        "هام": 6,
        "بالغ الاهمية": 8
    },
    {
        "sector": "الخميس",
        "عادى": 13,
        "هام": 10
    },
    {
        "sector": "الجمعة",
        "عادى": 16,
        "هام": 9
    }
   
];

let series = [];

// Config Legend
chart.legend = new am4charts.Legend();
chart.legend.position = "right";
chart.legend.valign = "top";
chart.legend.itemContainers.template.paddingTop = 5;
chart.legend.itemContainers.template.paddingBottom = 5;
chart.legend.useDefaultMarker = true;
chart.legend.labels.template.text = "[font-size:10px {color}]{name}[/]";
var markerTemplate = chart.legend.markers.template;
markerTemplate.width = 12;
markerTemplate.height = 12;

// Config scrollbarX
/*  chart.scrollbarX = new am4core.Scrollbar();
chart.scrollbarX.dy = -30;
*/
// Config padding
chart.paddingLeft = 40;
chart.paddingRight = 20;
chart.paddingTop = 30;
chart.width = 750 ;

// Add data chart
chart.data = data;

// Create category axis
var categoryAxis = chart.xAxes.push(new am4charts.CategoryAxis());
categoryAxis.dataFields.category = "sector";
categoryAxis.renderer.grid.template.location = 0;
categoryAxis.renderer.minGridDistance = 10;
categoryAxis.renderer.cellStartLocation = 0.1;
categoryAxis.renderer.cellEndLocation = .9;

// Config value axis
var valueAxis = chart.yAxes.push(new am4charts.ValueAxis());
valueAxis.renderer.inside = false;
valueAxis.renderer.labels.template.disabled = false;
valueAxis.extraMax = 0;
valueAxis.strictMinMax = false;

series = reduceSeries(data);

series.forEach((v) => {
    if (v !== 'sector') {
        createSeries(v, v);
    }
});

// Config label calculateTotals
// var totalBullet = totalSeries.bullets.push(new am4charts.LabelBullet());
// totalBullet.dy = -20;
totalBullet.label.text = "{valueY.total}";
// totalBullet.label.hideOversized = false;
// totalBullet.label.fontSize = 11;
// totalBullet.label.background.fill = totalSeries.stroke;
// totalBullet.label.dy = -20;
// totalBullet.label.background.fillOpacity = 0.2;
// totalBullet.label.padding(5, 0, 5, 0);
// totalBullet.label.truncate = false;


// Create series
function createSeries(field, name) {
    // Set up series
    let series = chart.series.push(new am4charts.ColumnSeries());

    series.name = name;
    series.dataFields.valueY = field;
    series.dataFields.categoryX = "sector";
    series.sequencedInterpolation = true;

    // Make it stacked
    series.stacked = true;

    // Configure columns
    // series.columns.template.width = am4core.percent(55);
    // series.columns.template.tooltipText = "[bold]{name}[/]\n[font-size:14px]{categoryX}: {valueY}";

    // Add label Bullet
    let labelBullet = series.bullets.push(new am4charts.LabelBullet());
    labelBullet.label.text = "{valueY}";
    labelBullet.label.fill = am4core.color("#fff");
    labelBullet.label.fontSize = 10;
    labelBullet.label.dy = 0;
    labelBullet.label.hideOversized = true;
    labelBullet.locationY = 0.5;

    return series;
}

// Add legend
chart.legend = new am4charts.Legend();


/*
* Objenemos los nombres únicos para generar series
*/
function reduceSeries(x) {
    let a = x.reduce(function (res, obj) {
        let keys = Object.keys(obj);
        keys.forEach(item => {
            if (!(item in res)) {
                res.__array.push(res[item] = item);
            }
        });

        return res;
    }, { __array: [] }).__array;

    return a;
}
