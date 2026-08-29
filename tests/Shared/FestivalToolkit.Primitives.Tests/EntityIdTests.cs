namespace FestivalToolkit.Primitives.Tests;

// Jeder Bezeichnertyp erbt den Vertrag aus EntityIdContract. Mehr ist je Typ nicht
// nötig — und ein Typ, der hier fehlt, fällt beim Lesen dieser Datei auf.

public sealed class OrganizationIdTests : EntityIdContract<OrganizationId>;

public sealed class EditionIdTests : EntityIdContract<EditionId>;

public sealed class PersonIdTests : EntityIdContract<PersonId>;

public sealed class DepartmentIdTests : EntityIdContract<DepartmentId>;
