using ccisurvey.data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ccisurvey.data.Repositories
{
	public interface ISurveyRepository
	{
		Task<List<Survey>> GetAllAsync();
		Task<Survey> GetAsync(int id, bool withProps = true);
		Task<int> AddAsync(Survey survey);
		Task DeleteAsync(int id);
		Task UpdateAsync(Survey survey);
	}
}
