using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#pragma warning disable
namespace WebApplication7.Models
{
    [Table("AgendaEntity")]
    public class AgendaEntity
    {
        [Key]
	    public int AgendaEntityId { get; set; }

        public string Nome { get; set; }

        public string Telefone { get; set; }

        public bool Ativo { get; set; }

        public AgendaEntity()
        #region MyRegion
        {
            this.Nome = string.Empty;
            this.Telefone = string.Empty;
            this.Ativo = true;
            this.AgendaEntityId = 0;
        } 
        #endregion
    }
}
