using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts;
using Repositories.Xml;
using DataContracts;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;


namespace Solution
{
    internal class XmlRepositoryProvider : AbstractRepositoryProvider
    {
        IConfiguration Config;
        Settings Settings;
        public XmlRepositoryProvider()
        {
        
            Config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();
            Settings = Config.GetRequiredSection("Settings").Get<Settings>();
                //The keyName is a key to store FileName of XML Storage
                //string KeyName = typeof(T).ToString() + "ListFile";
            //FileName = Settings..UserListFile
            // Get values from the config given their key and their target type.
            //AppSettings settings = config.GetRequiredSection("Settings").Get<Settings>();

            //Console.WriteLine($"UserListFile = {Settings.UserListFile}");



        }
        public override IRepository<User> GetUserRepository()
        {
            return new XmlObjectRepository<User>(Settings.UserListFile.ToString());
        }

        public override IRepository<Project> GetProjectRepository()
        {
            return new XmlObjectRepository<Project>(Settings.ProjectListFile.ToString());
        }
        public override IRepository<TimeTrackEntry> GetTimeTrackEntryRepository()
        {
            return new XmlObjectRepository<TimeTrackEntry>(Settings.EntryListFile.ToString());
        }
    }
    public class Settings
    {
        public string UserListFile { get; set; }
        public string ProjectListFile { get; set; }
        public string EntryListFile { get; set; }
    }
}
