using ccisurvey.data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ccisurvey.data.Repositories
{
	public class PropositionRepository : IPropositionRepository
	{
		private readonly AppDBContext _db;
		public PropositionRepository(AppDBContext db)
		{
			_db = db;
		}


		public async Task AddAsync(Proposition proposition)
		{
			await _db.Proposition.AddAsync(proposition);
			var survey = await _db.Survey.FindAsync(proposition.Survey.Id);
			survey.Propositions.Add(proposition);
			_db.Survey.Update(survey);
			await _db.SaveChangesAsync();
			return;
		}

		public async Task<List<Proposition>> GetAllFromSurvey(int id)
		{
			return await _db.Proposition.Include(p => p.Participants).Where(p => p.Survey.Id == id).ToListAsync();
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
