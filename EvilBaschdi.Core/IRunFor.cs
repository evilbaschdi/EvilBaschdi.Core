namespace EvilBaschdi.Core
{
    /// <summary>
    /// </summary>
    /// <typeparam name="TIn"></typeparam>
    public interface IRunFor<in TIn>
    {
        /// <summary>
        /// </summary>
        void RunFor(TIn value);
    }
}