// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.


// Or with jQuery

$(document).ready(function () {
    $('.carousel.carousel-slider').carousel();
    $('select').formSelect();
    $('.datepicker').pickadate({

        selectMonths: true, // Creates a dropdown to control month
        selectYears: 15 // Creates a dropdown of 15 years to control year
    });
});

$('.carousel.carousel-slider').carousel({
    fullWidth: true,
    indicators: true
});



