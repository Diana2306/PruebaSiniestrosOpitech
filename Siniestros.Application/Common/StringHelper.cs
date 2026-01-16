using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace Siniestros.Application.Common;

/// <summary>
/// Clase de utilidad para operaciones de normalización y manipulación de cadenas de texto.
/// Proporciona métodos para normalizar strings de manera que las búsquedas sean más flexibles,
/// ignorando diferencias de mayúsculas/minúsculas, acentos y espacios.
/// </summary>
public static class StringHelper
{
    /// <summary>
    /// Normaliza un string para realizar búsquedas flexibles.
    /// Este método realiza las siguientes transformaciones:
    /// 1. Convierte el texto a minúsculas
    /// 2. Elimina todos los acentos y diacríticos (ej: "Bogotá" se convierte en "bogota")
    /// 3. Normaliza espacios múltiples a un solo espacio y elimina espacios al inicio y final
    /// 
    /// Esto permite que búsquedas como "bogota", "Bogotá", "bogota dc" o "Bogotá D.C." 
    /// encuentren el mismo resultado.
    /// </summary>
    /// <param name="input">Cadena de texto a normalizar. Puede ser null o vacía.</param>
    /// <returns>
    /// Cadena normalizada lista para comparación. Retorna string vacío si el input es null o vacío.
    /// </returns>
    /// <example>
    /// <code>
    /// StringHelper.NormalizeForSearch("Bogotá D.C.") // retorna "bogota d.c."
    /// StringHelper.NormalizeForSearch("  Medellín  ") // retorna "medellin"
    /// StringHelper.NormalizeForSearch("bogota")       // retorna "bogota"
    /// </code>
    /// </example>
    public static string NormalizeForSearch(string? input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return string.Empty;

        // Convertir a minúsculas y trim
        var normalized = input.Trim().ToLowerInvariant();

        // Quitar acentos y caracteres especiales
        normalized = RemoveDiacritics(normalized);

        // Normalizar espacios múltiples a uno solo
        normalized = Regex.Replace(normalized, @"\s+", " ");

        return normalized;
    }

    /// <summary>
    /// Remueve todos los diacríticos (acentos) de un string utilizando normalización Unicode.
    /// Este método utiliza la normalización FormD de Unicode para separar los caracteres base
    /// de sus marcas diacríticas, y luego reconstruye el string sin las marcas.
    /// </summary>
    /// <param name="text">Texto del cual se desean remover los acentos.</param>
    /// <returns>Texto sin acentos ni diacríticos.</returns>
    /// <example>
    /// <code>
    /// RemoveDiacritics("Bogotá") // retorna "Bogota"
    /// RemoveDiacritics("José")   // retorna "Jose"
    /// </code>
    /// </example>
    private static string RemoveDiacritics(string text)
    {
        var normalizedString = text.Normalize(NormalizationForm.FormD);
        var stringBuilder = new StringBuilder();

        foreach (var c in normalizedString)
        {
            var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
            if (unicodeCategory != UnicodeCategory.NonSpacingMark)
            {
                stringBuilder.Append(c);
            }
        }

        return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
    }
}
