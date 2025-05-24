﻿using AthletesApplication.Service.Implementation;
using AthletesApplication.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AthletesApplication.Web.Controllers
{
    public class TournamentsController : Controller
    {
        private readonly ITournamentService _tournamentsService;

        public TournamentsController(ITournamentService tournamentsService)
        {
            _tournamentsService = tournamentsService;
        }

        // POST: Tournaments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Create()
        {
            // TODO: Implement method
            // 1. Get current user
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            // 2. Call method CreateTournament from _tournamentsService
            var tournament = _tournamentsService.CreateTournament(userId);
            // 3. Redirect to Details
            return RedirectToAction("Details", new { tournament.Id });
        }

        // GET: Tournaments/Details/5
        // Bonus task
        public IActionResult Details(Guid id)
        {
            // TODO: Implement method
            // Call service method, return view with tournament
            var tournament = _tournamentsService.GetTournamentDetails(id);
            return View(tournament);
        }
    }
}
