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


var createStarDate;
var createEndDate;
var publishStarDate;
var publishEndDate;
var status;
var gradingSchema;

$(document).ready(function () {
    initDateFilters();
});

//-----------------------------------------FILTER-------------------------------//
function initDateFilters() {
    hideAllfilters();

    var createDatePicker = $('input[name="templateCreateDateRange"]');
    createDatePicker.daterangepicker({
        opens: 'right',
        autoUpdateInput: false,
        locale: {
            cancelLabel: 'Clear'
        }
    }, function (start, end, label) {
            console.log(start);
            console.log(end);
    });

    var today = moment();
    createDatePicker.data('daterangepicker').setStartDate();
    createDatePicker.data('daterangepicker').setEndDate(today);

    var publishDatePicker = $('input[name="templatepublishDateRange"]');
    publishDatePicker.daterangepicker({
        opens: 'right',
        autoUpdateInput: false,
        locale: {
            cancelLabel: 'Clear'
        }
    }, function (start, end, label) {

    });

    publishDatePicker.data('daterangepicker').setStartDate();
    publishDatePicker.data('daterangepicker').setEndDate(today);
}

$("#addFilterToggleBtn").click(function () {
    showFilter('templateFilterDialog');
    
});

$("#filterFormCancelBtn").click(function () {
    filterClose('templateFilterDialog');
});


$('input[name="templateCreateDateRange"]').on('apply.daterangepicker', function (ev, picker) {
    $(this).val('Created Date: ' + picker.startDate.format('MM.DD.YYYY') + ' ~ ' + picker.endDate.format('MM.DD.YYYY'));
    this.style.width = ((this.value.length + 1) * 7) + 'px';
});

$('input[name="templateCreateDateRange"]').on('cancel.daterangepicker', function (ev, picker) {
    $(this).val('');
});

$('input[name="templatepublishDateRange"]').on('apply.daterangepicker', function (ev, picker) {
    $(this).val('Published Date: ' + picker.startDate.format('MM.DD.YYYY') + ' ~ ' + picker.endDate.format('MM.DD.YYYY'));
    this.style.width = ((this.value.length + 1) * 7) + 'px';
});

$('input[name="templatepublishDateRange"]').on('cancel.daterangepicker', function (ev, picker) {
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

function setStatus(value) {
    $("#statusValue").text(value);
    status = value;

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
        PublishStartDate: publishStarDate,
        PublishEndDate: publishEndDate,
        Status: status,
        GradingSchema: gradingSchema
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
    
    var isTaskMandatory = $('.isMandatoryRadio:radio:checked').map(function () {
        return this.value;
    }).get();
    if (isTaskMandatory.length != 1) {
        showErrorMsg("#isTaskMandatoryError", radioErrorMessage);
        isvalid = false;
    }
    else
        $("#mandatoryRadioInput").val(isTaskMandatory[0]);

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
    var posting = $.post("/Template/DeleteTemplate/" + id);
    //$("#successMessage").html("The record has been deleted successfully!");
    modalClose('deleteModal'); // now close modal

    posting.done(function(){
        //TBD Current bug: the page won't refresh.
        window.location.reload();
    });
    
}  

