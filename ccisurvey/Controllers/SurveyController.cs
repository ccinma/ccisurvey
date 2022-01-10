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


		[HttpGet]
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


		[HttpGet]
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
				if (survey == null)
				{
					ViewData["ErrorMessage"] = "Page non trouvée.";
					ViewData["ErrorCode"] = "404";

					return View("Error");
				}

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


		[HttpGet]
		public async Task<IActionResult> View(int id)
		{
			var claims = HttpContext.User.Claims.ToArray();
			var userId = Int32.Parse(claims[0].Value, NumberStyles.Integer);
			var user = await _urepo.GetAsync(userId);

			var survey = await _srepo.GetAsync(id);

			if (survey == null)
			{
				ViewData["ErrorMessage"] = "Page non trouvée.";
				ViewData["ErrorCode"] = "404";

				return View("Error");
			}

			if (!survey.Participants.Contains(user) && !survey.User.Equals(user))
			{
				return Redirect("/home");
			}

			ViewData["Survey"] = survey;
			ViewData["User"] = user;
			return View();
		}


		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Vote(IFormCollection data, ChoiceRadioViewModel model, int id)
		{

			/*  ____________________________________________________
			 * |													|
			 * | THIS PART IS ABOUT INITIALIZING USER / SURVEY		|
			 * | AND AUTHORIZE USER TO INTERACT WITH THE ANSWERS	|
			 * | IF IN PARTICPANTS LIST OR IS THE CREATOR			|
			 * |____________________________________________________|
			 */
			var claims = HttpContext.User.Claims.ToArray();
			var userId = Int32.Parse(claims[0].Value, NumberStyles.Integer);
			var user = await _urepo.GetAsync(userId);

			var survey = await _srepo.GetAsync(id);

			if (survey == null)
			{
				ViewData["ErrorMessage"] = "Page non trouvée.";
				ViewData["ErrorCode"] = "404";

				return View("Error");
			}

			if (!survey.Participants.Contains(user) && !survey.User.Equals(user))
			{
				return Redirect("/home");
			}


			/*  ____________________________________________________
			 * |													|
			 * | THIS PART DETERMINES HOW THE ANSWERS ARE TREATED	|
			 * | DEPENDING IF IT'S A SIMPLE OR MULTIPLE CHOICE		|
			 * | QUESTION											|
			 * |____________________________________________________|
			 */
			var surveyProps = await _prepo.GetAllFromSurvey(id);
			var answers = new List<int>();

			if (survey.IsMultipleChoice)
			{
				var formData = data.Keys.SkipLast(1).ToArray();
				foreach (var answer in formData)
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
			} else
			{
				if (model.RadioField == null)
				{
					return Redirect($"/survey/view/{id}");
				}

				int answer = -1;
				if (model.RadioField != "no-choice")
				{
					try
					{
						answer = Int32.Parse(model.RadioField, NumberStyles.Integer);
					} catch {
						return Redirect($"/survey/view/{id}");
					}
				}

				foreach (var proposition in surveyProps)
				{
					if (model.RadioField == "no-choice")
					{
						if (proposition.Participants.Contains(user))
						{
							proposition.Participants.Remove(user);
							await _prepo.UpdateAsync(proposition);
						}
					} else
					{
						if (proposition.Id == answer)
						{
							if (!proposition.Participants.Contains(user))
							{
								proposition.Participants.Add(user);
								await _prepo.UpdateAsync(proposition);
							}
						} else
						{
							proposition.Participants.Remove(user);
							await _prepo.UpdateAsync(proposition);
						}
					}
				}
			}

			return Redirect($"/survey/view/{id}");
		}

	}
}
