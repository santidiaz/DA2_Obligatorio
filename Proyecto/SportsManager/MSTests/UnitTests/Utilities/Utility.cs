using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace UnitTests.Utilities
{
    public class Utility
    {
        private static string[] maleNames = new string[5] { "Alfred", "Tony", "Bart", "Peter", "Jhon" };
        private static string[] femaleNames = new string[5] { "Carol", "Jennifer", "Storm", "Leia", "Jessica" };
        private static string[] lastNames = new string[5] { "Richards", "Kovacs", "Wayne", "Johnes", "Stark" };
        private static string[] emails = new string[5] { "aaa@bbb.com", "ccc@ddd.com", "eee@fff.com", "ggg@hhh.com", "iii@jjj.com" };
        private static string[] subjectNames = new string[5] { "Maths", "Physics", "Chemistry", "Geography", "History" };
        private static string[] userNames = new string[5] { "fox", "pepsi", "mcdonalds", "ford", "norteña" };
        private static string[] teamNames = new string[10] { "Juventud", "Rampla", "Tanque", "Albion", "Salus", "Lakers", "GSW", "Celtics", "Chicago Bulls", "Miami Heats" };
        private static string[] sportNames = new string[5] { "Football", "Basketball", "Baseball", "Hockey", "Rugby" };

        public static bool CompareLists<T>(List<T> real, List<T> toBeCompareWith)
            where T : class
        {
            bool result = real.Count == toBeCompareWith.Count;
            for (var index = 0; (index < real.Count || !result); index++)
            {
                result = real.ElementAt(index).Equals(toBeCompareWith.ElementAt(index));
            }
            return result;
        }

        public static User GenerateRandomUser(string userName = null, string password = null)
        {
            string randomUserName;
            if (string.IsNullOrEmpty(userName))
                randomUserName = userNames[GetRandomNumber(userNames.Length)];
            else
                randomUserName = userName;

            string randomPassword;
            if (string.IsNullOrEmpty(password))
                randomPassword = GenerateRandomPassword();
            else
                randomPassword = password;

            string randomName = GetRandomName();
            string randomLastName = lastNames[GetRandomNumber(lastNames.Length)];
            string randomEmail = emails[GetRandomNumber(emails.Length)];

            User randomUser = new User(randomName, randomLastName, randomUserName, randomPassword, randomEmail);
            return randomUser;
        }        
        
        public static Event GenerateRandomEvent()
        {
            DateTime randomDate = DateTime.Now.AddDays(GetRandomNumber(10));
            Team team1 = new Team { Name = GetRandomTeamName() };
            Team team2 = new Team { Name = GetRandomTeamName(team1.Name) };

            List<Team> teams = new List<Team> { team1, team2 };
            Sport sport1 = new Sport
            {
                Name = GetRandomSportName(),
                Teams = teams
            };

            return new Event(randomDate, sport1, team1, team2);
        }
                          

        public static string GetRandomTeamName(string avoidName = null)
        {
            string name = string.Empty;

            do
            {
                name = teamNames[GetRandomNumber(teamNames.Length)];
            }// <avoidName> not null and != <name>
            while (avoidName?.Equals(name) ?? false);

            return name;
        }

        public static string GetRandomSportName()
        {
            return sportNames[GetRandomNumber(sportNames.Length)]; ;
        }

        public static Team GenerateRandomTeam(string teamName = null)
        {
            string randomUserName;
            if (string.IsNullOrEmpty(teamName))
                randomUserName = GetRandomTeamName();
            else
                randomUserName = teamName;

            byte[] photo = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 };
            Team randomUser = new Team(randomUserName, photo);
            return randomUser;
        }

        public static string GetRandomName()
        {
            Random randomNumber = new Random(DateTime.Now.Second);
            string name = string.Empty;
            if (randomNumber.Next(1, 2) == 1)
                name = maleNames[randomNumber.Next(0, maleNames.Length - 1)];
            else
                name = femaleNames[randomNumber.Next(0, femaleNames.Length - 1)];

            return name;
        }

        public static string GetRandomLastName()
        {
            Random randomNumber = new Random(DateTime.Now.Second);
            return lastNames[randomNumber.Next(0, lastNames.Length - 1)];
        }

        public static string GenerateRandomPassword(int length = 6)
        {
            const string seed = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder result = new StringBuilder();
            Random random = new Random();

            while (0 < length--)
                result.Append(seed[random.Next(seed.Length)]);

            return result.ToString();
        }

        public static List<int> GetMonthsOfTheYear()
        {
            return new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
        }

        public static string GenerateHash(string valueToBeHashed)
        {
            using (SHA1Managed sha1 = new SHA1Managed())
            {
                var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(valueToBeHashed));
                var sb = new StringBuilder(hash.Length * 2);

                foreach (byte b in hash)
                    sb.Append(b.ToString("X2"));

                return sb.ToString();
            }
        }

        public static bool CompareLists<T>(List<T> real, List<T> toBeCompareWith)
    where T : class
        {
            bool result = real.Count == toBeCompareWith.Count;
            for (var index = 0; (index < real.Count || !result); index++)
            {
                result = real.ElementAt(index).Equals(toBeCompareWith.ElementAt(index));
            }
            return result;
        }

        #region Private methods
        private static int GetRandomNumber(int arrayLength)
        {
            return new Random(DateTime.Now.Second).Next(0, arrayLength - 1);
        }
        #endregion
    }
}
