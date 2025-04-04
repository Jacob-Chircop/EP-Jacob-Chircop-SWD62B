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
        void Vote(int pollId, int optionNumber);
        void CreatePoll(Poll poll);
    }
}
