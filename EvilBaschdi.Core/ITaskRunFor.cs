using System.Threading.Tasks;

namespace EvilBaschdi.Core
{
    /// <summary>
    /// </summary>
    // ReSharper disable once UnusedType.Global
    public interface ITaskRunFor<in TIn>
    {
        /// <summary>
        /// </summary>
        // ReSharper disable once UnusedMember.Global
        Task TaskRunFor(TIn value);
    }
}