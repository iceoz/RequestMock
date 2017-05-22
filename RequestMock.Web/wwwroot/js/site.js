// Write your Javascript code.
$(function () {
    chamaraPlugins();
    montarBinds();
    
});

function chamaraPlugins() {
    $(".chosen-select").chosen({ no_results_text: "nenhum resultado encontrado" });
}

function montarBinds() {
    $('body').on('click', '.btn-show-headers', trocarVisibilidadeElementosHeaders);    
    $('body').on('click', '.btn-hide-headers', trocarVisibilidadeElementosHeaders);
    $('body').on('click', '#requestheaders a', addHeader);
}

function addHeader() {
    $('#requestheaders a').remove();
    var headerInput = '<div class="col-sm-offset-3 col-md-offset-2 col-lg-offset-2 col-xs-12 col-sm-9 col-md-10 col-lg-8">' +
        '<input type="text" name="headernames[]" class="header-titulo form-control" />' +
        '<span> : </span>' +
        '<input type="text" name="headervalues[]" class="header-valor form-control" />' +
        ' <a href="#" class="btn btn-success btn-sm" type="button"><i class="glyphicon glyphicon-plus"></i></a>' +
        '</div>';
    $('#requestheaders').append(headerInput);
}

function trocarVisibilidadeElementosHeaders(){
    $('.btn-hide-headers').toggleClass('hidden');
    $('.btn-show-headers').toggleClass('hidden');
    $('#requestheaders').toggleClass('hidden');
}

