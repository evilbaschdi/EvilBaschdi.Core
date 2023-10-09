namespace EvilBaschdi.Core.Internal;

/// <inheritdoc />
/// <summary>
///     Constructor
/// </summary>
/// <param name="readKeyFromConsole"></param>
/// <exception cref="ArgumentNullException"></exception>
// ReSharper disable once UnusedType.Global
public class ValidateValue(IReadKeyFromConsole readKeyFromConsole) : IValidateValue
{
    private readonly IReadKeyFromConsole _readKeyFromConsole = readKeyFromConsole ?? throw new ArgumentNullException(nameof(readKeyFromConsole));

    /// <inheritdoc />
    public void RunFor(string key, string s)
    {
        Console.WriteLine($"{key}: {s}");
        var response = _readKeyFromConsole.ValueFor("Correct [yes] / [no]").ToLower();

        if (response.Contains("yes"))
        {
            return;
        }

        Console.WriteLine("Exiting...");
        Environment.Exit(0);
    }
}