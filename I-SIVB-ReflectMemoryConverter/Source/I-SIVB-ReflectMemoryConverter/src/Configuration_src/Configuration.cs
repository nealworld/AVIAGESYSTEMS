using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace I_SIVB_ReflectMemoryConverter.src.Configuration_src
{
    enum SourceSelection
    {
        IrionBird,
        FCSMiniRig,
    };

    /// <summary>
    /// struct VaisParamConfig is to define the NPD data that read from VAIS config 
    /// </summary>
    /// <remarks>
    /// struct VaisParamConfig is to define the NPD data that read from VAIS config that will be used for both vais and rfm 
    /// </remarks>
    struct VaisParamConfig
    {
        /// <summary>
        /// string name is to define the NPD parameter name 
        /// </summary>
        public string name;

        /// <summary>
        /// string type is to define the NPD parameter data type 
        /// </summary>
        public string type;
    };

    /// <summary>
    /// class VaisMessageConfig is to define one NPD message 
    /// </summary>
    /// <remarks>
    /// class VaisMessageConfig is to define one NPD message which included several parameters
    class VaisMessageConfig
    {
        /// <summary>
        /// constructor
        /// </summary>
        public VaisMessageConfig()
        {
            parameters = new List<VaisParamConfig>();
        }

        /// <summary>
        /// string name is to define one NPD message name
        /// </summary>
        public string name;

        /// <summary>
        /// List<VaisParamConfig> is to define one NPD message with parameters 
        /// </summary>
        public List<VaisParamConfig> parameters;

    }//Shared config struct.

    /// <summary>
    /// class ParticipantString is to define the C-SIVB participant  
    /// </summary>
    /// <remarks>
    /// class ParticipantString is to define the C-SIVB participant 
    /// </remarks>
    /// <exception>
    /// null
    /// </exception>
    /// <returns>
    /// null
    /// </returns>
    class ParticipantString
    {
        public ParticipantString()
        {
            Name = "I-SIVB_RMC";
            Description = "AVIAGE SYSTEMS";
            PartNumber = "1";
            Version = "1";
        }
        public string Name;
        public string Description;
        public string PartNumber;
        public string Version;
    }

    /// <summary>
    /// struct DataAddress is to define the rfm data block addresses.
    /// </summary>
    struct DataAddress
    {
        /// <summary>
        /// string offset is to define the rfm data block began offset.
        /// </summary>
        public string offset;

        /// <summary>
        /// string end is to define the rfm data block end addresses.
        /// </summary>
        public string end;

    }

    /// <summary>
    /// struct SourceSelection is to define the rfm data1 dource.
    /// </summary>
    struct RfmDatablockAddresses
    {
        /// <summary>
        /// string value is to define the data1 dource.
        /// </summary>
        public string Value;


        /// <summary>
        /// DataAddress data1Address is to define the data1 message property.
        /// </summary>
        public DataAddress data1IronBirdAddress;

        /// <summary>
        /// DataAddress data1Address is to define the data1 message property.
        /// </summary>
        public DataAddress data1FCSMiniRIGAddress;

        /// <summary>
        /// DataAddress data4Address is to define the data4 message property.
        /// </summary>
        public DataAddress data4Address;
    };

    /// <summary>
    /// struct VaisConfig is to be used to initialize the vais.
    /// </summary>
    struct VaisConfig
    {
        /// <summary>
        /// string ParticipantString is to define the I-SIVB Participant.
        /// </summary>
        public ParticipantString ParticipantConfig;
        /// <summary>
        /// string RefreshRate is to define the I-SIVB simulation refresh rate.
        /// </summary>
        public string RefreshRate;
    };

    /// <summary>
    /// struct RfmConfig is to be used to initialize the rfm.
    /// </summary>
    struct RfmConfig
    {
        /// <summary>
        /// RfmDatablockAddresses DataBlocks is to define the Data1/4 Blocks addresses.
        /// </summary>
        public RfmDatablockAddresses DataBlocks;

        /// <summary>
        /// string DeviceID is to define the rfm card id.
        /// </summary>
        public string DeviceID;

        /// <summary>
        /// string ByteSwap is to define the rfm card byteswap character.
        /// </summary>
        public string ByteSwap;
    }
    class SharedConfig
    {
        VaisMessageConfig[] mMessageConfigs;

        /// <summary>
        /// get and set mMessageConfigs.
        /// </summary>
        public VaisMessageConfig[] MessageConfigs
        {
            get
            {
                return ( mMessageConfigs );
            }
            set
            {
                if ( ( value != null ) && ( value.Length == 2 ) )
                {
                    mMessageConfigs = value;
                }
                else
                {
                    LogGlobalManager.LogMgr.PrintLine( "Error in shared vais message set!" );
                }
            }
        }

        /// <summary>
        /// constructor
        /// </summary>
        public SharedConfig()
        {
            mMessageConfigs = new VaisMessageConfig[2];
        }

    }
    /// <summary>
    /// enum DataSwap is to define the data swap type : big ending or little ending.
    /// </summary>
    enum DataSwap
    {
        BigEndian = 0,
        LittleEndian = 1
    }
    enum DataSourceStatus
    {
        Ready,
        Shutdown,
        Pause,//OTHER STATUS FOR EXAMPLE PAUSE / NOT CORRECT BUT UPDATING...
    }
}
