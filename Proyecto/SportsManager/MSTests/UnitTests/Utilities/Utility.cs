﻿using BusinessEntities;
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

        public static List<int> GetMonthsOfTheYear()
        {
            return new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
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

        private static int GetRandomNumber(int arrayLength)
        {
            return new Random(DateTime.Now.Second).Next(0, arrayLength - 1);
        }
    }
}