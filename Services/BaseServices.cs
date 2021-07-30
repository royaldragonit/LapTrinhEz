using LapTrinhEZ.Models.Entites;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LapTrinhEZ.Services
{
    public abstract class BaseServices
    {
        public readonly LaptrinhezdbContext _db;
        public readonly LaptrinhezdbContextProcedures _sp;
        public readonly IConfiguration _config;
        public readonly string _host;
        public readonly IWebHostEnvironment _environment;
        public BaseServices(LaptrinhezdbContext db, IConfiguration config,IWebHostEnvironment environment)
        {
            _environment = environment;
            _db = db;
            _sp = new LaptrinhezdbContextProcedures(_db);
            _config = config;
            _host = _config["Host"];
        }
    }
}
