
namespace SunamoCl.Results;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class RunWithRunArgsResults
{
    public ServiceProvider ServiceProvider { get; set; }
    public string Runned { get; set; }

    public void Deconstruct(out ServiceProvider serviceProvider, out string arg)
    {
        serviceProvider = ServiceProvider;
        arg = Runned;
    }
}