using Domain.Interfaces;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class PollFileRepository : IPollRepository
    {
        private string _filePath = "polls.json";

        public IQueryable<Poll> GetPolls()
        {
            if (!File.Exists(_filePath))
                return new List<Poll>().AsQueryable();

            string json = File.ReadAllText(_filePath);
            var polls = JsonSerializer.Deserialize<List<Poll>>(json) ?? new List<Poll>();
            return polls.AsQueryable();
        }

        public Poll GetPollById(int id)
        {
            return GetPolls().FirstOrDefault(p => p.Id == id);
        }

        public void Vote(int pollId, int optionNumber, string userId)
        {
            var polls = GetPolls().ToList();
            var poll = polls.FirstOrDefault(p => p.Id == pollId);
            if (poll != null && !poll.VotersID.Contains(userId))
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
                poll.VotersID.Add(userId);
                string updatedJson = JsonSerializer.Serialize(polls, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(_filePath, updatedJson);
            }
        }

        public void CreatePoll(Poll newPoll)
        {
            var polls = GetPolls().ToList();

            newPoll.Id = polls.Any() ? polls.Max(p => p.Id) + 1 : 1;

            polls.Add(newPoll);

            string updatedJson = JsonSerializer.Serialize(polls, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_filePath, updatedJson);
        }

        public bool HasUserVoted(int pollId, string userId)
        {
            var poll = GetPollById(pollId);
            return poll != null && poll.VotersID.Contains(userId);
        }
    }
}
