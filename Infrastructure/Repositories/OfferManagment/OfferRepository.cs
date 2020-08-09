using Domain.Common.Interfaces;
using Domain.OffersManagment.AggregatesModel;
using Microsoft.EntityFrameworkCore;
using Persistence.OfferManagment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.OfferManagment
{
    public class OfferRepository
       : IOfferRepository
    {
        private readonly OfferContext _context;

        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }

        public OfferRepository(OfferContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Offer Add(Offer offer)
        {
            return _context.Offers
                   .Add(offer)
                   .Entity;
        }

        public void Update(Offer offer)
        {
            _context.Entry(offer).State = EntityState.Modified;
        }

        public async Task<(int, List<Offer>)> GetAllAsync(int pageNumber, int pageSize, string keyWord)
        {
            var query = _context.Offers
                .Where(x => x.IsDeleted == false)
                .AsQueryable();

            // fillter by keyword
            if (!string.IsNullOrEmpty(keyWord))
            {
                query = query.Where(x =>
                x.Id.ToString().Contains(keyWord) ||
                x.Name.Contains(keyWord)
                );
            }


            query = query.OrderBy(x => x.Order);

            // apply pagination to offers
            var offers = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            var count = query.Count();

            return (count, offers);
        }

        public async Task<Offer> FindByIdAsync(string id)
        {
            return await _context.Offers
                .Where(x => x.IsDeleted == false)
                .Include(x => x.Products)
                .ThenInclude(x => x.Units)
                   .FirstOrDefaultAsync(x => x.Id.ToString() == id);
        }

        public void Delete(Offer offer)
        {
            _context.Offers.Remove(offer);
        }

        public void UpdateAll(List<Offer> offers)
        {
            _context.Offers.UpdateRange(offers);
        }

        public async Task<List<Offer>> FindByIdsAsync(List<string> ids)
        {
            var offers = _context.Offers.Where(x => ids.Contains(x.Id.ToString()));
            return await offers.ToListAsync();
        }
    }
}
