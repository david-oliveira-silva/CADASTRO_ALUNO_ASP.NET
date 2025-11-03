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