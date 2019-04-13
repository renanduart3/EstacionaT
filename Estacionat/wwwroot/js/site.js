// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.


// Or with jQuery

$(document).ready(function () {
    $('.carousel.carousel-slider').carousel();

});

$('.carousel.carousel-slider').carousel({
    fullWidth: true,
    indicators: true
});