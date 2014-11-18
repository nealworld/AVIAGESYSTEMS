// DeviceQuery.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <windows.h>
#include "setupapi.h"
#include "Cfgmgr32.h"
#include <devguid.h> 
#include"tlhelp32.h"

#include <stdio.h>
#include <iostream>
#include <string>
#include "CommonSimCore.h"

using namespace std;

void SendConfiguration(string a3rdPartySWMessage, string aDriverMessage, string aPostfix);
void Get3rdPartySWConfiguration(char* pConfiguration);
void GetDriverConfiguration(char* pConfiguration, char* pParameter);

////////////////////////////////////////////////////////////////////////////////////////////////////
// Check the device is enabled or not
//
bool IsDeviceDisabled(DWORD dwDevID, HDEVINFO hDevInfo, DWORD &dwStatus)
{
    SP_DEVINFO_DATA DevInfoData = {sizeof(SP_DEVINFO_DATA)};
    DWORD dwDevStatus,dwProblem;

    if(!SetupDiEnumDeviceInfo(hDevInfo,dwDevID,&DevInfoData))
    {
        return FALSE;
    }

    if(CM_Get_DevNode_Status(&dwDevStatus,&dwProblem,DevInfoData.DevInst,0)!=CR_SUCCESS)
    {
        return FALSE;
    }

    dwStatus = dwProblem;

//    return ( (dwProblem == CM_PROB_FAILED_INSTALL));
    return true;
}


////////////////////////////////////////////////////////////////////////////////////////////////////
// Convert the driver version information to be readable
//
//For example: version = 1407379348914176，to 5.1.2600.0
//
void GetVersionFromLong( DWORDLONG version, char* pVersion)
{
    long baseNumber = 0xFFFF;
    long temp = 0L;
    int strSize = 100;
    for( int offset = 48; offset >= 0; offset -= 16 )
    {
        temp = (version >> offset) & baseNumber;
        _ltoa_s(temp,pVersion,strSize,10);
        while(*pVersion != '\0')
        {
            pVersion++;
            strSize--;
        }
        *pVersion++ = '.';
        strSize--;
    }
    --pVersion;
    *pVersion = '\0';
}

