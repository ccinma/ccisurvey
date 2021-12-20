using ccisurvey.data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ccisurvey.data.Repositories
{
	internal class PropositionRepository : IPropositionRepository
	{
		private readonly AppDBContext _db;
		public PropositionRepository(AppDBContext db)
		{
			_db = db;
		}


		public async Task AddAsync(Proposition proposition)
		{
			await _db.AddAsync(proposition);
		}

		public async Task<Proposition> GetAsync(int id)
		{
			return await _db.Proposition.FindAsync(id);
		}

		public async Task UpdateAsync(Proposition proposition)
		{
			_db.Proposition.Update(proposition);
			await _db.SaveChangesAsync();
		}
	}
}
