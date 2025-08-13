using Microsoft.Extensions.Caching.Distributed;
using WebApplication7.Core.Core.Context;
using WebApplication7.Models;

#pragma warning disable
namespace WebApplication7.Core.Core.Uow
{
    public abstract class TesteServiceUow
    {
        protected PgsqlContext context { get; set; }
        protected IDistributedCache cache { get; set; }

        public TesteServiceUow(PgsqlContext _context, IDistributedCache _cache)
        #region MyRegion
        {
            this.context = _context;
            this.cache = _cache;
        } 
        #endregion


        protected object dataModel { get; set; }

        public abstract List<AgendaEntity> getLista();

        public virtual void setParam(object param)
        #region MyRegion
        {
            this.dataModel = param;
        }
        #endregion

        public abstract void setLista();
    }
}
