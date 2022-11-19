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

const dateFormat = "DD/MM/YYYY";
const searchInterval = 1000; 

var createStarDate;
var createEndDate;
var modifyStarDate;
var modifyEndDate;
var status;
var gradingSchema;
var searchString;
var isPublished;

var typingTimer;               


$(document).ready(function () {
    initDateFilters();
    $("#statusSelectionDiv").show(); // enabled to display records in active state by default
    $("#statusValue").text('Active');
});

//-----------------------------------------FILTER-------------------------------//

function initDateFilters() {
    hideAllfilters();

    var createDatePicker = $('input[name="templateCreateDateRange"]');
    createDatePicker.daterangepicker({
        opens: 'right',
        autoUpdateInput: false,
        locale: {
            cancelLabel: 'Clear',
            format: dateFormat
        }
    }, function (start, end, label) {
            createStarDate = start.format(dateFormat);
            createEndDate = end.format(dateFormat);

            filterTemplates();
    });

    var modifyDatePicker = $('input[name="templateModifyDateRange"]');
    modifyDatePicker.daterangepicker({
        opens: 'right',
        autoUpdateInput: false,
        locale: {
            cancelLabel: 'Clear',
            format: dateFormat
        }
    }, function (start, end, label) {
            modifyStarDate = start.format(dateFormat);
            modifyEndDate = end.format(dateFormat);

            filterTemplates();
    });

}

$("#addFilterToggleBtn").click(function () {
    showFilter('templateFilterDialog');
    
});

$("#filterFormCancelBtn").click(function () {
    filterClose('templateFilterDialog');
});


$("#templateSearchInput").on('keyup', function () {
    clearTimeout(typingTimer);
    typingTimer = setTimeout(searchTemplateName, searchInterval);
});

$("#templateSearchInput").on('keydown', function () {
    clearTimeout(typingTimer);
});

function searchTemplateName() {
    searchString = $("#templateSearchInput").val().trim();
    if (searchString && searchString.length > 0) {
        filterTemplates();
    } else { //Reset the search string
        searchString = '';
        filterTemplates();
    }
}

$('input[name="templateCreateDateRange"]').on('apply.daterangepicker', function (ev, picker) {
    $(this).val('Created Date: ' + picker.startDate.format(dateFormat) + ' ~ ' + picker.endDate.format(dateFormat));
    this.style.width = ((this.value.length + 1) * 7) + 'px';
});

$('input[name="templateCreateDateRange"]').on('cancel.daterangepicker', function (ev, picker) {
    $(this).val('');
});

$('input[name="templateModifyDateRange"]').on('apply.daterangepicker', function (ev, picker) {
    $(this).val('Modified Date: ' + picker.startDate.format('MM.DD.YYYY') + ' ~ ' + picker.endDate.format('MM.DD.YYYY'));
    this.style.width = ((this.value.length + 1) * 7) + 'px';
});

$('input[name="templateModifyDateRange"]').on('cancel.daterangepicker', function (ev, picker) {
    $(this).val('');
});

$("#applyFilterButton").click(function () {
    hideAllfilters();
    var arr = [];
    $('#templateFilterDialog input.custom-control-input:checkbox:checked').each(function () {
        arr.push($(this).val());
    });

    if (arr && arr.length > 0)
        filterSelectionShow(arr);

    filterClose('templateFilterDialog');
});

function filterSelectionShow(arr) {
    $(arr).each(function () {
        $("#" + this + "SelectionDiv").show();
    });
}

function filterSelectionHide(target) {
    $("#" + target + "SelectionDiv").hide();

    switch (target) {
        case "createDate":
            createStarDate = '';
            createEndDate = '';
            break;
        case "modifyDate":
            modifyStarDate = '';
            modifyEndDate = '';
            break;
        case "status":
            status = '';
            break;
        case "gradingSchema":
            gradingSchema = '';
            break;
        case "isPublished":
            isPublished = '';
            break;
    }
    filterTemplates();

    $('#templateFilterDialog input.custom-control-input:checkbox:checked').each(function () {
        if ($(this).val() == target) {
            $(this).prop('checked', false);
        }
    });
}

function setStatus(value) {
    $("#statusValue").text(value == 'Delete' ? "Deleted" : "Active");
    status = value;

    filterTemplates();
}

function setPublished(value) {
    $("#isPublishedValue").text(value == 'true' ? 'Yes' : 'No');
    isPublished = value;

    filterTemplates();
}

function setGradingSchema(value) {
    var gradingDisplay = "";
    if (value == "PassFail")
        gradingDisplay = "BASIC - Pass / Fail";
    else if (value == "Score")
        gradingDisplay = "ADVANCED - Score";

    $("#gradingSchemaValue").text(gradingDisplay);
    gradingSchema = value;

    filterTemplates();
}

//When user clicks outside the filter dialog(popup), close the dialog.
window.addEventListener('click', function(e){   
    //If clicked outside the filter box
    if (!document.getElementById('templateFilterDialog').contains(e.target)){
        //If clicked outside the 'Add Filter' button
        if (!document.getElementById('addFilterToggleBtn').contains(e.target)) {
            
            if ($('#templateFilterDialog').hasClass('active')) { //If the filter dialog is currently open
                filterClose('templateFilterDialog');
            }
        }
        //If 'Add Filter' button is clicked, leave the filter dialog open.
    }
});

