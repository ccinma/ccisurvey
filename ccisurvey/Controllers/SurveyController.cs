using ccisurvey.data.Models;
using ccisurvey.services;
using ccisurvey.services.VMs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;
using Microsoft.AspNetCore.Authorization;
using ccisurvey.data.Repositories;
using Microsoft.AspNetCore.Http;

namespace ccisurvey.Controllers
{
	[Authorize]
	public class SurveyController : Controller
	{
		private readonly IUserService _users;
		private readonly ISurveyRepository _srepo;
		private readonly IUserRepository _urepo;
		private readonly IPropositionRepository _prepo;
		public SurveyController(IUserService us, ISurveyRepository srepo, IUserRepository urepo, IPropositionRepository prepo)
		{
			_users = us;
			_srepo = srepo;
			_urepo = urepo;
			_prepo = prepo;
		}
		public async Task<IActionResult> Create()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(CreateSurveyViewModel model)
		{
			var errors = new List<string>();

			if (ModelState.IsValid)
			{
				var claims = HttpContext.User.Claims.ToArray();
				var idClaim = claims[0].Value;
				var id = Int32.Parse((string)idClaim, NumberStyles.Integer);
				var creator = await _users.GetById(id);

				var survey = new Survey()
				{
					Label = model.Label,
					Description = model.Description,
					IsMultipleChoice = model.IsMultipleChoice,
					User = creator,
				};

				var lastId = await _srepo.AddAsync(survey);
				return Redirect($"/survey/addproposition/{lastId}");
				
			} else
			{
				errors.Add("Tous les champs sont obligatoires.");
			}

			ViewData["Errors"] = errors;
			return View();
		}

		public async Task<IActionResult> AddProposition(int id)
		{
			var survey = await _srepo.GetAsync(id);

			ViewData["Survey"] = survey;
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> AddProposition(CreatePropositionViewModel model, int id)
		{
			if (ModelState.IsValid)
			{
				var survey = await _srepo.GetAsync(id);
				var proposition = new Proposition()
				{
					Label = model.Label,
					Survey = survey
				};
				await _prepo.AddAsync(proposition);

				return Redirect($"/survey/addproposition/{id}");
			}
			return View();
		}

		public async Task<IActionResult> View(int id)
		{
			var claims = HttpContext.User.Claims.ToArray();
			var userId = Int32.Parse(claims[0].Value, NumberStyles.Integer);
			var user = await _urepo.GetAsync(userId);

			var survey = await _srepo.GetAsync(id, false);

			if (!survey.Participants.Contains(user) && !survey.User.Equals(user))
			{
				return Redirect("/home");
			}

			var surveyProps = await _prepo.GetAllFromSurvey(id);
			survey.Propositions = surveyProps;

			ViewData["Survey"] = survey;
			ViewData["User"] = user;
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Vote(IFormCollection data, int id)
		{
			var claims = HttpContext.User.Claims.ToArray();
			var userId = Int32.Parse(claims[0].Value, NumberStyles.Integer);
			var user = await _urepo.GetAsync(userId);
			var survey = await _srepo.GetAsync(id);

			if (!survey.Participants.Contains(user) && !survey.User.Equals(user))
			{
				return Redirect("/home");
			}

			var surveyProps = await _prepo.GetAllFromSurvey(id);
			var answers = new List<int>();
			foreach (var answer in data.Keys.SkipLast(1).ToArray())
			{
				answers.Add(Int32.Parse(answer, NumberStyles.Integer));
			}

			foreach (var proposition in surveyProps)
			{
				if (answers.Contains(proposition.Id) && !proposition.Participants.Contains(user))
				{
					proposition.Participants.Add(user);
					await _prepo.UpdateAsync(proposition);
				}
				else
				{
					if (!answers.Contains(proposition.Id) && proposition.Participants.Contains(user))
					{
						proposition.Participants.Remove(user);
						await _prepo.UpdateAsync(proposition);
					}
				}
			}

			return Redirect($"/survey/view/{id}");
		}
	}
}
