using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Rfm2gDriverCsharp
{
    public enum RFM2G_STATUS_ENUM_EXPORT
    {
        STATUS_SUCCESS,              /* No error                      */
        STATUS_NOT_IMPLEMENTED,      /* Capability not implemented    */
        STATUS_DRIVER_ERROR,         /* Nonspecific error             */
        STATUS_TIMED_OUT,            /* A wait timed out              */
        STATUS_LOW_MEMORY,           /* A memory allocation failed    */
        STATUS_MEM_NOT_MAPPED,       /* Memory is not mapped          */
        STATUS_OS_ERROR,             /* OS defined error              */
        STATUS_EVENT_IN_USE,         /* Event is being waited on      */
        STATUS_NOT_SUPPORTED,        /* Capability not supported      */
        STATUS_NOT_OPEN,             /* Device not open               */
        STATUS_NO_STATUS_BOARD,       /* Device not open               */
        STATUS_BAD_PARAMETER_1,      /* Function Parameter 1 invalid  */
        STATUS_BAD_PARAMETER_2,      /* Function Parameter 2 invalid  */
        STATUS_BAD_PARAMETER_3,      /* Function Parameter 3 invalid  */
        STATUS_BAD_PARAMETER_4,      /* Function Parameter 4 invalid  */
        STATUS_BAD_PARAMETER_5,      /* Function Parameter 5 invalid  */
        STATUS_BAD_PARAMETER_6,      /* Function Parameter 6 invalid  */
        STATUS_BAD_PARAMETER_7,      /* Function Parameter 7 invalid  */
        STATUS_BAD_PARAMETER_8,      /* Function Parameter 8 invalid  */
        STATUS_BAD_PARAMETER_9,      /* Function Parameter 9 invalid  */
        STATUS_OUT_OF_RANGE,         /* Board offset/range exceeded   */
        STATUS_MAP_NOT_ALLOWED,      /* Board Offset is not legal     */
        STATUS_LINK_TEST_FAIL,       /* Link Test failed              */
        STATUS_MEM_READ_ONLY,        /* Outside of User Memory Area   */
        STATUS_UNALIGNED_OFFSET,     /* Offset not aligned for width  */
        STATUS_UNALIGNED_ADDRESS,    /* Address not aligned for width */
        STATUS_LSEEK_ERROR,          /* lseek operation failed        */
        STATUS_READ_ERROR,           /* read operation failed         */
        STATUS_WRITE_ERROR,          /* write operation failed        */
        STATUS_HANDLE_NOT_NULL,      /* Non-NULL handle init request  */
        STATUS_MODULE_NOT_LOADED,    /* Driver not loaded             */
        STATUS_NOT_ENABLED,          /* Interrupt not enabled         */
        STATUS_ALREADY_ENABLED,      /* Interrupt already enabled     */
        STATUS_EVENT_NOT_IN_USE,     /* No process waiting on int     */
        STATUS_BAD_STATUS_BOARD_ID,   /* Invalid RFM2G Board ID        */
        STATUS_NULL_DESCRIPTOR,      /* RFM2G Handle is null          */
        STATUS_WAIT_EVENT_CANCELED,  /* Event Wait Canceled           */
        STATUS_DMA_FAILED,           /* DMA failed                    */
        STATUS_NOT_INITIALIZED,      /* User has not called RFM2gInit() yet */
        STATUS_UNALIGNED_LENGTH,     /* Data transfer length not 4 byte aligned */
        STATUS_SIGNALED,             /* Signal from OS                */
        STATUS_NODE_ID_SELF,         /* Cannot send event to self     */
        STATUS_MAX_ERROR_CODE,
        STATUS_ERROR_NULL   /*NULL NO DEVICE IS AVALIABLE ,PLEASE OPEN IT FIRST*/
    } ;

    public enum RFM2G_EVENTTYPE_EXPORT
    {
        EVENT_RESET,       /* Reset Interrupt                                */
        EVENT_INTR1,       /* Network Interrupt 1                            */
        EVENT_INTR2,       /* Network Interrupt 2                            */
        EVENT_INTR3,       /* Network Interrupt 3                            */
        EVENT_INTR4,       /* Network Interrupt 4 (Init interrupt)           */
        EVENT_BAD_DATA,    /* Bad Data                                       */
        EVENT_RXFIFO_FULL, /* RX FIFO has been full one or more times.       */
        EVENT_ROGUE_PKT,   /* the board detected and removed a rogue packet. */
        EVENT_RXFIFO_AFULL,/* RX FIFO has been almost full one or more times.*/
        EVENT_SYNC_LOSS,   /* Sync loss has occured one of more times.       */
        EVENT_MEM_WRITE_INHIBITED, /* Write to local memory was attempted and inhibited */
        EVENT_LOCAL_MEM_PARITY_ERR, /* Parity errors have been detected on local memory accesses */
        EVENT_LAST         /* Number of Events                               */
    } ;

    public class Rfm2gDriverCsharp
    {
        const string gDllroot = @"D:\Sim\Product\I-SIVB-ReflectMemoryConverter\Source\Library\Release\ReflectMemoryCardDriverCpp.dll";
        //const string gDllroot = @"ReflectMemoryCardDriverCpp.dll";
        public static RFM2G_STATUS_ENUM_EXPORT CSharpRFM2gReadSafe( UInt32 aOffset, ref byte[] aBuffer, UInt32 aByteLength )
        {
            unsafe
            {
                RFM2G_STATUS_ENUM_EXPORT lResult;
                fixed ( byte* RECEIVEDARA2 = &aBuffer[0] )
                {
                    lResult = CSharpRFM2gRead( aOffset, RECEIVEDARA2, aByteLength );
                    return ( lResult );
                }
            }
        }
        [DllImport( gDllroot, EntryPoint = "CSharpRFM2gOpen", CallingConvention = CallingConvention.Cdecl )]
        public extern static RFM2G_STATUS_ENUM_EXPORT CSharpRFM2gOpen( int aDeviceId );
        
        [DllImport( gDllroot, EntryPoint = "CSharpRFM2gRead", CallingConvention = CallingConvention.Cdecl )]
        private unsafe extern static RFM2G_STATUS_ENUM_EXPORT CSharpRFM2gRead( UInt32 aOffset, byte* aBuffer, UInt32 aByteLength );//need to validate
        
        //public static  RFM2G_STATUS_ENUM_EXPORT CSharpRFM2gReadSafe( UInt32 aOffset, ref byte[] aBuffer, UInt32 aByteLength )
        //{
        //    unsafe
        //    {
        //        RFM2G_STATUS_ENUM_EXPORT lResult;
        //        fixed ( byte* RECEIVEDARA2 = &aBuffer[0] )
        //        {
        //            lResult = CSharpRFM2gRead( aOffset, RECEIVEDARA2, aByteLength );
        //            return ( lResult );
        //        }
        //    }
        //}

        [DllImport( gDllroot, EntryPoint = "CSharpRFM2gWrite", CallingConvention = CallingConvention.Cdecl )]
        public extern static RFM2G_STATUS_ENUM_EXPORT CSharpRFM2gWrite( UInt32 aOffset, byte[] aBuffer, UInt32 aByteLength );

        [DllImport( gDllroot, EntryPoint = "CsharpEventWait", CallingConvention = CallingConvention.Cdecl )]
        public extern static RFM2G_STATUS_ENUM_EXPORT CsharpEventWait( RFM2G_EVENTTYPE_EXPORT aEventType, UInt32 aTimeOut, ref UInt32 ExtendedInfo, ref UInt16 NodeId );

        [DllImport( gDllroot, EntryPoint = "CsharpEventSend", CallingConvention = CallingConvention.Cdecl )]
        public extern static RFM2G_STATUS_ENUM_EXPORT CsharpEventSend( UInt16 aTargetNodeId, RFM2G_EVENTTYPE_EXPORT aEventType, UInt32 aExtendedInfo );

        //[DllImport( gDllroot, EntryPoint = "CSharpGetRFM2gNodeID", CallingConvention = CallingConvention.StdCall )]
        //public extern static RFM2G_STATUS_ENUM_EXPORT CSharpGetRFM2gNodeID( ref UInt16 aTargetNodeId );

        [DllImport( gDllroot, EntryPoint = "CSharpRFM2gGetDMAByteSwap", CallingConvention = CallingConvention.Cdecl )]
        public extern static RFM2G_STATUS_ENUM_EXPORT CSharpRFM2gGetDMAByteSwap( ref bool aSwap );

        [DllImport( gDllroot, EntryPoint = "CSharpRFM2gSetDMAByteSwap", CallingConvention = CallingConvention.Cdecl )]
        public extern static RFM2G_STATUS_ENUM_EXPORT CSharpRFM2gSetDMAByteSwap( bool aSwap );

        [DllImport( gDllroot, EntryPoint = "CSharpRFM2gClose", CallingConvention = CallingConvention.Cdecl )]
        public extern static RFM2G_STATUS_ENUM_EXPORT CSharpRFM2gClose();

        [DllImport( gDllroot, EntryPoint = "CSharpDeleteRFMDevice", CallingConvention = CallingConvention.Cdecl )]
        public extern static void CSharpDeleteRFMDevice();


    }
}
