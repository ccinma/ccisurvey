using ccisurvey.data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ccisurvey.data.Repositories
{
	public class SurveyRepository : ISurveyRepository
	{
		private AppDBContext _db;
		public SurveyRepository(AppDBContext db)
		{
			_db = db;
		}

		public async Task AddAsync(Survey survey)
		{
			await _db.Survey.AddAsync(survey);
			return;
		}

		public async Task DeleteAsync(int id)
		{
			Survey survey = await _db.Survey.FindAsync(id);
			_db.Survey.Remove(survey);
			await _db.SaveChangesAsync();
			return;
		}

		public async Task<List<Survey>> GetAllAsync()
		{
			return await _db.Survey
				.Include(survey => survey.Propositions)
				.ToListAsync();
		}

		public async Task<Survey> GetAsync(int id)
		{
			return await _db.Survey.FindAsync(id);
		}

		public async Task UpdateAsync(Survey survey)
		{
			_db.Survey.Update(survey);
			await _db.SaveChangesAsync();
			return;
		}
	}
}
