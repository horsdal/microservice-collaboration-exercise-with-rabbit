namespace LoyaltyProgramMicroservice
{
    using System.Collections.Generic;
    using EasyNetQ;

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

        public static void Main(string[] args)
        {
            var bus = RabbitHutch.CreateBus("host=192.168.99.100");
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

