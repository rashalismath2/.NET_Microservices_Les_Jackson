using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlatformController.SyncDataService.Http;
using PlatformService.Data;
using PlatformService.Dto;
using PlatformService.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlatformService.Controllers
{
	[ApiController]
	[Route("api/platform")]
	public class PlatformController : ControllerBase
	{
		private readonly IPlatformRepo platformRepo;
		private readonly IMapper mapper;
		private readonly ICommandDataClient commandClient;

		public PlatformController(IPlatformRepo platformRepo, IMapper mapper, ICommandDataClient commandClient)
		{
			this.platformRepo = platformRepo;
			this.mapper = mapper;
			this.commandClient = commandClient;
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
		public async Task<ActionResult> Create(PlatformCreateDto create)
		{
			if (!ModelState.IsValid) {
				return BadRequest(ModelState);
			}
			var platform = mapper.Map<Platform>(create);
			platformRepo.CreatePlatform(platform);

			var saved=platformRepo.SaveChanges();
			if (saved) {
				var platformForSend = mapper.Map<PlatformReadDto>(platform);

				try
				{
					await commandClient.SendPlatformToCommand(platformForSend);
				}
				catch (Exception e)
				{
					Console.WriteLine(e.Message);
				}

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
