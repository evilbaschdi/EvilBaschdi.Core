using System.Threading.Tasks;

namespace EvilBaschdi.Core
{
    /// <summary>
    /// </summary>
    // ReSharper disable once UnusedType.Global
    public interface ITaskValue<TResult>
    {
        /// <summary>
        /// </summary>

        // ReSharper disable once UnusedMember.Global
        Task<TResult> TaskValue();
    }
}