using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;

namespace GPostgreSQLExample.Repositories
{
    public interface IMainContextInitialization
    {
        ValueTask InitializationAsync(IWebHostEnvironment env);
    }
}
