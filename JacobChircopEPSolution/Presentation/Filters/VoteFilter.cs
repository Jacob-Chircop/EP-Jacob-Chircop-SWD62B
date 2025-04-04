using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace Presentation.Filters
{
    public class VoteFilter : IActionFilter
    {
        private readonly IPollRepository _pollRepository;

        public VoteFilter(IPollRepository pollRepository)
        {
            _pollRepository = pollRepository;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ActionArguments.TryGetValue("pollId", out var pollIdObj) &&
                context.HttpContext.User.Identity?.IsAuthenticated == true)
            {
                int pollId = (int)pollIdObj;
                string userId = context.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (_pollRepository.HasUserVoted(pollId, userId))
                {
                    var controller = (Controller)context.Controller;
                    controller.TempData["AlreadyVoted"] = "You have already voted in this poll.";
                    context.Result = new RedirectToActionResult("PollDetails", "Poll", new { id = pollId });
                }
            }
        }

        public void OnActionExecuted(ActionExecutedContext context) { }
    }
}