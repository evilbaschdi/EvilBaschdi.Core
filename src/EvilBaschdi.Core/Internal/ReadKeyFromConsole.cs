namespace EvilBaschdi.Core.Internal;

/// <inheritdoc />
// ReSharper disable once UnusedType.Global
public class ReadKeyFromConsole : IReadKeyFromConsole
{
    /// <summary>
    ///     Constructor
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public string ValueFor([NotNull] string key)
    {
        ArgumentNullException.ThrowIfNull(key);

        Console.Write($"{key}: ");

        var retry = false;
        var value = string.Empty;

        while (string.IsNullOrWhiteSpace(value))
        {
            if (retry)
            {
                Console.WriteLine("Please enter a valid value!");
            }

            value = Console.ReadLine();
            retry = true;
        }

        return value;
    }
}