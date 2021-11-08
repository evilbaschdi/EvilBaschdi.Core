namespace EvilBaschdi.Core.Internal
{
    /// <inheritdoc cref="IRunForAsync2{TIn1,TIn2}" />
    /// <inheritdoc cref="IValueForAsync2{TIn1,TIn2,TResult}" />
    public interface ICopyDirectory : IRunForAsync2<string, string>, IValueForAsync2<string, string, int>
    {
    }
}