/*
/// <summary>
/// Module purpose: javascript to define the frontend behaviour of User view
/// Authors: Hansol Lee / Jei Yang
/// Date: Oct 26, 2022
/// Source: Created for COMP7022 project
/// Revision History:
///     
///     
/// </summary>
*/
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


function getUserInfo(id) {
    var modalTitle = "";
    $.ajax({
        type: "Get",
        url: "User/GetUserForm/" + id,
        success: function (data) {
            $("#userModalBody").html(data);

            if (id == "0")
                modalTitle = "Create";
            else
                modalTitle = "Edit";

            $("#createUserModal .action").text(modalTitle);
            showModal("createUserModal");
        },
        error: function (error) {
            alert(error);
        }
    });
}

function getDeleteUserInfo(id) {
    var modalname = "deleteBasicDefaultModal";

    $.ajax({
        type: "Get",
        url: "User/GetDeleteUserForm/" + id,
        success: function (data) {
            $("#" + modalname + " #modalBody").html(data);
            $("#" + modalname + " .action").text("User");
            showModal(modalname);
        },
        error: function (error) {
            alert(error);
        }
    });
}

function submitDelete() {
    $("#deleteBasicDefaultModal #deleteForm").submit();
}


function validateUserForm(formID) {
    var isvalid = true;
    //Adding # prefix by default, for consistency
    formID = "#" + formID;
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



