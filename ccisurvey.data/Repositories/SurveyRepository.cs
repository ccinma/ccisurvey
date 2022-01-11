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

		public async Task<int> AddAsync(Survey survey)
		{
			await _db.Survey.AddAsync(survey);
			await _db.SaveChangesAsync();
			
			int lastId = _db.Survey.Max(survey => survey.Id);
			return lastId;
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
			return await _db.Survey
				.Include(s => s.User)
				.Include(s => s.Propositions)
				.ThenInclude(p => p.Participants)
				.FirstOrDefaultAsync(s => s.Id == id);
		}

		public async Task<List<Survey>> GetAllByUserAsync(User user)
		{
			var mail = user.Email;
			return await _db.Survey
				.Where(
					s => s.User == user 
					|| s.Participants.Contains(mail)
				)
				.Include(s => s.User)
				.ToListAsync();
		}

		public async Task UpdateAsync(Survey survey)
		{
			_db.Survey.Update(survey);
			await _db.SaveChangesAsync();
			return;
		}
	}
}
