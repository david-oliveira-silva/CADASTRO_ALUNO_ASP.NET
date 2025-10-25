function configurarBusca(tipo) {
    const inputBusca = document.getElementById('termoBuscaInput');

    if (tipo === 'Matricula') {
        inputBusca.placeholder = 'Pesquisar aluno pela matrícula...';
        inputBusca.name = 'matricula'; 
        inputBusca.type = 'number';
    } else if (tipo === 'Nome') {
        inputBusca.placeholder = 'Pesquisar aluno por Nome...';
        inputBusca.name = 'alunoNome'; 
        inputBusca.type = 'text';
    }
    inputBusca.value = ''; 
}

document.addEventListener('DOMContentLoaded', (event) => {
    configurarBusca('Matricula');
});