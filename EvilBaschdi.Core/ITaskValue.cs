using System.Threading.Tasks;

namespace EvilBaschdi.Core
{
    /// <summary>
    /// </summary>
    public interface ITaskValue<TResult>
    {
        /// <summary>
        /// </summary>
        Task<TResult> TaskValue();
    }
}