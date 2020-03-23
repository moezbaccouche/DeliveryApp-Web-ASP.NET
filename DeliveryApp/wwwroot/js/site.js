// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$("body").on('click', '.ajaxBtn', function (event) {
    var link = $(this).data('link');
    var title = $(this).data('title');

    $('#ajaxModal').remove();

    $.get(link, function (ajaxView) {
        var modalString = '<div class="modal fade in" id="ajaxModal">'
            + '<div class="modal-dialog" >'
            + '<div class="modal-content" >'
            + '<div class="modal-header">'
            + '<button type="button" class="close" data-dismiss="modal" aria-label="Close">'
            + '<span aria-hidden="true">&times;</span>'
            + '</button>'
            + '<h5 class="modal-title">' + title + '</h5>'
            + '</div>'
            + '<div class="modal-body">'
            + ajaxView
            + '</div>'
            + '</div>'
            + '</div>'
            + '</div>';
        $(modalString).modal('show');


    });
});


//Modal for EditProduct
$("body").on('click', '.editBtn', function (event) {
    var link = $(this).data('link');
    var title = $(this).data('title');

    $('#editModal').remove();

    $.get(link, function (ajaxView) {
        var modalString = '<div class="modal fade in" id="editModal">'
            + '<div class="modal-dialog modal-lg" >'
            + '<div class="modal-content" >'
            + '<div class="modal-header">'
            + '<button type="button" class="close" data-dismiss="modal" aria-label="Close">'
            + '<span aria-hidden="true">&times;</span>'
            + '</button>'
            + '<h5 class="modal-title">' + title + '</h5>'
            + '</div>'
            + '<div class="modal-body">'
            + ajaxView
            + '</div>'
            + '</div>'
            + '</div>'
            + '</div>';
        $(modalString).modal('show');


    });
});