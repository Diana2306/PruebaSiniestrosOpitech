using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Siniestros.Application.Commands;
using Siniestros.Application.Handlers;
using Siniestros.Tests.Helpers;
using Xunit;

namespace Siniestros.Tests.Application;

public class CrearSiniestroHandlerTests
{
    [Fact]
    public async Task Handle_deberia_guardar_siniestro_y_retornar_id()
    {
        // Arrange
        await using var db = DbContextHelper.CreateInMemoryDb();
        var handler = new CrearSiniestroHandler(
            db,
            new Siniestros.Infrastructure.Repositories.CiudadRepository(db),
            new Siniestros.Infrastructure.Repositories.TipoSiniestroRepository(db),
            new Siniestros.Infrastructure.Repositories.SiniestroRepository(db));

        // Crear datos de prueba
        var departamento = new Siniestros.Domain.Entities.Departamento { Id = "73", Nombre = "Tolima" };
        var ciudad = new Siniestros.Domain.Entities.Ciudad { Id = "73001", IdDepartamento = "73", Nombre = "Ibagué", Departamento = departamento };
        var tipoSiniestro = new Siniestros.Domain.Entities.TipoSiniestro { Id = 1, Nombre = "Choque frontal" };

        db.Departamentos.Add(departamento);
        db.Ciudades.Add(ciudad);
        db.TiposSiniestro.Add(tipoSiniestro);
        await db.SaveChangesAsync();

        var command = new CrearSiniestroCommand(
         FechaHora: DateTime.UtcNow,
         IdCiudad: "73001",
         IdTipoSiniestro: 1,
         VehiculosInvolucrados: 2,
         NumeroVictimas: 0,
         Descripcion: "Test handler"
     );

        // Act
        var id = await handler.Handle(command, CancellationToken.None);

        // Assert
        id.Should().NotBe(Guid.Empty);

        var entity = await db.Siniestros.FirstOrDefaultAsync(x => x.Id == id);
        entity.Should().NotBeNull();
    }
}