function hideAllfilters() {
    $('#appliedFilterDiv').find('.filterSelections').hide();
}

function filterTemplates() {
    var filterdata = {
        CreateStarDate: createStarDate,
        CreateEndDate: createEndDate,
        ModifyStartDate: modifyStarDate,
        ModifyEndDate: modifyEndDate,
        Status: status,
        GradingSchema: gradingSchema,
        SearchString: searchString,
        IsPublished: isPublished
    };

    $.ajax({
        type: "POST",
        url: "Template/FilterTemplates",
        contentType: "application/json",
        data: JSON.stringify(filterdata),  
        success: function (data) {
            $("#templateListBody").html(data);
        },
        error: function (error) {
            alert(error);
        }
    });
}


//--------------------------------CREATE TEMPLATE--------------------------//

$("#createTemplateModalBtn").click(function () {
    showModal('createTemplateModal');
});

function validateTemplateForm(formID) {
    var isvalid = true;
    //Adding # prefix by default, for consistency
    formID = "#" + formID;
    $(formID + " .error").empty();
    //TBD
    var tempName = $(formID).find('input[name="Template.Name"]').val();
    if (tempName == '') {
        showErrorMsg("#nameError", requiredErrorMessage);
        isvalid = false;
    }

    var gradingSchema = $('#gradingSchema option:selected');
    if (!gradingSchema.text() || gradingSchema.text() == '' || gradingSchema.hasClass('placeholder-message')) {
        showErrorMsg("#gradingSchemaError", requiredErrorMessage);
        isvalid = false;
    }

    /*
    var desc = $(formID).find('input[name="Template.Description"]').val()
    if (!desc || desc == '') {
        showErrorMsg("#descriptionError", requiredErrorMessage);
        isvalid = false;
    }
    */

    //set checkbox value
    
    //IsTaskMandatory
    var isTaskMandatory = $('.isMandatoryRadio:radio:checked').map(function () {
        return this.value;
    }).get();
    if (isTaskMandatory.length != 1) {
        showErrorMsg("#isTaskMandatoryError", radioErrorMessage);
        isvalid = false;
    }
    else
        $("#mandatoryRadioInput").val(isTaskMandatory[0]);

    //IsPublished
    var isPublished = $('.isPublishedRadio:radio:checked').map(function () {
        return this.value;
    }).get();
    if (isPublished.length > 0) {
        $("#isPublishedRadioInput").val(isPublished[0]);
    }

    //AttemptAllow, scriptNumber
    var attemptAllow = $(formID).find('input[name="Template.AttemptsAllowedPerTask"]').val();
    if (!attemptAllow || attemptAllow == '' || attemptAllow <= 0) {
        showErrorMsg("#attemptsAllowedPerTaskError", requiredErrorMessage);
        isvalid = false;
    }

    if (isvalid)
        $(formID).submit();
}

//------------------------UDPATE TEMPLATE-----------------------//

function getTemplateInfo(templateID) {
    var modalTitle = "Create";

    if (templateID != "0") {
        modalTitle = "Edit";
    }
        
    $.ajax({
        type: "Get",
        url: "Template/GetTemplateForm/" + templateID,
        data: { "templateID" : templateID },
        success: function (data) {
            $("#templateModalBody").html(data);
            $("#createTemplateModal .action").text(modalTitle);
            showModal("createTemplateModal");
            
            var isTaskMandatory = $("#mandatoryRadioInput").val();
            if (isTaskMandatory == '') {
                $("#allTaskMandatoryYes").prop("checked", true);
            }

            //State
            var state = $('#state').val();
            var stateText = 'Active';
            if (state) {
                if (state == 2) {
                    stateText = 'Deleted';

                    //Deleted records should have all fields disabled - Description, ScriptNumber, AttemptsAllowedPerTask, AllTaskMandatroy, Published
                    $("#templateDescription").prop( "disabled", true);
                    $("#scriptNumber").prop( "disabled", true);
                    $("#attemptAllow").prop( "disabled", true);
                    $("#allTaskMandatoryYes").prop( "disabled", true);
                    $("#allTaskMandatoryNo").prop( "disabled", true);
                    $("#isPublishedYes").prop( "disabled", true);
                    $("#isPublishedNo").prop( "disabled", true);
                } else if (state == 3) {
                    stateText = 'Pending';
                }
                $("#stateText").val(stateText);
            }
            
        },
        error: function (error) {
            alert(error);
        }
    });


}


//------------------------DELETE TEMPLATE-----------------------//

//Open the confirmation modal when user clicks on 'Delete' button from index page (main table)
function confirmDeleteTemplate(id) {
    //if (confirm('Are you sure you want to remove this record?'))
    showModal('deleteModal');
    //add onclick event to the button with the templateID
    $('#deleteButton').attr('onClick', 'deleteTemplate("' + id+ '")');
}


function deleteTemplate(id) {
    var posting = $.post("/Template/DeleteTemplate/" + id).done(function(){
        //TBD Current bug: the page won't refresh.
        window.location.reload();
    }).fail(function(xhr){
        modalClose('deleteModal'); // now close modal
        showModal('errorAlertDialog');
        $('#errorMessage').html(xhr.responseJSON.message)
    });

   
}  

