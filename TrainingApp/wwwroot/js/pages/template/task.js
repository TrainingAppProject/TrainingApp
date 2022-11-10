/*
/// <summary>
/// Module purpose: javascript to define the frontend behaviour of Task (associated to Template)
/// Authors: Hansol Lee / Jei Yang
/// Date: Oct 26, 2022
/// Source: Created for COMP7022 project
/// Revision History:
///     
///     
/// </summary>
*/

//-------------------------DELETE-----------------------//
function confirmDeleteTemplateElement(id) {
    //if (confirm('Are you sure you want to remove this record?'))
    showModal('deleteModal');
    //add onclick event to the button with the templateID
    $('#deleteButton').attr('onClick', 'deleteTask("' + id+ '")');
}


function deleteTask(id) {
    var posting = $.post("/Template/DeleteTask/" + id);
    //$("#successMessage").html("The record has been deleted successfully!");

    modalClose('deleteModal'); // now close modal

    posting.done(function(){
        //TBD Current bug: the page won't refresh.
        window.location.reload();
    });
    
}  



//--------------------------CREATE----------------------//
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