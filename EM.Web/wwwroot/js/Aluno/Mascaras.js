function formatarData(campo) {

    let valor = campo.value.replace(/\D/g, '');


    if (valor.length > 2) {
        valor = valor.substring(0, 2) + '/' + valor.substring(2);
    }
    if (valor.length > 5) {
        valor = valor.substring(0, 5) + '/' + valor.substring(5);
    }


    if (valor.length > 10) {
        valor = valor.substring(0, 10);
    }

    campo.value = valor;
}

function formatarCPF(campo) {

    let valor = campo.value.replace(/\D/g, '');


    if (valor.length > 3) {

        valor = valor.substring(0, 3) + '.' + valor.substring(3);
    }
    if (valor.length > 7) {

        valor = valor.substring(0, 7) + '.' + valor.substring(7);
    }
    if (valor.length > 11) {

        valor = valor.substring(0, 11) + '-' + valor.substring(11);
    }


    if (valor.length > 14) {
        valor = valor.substring(0, 14);
    }

    campo.value = valor;
}