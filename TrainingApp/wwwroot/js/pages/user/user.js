
$(document).ready(function () {

    $(".roleCheckbox input[type=checkbox]").change(function () {
        if ($(this).prop("checked")) {
            console.log('here');
            $("#rolesCheckboxInput").val();
        } else {
            $("#rolesCheckboxInput").val();
        }
    });

});

$("#createUserModalBtn").click(function () {
    showModal("createUserModal");
});

function validateUserForm(formID) {
    var isvalid = true;
    $(formID + " .error").empty();

    if ($(formID).find('input[name="User.FirstName"]').val() == '') {
        showErrorMsg("#firstNameError", requiredErrorMessage);
        isvalid = false;
    }

    if ($(formID).find('input[name="User.LastName"]').val() == '') {
        showErrorMsg("#lastNameError", requiredErrorMessage);
        isvalid = false;
    }

    if ($(formID).find('input[name="User.UserName"]').val() == '') {
        showErrorMsg("#userNameError", requiredErrorMessage);
        isvalid = false;
    }

    //set checkbox value
    var roles = $('.roleCheckbox:checkbox:checked').map(function () {
        return this.value;
    }).get();
    if (roles.length <= 0) {
        showErrorMsg("#rolesError", selectionErrorMessage);
        isvalid = false;
    }
    else
        $("#rolesCheckboxInput").val(roles);

    if (isvalid)
        $(formID).submit();
}




