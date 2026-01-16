using FluentAssertions;
using Siniestros.Domain.Entities;
using Xunit;

namespace Siniestros.Tests.Domain;

public class SiniestroTests
{
    [Fact]
    public void Crear_deberia_crear_siniestro_valido()
    {
        var siniestro = Siniestro.Crear(
            fechaHora: DateTime.UtcNow,
            idCiudad: "73001",
            idTipoSiniestro: 1,
            vehiculosInvolucrados: 2,
            numeroVictimas: 0,
            descripcion: "Test"
        );

        siniestro.Should().NotBeNull();
        siniestro.Id.Should().NotBe(Guid.Empty);
        siniestro.FechaHora.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        siniestro.IdCiudad.Should().Be("73001");
    }

    [Fact]
    public void Crear_deberia_lanzar_excepcion_si_idCiudad_es_vacio()
    {
        Action act = () => Siniestro.Crear(
            fechaHora: DateTime.UtcNow,
            idCiudad: string.Empty,
            idTipoSiniestro: 1,
            vehiculosInvolucrados: 2,
            numeroVictimas: 0,
            descripcion: "Test"
        );

        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Crear_deberia_lanzar_excepcion_si_idCiudad_no_tiene_5_digitos()
    {
        Action act = () => Siniestro.Crear(
            fechaHora: DateTime.UtcNow,
            idCiudad: "123",
            idTipoSiniestro: 1,
            vehiculosInvolucrados: 2,
            numeroVictimas: 0,
            descripcion: "Test"
        );

        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Crear_deberia_lanzar_excepcion_si_idTipoSiniestro_es_invalido()
    {
        Action act = () => Siniestro.Crear(
            fechaHora: DateTime.UtcNow,
            idCiudad: "73001",
            idTipoSiniestro: 0,
            vehiculosInvolucrados: 2,
            numeroVictimas: 0,
            descripcion: "Test"
        );

        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Crear_deberia_lanzar_excepcion_si_vehiculosInvolucrados_es_negativo()
    {
        Action act = () => Siniestro.Crear(
            fechaHora: DateTime.UtcNow,
            idCiudad: "73001",
            idTipoSiniestro: 1,
            vehiculosInvolucrados: -1,
            numeroVictimas: 0,
            descripcion: "Test"
        );

        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Crear_deberia_lanzar_excepcion_si_numeroVictimas_es_negativo()
    {
        Action act = () => Siniestro.Crear(
            fechaHora: DateTime.UtcNow,
            idCiudad: "73001",
            idTipoSiniestro: 1,
            vehiculosInvolucrados: 2,
            numeroVictimas: -1,
            descripcion: "Test"
        );

        act.Should().Throw<ArgumentException>();
    }
}
