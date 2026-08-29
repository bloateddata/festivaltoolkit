namespace FestivalToolkit.Primitives;

/// <summary>
/// Ein stark typisierter Bezeichner. Der Sinn ist nicht Eleganz, sondern dass
/// <c>Assign(PersonId, ShiftId)</c> beim Vertauschen der Argumente nicht mehr
/// übersetzt — mit rohen <see cref="Guid"/>-Werten fällt so etwas erst auf,
/// wenn während des Festivals die falsche Person auf der Schicht steht.
/// </summary>
public interface IEntityId<TSelf> : IEquatable<TSelf>, IComparable<TSelf>
    where TSelf : struct, IEntityId<TSelf>
{
    /// <summary>Der zugrunde liegende Wert. Nur für Persistenz und Serialisierung.</summary>
    Guid Value { get; }

    /// <summary>Ob der Bezeichner nie vergeben wurde.</summary>
    bool IsEmpty { get; }

    /// <summary>Der nicht vergebene Bezeichner; identisch mit <c>default</c>.</summary>
    static abstract TSelf Empty { get; }

    /// <summary>Erzeugt einen neuen, zeitlich sortierbaren Bezeichner.</summary>
    static abstract TSelf Create();

    /// <summary>Übernimmt einen bestehenden Wert, etwa aus der Datenbank.</summary>
    static abstract TSelf From(Guid value);

    static abstract TSelf Parse(string text);

    static abstract bool TryParse(string? text, out TSelf id);

    /// <summary>
    /// Die kanonische Textform: 36 Zeichen mit Bindestrichen, ohne Klammern.
    /// Teil des Vertrags, weil Bezeichner in URLs, Protokollen und QR-Codes auftauchen.
    /// </summary>
    string ToString();
}
