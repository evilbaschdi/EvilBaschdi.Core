using System.Threading.Tasks;

namespace EvilBaschdi.Core
{
    /// <summary>
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TIn"></typeparam>
    // ReSharper disable once UnusedType.Global
    public interface IValueForAsync<in TIn, TResult>
    {
        /// <summary>
        /// </summary>
        // ReSharper disable once UnusedMember.Global
        Task<TResult> ValueForAsync(TIn value);
    }
}