
using System.ComponentModel.DataAnnotations;
using EM.Domain.Extensions.EM.Domain.Extensions;

namespace EM.Domain.Models
    {
        public class Pessoa
        {
        [Required(ErrorMessage ="Digite o nome do aluno")]
        [StringLength(100,MinimumLength = 3,ErrorMessage = "O nome deve conter entre 3 e 100 caracteres.")]
            public string nome {  get; set; }

        [ValidacaoCPFAtributo(ErrorMessage = "O número de CPF não é válido.")]
        public string ?CPF {  get; set; }

        [Required(ErrorMessage = "A data de nascimento é obrigatória.")]
        public DateOnly ?dtNascimento { get; set; }

            public Pessoa() {
          dtNascimento = null;
        }

            public Pessoa(string nome, string CPF, DateOnly? dtNascimento) 
            { 
                this.nome = nome;
                this.CPF = CPF;
                this.dtNascimento = dtNascimento;
            }
        }
    }
