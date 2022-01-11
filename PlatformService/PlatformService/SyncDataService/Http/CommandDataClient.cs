using Microsoft.Extensions.Configuration;
using PlatformService.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PlatformController.SyncDataService.Http
{
	public class CommandDataClient : ICommandDataClient
	{
		private readonly HttpClient httpClient;
		private readonly IConfiguration configuration;

		public CommandDataClient(HttpClient	httpClient,IConfiguration configuration)
		{
			this.httpClient = httpClient;
			this.configuration = configuration;
		}

		public async Task SendPlatformToCommand(PlatformReadDto platform)
		{
			var httpContent = new StringContent(
				JsonSerializer.Serialize(platform),
				Encoding.UTF8,
				"application/json"
			);

			var response =await httpClient.PostAsync(configuration["CommandService"], httpContent);

			if (response.IsSuccessStatusCode) {
				Console.WriteLine("Post for platform service ok");
			}
		}
	}
}
