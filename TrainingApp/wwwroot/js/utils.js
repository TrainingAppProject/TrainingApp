/// <summary>
/// Module purpose: Commonly used javascript functions within the system
/// Authors: Hansol Lee / Jei Yang
/// Date: Oct 26, 2022
/// Source: Created for the COMP7022 project 
/// Revision History:
///
/// </summary>

function clearform(formID) {
    $(':input', formID)
        .not(':button, :submit, :reset, :hidden')
        .val('')
        .prop('checked', false)
        .prop('selected', false);

    modalClose(formID)
}

function showModal(id) {
    //Adding # prefix by default, for consistency
    $('#' + id).show();
    $(".modal-backdrop").css("display", "block");
    $(".modal-backdrop").toggleClass("hide show");
}

function modalClose(id) {
    //Adding # prefix by default, for consistency
    $('#' + id).hide();
    $(".modal-backdrop").css("display", "none");
    $(".modal-backdrop").toggleClass("hide show");
}

function showErrorMsg(target, Msg) {
    $(target).text(Msg);
}

function showFilter(id) {
    //Adding # prefix by default, for consistency
    id = '#' + id;
    $(id).show();
    $(id).addClass('active');
}

function filterClose(id) {
    //Adding # prefix by default, for consistency
    id = '#' + id;
    $(id).hide();
    $(id).remove('active');
}

function showloader() {
    $('.preloader').show();
}

function hideloader() {
    $('.preloader').hide();
}


//-----------------------------------PAGINATIN----------------------------------//

function pagination(numOfRows) {
    $('.table').after('<nav id="paginationNav" style="display: flex; overflow: hidden; justify-content:space-between;" aria-label="Page navigation"><div id="paginationMsg" class="paginationMsg"></div>'
         + '<div class="pagination" id="pagination"></div></nav>');
    var rowsShown = 5;
    //If numOfRows is given, set the number of rows shown in one page to the given value.
    if(numOfRows) {
        rowsShown = numOfRows;
    }
    var rowsTotal = $('.table tbody tr').length;
    var numPages = Math.ceil(rowsTotal / rowsShown);
    var numOfRowsOnFirstPage = (rowsShown > rowsTotal) ? rowsTotal : rowsShown;
    $("#paginationMsg").html('<label style="color: #666; padding: 5px;">Showing data 1 to ' + numOfRowsOnFirstPage + ' of ' + rowsTotal + ' entries</label>');
    
    for (i = 0; i < numPages; i++) {
        var pageNum = i + 1;
        $('#pagination').append('<li class="page-item"><a class="page-link" rel="' + i + '"href="#">' + pageNum + '</a></li>');
    }
    $('.table tbody tr').hide();
    $('.table tbody tr').slice(0, rowsShown).show();
    //Testing
    //$('#pagination').append('<li class="page-item"><a class="page-link" rel="' + 2 + '"href="#">' + 3 + '</a></li>');
    //$('#pagination').append('<li class="page-item"><a class="page-link" rel="' + 3 + '"href="#">' + 4 + '</a></li>');
    $('#pagination a:first').addClass('active');
    $('#pagination a').bind('click', function () {
        $('#pagination a').removeClass('active');
        $(this).addClass('active');
        var currPage = $(this).attr('rel');
        var startItem = currPage * rowsShown;
        var endItem = startItem + rowsShown;

        if (endItem > rowsTotal)
            endItem = rowsTotal;

        $('.table tbody tr').css('opacity', '0.0').hide().slice(startItem, endItem).
            css('display', 'table-row').animate({ opacity: 1 }, 300);
        var navDisplayMessage = '<label style="color: #666; padding: 5px;">Showing data ' + (startItem+1) + ' to ' + endItem + ' of ' + rowsTotal + ' entries</label>';
        $("#paginationMsg").html(navDisplayMessage);
    });
    
}

