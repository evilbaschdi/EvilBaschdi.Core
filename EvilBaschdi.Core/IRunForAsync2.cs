using System.Threading.Tasks;

namespace EvilBaschdi.Core
{
    /// <summary>
    /// </summary>
    /// <typeparam name="TIn1"></typeparam>
    /// <typeparam name="TIn2"></typeparam>
    // ReSharper disable once UnusedType.Global
    public interface IRunForAsync2<in TIn1, in TIn2>
    {
        /// <summary>
        /// </summary>
        // ReSharper disable once UnusedMember.Global
        Task RunForAsync(TIn1 valueIn1, TIn2 valueIn2);
    }
}