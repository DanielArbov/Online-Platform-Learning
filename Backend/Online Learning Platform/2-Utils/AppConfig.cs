using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;



namespace Talent;

static class AppConfig
{


    public static readonly string ConnectionString = null!;

    static AppConfig()
    {
        IConfigurationRoot settings = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        ConnectionString = settings.GetConnectionString("LearningPlatform")!;

    }
}

