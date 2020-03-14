// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
    $(".guess_box").click(function () {
        $(".guess_box p").remove();
        var discount = Math.floor(Math.random() * 5 + 5);
        var discount_msg = "<p>You have a discount of " + discount + " </p>"
        $(this).append(discount_msg);
    });
});