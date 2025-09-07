
    namespace EM.Domain.Models
    {
        public class Pessoa
        {
            public string nome {  get; set; }

            public string CPF {  get; set; }

            public DateOnly dtNascimento { get; set; }

            public Pessoa() { }

            public Pessoa(string nome, string CPF, DateOnly dtNascimento) 
            { 
                this.nome = nome;
                this.CPF = CPF;
                this.dtNascimento = dtNascimento;
            }
        }
    }
