
using PlatformService.Dto;
using System.Threading.Tasks;

namespace PlatformController.SyncDataService.Http
{
	public interface ICommandDataClient
	{
		Task SendPlatformToCommand(PlatformReadDto platform);
	}
}
