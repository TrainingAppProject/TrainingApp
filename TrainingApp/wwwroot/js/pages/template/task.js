/*
/// <summary>
/// Module purpose: javascript to define the frontend behaviour of Template view
/// Authors: Hansol Lee / Jei Yang
/// Date: Oct 26, 2022
/// Source: Created for COMP7022 project
/// Revision History:
///     
///     
/// </summary>
*/

function confirmDeleteTemplateElement(id) {

}

function openCreateTaskModal (formID, templateID) {
    showModal(formID);
    $("#templateID").val(templateID);
}



//_CreateTask validation function
function validateTaskForm(formID) {
    var isvalid = true;

    //Adding # prefix by default, for consistency
    formID = "#" + formID;
    $(formID + " .error").empty();

    //TBD
    var tempName = $(formID).find('input[name="TaskViewModel.Name"]').val();
    if (!tempName || tempName == '') {
        showErrorMsg("#nameError", requiredErrorMessage);
        isvalid = false;
    }

    var tempID = $(formID).find('input[name="TaskViewModel.TemplateID"]').val(); //TBD - Testing the valuez

    if (isvalid)
        $(formID).submit();
}