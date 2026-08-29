namespace FestivalToolkit.Primitives.Tests;

/// <summary>
/// Der Vertrag, den jeder typisierte Bezeichner erfüllt. Ein neuer Bezeichnertyp
/// erbt diese Klasse und ist damit vollständig geprüft — die Regeln stehen einmal,
/// nicht einmal je Typ.
/// </summary>
public abstract class EntityIdContract<TId>
    where TId : struct, IEntityId<TId>
{
    [Fact]
    public void Create_never_returns_an_empty_id()
    {
        TId.Create().IsEmpty.ShouldBeFalse();
    }

    [Fact]
    public void Create_returns_a_different_id_every_time()
    {
        var ids = Enumerable.Range(0, 1_000).Select(_ => TId.Create()).ToArray();

        ids.Distinct().Count().ShouldBe(ids.Length);
    }

    [Fact]
    public void Create_returns_a_time_ordered_uuid()
    {
        // UUIDv7. Die Begründung steht in docs/23: Bezeichner werden von jedem Dienst
        // ohne Absprache erzeugt, landen aber im selben B-Baum-Index in Postgres.
        // Zufällige Bezeichner streuen die Schreibzugriffe über den ganzen Index.
        TId.Create().Value.Version.ShouldBe(7);
    }

    [Fact]
    public void Empty_and_default_are_the_same_empty_id()
    {
        TId.Empty.ShouldBe(default(TId));
        TId.Empty.IsEmpty.ShouldBeTrue();
        TId.Empty.Value.ShouldBe(Guid.Empty);
    }

    [Fact]
    public void From_keeps_the_given_value()
    {
        var value = Guid.CreateVersion7();

        TId.From(value).Value.ShouldBe(value);
    }

    [Fact]
    public void Ids_with_the_same_value_are_equal()
    {
        var value = Guid.CreateVersion7();

        var left = TId.From(value);
        var right = TId.From(value);

        left.ShouldBe(right);
        left.Equals(right).ShouldBeTrue();
        left.GetHashCode().ShouldBe(right.GetHashCode());
    }

    [Fact]
    public void Ids_with_different_values_are_not_equal()
    {
        TId.Create().ShouldNotBe(TId.Create());
    }

    [Fact]
    public void ToString_and_Parse_round_trip()
    {
        var id = TId.Create();

        TId.Parse(id.ToString()).ShouldBe(id);
    }

    [Fact]
    public void ToString_uses_the_plain_hyphenated_form()
    {
        var id = TId.From(Guid.Parse("0198a1b2-c3d4-7e5f-8a9b-0c1d2e3f4a5b"));

        id.ToString().ShouldBe("0198a1b2-c3d4-7e5f-8a9b-0c1d2e3f4a5b");
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("nicht-mal-annaehernd")]
    [InlineData("0198a1b2-c3d4-7e5f-8a9b-0c1d2e3f4a5")]
    public void TryParse_rejects_anything_that_is_not_an_id(string? text)
    {
        TId.TryParse(text, out var parsed).ShouldBeFalse();
        parsed.ShouldBe(TId.Empty);
    }

    [Fact]
    public void Parse_throws_on_garbage()
    {
        Should.Throw<FormatException>(() => TId.Parse("nicht-mal-annaehernd"));
    }

    [Fact]
    public void Ids_created_later_sort_after_ids_created_earlier()
    {
        var earlier = TId.From(Guid.CreateVersion7(new DateTimeOffset(2026, 6, 1, 12, 0, 0, TimeSpan.Zero)));
        var later = TId.From(Guid.CreateVersion7(new DateTimeOffset(2026, 6, 1, 12, 0, 1, TimeSpan.Zero)));

        earlier.CompareTo(later).ShouldBeLessThan(0);
        later.CompareTo(earlier).ShouldBeGreaterThan(0);
        earlier.CompareTo(earlier).ShouldBe(0);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    [InlineData(6)]
    [InlineData(7)]
    [InlineData(8)]
    [InlineData(9)]
    [InlineData(10)]
    [InlineData(11)]
    [InlineData(12)]
    [InlineData(13)]
    [InlineData(14)]
    [InlineData(15)]
    public void CompareTo_follows_the_uuid_byte_order_at_every_position(int index)
    {
        // Der 48-Bit-Zeitstempel einer UUIDv7 liegt in den Bytes 0..5, und .NET legt
        // eine Guid intern als int/short/short/byte[8] ab. Sortierte Bezeichner sind
        // nur dann wirklich zeitlich sortiert, wenn der Vergleich der RFC-Bytefolge
        // folgt und nicht dieser Feldaufteilung — an jeder einzelnen Byteposition.
        var lower = TId.From(GuidWithSingleByte(index, 0x00));
        var higher = TId.From(GuidWithSingleByte(index, 0x80));

        lower.CompareTo(higher).ShouldBeLessThan(0);
        higher.CompareTo(lower).ShouldBeGreaterThan(0);
    }

    [Fact]
    public void Sorting_a_sequence_yields_creation_order()
    {
        var created = Enumerable.Range(0, 50)
            .Select(second => TId.From(Guid.CreateVersion7(
                new DateTimeOffset(2026, 6, 1, 12, 0, 0, TimeSpan.Zero).AddSeconds(second))))
            .ToArray();

        var shuffled = created.OrderBy(_ => Random.Shared.Next()).ToArray();

        shuffled.Order().ShouldBe(created);
    }

    private static Guid GuidWithSingleByte(int index, byte value)
    {
        Span<byte> bytes = stackalloc byte[16];
        bytes[index] = value;
        return new Guid(bytes, bigEndian: true);
    }
}
