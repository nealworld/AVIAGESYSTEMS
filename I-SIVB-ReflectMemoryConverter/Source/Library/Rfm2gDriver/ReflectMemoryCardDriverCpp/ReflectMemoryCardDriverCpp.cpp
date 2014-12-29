// ReflectMemoryCardDriverCpp.cpp : Defines the exported functions for the DLL application.
//
#include "stdafx.h"
#include "Rfm2gDrv.h"
#include<string.h>
#include <stdio.h>
///*----------------------------------------------------------------------------------------
//
//	ReflectMemoryCardDriverCpp.cpp
//	  rfm2G card device driver C++ version for Csharp application
//	 the generated dll will be used as DllImport binariy.
//	History (Newest note first):
//		1/22/14	Julain Xie
//			Initial creation.
//		mm/dd/yy	xxx		
//		
//	To Do (Newest note first):			<-- Optional!
//		mm/dd/yy	xxx		
//			Item we need to do in the future. (newest note first)
//
//			--- PROPRIETARY AND CONFIDENTIALITY NOTICE ---
//	The information contained herein is confidential and proprietary to
//	AVIAGE SYSTEMS, and shall not be reproduced,
//	distributed, modified, or disclosed in whole or in part or used for any
//	design or manufacture except by an authorized user who possesses direct
//	written authorization from AVIAGE SYSTEMS and
//	who is obligated to maintain the confidentiality of the information.
//
//	Copyright 2013, 2014, 2015, AVIAGE SYSTEMS, All rights reserved.
//----------------------------------------------------------------------------------------*/
extern "C" __declspec(dllexport) int xxx()
{
    return(2);
};
DLL_C_API RFM2G_STATUS_EXPORT CSharpRFM2gOpen(RFM2G_UINT32 DeviceId )
{
    RFM2G_STATUS_EXPORT   result=STATUS_NOT_SUPPORTED ;
    printf("xxxx");
   
    if(gDevice==NULL)
    {
        gDevice= new RMFDeviceInterface();
    }
    
    result=( RFM2G_STATUS_EXPORT ) ( gDevice->openDevice( DeviceId ));
    
    return( result );
};



//
//
//
//char* CSharpErrorMsg(RFM2G_STATUS_EXPORT aMsg )
//{
//    if(aMsg==STATUS_ERROR_NULL)
//    {
//        
//        return("NULL: NO DEVICE IS AVALIABLE ,PLEASE OPEN IT FIRST");
//    }
//    else if(aMsg==STATUS_MAX_ERROR_CODE)
//    {
//        return("ERROR ! EXCEED THE MAX DEVICE ID DEFINED");
//    }
//    else
//    {
//        return("Test!");
//        //return(RFM2gErrorMsg((RFM2G_STATUS)aMsg));
//    }
//};


RFM2G_STATUS_EXPORT CSharpRFM2gRead(RFM2G_UINT32 aOffset,void * aBuffer,RFM2G_UINT32 aByteLength )
{
    RFM2G_STATUS_EXPORT  result=STATUS_SUCCESS;
    if(gDevice==NULL)
    {
        return(STATUS_ERROR_NULL);
    }
    result=(RFM2G_STATUS_EXPORT) (gDevice->read(aOffset,aBuffer,aByteLength));
    return(result);
};


RFM2G_STATUS_EXPORT CSharpRFM2gWrite(RFM2G_UINT32 aOffset,void * aBuffer,RFM2G_UINT32 aByteLength )
{
    RFM2G_STATUS_EXPORT  result=STATUS_SUCCESS;
    if(gDevice==NULL)
    {
        return(STATUS_ERROR_NULL);
    }
    result=(RFM2G_STATUS_EXPORT) (gDevice->write(aOffset,aBuffer,aByteLength));
    return(result);
};

RFM2G_STATUS_EXPORT CSharpRFM2gClose(void )
{
    RFM2G_STATUS_EXPORT  result=STATUS_SUCCESS;
    if(gDevice==NULL)
    {
        return(STATUS_ERROR_NULL);
    }
    result=(RFM2G_STATUS_EXPORT) (gDevice->closeDevice());
    return(result);
};

void CSharpDeleteRFMDevice()
{
    if(gDevice!=NULL)
    {
        delete gDevice;
        gDevice=NULL;
    }
};

RFM2G_STATUS_EXPORT CSharpGetRFM2gNodeID(RFM2G_NODE* aNodeID)
{
    RFM2G_STATUS_EXPORT   result=STATUS_SUCCESS;

    if(gDevice==NULL)
    {
        return(STATUS_ERROR_NULL);
    }
    result=(RFM2G_STATUS_EXPORT) (gDevice->getDeviceID(aNodeID));
    return(result);
};

RFM2G_STATUS_EXPORT CsharpEventWait( RFM2G_EVENTTYPE_EXPORT aEventType,RFM2G_UINT32 aTimeOut,RFM2G_UINT32* ExtendedInfo,UINT16*  NodeId )
{
    RFM2G_STATUS_EXPORT   result=STATUS_SUCCESS;
    if(gDevice==NULL)
    {
        return(STATUS_ERROR_NULL);
    }
    result=(RFM2G_STATUS_EXPORT) (gDevice->waitEvent(aEventType,aTimeOut,ExtendedInfo,NodeId));
    return(result);
};

RFM2G_STATUS_EXPORT CsharpEventSend( RFM2G_NODE aTargetNodeId,RFM2G_EVENTTYPE_EXPORT aEventType,RFM2G_UINT32 aExtendedInfo )
{
    RFM2G_STATUS_EXPORT   result;
    if(gDevice==NULL)
    {
        return(STATUS_ERROR_NULL);
    }
    result=(RFM2G_STATUS_EXPORT) (gDevice->sendEvent(aTargetNodeId,aEventType,aExtendedInfo));
    return(result);
};   

