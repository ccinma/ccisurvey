using ccisurvey.data.Models;
using ccisurvey.services.VMs;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ccisurvey.Controllers
{
	public class SurveyController : Controller
	{
		public async Task<IActionResult> Create()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(CreateSurveyViewModel model)
		{
			var errors = new List<string>();

			if (ModelState.IsValid)
			{
				var survey = new Survey()
				{
					Label = model.Label,
					Description = model.Description,
					IsMultipleChoice = model.IsMultipleChoice
				};

				if (survey.Description != null)
				{

				}
			} else
			{
				errors.Add("Tous les champs sont obligatoires.");
			}

			ViewData["Errors"] = errors;
			return View();
		}
	}
}
