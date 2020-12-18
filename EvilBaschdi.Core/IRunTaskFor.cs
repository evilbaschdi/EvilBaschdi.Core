using System.Threading.Tasks;

namespace EvilBaschdi.Core
{
    /// <summary>
    /// </summary>
    // ReSharper disable once UnusedType.Global
    public interface IRunTaskFor<in TIn>
    {
        /// <summary>
        /// </summary>
        // ReSharper disable once UnusedMember.Global
        Task RunTaskFor(TIn value);
    }
}