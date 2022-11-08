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

