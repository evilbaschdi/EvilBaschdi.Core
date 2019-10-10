using System.Threading.Tasks;

namespace EvilBaschdi.Core
{
    /// <summary>
    /// </summary>
    public interface ITaskValueFor<in TIn, TResult>
    {
        /// <summary>
        /// </summary>
        Task<TResult> TaskValueFor(TIn value);
    }
}