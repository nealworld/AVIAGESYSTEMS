
/*----------------------------------------------------------------------------------------

	Rfm2gDrv.h
	 rfm2G card device driver C++ version for Csharp application
	 the generated dll will be used as DllImport binariy.
     The head file is the interface for Csharp dll.
	History (Newest note first):
		1/22/14	Julain Xie
			Initial creation.
		mm/dd/yy	xxx		
		
	To Do (Newest note first):			<-- Optional!
		mm/dd/yy	xxx		
			Item we need to do in the future. (newest note first)

			--- PROPRIETARY AND CONFIDENTIALITY NOTICE ---
	The information contained herein is confidential and proprietary to
	AVIAGE SYSTEMS, and shall not be reproduced,
	distributed, modified, or disclosed in whole or in part or used for any
	design or manufacture except by an authorized user who possesses direct
	written authorization from AVIAGE SYSTEMS and
	who is obligated to maintain the confidentiality of the information.

	Copyright 2013, 2014, 2015, AVIAGE SYSTEMS, All rights reserved.
----------------------------------------------------------------------------------------*/

#pragma once;
#define DLL_C_API extern "C" __declspec(dllexport)

#if (defined(WIN32))

#include "rfm2g_windows.h"
#endif
#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include "rfm2g_api.h"

/*
* RMF card path prefix
*/
#if defined(WIN32)
       const char* DEVICE_PREFIX =  "\\\\.\\rfm2g";
#else
#error Please define DEVICE_PREFIX for your driver
#endif
/*
* Define the maximum device id in this machine
*/
const int  MAX_DEVICE_ID  = 9;

typedef enum RFM2G_STATUS_ENUM_EXPORT
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
} RFM2G_STATUS_EXPORT;

typedef enum RFM2G_EventType_Enum_Export
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
}  RFM2G_EVENTTYPE_EXPORT;

class RMFDeviceInterface
{
private :

    RFM2GHANDLE mHandle;
    
    RFM2G_BOOL mDeviceOpened;
    
public:
    RMFDeviceInterface() 
    {
    
    };
   // RMFDeviceInterface();

    RFM2G_STATUS openDevice( RFM2G_UINT32 aDeviceId );

    RFM2G_STATUS read( RFM2G_UINT32 aOffset,void * aBuffer,RFM2G_UINT32 aByteLength );

    RFM2G_STATUS write( RFM2G_UINT32 aOffset,void * aBuffer,RFM2G_UINT32 aByteLength );

    RFM2G_STATUS getDeviceID(RFM2G_NODE* aNodeID);

    RFM2G_STATUS waitEvent(RFM2G_EVENTTYPE_EXPORT aEventType,RFM2G_UINT32 aTimeOut,RFM2G_UINT32* aExtendedInfo,RFM2G_NODE*  aNodeId);
    
    RFM2G_STATUS sendEvent(RFM2G_NODE aTargetNodeId,RFM2G_EVENTTYPE_EXPORT aEventType,RFM2G_UINT32 aExtendedInfo );
    
    RFM2G_STATUS setDMAByteSwap( RFM2G_BOOL aByteSwap );
    
    RFM2G_STATUS getDMAByteSwap( RFM2G_BOOL* aByteSwap );
    
    RFM2G_STATUS closeDevice();
    
    ~RMFDeviceInterface();
};




RMFDeviceInterface* gDevice=NULL;

//extern "C" __declspec(dllexport) char* CSharpErrorMsg(RFM2G_STATUS_EXPORT aMsg);

extern "C" __declspec(dllexport) RFM2G_STATUS_EXPORT CSharpRFM2gOpen( RFM2G_UINT32 DeviceId );

DLL_C_API RFM2G_STATUS_EXPORT CSharpRFM2gRead(RFM2G_UINT32 aOffset,void * aBuffer,RFM2G_UINT32 aByteLength );

DLL_C_API RFM2G_STATUS_EXPORT CSharpRFM2gWrite(RFM2G_UINT32 aOffset,void * aBuffer,RFM2G_UINT32 aByteLength );

DLL_C_API RFM2G_STATUS_EXPORT CSharpRFM2gSetDMAByteSwap( RFM2G_BOOL aByteSwap );

DLL_C_API RFM2G_STATUS_EXPORT CSharpRFM2gGetDMAByteSwap( RFM2G_BOOL* aByteSwap );

DLL_C_API RFM2G_STATUS_EXPORT CsharpEventWait( RFM2G_EVENTTYPE_EXPORT aEventType,RFM2G_UINT32 aTimeOut,RFM2G_UINT32* ExtendedInfo,RFM2G_NODE*  NodeId );

DLL_C_API RFM2G_STATUS_EXPORT CsharpEventSend( RFM2G_NODE aTargetNodeId,RFM2G_EVENTTYPE_EXPORT aEventType,RFM2G_UINT32 aExtendedInfo );

//DLL_C_API RFM2G_STATUS_EXPORT CSharpGetRFM2gNodeID(RFM2G_NODE* aNodeID);

DLL_C_API RFM2G_STATUS_EXPORT CSharpRFM2gClose(void );

DLL_C_API void CSharpDeleteRFMDevice(void);