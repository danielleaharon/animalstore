// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
h = function (e) {
    if ($(this).val().length > 2) {
        var from = $('#mySearch');
        var url = from.attr('action');
        $.ajax(
            {
                url: url,
                data: from.serialize(),
                success: function (res) {
                    $('body').html(res);

                }
            }

        );
    }
}
h2 = function (e) {
    if ($(this).val().length > 2) {
        var from = $('#mySearch');
        var url = from.attr('action');
        $.ajax(
            {
                url: url,
                data: from.serialize(),
                success: function (res) {
                    $('body').html(res);

                }
            }

        );
    }
}

function like() {

    var from = $(this);
        var url = from.attr('asp-action');
        $.ajax(
            {
                type:post,
                url: url,
                data: from.serialize(),
                success: function (res) {
                    $('body').html(res);

                }
            }

        );
    }



$(function () {
  
        $('#name,#Location').keyup(h);
    

});







//$(document).ready(function  () {

//    // Specific code for the heart fill toggle
//    $("#heart--liked").click(function (e) {
//        $(this).toggleClass("far").toggleClass("fas"); // Toggle the filling !
//    });

//    // Action when click on a link (color)
//    $(".product__list__item--icons a").click(function (e) {
//        e.preventDefault(); // Modified: stop link # from loading (Why using link then?)
//        $(this).toggleClass("selected"); // Toggle the colored class !
//    });

//});