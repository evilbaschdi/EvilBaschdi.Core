namespace EvilBaschdi.Core.Internal;

/// <inheritdoc />
// ReSharper disable once UnusedType.Global
public class ValidateValue : IValidateValue
{
    private readonly IReadKeyFromConsole _readKeyFromConsole;

    /// <summary>
    ///     Constructor
    /// </summary>
    /// <param name="readKeyFromConsole"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public ValidateValue(IReadKeyFromConsole readKeyFromConsole)
    {
        _readKeyFromConsole = readKeyFromConsole ?? throw new ArgumentNullException(nameof(readKeyFromConsole));
    }

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