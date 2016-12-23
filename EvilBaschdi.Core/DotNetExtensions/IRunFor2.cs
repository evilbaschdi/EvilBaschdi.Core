namespace EvilBaschdi.Core.DotNetExtensions
{
    /// <summary>
    /// </summary>
    /// <typeparam name="TIn1"></typeparam>
    /// <typeparam name="TIn2"></typeparam>
    public interface IRunFor2<in TIn1, in TIn2>
    {
        /// <summary>
        /// </summary>
        void Run(TIn1 valueIn1, TIn2 valueIn2);
    }
}