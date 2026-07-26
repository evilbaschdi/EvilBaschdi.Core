namespace EvilBaschdi.Core.Extensions;

/// <summary>
/// </summary>
// ReSharper disable once UnusedType.Global
public static class ObjectExtensions
{
    /// <param name="obj"></param>
    extension(object obj)
    {
        /// <summary>
        ///     obj:null => true
        /// </summary>
        /// <returns></returns>
        [ContractAnnotation("obj:null => true")]
        // ReSharper disable once UnusedMember.Global
        public bool IsNull()
        {
            return obj is null;
        }

        /// <summary>
        ///     obj:null => false
        /// </summary>
        /// <returns></returns>
        [ContractAnnotation("obj:null => false")]
        // ReSharper disable once UnusedMember.Global
        public bool IsNotNull()
        {
            return obj is not null;
        }
    }
}