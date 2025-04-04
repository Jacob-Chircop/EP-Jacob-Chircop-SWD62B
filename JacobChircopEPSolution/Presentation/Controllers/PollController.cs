﻿using DataAccess.Repositories;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class PollController : Controller
    {
        public IActionResult CreatePoll()
        { 
            return View();
        }
        [HttpPost]
        public IActionResult CreatePoll(Poll newPoll, [FromServices] PollRepository pollRepository)
        {
            
            newPoll.Option1VotesCount = 0;
            newPoll.Option2VotesCount = 0;
            newPoll.Option3VotesCount = 0;
            newPoll.DateCreated = DateTime.Now;

            pollRepository.CreatePoll(newPoll);
            return View();
        }
    }
}
