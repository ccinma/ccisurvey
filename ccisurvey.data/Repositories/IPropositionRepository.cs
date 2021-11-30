using ccisurvey.data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ccisurvey.data.Repositories
{
	public interface IPropositionRepository
	{
		Task<List<Proposition>> GetAllAsync();
		Task<Proposition> GetAsync(int id);
		Task AddAsync(Proposition proposition);
		//Task DeleteAsync(int id);
		Task UpdateAsync(Proposition proposition);
	}
}
