using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Interfaces
{
    public interface IPollRepository
    {
        IQueryable<Poll> GetPolls();
        Poll GetPollById(int id);
        void Vote(int pollId, int optionNumber, string userId);
        void CreatePoll(Poll poll);
        bool HasUserVoted(int pollId, string userId);
    }
}
