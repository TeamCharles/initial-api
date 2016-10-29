// Write your Javascript code.
$(document).ready(function () {
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

  $("#NewProduct_ProductTypeId").on("change", function () {

    var formData = {
      name: $('#NewProduct_Name').val(),
      description: $('#NewProduct_Description').val(),
      price: parseFloat($('#NewProduct_Price').val())   
    };

    console.log($('#NewProduct_Price').val())

    $.ajax({
      url: `/Products/GetSubTypes/${$(this).val()}`,
      method: "POST",
      dataType: "json",
      data: JSON.stringify(formData),
      contentType: 'application/json; charset=utf-8'
    });
  });
});