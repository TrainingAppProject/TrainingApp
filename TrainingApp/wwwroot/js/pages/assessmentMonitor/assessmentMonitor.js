/*
/// <summary>
/// Module purpose: javascript to define the frontend behaviour of Assessment Monitor view
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

var createStartDate;
var createEndDate;
var modifyStartDate;
var modifyEndDate;
var status = 'Active';
var result;
var gradingSchema;
var searchString;

var typingTimer;
var checkedFilters= [];


$(document).ready(function () {

    initDateFilters();
    $("#statusSelectionDiv").show(); // enabled to display records in active state by default
    $("#statusValue").text('Active');
    
});


//-----------------------------------------FILTER-------------------------------//

function initDateFilters() {
    var createDatePicker = $('input[name="assessmentCreateDateRange"]');
    createDatePicker.daterangepicker({
        opens: 'right',
        autoUpdateInput: false,
        locale: {
            cancelLabel: 'Clear',
            format: dateFormat
        }
    }, function (start, end, label) {
            createStartDate = start.format(dateFormat);
            createEndDate = end.format(dateFormat);
        
            filterAssessments();
    });

    var modifyDatePicker = $('input[name="assessmentModifyDateRange"]');
    modifyDatePicker.daterangepicker({
        opens: 'right',
        autoUpdateInput: false,
        locale: {
            cancelLabel: 'Clear',
            format: dateFormat
        }
    }, function (start, end, label) {
            modifyStartDate = start.format(dateFormat);
            modifyEndDate = end.format(dateFormat);

            filterAssessments();
    });
}

$("#addFilterToggleBtn").click(function () {
    showFilter('assessmentFilterDialog');
    
});

$("#filterFormCancelBtn").click(function () {
    filterClose('assessmentFilterDialog');
});

$("#assessmentSearchInput").on('keyup', function () {
    clearTimeout(typingTimer);
    typingTimer = setTimeout(searchAssessmentName, searchInterval);
});

$("#assessmentSearchInput").on('keydown', function () {
    clearTimeout(typingTimer);
});

function searchAssessmentName() {
    searchString = $("#assessmentSearchInput").val().trim();
    if (searchString && searchString.length > 0) {
        filterAssessments();
    } else { //Reset the search string
        searchString = '';
        filterAssessments();
    }
}

$('input[name="assessmentCreateDateRange"]').on('apply.daterangepicker', function (ev, picker) {
    $(this).val('Created Date: ' + picker.startDate.format(dateFormat) + ' ~ ' + picker.endDate.format(dateFormat));
    this.style.width = ((this.value.length + 1) * 7) + 'px';
});

$('input[name="assessmentCreateDateRange"]').on('cancel.daterangepicker', function (ev, picker) {
    $(this).val('');
});

$('input[name="assessmentModifyDateRange"]').on('apply.daterangepicker', function (ev, picker) {
    $(this).val('Modified Date: ' + picker.startDate.format('MM.DD.YYYY') + ' ~ ' + picker.endDate.format('MM.DD.YYYY'));
    this.style.width = ((this.value.length + 1) * 7) + 'px';
});

$('input[name="assessmentModifyDateRange"]').on('cancel.daterangepicker', function (ev, picker) {
    $(this).val('');
});


$("#applyFilterButton").click(function () {
    let tempCheckedFilters = [];
    hideAllfilters();
    
    $('#assessmentFilterDialog input.custom-control-input:checkbox').each(function () {
        let val = $(this).val();

        //filter checkbox checked
        if ($(this).is(':checked')) {
            tempCheckedFilters.push(val);
        } else {
            //removed from original filter, reset the value
            if (jQuery.inArray(val, checkedFilters) == 0) {
                filterSelectionReset(val);
            }
        }
    });

    checkedFilters = tempCheckedFilters;
    if (checkedFilters && checkedFilters.length > 0)
        filterSelectionShow(checkedFilters);

    filterClose('assessmentFilterDialog');
});

function filterSelectionShow(arr) {
    $(arr).each(function () {
        $("#" + this + "SelectionDiv").show();
    });
}

function filterSelectionReset(target) {
    $("#" + target + "SelectionDiv").hide();
    removeFilterValue(target);
    console.log(target);
    filterAssessments();
}

function filterSelectionHide(target) {
    $("#" + target + "SelectionDiv").hide();

    removeFilterValue(target);
    filterAssessments();

    $('#assessmentFilterDialog input.custom-control-input:checkbox:checked').each(function () {
        if ($(this).val() == target) {
            $(this).prop('checked', false);
        }
    });
}


function removeFilterValue(target) {
    switch (target) {
        case "createDate":
            createStartDate = '';
            createEndDate = '';
            resetDatePicker($("#assessmentCreateDateRange"), 'Created Date: Select');
            break;
        case "modifyDate":
            modifyStartDate = '';
            modifyEndDate = '';
            resetDatePicker($("#assessmentModifyDateRange"), 'Modified Date: Select');
            break;
        case "status":
            status = '';
            $("#statusValue").text('Select');
            break;
        case "result":
            result = '';
            $("#resultValue").text('Select');
            break;
        case "gradingSchema":
            gradingSchema = '';
            $("#gradingSchemaValue").text('Select');
            break;
    }
}

function resetDatePicker(target, defaultValue) {
    target.val(defaultValue);
    target.css("width", "");
}

function setStatus(value) {
    var displayValue = value;
    switch (value) {
        case "Delete":
            displayValue = "Deleted";
            break;
    }
    $("#statusValue").text(displayValue);
    status = value;

    filterAssessments();
}


function setResult(value) {
    var displayValue = value;
    switch (value) {
        case "NotSet" :
            displayValue = "Not Set";
            break;
        case "PartialPass":
            displayValue = "Partial Pass";
            break;
    }
    $("#resultValue").text(displayValue);
    result = value;

    filterAssessments();
}

function setGradingSchema(value) {
    var gradingDisplay = "";
    if (value == "PassFail")
        gradingDisplay = "BASIC - Pass / Fail";
    else if (value == "Score")
        gradingDisplay = "ADVANCED - Score";

    $("#gradingSchemaValue").text(gradingDisplay);
    gradingSchema = value;

    filterAssessments();
}

//When user clicks outside the filter dialog(popup), close the dialog.
window.addEventListener('click', function(e){   
    //If clicked outside the filter box
    if (!document.getElementById('assessmentFilterDialog').contains(e.target)){
        //If clicked outside the 'Add Filter' button
        if (!document.getElementById('addFilterToggleBtn').contains(e.target)) {
            
            if ($('#assessmentFilterDialog').hasClass('active')) { //If the filter dialog is currently open
                filterClose('assessmentFilterDialog');
            }
        }
        //If 'Add Filter' button is clicked, leave the filter dialog open.
    }
});

function hideAllfilters() {
    $('#appliedFilterDiv').find('.filterSelections').hide();
}

function filterAssessments() {
    var filterdata = {
        CreateStartDate: createStartDate,
        CreateEndDate: createEndDate,
        ModifyStartDate: modifyStartDate,
        ModifyEndDate: modifyEndDate,
        Status: status,
        Result: result,
        GradingSchema: gradingSchema,
        SearchString: searchString,
    };

    $.ajax({
        type: "POST",
        url: "AssessmentMonitor/FilterAssessments",
        contentType: "application/json",
        data: JSON.stringify(filterdata),  
        success: function (data) {
            $("#assessmentListBody").html(data);
        },
        error: function (error) {
            alert(error);
        }
    });  
}


//--------------------------------UPDATE ASSESSMENT---------------------------//

function getAssessmentInfo(assessmentID) {
    $.ajax({
        type: "Get",
        url: "AssessmentMonitor/GetAssessmentForm/" + assessmentID,
        data: { "assessmentID" : assessmentID },
        success: function (data) {
            $("#assessmentModalBody").html(data);
            $("#editAssessmentModal .action").text("Edit");
            showModal("editAssessmentModal");

            //State
            var state = $('#state').val();
            if (state != "Closed") { //When record is in 'Active' or 'Deleted' state, it is not editable.
                //Deleted records should have all fields disabled
                $("#assessmentDescription").prop( "disabled", true);
                $("#assessmentName").prop( "disabled", true);
            }
            
            //OverallGrade (Result)
            var overallGrade = $('#overallGrade').val();
            if (!overallGrade) {
                overallGrade = "Not Set";
            } else if(overallGrade == "PartialPass")
                overallGrade = "Partial Pass";

            $("#overallGradeText").val(overallGrade);
            
        },
        error: function (error) {
            alert(error);
        }
    });
}

function validateAssessmentForm(formID) {
    var isvalid = true;
    //Adding # prefix by default, for consistency
    formID = "#" + formID;
    $(formID + " .error").empty();
    //TBD
    var name = $(formID).find('input[name="Assessment.Name"]').val();
    if (name == '') {
        showErrorMsg("#nameError", requiredErrorMessage);
        isvalid = false;
    }

    if (isvalid)
        $(formID).submit();
}
//-------------------------------DELETE ASSESSMENT----------------------------//

//Open the confirmation modal when user clicks on 'Delete' button from index page (main table)
function confirmDeleteAssessment(assessmentID) {
    showModal('deleteModal');
    //add onclick event to the button with the assessmentID
    $('#deleteButton').attr('onClick', 'deleteAssessment("' + assessmentID+ '")');
}

function deleteAssessment(id) {
    var posting = $.post("/AssessmentMonitor/DeleteAssessment/" + id).done(function(){
        //TBD Current bug: the page won't refresh.
        window.location.reload();
    }).fail(function(xhr){
        modalClose('deleteModal'); // now close modal
        showModal('errorAlertDialog');
        $('#errorMessage').html(xhr.responseJSON.message);
    });
    
}  
