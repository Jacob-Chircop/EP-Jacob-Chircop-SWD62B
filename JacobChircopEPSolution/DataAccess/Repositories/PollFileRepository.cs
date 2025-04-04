using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class PollFileRepository
    {
        private readonly string _filePath = "polls.json";

        public List<Poll> GetPolls()
        {
            if (!File.Exists(_filePath))
                return new List<Poll>();

            string json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<Poll>>(json) ?? new List<Poll>();
        }

        public void CreatePoll(Poll newPoll)
        {
            var polls = GetPolls();

            newPoll.Id = polls.Any() ? polls.Max(p => p.Id) + 1 : 1;

            polls.Add(newPoll);

            string updatedJson = JsonSerializer.Serialize(polls, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_filePath, updatedJson);
        }
    }
}
