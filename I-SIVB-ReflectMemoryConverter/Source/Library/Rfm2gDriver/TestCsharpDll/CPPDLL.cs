using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using Rfm2gDriverCsharp;
namespace TestCsharpDll
{
    class CPPDLL
    {
        const string Lstr = @"\..\..\..\..\Release\ReflectMemoryCardDriverCpp.dll";//@"D:\Sim\Product\I-SIVB-ReflectMemoryConverter\Source\Library\Release\ReflectMemoryCardDriverCpp.dll";
        //@"D:\Sim\Product\I-SIVB ReflectMemoryConverter\Library\x64\Release\ReflectMemoryCardDriverCpp.dll";
         
        

      [DllImport( Lstr, EntryPoint = "CSharpRFM2gOpen", CallingConvention = CallingConvention.Cdecl )]

      public static extern int CSharpRFM2gOpen( int DeviceId );

        

      //  [DllImport( Lstr, EntryPoint = "CSharpRFM2gWrite", CallingConvention = CallingConvention.Cdecl )]
       // public extern static RFM2G_STATUS_ENUM_EXPORT CSharpRFM2gWrite( UInt32 aOffset, byte[] aBuffer, UInt32 aByteLength );


    }
}
