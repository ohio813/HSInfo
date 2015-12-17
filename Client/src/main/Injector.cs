
/*
 * File: Injector.cs
 * Notes:
 */

using System.Runtime.InteropServices;

namespace HSInfo {
    namespace Client {
        class Injector {

            [DllImport(@"mono-assembly-injector.dll", CallingConvention = CallingConvention.Cdecl)]
            private extern static int TryInject([MarshalAsAttribute(UnmanagedType.LPWStr)] string target, 
                [MarshalAsAttribute(UnmanagedType.LPWStr)] string dll, 
                [MarshalAsAttribute(UnmanagedType.LPWStr)] string nameSpace,
                [MarshalAsAttribute(UnmanagedType.LPWStr)] string className,
                [MarshalAsAttribute(UnmanagedType.LPWStr)] string methodName);

            public static int Inject(string target, string dll, string nameSpace, string className, string methodName) {
                return TryInject(target, dll, nameSpace, className, methodName);
            }

        }
    }
}
