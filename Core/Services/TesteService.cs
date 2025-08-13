using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using WebApplication7.Core.Commons;
using WebApplication7.Core.Core.Context;
using WebApplication7.Core.Core.Uow;
using WebApplication7.Models;

#pragma warning disable
namespace WebApplication7.Core.Services
{
    public class TesteService : TesteServiceUow
    {
        public TesteService(PgsqlContext _context, IDistributedCache _cache) : base(_context, _cache) { }

        public override List<AgendaEntity> getLista()
        #region MyRegion
        {
            var cached = DistributedCacheHelper.getData(this.cache, "TesteService__getLista__", new AgendaEntity());

            if (cached != null)
            {
                return JsonConvert.DeserializeObject<List<AgendaEntity>>((string)cached);
            }

            List<AgendaEntity> listaCompleta = this.context.Agenda.Where(c => (c.Ativo == true)).ToList();
            DistributedCacheHelper.Store(this.cache, "TesteService__getLista__", new AgendaEntity(), 5, listaCompleta);

            return listaCompleta;
        }
        #endregion

        public override void setLista()
        #region MyRegion
        {
            ResponseListaContatos model = 
                (ResponseListaContatos)this.dataModel;

            this.context.Database.BeginTransaction();

            AgendaEntity agendaEntity = new AgendaEntity();
            agendaEntity.Nome = model.Nome;
            agendaEntity.Telefone = model.Telefone;

            this.context.Add(agendaEntity);
            this.context.SaveChanges();

            this.context.Database.CommitTransaction();

            List<AgendaEntity> listaCompleta = this.context.Agenda.Where(c => (c.Ativo == true)).ToList();
            DistributedCacheHelper.Store(this.cache, "TesteService__getLista__", new AgendaEntity(), 5, listaCompleta);
        }
        #endregion
    }
}
