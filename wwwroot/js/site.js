

function graphSummaryData(data, target) {

    var chart = bb.generate({
        bindto: target,
        data: {
            x: "x",
            type: "bar",
            columns: data
        },
        axis: {
            x: {
                type: "timeseries",
                tick: {
                    format: "%Y-%m-%d"
                }
            }
        },
    });
}
