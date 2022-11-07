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
    showModal("createTemplateModal");
});

$("#addFilterToggleBtn").click(function () {
    $(".filterDropDown").toggleClass("show hide");
});

$('input[name="templateDateRange"]').on('apply.daterangepicker', function (ev, picker) {
    $(this).val('Created Date: ' + picker.startDate.format('MM.DD.YYYY') + ' ~ ' + picker.endDate.format('MM.DD.YYYY'));
    this.style.width = ((this.value.length + 1) * 7) + 'px';
});

$('input[name="templateDateRange"]').on('cancel.daterangepicker', function (ev, picker) {
    $(this).val('');
});


