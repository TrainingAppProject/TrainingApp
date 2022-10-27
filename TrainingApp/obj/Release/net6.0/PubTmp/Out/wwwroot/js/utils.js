

function showModal(id) {
    $("#" + id).show();
    $(".modal-backdrop").css("display", "block");
    $(".modal-backdrop").toggleClass("hide show");
}

function modalClose(id) {
    $("#" + id).hide();
    $(".modal-backdrop").css("display", "none");
    $(".modal-backdrop").toggleClass("hide show");
}