RFM2G_STATUS_EXPORT CSharpRFM2gSetDMAByteSwap( RFM2G_BOOL aByteSwap )
{
    RFM2G_STATUS_EXPORT   result= STATUS_SUCCESS;
     if(gDevice==NULL)
    {
         return(STATUS_ERROR_NULL);
    }
     result=(RFM2G_STATUS_EXPORT) (gDevice->setDMAByteSwap((RFM2G_BOOL)aByteSwap));
     return(result);
};

RFM2G_STATUS_EXPORT CSharpRFM2gGetDMAByteSwap( RFM2G_BOOL* aByteSwap )
{
    RFM2G_STATUS_EXPORT   result= STATUS_SUCCESS;
     if(gDevice==NULL)
    {
         return(STATUS_ERROR_NULL);
    }
     result=(RFM2G_STATUS_EXPORT) (gDevice->getDMAByteSwap((RFM2G_BOOL*)aByteSwap));
     return(result);
};




RMFDeviceInterface::~RMFDeviceInterface()
{
    if(mHandle!=NULL)
    {
        if(mDeviceOpened)
        {
            closeDevice();
        }
        delete mHandle;
        mHandle=NULL;
        mDeviceOpened=false;
    }
}

RFM2G_STATUS RMFDeviceInterface::openDevice( RFM2G_UINT32 aDeviceId )
{
    // Return codes from RFM2g API calls 
    RFM2G_STATUS  result = RFM2G_SUCCESS; 
    int llength = strlen(DEVICE_PREFIX);
    int lChars=(aDeviceId/10)+1;
    char* lDeviceId = new char[lChars+1];
    char* lDeviceStr =new char[llength+lChars+1];
    itoa( aDeviceId, lDeviceId, 10 );
    if(aDeviceId>MAX_DEVICE_ID)
    {
        result= RFM2G_MAX_ERROR_CODE;
    }
    else
    {
         strcpy(lDeviceStr,DEVICE_PREFIX);
         strcpy(lDeviceStr+llength,lDeviceId);
         
         //printf("_______________\n");
         result = RFM2gOpen( lDeviceStr, &mHandle );
         if(result==RFM2G_SUCCESS)
         {
            // printf("Success\n");
             mDeviceOpened=true;
         }
         else
         {
             //printf("False\n");
             mDeviceOpened=false;
         
         }
    }
    return(result);
};

RFM2G_STATUS RMFDeviceInterface::read(RFM2G_UINT32 aOffset,void * aBuffer,RFM2G_UINT32 aByteLength)
{
    RFM2G_STATUS  result; 
    result = RFM2gRead( mHandle, aOffset, aBuffer, aByteLength );
    return(result);
}

RFM2G_STATUS RMFDeviceInterface::write( RFM2G_UINT32 aOffset,void * aBuffer,RFM2G_UINT32 aByteLength)
{
    RFM2G_STATUS  result; 
    result = RFM2gWrite( mHandle, aOffset, aBuffer, aByteLength );
    return(result);
}

RFM2G_STATUS RMFDeviceInterface::setDMAByteSwap(  RFM2G_BOOL aByteSwap )
{
    RFM2G_STATUS  result; 
    result = RFM2gSetDMAByteSwap( mHandle, aByteSwap );
    return(result);
}

RFM2G_STATUS RMFDeviceInterface::getDMAByteSwap(  RFM2G_BOOL* aByteSwap )
{
    RFM2G_STATUS  result; 
    result = RFM2gGetDMAByteSwap( mHandle, aByteSwap );
    return(result);
}

RFM2G_STATUS RMFDeviceInterface::getDeviceID(RFM2G_NODE* aNodeID)
{
    RFM2G_STATUS result;  
    result = RFM2gNodeID( mHandle, aNodeID );
    return(result);
};

RFM2G_STATUS RMFDeviceInterface::closeDevice()
{
    RFM2G_STATUS   result;   
    result = RFM2gClose( &mHandle );
    if(result==RFM2G_SUCCESS)
    {
        mDeviceOpened=false;
    }
    return(result);
};

RFM2G_STATUS RMFDeviceInterface::waitEvent(RFM2G_EVENTTYPE_EXPORT aEventType,RFM2G_UINT32 aTimeOut,RFM2G_UINT32* ExtendedInfo,UINT16*  NodeId)
{
    RFM2GEVENTINFO lEventInfo;
    lEventInfo.Event = (RFM2GEVENTTYPE)aEventType;  // We'll wait on this interrupt 
    lEventInfo.Timeout = aTimeOut; 
    RFM2G_STATUS   result; 
    result = RFM2gEnableEvent( mHandle,  (RFM2GEVENTTYPE)aEventType );
    if( result != RFM2G_SUCCESS )
    {
        return( result );
    }
    result = RFM2gWaitForEvent( mHandle, &lEventInfo );
    *ExtendedInfo=lEventInfo.ExtendedInfo;
    *NodeId=lEventInfo.NodeId;
    return(result);
};

RFM2G_STATUS RMFDeviceInterface::sendEvent(RFM2G_NODE aTargetNodeId,RFM2G_EVENTTYPE_EXPORT aEventType,RFM2G_UINT32 aExtendedInfo )
{
    RFM2G_STATUS  result; 
    result=RFM2gSendEvent( mHandle, aTargetNodeId, (RFM2GEVENTTYPE)aEventType, aExtendedInfo );
    return(result);
};

