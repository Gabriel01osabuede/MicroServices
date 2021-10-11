using CommandsService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommandsService.Data
{
    public interface ICommandRepo
    {
        bool SaveChanges();
        //Platforms related commands
        IEnumerable<platform> GetAllPlatforms();
        void CreatePlatform(platform plat);
        bool PlatformExist(int platformId);
        bool ExternalPlatformExist(int externalPlatformId);

        //commands relateed services
        IEnumerable<Command> GetCommandsForPlatform(int platformId);
        Command GetCommand(int platformId, int commandId);
        void CreateCommand(int platformId, Command command);
    }
}
