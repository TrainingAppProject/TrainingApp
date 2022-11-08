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

    $('input[name="templateDateRange"]').daterangepicker({
        opens: 'right',
        autoUpdateInput: false,
        locale: {
            cancelLabel: 'Clear'
        }
    }, function (start, end, label) {
        
    });
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