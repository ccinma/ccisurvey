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
		public Task AddAsync(Proposition proposition)
		{
			throw new NotImplementedException();
		}

		public Task<List<Proposition>> GetAllAsync()
		{
			throw new NotImplementedException();
		}

		public Task<Proposition> GetAsync(int id)
		{
			throw new NotImplementedException();
		}

		public Task UpdateAsync(Proposition proposition)
		{
			throw new NotImplementedException();
		}
	}
}
