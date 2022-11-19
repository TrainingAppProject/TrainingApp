
/*
/// <summary>
/// Module purpose: javascript to define the frontend behaviour of Assessment view
/// Authors: Hansol Lee / Jei Yang
/// Date: Oct 26, 2022
/// Source: Created for COMP7022 project
/// Revision History:
///     
///     
/// </summary>
*/
$(document).ready(function () {
    console.log('here');
});

$("#createAssessmentModalBtn").click(function () {
    showModal("createAssessmentModal");
});


function validateAssessmentForm(formID) {
    var isvalid = true;

    //Adding # prefix by default, for consistency
    formID = "#" + formID;
    $(formID + " .error").empty();

    if ($(formID).find('input[name="Assessment.Name"]').val() == '') {
        showErrorMsg("#assessmentNameError", requiredErrorMessage);
        isvalid = false;
    }

    if ($(formID).find('select[name="Assessment.TemplateID"] option:selected').val() == '0') {
        showErrorMsg("#templateSelectionError", requiredErrorMessage);
        isvalid = false;
    }

    if ($(formID).find('select[name="Assessment.TraineeID"]').val() == '0') {
        showErrorMsg("#traineeSelectionError", requiredErrorMessage);
        isvalid = false;
    }

    //TODO save purpose

    if (isvalid)
        $(formID).submit();
}

