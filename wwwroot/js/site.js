// Write your Javascript code.
$(document).ready(function () {
  $("#ChosenUser_UserId").on("change", function (e) {
    $.ajax({
      url: `/User/Activate/${$(this).val()}`,
      method: "POST",
      dataType: "json",
      contentType: 'application/json; charset=utf-8'
    }).done(() => {
      location.reload();
    });
  });
});