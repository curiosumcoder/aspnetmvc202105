$(function () {
  console.log("DOM is ready!");

  $("main > form").submit(function (event) {
    event.preventDefault();
    console.log("Submiting ...");

    const filter = $("#filter").val();

    if (filter !== "") {
      $("#lResults").empty().hide();

      $.getJSON(`api/Product?filter=${filter}`, function (data) {
        console.log(data);

        if (data.length > 0) {
          let items = [];

          $.each(data, function (key, p) {
            console.log(p);
            items.push(`<li class='list-group-item'>
                        <a class='row align-items-center' data-id='${p.ProductID}'>
                        <p class='col-8'>${p.ProductName}</p>
                        <p class='col-3'>$ ${p.UnitPrice}</p>
                        <p class='col-1'><i class="bi bi-chevron-double-right"></i></p>
                    </a></li>`);
          });

          $(items.join("")).appendTo("#lResults");
          
        } else {
          $("<p>¡No se encontraron datos!</p>").appendTo("#lResults");
        }
        $("#lResults").slideDown(600);
      });
    }
  });
});
