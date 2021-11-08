using System.Threading.Tasks;

namespace EvilBaschdi.Core
{
    /// <summary>
    /// </summary>
    /// <typeparam name="TIn"></typeparam>

    // ReSharper disable once UnusedType.Global
    public interface IRunForAsync<in TIn>
    {
        /// <summary>
        /// </summary>
        // ReSharper disable once UnusedMember.Global
        Task RunForAsync(TIn value);
    }
}