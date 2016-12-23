namespace EvilBaschdi.Core.DotNetExtensions
{
    /// <summary>
    /// </summary>
    /// <typeparam name="TIn1"></typeparam>
    /// <typeparam name="TIn2"></typeparam>
    /// <typeparam name="TIn3"></typeparam>
    /// <typeparam name="TIn4"></typeparam>
    /// <typeparam name="TIn5"></typeparam>
    public interface IRunFor5<in TIn1, in TIn2, in TIn3, in TIn4, in TIn5>
    {
        /// <summary>
        /// </summary>
        void Run(TIn1 valueIn1, TIn2 valueIn2, TIn3 valueIn3, TIn4 valueIn4, TIn5 valueIn5);
    }
}