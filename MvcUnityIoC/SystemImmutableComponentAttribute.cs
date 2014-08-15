using System;

namespace MvcUnityIoC
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = false, Inherited = true)]
    public class SystemImmutableComponentAttribute : Attribute
    {
    }
}