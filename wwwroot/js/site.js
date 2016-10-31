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

    $.ajax({
      url: `/Products/GetSubTypes/${$(this).val()}`,
      method: "POST",
      dataType: "json",
      contentType: 'application/json; charset=utf-8'
    }).done((data)=> {

      $('#sub-select').remove();

      var productSelect = $('.form-group:nth-last-child(2)');
      var htmlSelect = ``;

      htmlSelect = `<div class="form-group" id="sub-select">`;
      htmlSelect += `<label class="col-md-2 control-label" for="NewProduct_ProductSubTypeId">Product Sub Type</label>`;
      htmlSelect += `<div class="col-md-10">`;
      htmlSelect += `<select id="NewProduct_ProductSubTypeId" name="NewProduct.ProductSubTypeId">`;
      htmlSelect += `<option value="">Select Product Sub Type</option>`;
      
      data.subTypes.forEach((subType) => {
        htmlSelect += `<option value = "${subType.productSubTypeId}">${subType.label}</option>`;
      });

      htmlSelect += `</select>`;
      htmlSelect += `</div></div>`;

      $(productSelect).after(htmlSelect);

    });
  });
});