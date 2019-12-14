using System.Threading.Tasks;

namespace EvilBaschdi.Core
{
    /// <summary>
    /// </summary>
    public interface ITaskRunFor<in TIn>
    {
        /// <summary>
        /// </summary>
        Task TaskRunFor(TIn value);
    }
}