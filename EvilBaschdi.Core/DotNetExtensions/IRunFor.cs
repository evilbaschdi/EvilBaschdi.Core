namespace EvilBaschdi.Core.DotNetExtensions
{
    /// <summary>
    /// </summary>
    /// <typeparam name="TIn"></typeparam>
    public interface IRunFor<in TIn>
    {
        /// <summary>
        /// </summary>
        void Run(TIn value);
    }
}