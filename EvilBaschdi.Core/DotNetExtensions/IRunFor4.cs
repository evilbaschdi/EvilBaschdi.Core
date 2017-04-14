namespace EvilBaschdi.Core.DotNetExtensions
{
    /// <summary>
    /// </summary>
    /// <typeparam name="TIn1"></typeparam>
    /// <typeparam name="TIn2"></typeparam>
    /// <typeparam name="TIn3"></typeparam>
    /// <typeparam name="TIn4"></typeparam>
    public interface IRunFor4<in TIn1, in TIn2, in TIn3, in TIn4>
    {
        /// <summary>
        /// </summary>
        void RunFor(TIn1 valueIn1, TIn2 valueIn2, TIn3 valueIn3, TIn4 valueIn4);
    }
}