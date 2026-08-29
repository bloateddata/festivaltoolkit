namespace FestivalToolkit.Primitives;

// Alle typisierten Bezeichner des Kerns. Die Wiederholung je Typ ist bewusst sichtbar
// an einer Stelle gebündelt: Sobald sie schmerzt, ist der Quelltextgenerator fällig —
// vorher wäre er Aufwand ohne Nutzen.

/// <summary>Eine Organisation — der Mandant. Trennt alles voneinander, siehe docs/12-data-access.md.</summary>
public readonly record struct OrganizationId(Guid Value) : IEntityId<OrganizationId>
{
    public static OrganizationId Empty => default;

    public bool IsEmpty => Value == Guid.Empty;

    public static OrganizationId Create() => new(EntityId.CreateValue());

    public static OrganizationId From(Guid value) => new(value);

    public static OrganizationId Parse(string text) => new(EntityId.ParseValue(text));

    public static bool TryParse(string? text, out OrganizationId id)
    {
        var parsed = EntityId.TryParseValue(text, out var value);
        id = parsed ? new OrganizationId(value) : default;
        return parsed;
    }

    public int CompareTo(OrganizationId other) => EntityId.Compare(Value, other.Value);

    public static bool operator <(OrganizationId left, OrganizationId right) => left.CompareTo(right) < 0;

    public static bool operator <=(OrganizationId left, OrganizationId right) => left.CompareTo(right) <= 0;

    public static bool operator >(OrganizationId left, OrganizationId right) => left.CompareTo(right) > 0;

    public static bool operator >=(OrganizationId left, OrganizationId right) => left.CompareTo(right) >= 0;

    public override string ToString() => Value.ToString("D");
}

/// <summary>Eine Ausgabe eines Festivals, also ein Jahrgang. Siehe docs/01-domain-model.md.</summary>
public readonly record struct EditionId(Guid Value) : IEntityId<EditionId>
{
    public static EditionId Empty => default;

    public bool IsEmpty => Value == Guid.Empty;

    public static EditionId Create() => new(EntityId.CreateValue());

    public static EditionId From(Guid value) => new(value);

    public static EditionId Parse(string text) => new(EntityId.ParseValue(text));

    public static bool TryParse(string? text, out EditionId id)
    {
        var parsed = EntityId.TryParseValue(text, out var value);
        id = parsed ? new EditionId(value) : default;
        return parsed;
    }

    public int CompareTo(EditionId other) => EntityId.Compare(Value, other.Value);

    public static bool operator <(EditionId left, EditionId right) => left.CompareTo(right) < 0;

    public static bool operator <=(EditionId left, EditionId right) => left.CompareTo(right) <= 0;

    public static bool operator >(EditionId left, EditionId right) => left.CompareTo(right) > 0;

    public static bool operator >=(EditionId left, EditionId right) => left.CompareTo(right) >= 0;

    public override string ToString() => Value.ToString("D");
}

/// <summary>Eine Person. Nicht zu verwechseln mit dem Konto in Identity, siehe docs/14-identity.md.</summary>
public readonly record struct PersonId(Guid Value) : IEntityId<PersonId>
{
    public static PersonId Empty => default;

    public bool IsEmpty => Value == Guid.Empty;

    public static PersonId Create() => new(EntityId.CreateValue());

    public static PersonId From(Guid value) => new(value);

    public static PersonId Parse(string text) => new(EntityId.ParseValue(text));

    public static bool TryParse(string? text, out PersonId id)
    {
        var parsed = EntityId.TryParseValue(text, out var value);
        id = parsed ? new PersonId(value) : default;
        return parsed;
    }

    public int CompareTo(PersonId other) => EntityId.Compare(Value, other.Value);

    public static bool operator <(PersonId left, PersonId right) => left.CompareTo(right) < 0;

    public static bool operator <=(PersonId left, PersonId right) => left.CompareTo(right) <= 0;

    public static bool operator >(PersonId left, PersonId right) => left.CompareTo(right) > 0;

    public static bool operator >=(PersonId left, PersonId right) => left.CompareTo(right) >= 0;

    public override string ToString() => Value.ToString("D");
}

/// <summary>Ein Bereich im Bereichsbaum. Siehe docs/02-authz.md.</summary>
public readonly record struct DepartmentId(Guid Value) : IEntityId<DepartmentId>
{
    public static DepartmentId Empty => default;

    public bool IsEmpty => Value == Guid.Empty;

    public static DepartmentId Create() => new(EntityId.CreateValue());

    public static DepartmentId From(Guid value) => new(value);

    public static DepartmentId Parse(string text) => new(EntityId.ParseValue(text));

    public static bool TryParse(string? text, out DepartmentId id)
    {
        var parsed = EntityId.TryParseValue(text, out var value);
        id = parsed ? new DepartmentId(value) : default;
        return parsed;
    }

    public int CompareTo(DepartmentId other) => EntityId.Compare(Value, other.Value);

    public static bool operator <(DepartmentId left, DepartmentId right) => left.CompareTo(right) < 0;

    public static bool operator <=(DepartmentId left, DepartmentId right) => left.CompareTo(right) <= 0;

    public static bool operator >(DepartmentId left, DepartmentId right) => left.CompareTo(right) > 0;

    public static bool operator >=(DepartmentId left, DepartmentId right) => left.CompareTo(right) >= 0;

    public override string ToString() => Value.ToString("D");
}
