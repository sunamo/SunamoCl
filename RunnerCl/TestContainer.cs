// Instance variables refactored according to C# conventions
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
var configuration = config;
logger.LogCritical("Critical!");
logger.LogError("Error!");
dynamic dynamicObject = new ExpandoObject();
dynamicObject.To = "to";
logger.LogInformation(JsonSerializer.Serialize(dynamicObject as ExpandoObject));
}
}