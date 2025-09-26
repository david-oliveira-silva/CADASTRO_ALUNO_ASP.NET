
using System.ComponentModel.DataAnnotations;

namespace EM.Domain.Models
    {
        public class Pessoa
        {
        [Required(ErrorMessage ="Digite o nome do aluno")]
        [StringLength(100,MinimumLength = 3,ErrorMessage = "O nome deve conter entre 3 e 100 caracteres.")]
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
