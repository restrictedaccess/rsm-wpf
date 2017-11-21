using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Remote_Staff_Modules
{
    public class ModuleFactory
    {
        public static string Login = "login";
        public static string Version = "version";
        public static string StartWork = "startwork";
        public static string Cache = "cache";
        public static string Sync = "sync";
        public static string Timeblock = "timeblock";

        public static BaseModule Get(string moduleName)
        {

            if (moduleName == ModuleFactory.Login)
            {
                return new LoginModule();
            }

            if (moduleName == ModuleFactory.Version)
            {
                return new VersionModule();
            }

            if (moduleName == ModuleFactory.StartWork)
            {
                return new StartWorkModule();
            }

            if(moduleName == ModuleFactory.Cache)
            {
                return new CacheModule();
            }

            if (moduleName == ModuleFactory.Sync)
            {
                return new SyncModule();
            }

            if(moduleName == ModuleFactory.Timeblock)
            {
                return new TimeBlockModule();
            }

            return null;
        }
    }
}
