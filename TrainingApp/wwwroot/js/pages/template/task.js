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

$(document).ready(function () {
    // If it's a deleted template, disable adding new tasks as well as editing or removing existing tasks.
    if ($("#templateState").val() == 2) {
        $("#createTaskModalBtn").prop("disabled", true); //create disabled
        sortable.option("disabled", true); //drag-and-drop disabled
        $(".disabled-btn").prop("disabled", true); //edit and delete  disabled
    }
});

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

//-------------------------UPDATE TASK----------------------------//

function getTaskInfo(templateElementID, templateID) {
    var modalTitle = "Edit";

    if (templateElementID == "0") {
        modalTitle = "Create";
        //$("#templateID").val(templateID);
    }

    $.ajax({
        type: "Get",
        url: "GetTaskForm",
        data: { "templateElementID" : templateElementID, "templateID" : templateID },
        success: function (data) {
            $("#taskModalBody").html(data);
            $("#createTaskModal .action").text(modalTitle);
            showModal("createTaskModal");
        },
        error: function (error) {
            alert(error);
        }
    });

}
//---------------------------DRAG AND DROP--------------------//

var dropItems = document.getElementById('taskListBody'); //drop-items

var sortable = new Sortable(dropItems, {
    animation: 350,
    chosenClass: "sortable-chosen",
    dragClass: "sortable-drag",

    // Called when dragging element changes position
	onEnd: function (/**Event*/evt) {
        // Example: https://jsbin.com/nawahef/edit?js,output
        updateTaskListOrder(evt.item, evt.newIndex, evt.oldIndex);
    },
});

function updateTaskListOrder (draggedItem, newIndex, oldIndex) {
    var templateElementID = draggedItem.querySelector(".templateElementID").innerHTML;
    
    var url = "/Template/UpdateTaskOrder/";
    $.post(url, { templateElementID: templateElementID, newIndex: newIndex, oldIndex: oldIndex }, function (data) {  
        $("#taskListBody").html(data);
    });
}