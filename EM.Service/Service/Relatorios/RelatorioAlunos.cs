using EM.Domain.Models;
using iTextSharp5.text;
using iTextSharp5.text.pdf;
using System.Globalization;


namespace EM.Service.Service.Relatorios
{
    public  class RelatorioAlunos
    {
        public class PdfGenerator
        {

            public PdfGenerator()
            {
               
            }
            public byte[] GerarRelatorioAlunos(List<AlunoModel> alunos)
            {
            

           
                
                using (var ms = new MemoryStream())
                {


               
                    var documento = new Document(PageSize.A4.Rotate(), 20, 20, 20, 20);

                    PdfWriter.GetInstance(documento, ms);
                    

                    documento.Open();
                    

                    

                    var fontTitulo = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 20, BaseColor.BLACK);
                    var fontCabecalho = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10, BaseColor.WHITE);
                    var fontCorpo = FontFactory.GetFont(FontFactory.HELVETICA, 10, BaseColor.BLACK);
                    var fontData = FontFactory.GetFont(FontFactory.HELVETICA, 10, BaseColor.BLACK);


                    var titulo = new Paragraph("RELATÓRIO DE ALUNOS CADASTRADOS", fontTitulo);
                    titulo.Alignment = Element.ALIGN_CENTER;
                    documento.Add(titulo);

                    documento.Add(new Paragraph(" "));

                   
                    var tabela = new PdfPTable(6)
                    {
                        WidthPercentage = 100 
                    };

                    
                    float[] larguras = new float[] { 0.8f, 4f, 1.1f, 0.9f, 1.2f, 1.8f };
                    tabela.SetWidths(larguras);

                    // --- Cabeçalho da Tabela ---
                    AdicionarCelula(tabela, "Matrícula", fontCabecalho, BaseColor.DARK_GRAY);
                    AdicionarCelula(tabela, "Nome", fontCabecalho, BaseColor.DARK_GRAY);
                    AdicionarCelula(tabela, "CPF", fontCabecalho, BaseColor.DARK_GRAY);
                    AdicionarCelula(tabela, "Sexo", fontCabecalho, BaseColor.DARK_GRAY);
                    AdicionarCelula(tabela, "Nascimento", fontCabecalho, BaseColor.DARK_GRAY);
                    AdicionarCelula(tabela, "Cidade", fontCabecalho, BaseColor.DARK_GRAY);
                   

                    // --- Linhas de Dados ---
                    foreach (var aluno in alunos)
                    {
                        AdicionarCelula(tabela, aluno.matricula.ToString(), fontCorpo);
                        AdicionarCelula(tabela, aluno.nome, fontCorpo,null, Element.ALIGN_LEFT);
                        AdicionarCelula(tabela, aluno.CPF, fontCorpo  );
                        AdicionarCelula(tabela, aluno.sexo.ToString(), fontCorpo); 
                        AdicionarCelula(tabela, aluno.dtNascimento?.ToString("dd/MM/yyyy"), fontCorpo);
                        AdicionarCelula(tabela, $"{aluno.cidade?.cidadeNome} ({aluno.cidade?.cidadeUF})", fontCorpo );
                        
                    }

                    //--- Data Atual ---

                    CultureInfo culturaBrasileira = new CultureInfo("pt-BR");
                    DateTime hoje = DateTime.Now;
                    string dataPorExtenso = $"Goiânia, {hoje.Day} de {hoje.ToString("MMMM", culturaBrasileira)} de {hoje.Year}";
                    var paragrafoLocalData = new Paragraph(dataPorExtenso, fontData) 
                    {
                        Alignment = Element.ALIGN_RIGHT, 
                        SpacingBefore = 20f 
                    };
            
                    documento.Add(tabela);
                    documento.Add(paragrafoLocalData);

                    documento.Close();

                    return ms.ToArray();



                }

                


            }

            
            private void AdicionarCelula(PdfPTable tabela, string texto, Font fonte, BaseColor corFundo = null, int alinhamento = Element.ALIGN_CENTER)
            {
                var cell = new PdfPCell(new Phrase(texto, fonte));
                cell.HorizontalAlignment = alinhamento;
                cell.Padding = 5;
               
                if (corFundo != null)
                {
                    cell.BackgroundColor = corFundo;
                }
                tabela.AddCell(cell);
            }
 
        }
    }
}

