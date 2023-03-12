// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$("[name='addBtn']").click(function (e) {
    let task = $(e.target).closest("a")
    let taskId = task.attr("data-taskId");
    $("#TaskId").val(taskId)
})

$("[name='ViewTasksOfEmployee']").click(function (e) {
    let employee = $(e.target)
    let employeeId = employee.attr("data");
    $.ajax({
        type: 'GET',
        url: '/Employee/GetTasksOfEmployee/' + employeeId,
        success: function (result) {
            if (result === "No data") {
                $('#tasksResult').empty()
            } else {
                $('#tasksResult').html(result);
            }
        }
    })
})

var topFiveProjectDiv = $("#listOfTopFiveProjects")
var topFiveEmployeeDiv = $("#listOfTopFiveEmployees")

$("#topFiveEmployees").click(function () {
    if (topFiveEmployeeDiv.is(":hidden")) {
        if (topFiveProjectDiv.is(":visible")) {
            topFiveProjectDiv.hide()
        }
        topFiveEmployeeDiv.show();
    } else {
        topFiveEmployeeDiv.hide();
    }
})

$("#topFiveProjects").click(function () {
    if (topFiveProjectDiv.is(":hidden")) {
        if (topFiveEmployeeDiv.is(":visible")) {
            topFiveEmployeeDiv.hide()
        }
        topFiveProjectDiv.show();
    } else {
        topFiveProjectDiv.hide();
    }
})