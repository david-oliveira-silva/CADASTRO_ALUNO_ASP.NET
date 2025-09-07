
    namespace EM.Domain.Models
    {
        public class Pessoa
        {
            public string nome {  get; set; }

            public int CPF {  get; set; }

            public DateOnly dtNascimento { get; set; }

            public Pessoa() { }

            public Pessoa(string nome, int CPF, DateOnly dtNascimento) 
            { 
                this.nome = nome;
                this.CPF = CPF;
                this.dtNascimento = dtNascimento;
            }
        }
    }
