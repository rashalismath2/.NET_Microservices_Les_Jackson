using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Data;
using PlatformService.Dto;
using PlatformService.Models;
using System.Collections.Generic;

namespace PlatformService.Controllers
{
	[ApiController]
	[Route("api/platform")]
	public class PlatformController : ControllerBase
	{
		private readonly IPlatformRepo platformRepo;
		private readonly IMapper mapper;

		public PlatformController(IPlatformRepo platformRepo, IMapper mapper)
		{
			this.platformRepo = platformRepo;
			this.mapper = mapper;
		}

		[HttpGet("")]
		public ActionResult<IEnumerable<PlatformReadDto>> Index()
		{
			return Ok(mapper.Map<IEnumerable<PlatformReadDto>>(platformRepo.GetAllPlatforms()));
		}

		[HttpGet("{id}",Name = "Details")]
		public ActionResult<PlatformReadDto> Details(int id)
		{
			var platform = mapper.Map<PlatformReadDto>(platformRepo.GetPlatformById(id));
			if (platform==null) {
				return NotFound();
			}
			return Ok(platform);
		}

		[HttpPost]
		public ActionResult Create(PlatformCreateDto create)
		{
			if (!ModelState.IsValid) {
				return BadRequest(ModelState);
			}
			var platform = mapper.Map<Platform>(create);
			platformRepo.CreatePlatform(platform);

			var saved=platformRepo.SaveChanges();
			if (saved) {
				var platformForSend = mapper.Map<PlatformReadDto>(platform);
				return CreatedAtRoute(nameof(Details),new {Id= platformForSend.Id }, platformForSend);
			}
			return BadRequest(create);
		}


		[HttpPut]
		public ActionResult Edit(int id)
		{
			return Ok();
		}


		[HttpDelete]
		public ActionResult Delete(int id)
		{
			return Ok();
		}

	}
}