////////////////////////////////////////////////////////////////////////////////////////////////////
// Read the net card name and driver version
//
int FindCardWithClassGUID(const GUID* pGUID, char* pReturn, char* pPrefix, char* pPostfix)
{
    while(*pReturn != '\0')
        pReturn++;//the new information should append to the end

    HDEVINFO hDevInfo; 
    SP_DEVINFO_DATA DeviceInfoData; 
    DWORD i;
    bool bRet = false;
    bool bOk = false;

    //step1. Create a HDEVINFO with all present devices. 
    hDevInfo = SetupDiGetClassDevs(pGUID, 
        L"PCI", // Enumerator 
        0, 
        DIGCF_PRESENT);

    if (hDevInfo == INVALID_HANDLE_VALUE) 
    { 
        // Insert error handling here. 
        return bRet; 
    } 

    DWORD dwStatuts = -1;
    //step2. Enumerate through all devices in Set. 
    DeviceInfoData.cbSize = sizeof(SP_DEVINFO_DATA); 
    for (i=0; SetupDiEnumDeviceInfo(hDevInfo, i, &DeviceInfoData); i++) 
    { 
        if(SetupDiBuildDriverInfoList(hDevInfo, &DeviceInfoData, SPDIT_COMPATDRIVER))
        {
            SP_DRVINFO_DATA DriverInfoData;
            DriverInfoData.cbSize = sizeof(SP_DRVINFO_DATA); 
            if(SetupDiEnumDriverInfo(hDevInfo, &DeviceInfoData, SPDIT_COMPATDRIVER, 0, &DriverInfoData)) 
            {
                //printf("Desc:%S\n", DriverInfoData.Description);
                char pDesc[100] = {'\0'};
                WideCharToMultiByte( CP_ACP, 0, DriverInfoData.Description, -1, pDesc, 100, NULL, NULL );
                //printf("Desc:%s\n", pDesc);
                int i = 0;
                //copy the prefix first
                while(pPrefix[i] != '\0')
                {
                    *pReturn++ = pPrefix[i];
                    i++;
                }
                *pReturn++ = ',';
                
                //copy the driver description
                i = 0;
                while(pDesc[i] != '\0')
                {
                    *pReturn++ = pDesc[i];
                    i++;
                }
                *pReturn++ = ',';

                char pVersion[100] = {'\0'};
                GetVersionFromLong(DriverInfoData.DriverVersion, pVersion);
                //printf("Version:%s\n", pVersion);

                //copy the driver version
                i = 0;
                while(pVersion[i] != '\0')
                {
                    *pReturn++ = pVersion[i];
                    i++;
                }
                *pReturn++ = ',';

                //copy the postfix
                i = 0;
                while(pPostfix[i] != '\0')
                {
                    *pReturn++ = pPostfix[i];
                    i++;
                }
                *pReturn++ = '\n';
            }
        }

        //DWORD DataT; 
        //LPTSTR buffer = NULL; 
        //DWORD buffersize = 0; 
        //// Call function with null to begin with, 
        //// then use the returned buffer size 
        //// to Alloc the buffer. Keep calling until 
        //// success or an unknown failure. 
        //// 
        //while (!SetupDiGetDeviceRegistryProperty( 
        //    hDevInfo, 
        //    &DeviceInfoData, 
        //    SPDRP_DEVICEDESC, 
        //    &DataT, 
        //    (PBYTE)buffer, 
        //    buffersize, 
        //    &buffersize)) 
        //{ 
        //    if (GetLastError() == ERROR_INSUFFICIENT_BUFFER) 
        //    { 
        //        // Change the buffer size. 
        //        if (buffer) LocalFree(buffer); 
        //        buffer = (LPTSTR)LocalAlloc(LPTR,buffersize); 
        //    } 
        //    else 
        //    { 
        //        // Insert error handling here. 
        //        break; 
        //    } 
        //} 
        //printf("%S\n\n", buffer);

        //step3. find devices status
        //if (IsDeviceDisabled(i, hDevInfo, dwStatuts) && dwStatuts == 0)
        //{
        //}
        //printf( "SPDRP_DEVICEDESC:[%S] %d\n ",buffer, dwStatuts); 

        //if (buffer)
        //    LocalFree(buffer); 
    }

    // step4. Cleanup 
    SetupDiDestroyDeviceInfoList(hDevInfo);

    return dwStatuts;
}

////////////////////////////////////////////////////////////////////////////////////////////////////
// Read the drivers information and 3rd party SW information
// Then, send the information through CSI
//
int _tmain(int argc, _TCHAR* argv[])
{
    char pParameter[100] = {'\0'};
    if(argc == 2)
    {
        WideCharToMultiByte( CP_ACP, 0, argv[1], -1, pParameter, 100, NULL, NULL );
    }
    else//should a parameter passed in
        return -1;

    char* p3rdPartySWConfiguration = new char[10000];
    for(int i = 0; i < 10000; i++)
        p3rdPartySWConfiguration[i] = '\0';

    Get3rdPartySWConfiguration(p3rdPartySWConfiguration);


    char* pDriverConfiguration = new char[10000];
    for(int i = 0; i < 10000; i++)
        pDriverConfiguration[i] = '\0';

    GetDriverConfiguration(pDriverConfiguration, pParameter);


    //send the information out through CSI
    SendConfiguration(p3rdPartySWConfiguration, pDriverConfiguration, pParameter);

    delete[] p3rdPartySWConfiguration;
    delete[] pDriverConfiguration;

    return -1;
}


