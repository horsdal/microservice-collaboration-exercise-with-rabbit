namespace LoyaltyProgramMicroservice
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using EasyNetQ;
    using Newtonsoft.Json;
    using SpecialOffersContract;

    class Program
    {
        public static List<LoyaltyProgramUser> RegisteredUser =
            new List<LoyaltyProgramUser>
            {
                new LoyaltyProgramUser(1, "Christian", 100,
                    new LoyaltyProgramSettings("Cycling", "Whisky", "Music", "Software")),
                new LoyaltyProgramUser(2, "Jane", 100,
                    new LoyaltyProgramSettings("Electronics", "Beer", "Music", "Psycology")),
                new LoyaltyProgramUser(3, "Simon", 100, new LoyaltyProgramSettings("Football", "Electronics")),
                new LoyaltyProgramUser(4, "Maria", 100,
                    new LoyaltyProgramSettings("Cycling", "Chocolate", "Dance", "Software")),
                new LoyaltyProgramUser(5, "Dan", 100, new LoyaltyProgramSettings("Beer", "Golf", "Music", "Footbal")),
            };
        
        private static HttpClient Client = new HttpClient();

        public static void Main(string[] args)
        {
            var bus = RabbitHutch.CreateBus("host=192.168.99.100");
            bus.SubscribeAsync<SpecialOffer>("LoyaltyProgramSubscriber", HandleEvents);
        }

        private static Task HandleEvents(SpecialOffer arg)
        {
            Console.WriteLine("handling evens");
            var usersToNotifiy = RegisteredUser.Where(u => u.Settings.Interests.Intersect(arg.Keywords).Any());
            return Task.WhenAll(
                usersToNotifiy.Select(NotifyUser));
        }

        private static Task<HttpResponseMessage> NotifyUser(LoyaltyProgramUser user)
        {
            var notificationServiceUri = new Uri("http://localhost:5001/notifications");
            var notificationBody = JsonConvert.SerializeObject(new {Type = "email", Name = user.Name});
            Console.WriteLine($"Posting to notification service: {notificationServiceUri}, {notificationBody}");
            return Client.PostAsync(notificationServiceUri,
                new StringContent(notificationBody, Encoding.UTF8, "application/json"));
        }
    }

    public class LoyaltyProgramUser
    {
        public LoyaltyProgramUser(int id, string name, int loyaltyPoints, LoyaltyProgramSettings settings)
        {
            this.Id = id;
            this.Name = name;
            this.LoyaltyPoints = loyaltyPoints;
            this.Settings = settings;
        }

        public int Id { get; }
        public string Name { get; }
        public int LoyaltyPoints { get; }
        public LoyaltyProgramSettings Settings { get; }
    }

    public class LoyaltyProgramSettings
    {
        public LoyaltyProgramSettings(params string[] interests)
        {
            this.Interests = interests;
        }

        public string[] Interests { get; }
    }
}

