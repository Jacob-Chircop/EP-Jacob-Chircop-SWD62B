using DataAccess.DataContext;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class PollRepository
    {
        private PollDbContext _context;

        public PollRepository(PollDbContext context)
        {
            _context = context;
        }

        public IQueryable<Poll> GetPolls()
        {
            return _context.Polls;
        }

        public Poll GetPollById(int id)
        {
            return _context.Polls.FirstOrDefault(p => p.Id == id);
        }

        public void Vote(int pollId, int optionNumber)
        {
            var poll = _context.Polls.FirstOrDefault(p => p.Id == pollId);
            if (poll != null)
            {
                switch (optionNumber)
                {
                    case 1:
                        poll.Option1VotesCount++;
                        break;
                    case 2:
                        poll.Option2VotesCount++;
                        break;
                    case 3:
                        poll.Option3VotesCount++;
                        break;
                }
                _context.SaveChanges();
            }
        }

        public void CreatePoll(Poll poll)
        {
            _context.Polls.Add(poll);
            _context.SaveChanges();
        }
    }
}
