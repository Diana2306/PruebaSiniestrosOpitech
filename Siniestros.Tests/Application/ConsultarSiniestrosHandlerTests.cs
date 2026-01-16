using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Siniestros.Application.Common;
using Siniestros.Application.Handlers;
using Siniestros.Application.Queries;
using Siniestros.Domain.Entities;
using Siniestros.Tests.Helpers;
using Xunit;

namespace Siniestros.Tests.Application;

public class ConsultarSiniestrosHandlerTests
{
    [Fact]
    public async Task Handle_deberia_retornar_siniestros_paginados()
    {
        // Arrange
        await using var db = DbContextHelper.CreateInMemoryDb();
        var handler = new ConsultarSiniestrosHandler(db, new Siniestros.Infrastructure.Repositories.SiniestroRepository(db));

        // Crear datos de prueba
        var departamento = new Departamento { Id = "73", Nombre = "Tolima" };
        var ciudad = new Ciudad { Id = "73001", IdDepartamento = "73", Nombre = "Ibagué", Departamento = departamento };
        var tipoSiniestro = new TipoSiniestro { Id = 1, Nombre = "Choque frontal" };

        db.Departamentos.Add(departamento);
        db.Ciudades.Add(ciudad);
        db.TiposSiniestro.Add(tipoSiniestro);

        var siniestro1 = Siniestro.Crear(
            DateTime.UtcNow.AddDays(-1),
            "73001",
            1,
            2,
            0,
            "Test 1"
        );

        var siniestro2 = Siniestro.Crear(
            DateTime.UtcNow,
            "73001",
            1,
            1,
            0,
            "Test 2"
        );

        db.Siniestros.AddRange(siniestro1, siniestro2);
        await db.SaveChangesAsync();

        var query = new ConsultarSiniestrosQuery(null, null, null, null, 1, 10);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Items.Should().HaveCount(2);
        result.TotalItems.Should().Be(2);
        result.Page.Should().Be(1);
        result.PageSize.Should().Be(10);
    }

    [Fact]
    public async Task Handle_deberia_filtrar_por_departamento()
    {
        // Arrange
        await using var db = DbContextHelper.CreateInMemoryDb();
        var handler = new ConsultarSiniestrosHandler(db, new Siniestros.Infrastructure.Repositories.SiniestroRepository(db));

        var deptTolima = new Departamento { Id = "73", Nombre = "Tolima" };
        var deptCundinamarca = new Departamento { Id = "25", Nombre = "Cundinamarca" };
        var ciudadTolima = new Ciudad { Id = "73001", IdDepartamento = "73", Nombre = "Ibagué", Departamento = deptTolima };
        var ciudadCundinamarca = new Ciudad { Id = "25754", IdDepartamento = "25", Nombre = "Soacha", Departamento = deptCundinamarca };
        var tipoSiniestro = new TipoSiniestro { Id = 1, Nombre = "Choque frontal" };

        db.Departamentos.AddRange(deptTolima, deptCundinamarca);
        db.Ciudades.AddRange(ciudadTolima, ciudadCundinamarca);
        db.TiposSiniestro.Add(tipoSiniestro);

        var siniestro1 = Siniestro.Crear(DateTime.UtcNow, "73001", 1, 2, 0, "Tolima");
        var siniestro2 = Siniestro.Crear(DateTime.UtcNow, "25754", 1, 1, 0, "Cundinamarca");

        db.Siniestros.AddRange(siniestro1, siniestro2);
        await db.SaveChangesAsync();

        var query = new ConsultarSiniestrosQuery("Tolima", null, null, null, 1, 10);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        result.Items.Should().HaveCount(1);
        result.Items.First().Departamento.Should().Be("Tolima");
    }

    [Fact]
    public async Task Handle_deberia_filtrar_por_rango_fechas()
    {
        // Arrange
        await using var db = DbContextHelper.CreateInMemoryDb();
        var handler = new ConsultarSiniestrosHandler(db, new Siniestros.Infrastructure.Repositories.SiniestroRepository(db));

        var departamento = new Departamento { Id = "73", Nombre = "Tolima" };
        var ciudad = new Ciudad { Id = "73001", IdDepartamento = "73", Nombre = "Ibagué", Departamento = departamento };
        var tipoSiniestro = new TipoSiniestro { Id = 1, Nombre = "Choque frontal" };

        db.Departamentos.Add(departamento);
        db.Ciudades.Add(ciudad);
        db.TiposSiniestro.Add(tipoSiniestro);

        var desde = DateTime.UtcNow.AddDays(-5);
        var hasta = DateTime.UtcNow.AddDays(-1);

        var siniestro1 = Siniestro.Crear(desde.AddDays(1), "73001", 1, 2, 0, "Dentro del rango");
        var siniestro2 = Siniestro.Crear(DateTime.UtcNow, "73001", 1, 1, 0, "Fuera del rango");

        db.Siniestros.AddRange(siniestro1, siniestro2);
        await db.SaveChangesAsync();

        var query = new ConsultarSiniestrosQuery(null, null, desde, hasta, 1, 10);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        result.Items.Should().HaveCount(1);
        result.Items.First().Descripcion.Should().Be("Dentro del rango");
    }
}
