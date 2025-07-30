namespace RunnerCl;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

internal class TestContainer(ILogger logger, IConfiguration config)
{
internal void A()
{
var c = config;
logger.LogCritical("Critical!");
logger.LogError("Error!");
dynamic d = new ExpandoObject();
d.To = "to";
logger.LogInformation(JsonSerializer.Serialize(d as ExpandoObject));
}
}