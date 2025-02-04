﻿using Data.Interfaces;
using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
	public interface IUserRepository : IRepository<User>
	{
		Task<User?> GetByFullNameAsync(string name, string surname);
	}
}