////////////////////////////////////////////////////////////////////////////////////////////////////
// OnError
//
void OnError( const char* aWhere, CSI_Result aResult )
{
    std::cout << "ERROR (" << aWhere << "): " << CSI_GetErrorMessage( aResult ) << std::endl;
    getchar();

    throw std::runtime_error( "CSI_Result Error" );

    return;
}

////////////////////////////////////////////////////////////////////////////////////////////////////
// Send the information through CSI. 
//
void SendConfiguration(string a3rdPartySWMessage, string aDriverMessage, string aPostfix)
{
    CSI_ParticipantInformation lParticipantInformation;
    CSI_Result lResult;
    CSI_Bool lbResult = TRUE;

    try
    {
        string lSimulationName = "Electronic_Logbook_Slave_" + aPostfix;
        string lSimulationDescription = "Electronic_Logbook_Slave";
        string lSimulationPartNumber = "1002197-002";
        string lSimulationVersionInformation = "01";

        strncpy_s( lParticipantInformation.mName, lSimulationName.c_str(), lSimulationName.length());
        strncpy_s( lParticipantInformation.mDescription, lSimulationDescription.c_str(), lSimulationDescription.length() );
        strncpy_s( lParticipantInformation.mPartNumber, lSimulationPartNumber.c_str(), lSimulationPartNumber.length() );
        strncpy_s( lParticipantInformation.mVersionInformation, lSimulationVersionInformation.c_str(), lSimulationVersionInformation.length() );

        //create a participant
        CSI_ParticipantHandle lParticipantHandle = CSI_RegisterParticipant( &lParticipantInformation );

        if (lParticipantHandle != CSI_InvalidHandle)
        {
            //------------------------------------------------------------
            // Connect Participant to the mesh
            //------------------------------------------------------------
            lResult = CSI_ConnectToMesh( lParticipantHandle );

            if ( lResult != CSI_Success )
            {
                OnError( "CSI_ConnectToMesh", lResult );
            }

            string lMessageName = "NPD_ElectronicLogbookSlave_Message_" + aPostfix;
            CSI_CollectionHandle lCollectionHandle = NULL;
            //create a NPD message
            lResult = CSI_GetCollection(lParticipantHandle, lMessageName.c_str(), &lCollectionHandle);

            if ( lResult != CSI_Success )
            {
                OnError( "Get NPD message", lResult );
            }

            CSI_Char* lParameterDriverName = "NPD_Parameter_Driver";
            CSI_ParameterHandle lParameterDriverHandle;
            //create a parameter to the NPD message
            lResult = CSI_GetParameter(lCollectionHandle, lParameterDriverName, &lParameterDriverHandle);

            if ( lResult != CSI_Success )
            {
                OnError( "Get NPD parameter NPD_Parameter_Driver", lResult );
            }

            CSI_Char* lParameter3rdSWName = "NPD_Parameter_3rdParty";
            CSI_ParameterHandle lParameter3rdSWHandle;
            //create a parameter to the NPD message
            lResult = CSI_GetParameter(lCollectionHandle, lParameter3rdSWName, &lParameter3rdSWHandle);

            if ( lResult != CSI_Success )
            {
                OnError( "Get NPD parameter NPD_Parameter_3rdParty", lResult );
            }

            //this message will be sent out
            CSI_PublishCollection(lCollectionHandle);

            //set parameter value
            lResult = CSI_SetParameterValueCharArray(lParameterDriverHandle, aDriverMessage.c_str(), aDriverMessage.length());
            if ( lResult != CSI_Success )
            {
                OnError( "Set NPD parameter NPD_Parameter_Driver", lResult );
            }
            lResult = CSI_SetParameterValueCharArray(lParameter3rdSWHandle, a3rdPartySWMessage.c_str(), a3rdPartySWMessage.length());
            if ( lResult != CSI_Success )
            {
                OnError( "Set NPD parameter NPD_Parameter_3rdParty", lResult );
            }

            //send the message out
            CSI_SendCollection(lCollectionHandle);
            //Disconnect Participant from the mesh
            lResult = CSI_DisconnectFromMesh( lParticipantHandle );

            if ( lResult != CSI_Success )
            {
                OnError( "CSI_DisconnectFromMesh", lResult );
            }
        }
    }
    catch( std::runtime_error& )
    {
        //nop
        lbResult = FALSE;
    }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
// Read th ethernet card, A664 card, A429 card, A825 card, analog card information
//
void GetDriverConfiguration(char* pConfiguration, char* pParameter)
{
    //ethernet
    FindCardWithClassGUID(&GUID_DEVCLASS_NET, pConfiguration, "Ethernet Card", pParameter);
    
    //A429
    const GUID GUID_DEVCLASS_A429 = {0x7c797140, 0xf6d8, 0x11cf, 0x9f, 0xd6, 0x00, 0xa0, 0x24, 0x17, 0x8a, 0x17};
    FindCardWithClassGUID(&GUID_DEVCLASS_A429, pConfiguration, "A429 Card", pParameter);

    //A664
    const GUID GUID_DEVCLASS_A664 = {0xd695ed6a, 0x630d, 0x4d83, 0x8d, 0x8b, 0xf1, 0xf0, 0xac, 0x10, 0x7a, 0xd0};
    FindCardWithClassGUID(&GUID_DEVCLASS_A664, pConfiguration, "A664 Card", pParameter);

    //A825
    const GUID GUID_DEVCLASS_A825 = {0xd20399e0, 0x47bd, 0x4219, 0x8c, 0x5b, 0xa1, 0x0b, 0x87, 0xce, 0xb5, 0x45};
    FindCardWithClassGUID(&GUID_DEVCLASS_A825, pConfiguration, "A825 Card", pParameter);

    //Analog
    const GUID GUID_DEVCLASS_Analog = {0x5971fc40, 0x50c8, 0x11d3, 0xaa, 0x35, 0x00, 0x40, 0x05, 0x56, 0x58, 0xfb};
    FindCardWithClassGUID(&GUID_DEVCLASS_Analog, pConfiguration, "Analog Card", pParameter);
}

////////////////////////////////////////////////////////////////////////////////////////////////////
// Read the 3rd Party SW information
//
void Get3rdPartySWConfiguration(char* pConfiguration)
{
    HANDLE hProcessSnap = NULL;
    PROCESSENTRY32 pe32= {0};
    hProcessSnap = CreateToolhelp32Snapshot(TH32CS_SNAPPROCESS, 0);//get all the processes
    if (hProcessSnap == (HANDLE)-1)
    {
        printf("\nCreateToolhelp32Snapshot() failed:%d",GetLastError());
        return ;
    }
    pe32.dwSize = sizeof(PROCESSENTRY32);
    printf("\nProcessName      ProcessID");
    if (Process32First(hProcessSnap, &pe32))//get first process
    {
        do
        {
            char pFileName[260] = {'\0'};
            WideCharToMultiByte( CP_ACP, 0, pe32.szExeFile, wcslen(pe32.szExeFile), pFileName, 260, NULL, NULL );
            int i = 0;
            while(pFileName[i] != '\0')
            {
                *pConfiguration++ = pFileName[i];//read the process name
                i++;
            }
            *pConfiguration++ = ',';

            //printf("\n%-20s%d",pe32.szExeFile,pe32.th32ProcessID);
            DWORD lVersion = GetProcessVersion(pe32.th32ProcessID);
            char pVersion[100] = {'\0'};
            GetVersionFromLong(lVersion, pVersion);
            i = 4;
            while(pVersion[i] != '\0')
                *pConfiguration++ = pVersion[i++];

            *pConfiguration++ = ';';
        }
        while (Process32Next(hProcessSnap, &pe32));//get next process
    }
    else
    {
        printf("\nProcess32Firstt() failed:%d",GetLastError());
    }
    CloseHandle (hProcessSnap);
}