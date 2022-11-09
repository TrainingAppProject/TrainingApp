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
$(document).ready(function () {
    /*
    $('input[name="templateDateRange"]').daterangepicker({
        opens: 'right',
        autoUpdateInput: false,
        locale: {
            cancelLabel: 'Clear'
        }
    }, function (start, end, label) {
        
    });*/
});



$("#createTemplateModalBtn").click(function () {
    showModal('createTemplateModal');
});

$("#addFilterToggleBtn").click(function () {
    showFilter('templateFilterDialog');
    
});

$("#filterFormCancelBtn").click(function () {
    filterClose('templateFilterDialog');
});


$('input[name="templateDateRange"]').on('apply.daterangepicker', function (ev, picker) {
    $(this).val('Created Date: ' + picker.startDate.format('MM.DD.YYYY') + ' ~ ' + picker.endDate.format('MM.DD.YYYY'));
    this.style.width = ((this.value.length + 1) * 7) + 'px';
});

$('input[name="templateDateRange"]').on('cancel.daterangepicker', function (ev, picker) {
    $(this).val('');
});


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



function validateTemplateForm(formID) {
    var isvalid = true;
    //Adding # prefix by default, for consistency
    formID = "#" + formID;
    $(formID + " .error").empty();
    //TBD
    if ($(formID).find('input[name="Template.Name"]').val() == '') {
        showErrorMsg("#nameError", requiredErrorMessage);
        isvalid = false;
    }

    if ($(formID).find('input[name="Template.GradingSchema"]').val() == '') {
        showErrorMsg("#gradingSchemaError", requiredErrorMessage);
        isvalid = false;
    }

    if ($(formID).find('input[name="Template.Description"]').val() == '') {
        showErrorMsg("#descriptionError", requiredErrorMessage);
        isvalid = false;
    }

    //set checkbox value
    var isTaskMandatory = $('.isMandatoryRadio:radio:checked').map(function () {
        return this.value;
    }).get();
    if (isTaskMandatory.length != 1) {
        showErrorMsg("#isTaskMandatoryError", selectionErrorMessage);
        isvalid = false;
    }

    else
        $("#rolesCheckboxInput").val(roles);

    if (isvalid)
        $(formID).submit();
}




