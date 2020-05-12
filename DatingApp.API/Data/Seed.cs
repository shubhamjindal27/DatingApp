﻿using DatingApp.API.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace DatingApp.API.Data
{
	public class Seed
	{
		public static void SeedUsers(AppDbContext context)
		{
			if (!context.Users.Any())
			{
				var userData = System.IO.File.ReadAllText("Data/UserSeedData.json");
				var users = JsonConvert.DeserializeObject<List<User>>(userData);
				foreach (var user in users)
				{
					byte[] passwordHash, passwordSalt;
					CreatePasswordHash("password", out passwordHash, out passwordSalt);

					user.PasswordHash = passwordHash;
					user.PasswordSalt = passwordSalt;
					user.Username = user.Username.ToLower();
					context.Users.Add(user);
				}

				context.SaveChanges();
			}
		}

		private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
		{
			using (var hmac = new HMACSHA512())
			{
				passwordSalt = hmac.Key;
				passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
			}
		}
	}
}
