
/*
/// <summary>
/// Module purpose: javascript to define the frontend behaviour of evaluation view
/// Authors: Hansol Lee / Jei Yang
/// Date: Nov 20, 2022
/// Source: Created for COMP7022 project
/// Revision History:
///
///
/// </summary>
*/

var isReadOnly = false;

$(document).ready(function () {
    //set default grade as pass

    var overallGrade = $("#overallGradeInput").val();
    if (overallGrade) {
        var option = $('#selectAssessmentGrade option[value="' + overallGrade + '"]');
        option.prop('selected', true);
        overallGradeColorSet(overallGrade, $("#selectAssessmentGrade"));
    } else {
        $('#selectAssessmentGrade option:eq(1)').prop('selected', true);
    }

});

$("#selectAssessmentGrade").change(function () {
    var selected = $(this).val();
    var assessmentID = $("#assessmentID").val();
    switch (selected) {
        case "Pass":
            $(this).css({ 'background-color': 'green', 'color': 'white'  });
            break;
        case "PartialPass":
            $(this).css({ 'background-color': '#CCCC00', 'color': 'black'  });
            break;
        case "Fail":
            $(this).css({ 'background-color': 'red', 'color': 'white'  });
            break;
    }

    var overallGradeData = {
        AssessmentID: assessmentID,
        OverallGrade: selected
    };

    UpdateOverallGrade(overallGradeData);
});

$(".gradeButtons").click(function () {
    var assessmentID = $("#assessmentID").val();
    var gradeSelected = $(this).attr("data-grade");
    var gradeSchema = $(this).attr("data-grade");
    var elementId = $(this).attr("data-element");

    var parentDiv = $(this).parent().closest('div').attr('id');
    console.log(parentDiv);
    $("#" + parentDiv + " .gradeButtons").each(function (btn) {
        $(this).removeClass("active");
    });
    $(this).addClass("active");

    var gradeData = {
        AssessmentID: assessmentID,
        Grade: gradeSelected,
        GradeSchema: gradeSchema,
        ElementID: elementId
    };

    UpdateAssessmentElementGrade(gradeData);
})

function overallGradeColorSet(grade, target) {
    switch (grade) {
        case "Pass":
            target.css({ 'background-color': 'green', 'color': 'white' });
            break;
        case "PartialPass":
            target.css({ 'background-color': '#CCCC00', 'color': 'black' });
            break;
        case "Fail":
            target.css({ 'background-color': 'red', 'color': 'white' });
            break;
        default:
            target.css({ 'background-color': 'white', 'color': 'black' });
    }
}


function signAssessment(assessmentID, userID, userRole, inputTarget, errorTarget) {
    $(".error").empty();
    var errorTargetId = $("#" + errorTarget);
    var inputTargetId = $("#" + inputTarget);
    var password = inputTargetId.val();

    if (SignValidation(password, inputTargetId, errorTargetId)) {
        var signData = {
            AssessmentID: assessmentID,
            UserID: userID,
            UserRole: userRole,
            Password: password
        };
        SignAjaxCall(signData, inputTargetId, errorTargetId)
    }
}

function SignValidation(password, inputTargetId, errorTargetId) {
    if (password && password.length > 0) {
        return true;
    }
    errorTargetId.text("Please enter the password");
    return false;
}

function SignAjaxCall(signData, inputTargetId, errorTargetId) {
    var url = "/Assessment/SignAssessment"
   
    $.ajax({
        type: "POST",
        url: url,
        contentType: "application/json",
        data: JSON.stringify(signData),
        success: function (data) {
            if (data.success) {
                var signedText = '<p class="signedText">Signed ' + data.responseText + '</p>';
                var parentDidv = inputTargetId.parent();
                parentDidv.append(signedText);
                parentDidv.find("button").hide();
                inputTargetId.hide();
                location.reload();
            } else {
                //Show error message
                errorTargetId.show();
                errorTargetId.text(data.responseText);

                //remove input value
                inputTargetId.val('');
            }
        },
        error: function (error) {
            alert(error);
        }
    });
}

function UpdateOverallGrade(overallGradeData) {
    var url = "/Assessment/UpdateAssessmentGrade";

    $.ajax({
        type: "POST",
        url: url,
        contentType: "application/json",
        data: JSON.stringify(overallGradeData),
        success: function (data) {
            if (data.success) {

            } else {
                alert(data.responseText);
            }
        },
        error: function (error) {
            alert(error);
        }
    });
}

function UpdateAssessmentElementGrade(gradeData) {
    var url = "/Assessment/UpdateAssessmentElementGrade";

    $.ajax({
        type: "POST",
        url: url,
        contentType: "application/json",
        data: JSON.stringify(gradeData),
        success: function (data) {
            if (data.success) {

            } else {

            }
        },
        error: function (error) {
            alert(error);
        }
    });
}






