

function clearform(formID) {
    $(':input', formID)
        .not(':button, :submit, :reset, :hidden')
        .val('')
        .prop('checked', false)
        .prop('selected', false);

    modalClose(formID)
}

function showModal(id) {
    $("#" + id).show();
    $(".modal-backdrop").css("display", "block");
    $(".modal-backdrop").toggleClass("hide show");
}

function modalClose(id) {
    $(id).hide();
    $(".modal-backdrop").css("display", "none");
    $(".modal-backdrop").toggleClass("hide show");
}

function showErrorMsg(target, Msg) {
    $(target).text(Msg);
}

