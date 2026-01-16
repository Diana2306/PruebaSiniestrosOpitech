using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Siniestros.Infrastructure.Persistence;

namespace Siniestros.Tests.Helpers;

public static class DbContextHelper
{
    public static SiniestrosDbContext CreateInMemoryDb()
    {
        var options = new DbContextOptionsBuilder<SiniestrosDbContext>()
            .UseInMemoryDatabase($"SiniestrosDb_{Guid.NewGuid()}")
            .Options;

        return new SiniestrosDbContext(options);
    }
}