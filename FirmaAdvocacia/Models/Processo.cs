using System.ComponentModel.DataAnnotations;


namespace FirmaAdvocacia.Models
{
    public class Processo
    {
        public int ProcessoId { get; set; }
        public string Tipo { get; set; }
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }
        [Display(Name = "Abertura do Processo")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DataAbertura { get; set; }
        public ICollection<ClienteProcesso> ClientesProcessos { get; set; } = new List<ClienteProcesso>();
        public ICollection<AdvogadoProcesso> AdvogadosProcessos { get; set; } = new List<AdvogadoProcesso>();


    }
}
