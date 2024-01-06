namespace EvilBaschdi.Core.Internal;

/// <inheritdoc />
/// <summary>
///     Constructor
/// </summary>
/// <param name="readKeyFromConsole"></param>
/// <exception cref="ArgumentNullException"></exception>
// ReSharper disable once UnusedType.Global
public class ValidateValue(
    [NotNull] IReadKeyFromConsole readKeyFromConsole) : IValidateValue
{
    private readonly IReadKeyFromConsole _readKeyFromConsole = readKeyFromConsole ?? throw new ArgumentNullException(nameof(readKeyFromConsole));

    /// <inheritdoc />
    public void RunFor([NotNull] string key, [NotNull] string s)
    {
        ArgumentNullException.ThrowIfNull(key);
        ArgumentNullException.ThrowIfNull(s);
        Console.WriteLine($"{key}: {s}");
        var response = _readKeyFromConsole.ValueFor("Correct [y] / [n]").ToLower();

        if (response.Contains("y"))
        {
            return;
        }

        Console.WriteLine("Exiting...");
        Environment.Exit(0);
    }
}