using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Siniestros.Application.Handlers;
using Siniestros.Application.Queries;
using Siniestros.Domain.Entities;
using Siniestros.Tests.Helpers;
using Xunit;

namespace Siniestros.Tests.Application;

public class ObtenerSiniestroPorIdHandlerTests
{
    [Fact]
    public async Task Handle_deberia_retornar_siniestro_cuando_existe()
    {
        // Arrange
        await using var db = DbContextHelper.CreateInMemoryDb();
        var handler = new ObtenerSiniestroPorIdHandler(db, new Siniestros.Infrastructure.Repositories.SiniestroRepository(db));

        var departamento = new Departamento { Id = "73", Nombre = "Tolima" };
        var ciudad = new Ciudad { Id = "73001", IdDepartamento = "73", Nombre = "Ibagué", Departamento = departamento };
        var tipoSiniestro = new TipoSiniestro { Id = 1, Nombre = "Choque frontal" };

        db.Departamentos.Add(departamento);
        db.Ciudades.Add(ciudad);
        db.TiposSiniestro.Add(tipoSiniestro);

        var siniestro = Siniestro.Crear(
            DateTime.UtcNow,
            "73001",
            1,
            2,
            0,
            "Test"
        );

        db.Siniestros.Add(siniestro);
        await db.SaveChangesAsync();

        var query = new ObtenerSiniestroPorIdQuery(siniestro.Id);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result!.Id.Should().Be(siniestro.Id);
        result.Ciudad.Should().Be("Ibagué");
        result.Departamento.Should().Be("Tolima");
        result.TipoSiniestro.Should().Be("Choque frontal");
    }

    [Fact]
    public async Task Handle_deberia_retornar_null_cuando_no_existe()
    {
        // Arrange
        await using var db = DbContextHelper.CreateInMemoryDb();
        var handler = new ObtenerSiniestroPorIdHandler(db, new Siniestros.Infrastructure.Repositories.SiniestroRepository(db));

        var query = new ObtenerSiniestroPorIdQuery(Guid.NewGuid());

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().BeNull();
    }
}
