$('.money').mask('000.000.000.000.000,00', { reverse: true });
$('.time').mask('00:00');
$('.cpf').mask('000.000.000-00');
$('.rg').mask('00.000.000-0');
$('.cep').mask('00000-00');
$('.celular').mask('(00) 00000-0000');

var SPMaskBehavior = function (val) {
    return val.replace(/\D/g, '').length === 11 ? '(00) 00000-0000' : '(00) 0000-00009';
},
spOptions = {
    onKeyPress: function (val, e, field, options) {
        field.mask(SPMaskBehavior.apply({}, arguments), options);
    }
};

$('.sp_celphones').mask(SPMaskBehavior, spOptions);













