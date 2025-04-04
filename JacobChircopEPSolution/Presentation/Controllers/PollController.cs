using DataAccess.Repositories;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class PollController : Controller
    {
        public IActionResult Index([FromServices] IPollRepository pollRepository)
        {
            var polls = pollRepository.GetPolls().OrderByDescending(p => p.DateCreated);
            return View(polls);
        }

        public IActionResult PollDetails(int id, [FromServices] IPollRepository pollRepository)
        {
            var poll = pollRepository.GetPollById(id);
            
            return View(poll);
        }

        public IActionResult CreatePoll()
        { 
            return View();
        }

        [HttpPost]
        public IActionResult CreatePoll(Poll newPoll, [FromServices] IPollRepository pollRepository)
        {

            newPoll.Option1VotesCount = 0;
            newPoll.Option2VotesCount = 0;
            newPoll.Option3VotesCount = 0;
            newPoll.DateCreated = DateTime.Now;

            pollRepository.CreatePoll(newPoll);
            return RedirectToAction("Index");
        }

        public IActionResult Vote(int pollId, int optionNumber, [FromServices] IPollRepository pollRepository)
        {
            pollRepository.Vote(pollId, optionNumber);
            return RedirectToAction("PollDetails", new { id = pollId });
        }
    }
}
