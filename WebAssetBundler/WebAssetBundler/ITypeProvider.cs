
namespace WebAssetBundler
{
    using System;
    using System.Collections.Generic;

    public interface ITypeProvider
    {
        Type[] GetAllTypes();
        IEnumerable<Type> GetImplementationTypes(Type baseType);
    }
}
