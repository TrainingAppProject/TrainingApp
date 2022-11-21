
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
    $("#selectAssessmentGrade").css({ 'background-color': 'green' });
});

$("#selectAssessmentGrade").change(function () {
    var selected = $(this).val();

    switch (selected) {
        case "Pass":
            $(this).css({ 'background-color': 'green' });
            break;
        case "Fail":
            $(this).css({ 'background-color': 'red' });
            break;
    }
});


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
            console.log(data);
            if (data.success) {
                
                inputTargetId.attr("disabled", true);
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





