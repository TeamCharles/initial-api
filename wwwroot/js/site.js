// Write your Javascript code.
$(document).ready(function () {

  // update user selected on server
  $("#ChosenUser_UserId").on("change", function () {
    $.ajax({
      url: `/User/Activate/${$(this).val()}`,
      method: "POST",
      dataType: "json",
      contentType: 'application/json; charset=utf-8'
    }).done(()=> {
        location.replace("/");
    });
  });

  // update subtype for product on server
  $("#NewProduct_ProductTypeId").on("change", function () {
    if ($("#NewProduct_ProductTypeId").val() > 0) {
      $('#NewProduct_ProductSubTypeId').val(0);
      this.form.submit();
    }
  });

  // update subtype for product on server
  $("#CurrentProduct_ProductTypeId").on("change", function () {
    if ($("#CurrentProduct_ProductTypeId").val() > 0) {
      $('#CurrentProduct_ProductSubTypeId').val(0);
      this.form.submit();
    }
  });

});