namespace FestivalToolkit.Primitives;

/// <summary>
/// Die gemeinsame Mechanik hinter allen Bezeichnern aus <see cref="IEntityId{TSelf}"/>.
/// Steht hier einmal, damit die einzelnen Bezeichnertypen reine Deklarationen bleiben.
/// </summary>
public static class EntityId
{
    /// <summary>
    /// UUIDv7: die ersten 48 Bit sind ein Millisekunden-Zeitstempel. Jeder Dienst kann
    /// ohne Absprache Bezeichner vergeben, sie landen aber trotzdem am Ende desselben
    /// B-Baum-Index in Postgres statt über den ganzen Index verstreut.
    /// </summary>
    public static Guid CreateValue() => Guid.CreateVersion7();

    public static Guid ParseValue(string text) => Guid.Parse(text);

    public static bool TryParseValue(string? text, out Guid value) => Guid.TryParse(text, out value);

    public static int Compare(Guid left, Guid right) => left.CompareTo(right);
}
