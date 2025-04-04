using DataAccess.Repositories;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class PollController : Controller
    {
        /*public IActionResult Index([FromServices] PollRepository pollRepository)
        {
            var polls = pollRepository.GetPolls().OrderByDescending(p => p.DateCreated);
            //var polls = pollRepository.GetPolls();
            return View(polls);
        }*/

        public IActionResult Index([FromServices] PollFileRepository pollRepository)
        {
            var polls = pollRepository.GetPolls().OrderByDescending(p => p.DateCreated);
            //var polls = pollRepository.GetPolls();
            return View(polls);
        }

        public IActionResult PollDetails(int id, [FromServices] PollRepository pollRepository)
        {
            var poll = pollRepository.GetPollById(id);
            
            return View(poll);
        }

        public IActionResult CreatePoll()
        { 
            return View();
        }

        [HttpPost]
        /*public IActionResult CreatePoll(Poll newPoll, [FromServices] PollRepository pollRepository)
        {
            
            newPoll.Option1VotesCount = 0;
            newPoll.Option2VotesCount = 0;
            newPoll.Option3VotesCount = 0;
            newPoll.DateCreated = DateTime.Now;

            pollRepository.CreatePoll(newPoll);
            return RedirectToAction("Index");
        }*/

        public IActionResult CreatePoll(Poll newPoll, [FromServices] PollFileRepository pollRepository)
        {

            newPoll.Option1VotesCount = 0;
            newPoll.Option2VotesCount = 0;
            newPoll.Option3VotesCount = 0;
            newPoll.DateCreated = DateTime.Now;

            pollRepository.CreatePoll(newPoll);
            return RedirectToAction("Index");
        }

        public IActionResult Vote(int pollId, int optionNumber, [FromServices] PollRepository pollRepository)
        {
            pollRepository.Vote(pollId, optionNumber);
            return RedirectToAction("PollDetails", new { id = pollId });
        }
    }
}
