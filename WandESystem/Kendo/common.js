function callwebservice(controller, method, parameter, callbackFunction, isErrorHandle) {
    var url;
    url = controller + "/" + method;   
    if (typeof (parameter) === 'undefined')
        parameter = '';    
    try {
        var request = $.ajax({
            type: "POST",
            url: url,
            data: parameter,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false
        });


        // callback handler that will be called on success
        request.done(function (response) {            
            // log a message to the console
            var result = eval(response.d)[0];
            if (typeof (result) === 'undefined')
                return;
            callbackFunction(result);
        });

        // callback handler that will be called on failure
        request.fail(function (jqXhr, textStatus, errorThrown) {
            // log the error to the console
            if (isErrorHandle == true)
                callbackFunction("error");
            else {
                if (errorThrown != "")
                    alert("The following error occured: " + errorThrown);
                else
                    alert("There is an error while connecting to server. Please try again!");
            }
        });

        // callback handler that will be called regardless
        // if the request failed or succeeded
        request.always(function () {
        });
    }
    catch (e) {
        alert("Errour occurred " + e);
    }
}

function isDataAvailable(result, chartId) {

    if (result.isDataAvailable != undefined && !result.isDataAvailable) {
        $("#" + chartId).append("<h3 class='emptyDataTemplateTitle'>" + result.title.text + "</h3><br/><h1 class='emptyDataTemplateStyle'>No Data Available</h1>");
        return false;
    }
    return true;
}

//to create chart
function createChart(chartDivId, chartData) {
    $("#" + chartDivId).kendoChart(chartData);
}        