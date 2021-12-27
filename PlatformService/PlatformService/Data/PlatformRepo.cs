using PlatformService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlatformService.Data
{
	public class PlatformRepo : IPlatformRepo
	{
		private readonly AppDbContext _dbContext;

		public PlatformRepo(AppDbContext dbContext)
		{
			_dbContext = dbContext;
		}
		public void CreatePlatform(Platform platform)
		{
			_dbContext.Platforms.Add(platform);
		}

		public IEnumerable<Platform> GetAllPlatforms()
		{
			return _dbContext.Platforms.ToList();
		}

		public Platform GetPlatformById(int id)
		{
			throw new NotImplementedException();
		}

		public bool SaveChanges()
		{
			return _dbContext.SaveChanges()>=0;
		}
	}
}
