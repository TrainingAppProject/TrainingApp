
$(document).ready(function () {

    $('input[name="AssessmentMonitorDateRange"]').daterangepicker({
        opens: 'right',
        autoUpdateInput: false,
        locale: {
            cancelLabel: 'Clear'
        }
    }, function (start, end, label) {

    });
});


$("#addFilterToggleBtn").click(function () {
    $(".filterDropDown").toggleClass("show hide");
});

$('input[name="AssessmentMonitorDateRange"]').on('apply.daterangepicker', function (ev, picker) {
    $(this).val('Created Date: ' + picker.startDate.format('MM.DD.YYYY') + ' ~ ' + picker.endDate.format('MM.DD.YYYY'));
    this.style.width = ((this.value.length + 1) * 7) + 'px';
});

$('input[name="AssessmentMonitorDateRange"]').on('cancel.AssessmentMonitorDateRange', function (ev, picker) {
    $(this).val('');
});


