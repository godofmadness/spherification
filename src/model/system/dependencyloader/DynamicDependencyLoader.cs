using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Spherification.src.model.system.dependencyloader
{
    class DynamicDependencyLoader
    {
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int remoteFunction();


        public static void load(String path)
        {

            IntPtr pDll = NativeLoader.LoadLibrary(path);
            IntPtr pAddressOfFunctionToCall = NativeLoader.GetProcAddress(pDll, "remoteFunction");
            //oh dear, error handling here
            //if(pAddressOfFunctionToCall == IntPtr.Zero) 

            remoteFunction remoteFunction = (remoteFunction)Marshal.GetDelegateForFunctionPointer(
                                                                                    pAddressOfFunctionToCall,
                                                                                    typeof(remoteFunction));

            remoteFunction();

            bool result = NativeLoader.FreeLibrary(pDll);
            //remaining code here


        }
    }
}
