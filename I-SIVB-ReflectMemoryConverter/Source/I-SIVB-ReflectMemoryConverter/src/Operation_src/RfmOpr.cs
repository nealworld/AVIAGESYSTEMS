using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using I_SIVB_ReflectMemoryConverter.src.Configuration_src;

namespace I_SIVB_ReflectMemoryConverter.src.Operation_src
{
    /// <summary>
    /// class RfmOpr is to operate the reflect memory card functions   
    /// </summary>
    /// <remarks>
    /// class RfmOpr is to operate the reflect memory card functions
    /// </remarks>
    /// <exception>
    /// null
    /// </exception>
    /// <returns>
    /// null
    /// </returns>
    class RfmOpr
    {
        private RfmConfig mRfmConfig;

        private SharedConfig mMessgaconfig = null;

        private int mDeviceID;

        private byte[] mData1Read;

        private SourceSelection mSourceSelection;
        public SourceSelection SourceSelection
        {
            get
            {
                return ( mSourceSelection );
            }
            set
            {
                mSourceSelection = value;
            }
        }
        /// <summary>
        /// get the data1 which updated by reading in   
        /// </summary>
        /// <remarks>
        /// get the data1 which updated by reading in   
        /// </remarks>
        /// <exception>
        /// null
        /// </exception>
        /// <returns>
        /// byte[] 
        /// </returns>
        public byte[] Data1ReadIn
        {
            get
            {
                return ( mData1Read );
            }
        }

        private uint mOffsetData1IronBird;
        private uint mOffsetData1FCSMiniRig;

        private byte[] mData4Write;

        /// <summary>
        /// set the data4 whith new value    
        /// </summary>
        /// <remarks>
        /// set the data4 whith new value ,with a data length check    
        /// </remarks>
        /// <exception>
        /// null
        /// </exception>
        /// <returns>
        /// byte[] 
        /// </returns>
        public byte[] Data4ToWrite
        {
            set
            {
                if ( value.Length != mData4Write.Length )
                {
                    LogGlobalManager.LogMgr.PrintLine( "The lengh is not equal : data4 " );
                }
                else
                {
                    for ( int lIndex = 0; lIndex < mData4Write.Length; lIndex++ )
                    {
                        mData4Write[lIndex] = value[lIndex];
                    }
                }
            }
        }

        private uint mOffsetData4;

        private DataSwap mDataSwap;

        /// <summary>
        /// get the mDataSwap     
        /// </summary>
        /// <remarks>
        /// set the mDataSwap which was initialized at constructor    
        /// </remarks>
        /// <exception>
        /// null
        /// </exception>
        /// <returns>
        /// byte[] 
        /// </returns>
        public DataSwap DataSwap
        {
            get
            {
                return ( mDataSwap );
            }
        }


        //julain updated
        uint mOffsetData1IronBirdStatus;
        uint mOffsetData1FCSMiniRigStatus;

        uint mOffsetData4Status;
        /// <summary>
        /// constructor of RfmOpr class  
        /// </summary>
        /// <remarks>
        /// constructor of RfmOpr class
        /// </remarks>
        /// <exception>
        /// null
        /// </exception>
        /// <returns>
        /// null 
        /// </returns>
        public RfmOpr( RfmConfig aRfmConfig, SharedConfig aMessgaconfig )
        {
            mMessgaconfig = aMessgaconfig;
            mRfmConfig = aRfmConfig;
            switch ( aRfmConfig.ByteSwap )
            {
                case "true":
                    mDataSwap = DataSwap.BigEndian;
                    break;
                default:
                    mDataSwap = DataSwap.LittleEndian;
                    break;
            }
        }

        /// <summary>
        /// try to initialize the reflect memory card and open it 
        /// </summary>
        /// <remarks>
        /// try to initialize the reflect memory card with the configurations and open it 
        /// </remarks>
        /// <exception>
        /// null
        /// </exception>
        /// <returns>
        /// bool 
        /// </returns>
        public bool RmfInit()
        {
            bool lResult = false;

            if ( mDataSwap == DataSwap.BigEndian )
            {
                RfmSetDMAswap( true );
            }
            mDeviceID = Convert.ToInt32( mRfmConfig.DeviceID );
            string lSelectedSource = mRfmConfig.DataBlocks.Value;
            if ( lSelectedSource == "IronBird" )
            {
                mSourceSelection = SourceSelection.IrionBird;
            }
            else if ( lSelectedSource == "FCS-MiniRig" )
            {

                mSourceSelection = SourceSelection.FCSMiniRig;
            }

            mOffsetData1IronBirdStatus = Convert.ToUInt32( mRfmConfig.DataBlocks.data1IronBirdAddress.offset, 16 );
            mOffsetData1IronBird = mOffsetData1IronBirdStatus + 8;
            uint lEndIrionBirdData1 = Convert.ToUInt32(mRfmConfig.DataBlocks.data1IronBirdAddress.end, 16);

            mOffsetData1FCSMiniRigStatus = Convert.ToUInt32( mRfmConfig.DataBlocks.data1FCSMiniRIGAddress.offset, 16 );
            mOffsetData1FCSMiniRig = mOffsetData1FCSMiniRigStatus + 8;
            uint lEndFCSData1 = Convert.ToUInt32( mRfmConfig.DataBlocks.data1FCSMiniRIGAddress.end, 16 );

            if (mSourceSelection == SourceSelection.IrionBird)
            {
                mData1Read = new byte[lEndIrionBirdData1 - mOffsetData1IronBird + 1];
            }
            else {
                mData1Read = new byte[lEndIrionBirdData1 - mOffsetData1IronBird + 1];
            }
            
            int lVaisToRfmData4Length = RfmVaisDataConvert.getRfmlength( mMessgaconfig.MessageConfigs[1] );
            mData4Write = new byte[lVaisToRfmData4Length];
            mOffsetData4Status = Convert.ToUInt32( mRfmConfig.DataBlocks.data4Address.offset, 16 );
            mOffsetData4 = mOffsetData4Status + 8;

            lResult = RfmOpen( mDeviceID );
            return ( lResult );
        }
        private bool RfmSetDMAswap( bool aByteSwap )
        {
            bool lResult = false;
            Rfm2gDriverCsharp.RFM2G_STATUS_ENUM_EXPORT lCardStatus = Rfm2gDriverCsharp.RFM2G_STATUS_ENUM_EXPORT.STATUS_SUCCESS;
            try
            {
                lCardStatus = Rfm2gDriverCsharp.Rfm2gDriverCsharp.CSharpRFM2gSetDMAByteSwap( aByteSwap );
            }
            catch ( Exception e )
            {
                LogGlobalManager.LogMgr.PrintForQuit( e.Message );
            }
            if ( lCardStatus == Rfm2gDriverCsharp.RFM2G_STATUS_ENUM_EXPORT.STATUS_SUCCESS )
            {
                lResult = true;
            }
            else
            {
                LogGlobalManager.LogMgr.PrintLine( lCardStatus );
            }
            return ( lResult );

        }
        private bool RfmOpen( int id )
        {
            bool lResult = false;
            Rfm2gDriverCsharp.RFM2G_STATUS_ENUM_EXPORT lCardStatus = Rfm2gDriverCsharp.RFM2G_STATUS_ENUM_EXPORT.STATUS_SUCCESS;

            try
            {
                lCardStatus = Rfm2gDriverCsharp.Rfm2gDriverCsharp.CSharpRFM2gOpen( id );
            }
            catch ( Exception e )
            {
                LogGlobalManager.LogMgr.PrintForQuit( e.Message );
            }
            if ( lCardStatus == Rfm2gDriverCsharp.RFM2G_STATUS_ENUM_EXPORT.STATUS_SUCCESS )
            {
                lResult = true;
            }
            else
            {
                LogGlobalManager.LogMgr.PrintLine( lCardStatus );
            }
            return ( lResult );
        }




        /// <summary>
        /// Read Data1  
        /// </summary>
        /// <remarks>
        /// Read Data1 from the VMIC net on the initialzed address(by rmf_init) 
        /// </remarks>
        /// <exception>
        /// null
        /// </exception>
        /// <returns>
        /// bool 
        /// </returns>
        public bool RfmRead()
        {
            bool lResult = false;
            uint lDataOffSet = 0;

            SourceSelection lSource = mSourceSelection;
            switch ( lSource )
            {
                case SourceSelection.IrionBird:
                    lDataOffSet = mOffsetData1IronBird;
                    break;
                case SourceSelection.FCSMiniRig:
                    lDataOffSet = mOffsetData1FCSMiniRig;
                    break;
                default:
                    lDataOffSet = mOffsetData1IronBird;
                    LogGlobalManager.LogMgr.PrintLine( "Current Source selection is not supported, use the default IronBird Data Source!" );
                    break;
            }
            Rfm2gDriverCsharp.RFM2G_STATUS_ENUM_EXPORT lCardStatus = Rfm2gDriverCsharp.RFM2G_STATUS_ENUM_EXPORT.STATUS_SUCCESS;

            lCardStatus = Rfm2gDriverCsharp.Rfm2gDriverCsharp.CSharpRFM2gReadSafe( lDataOffSet, ref mData1Read, (uint)( mData1Read.Length ) );
            if ( lCardStatus == Rfm2gDriverCsharp.RFM2G_STATUS_ENUM_EXPORT.STATUS_SUCCESS )
            {
                lResult = true;
            }
            else
            {
                LogGlobalManager.LogMgr.PrintLine( lCardStatus );
            }
            return ( lResult );
        }

        private byte[] SetStatus( DataSourceStatus aStatus )
        {
            byte[] lStatusByte = new byte[8];
            switch ( aStatus )
            {
                case DataSourceStatus.NoData:
                    for ( int i = 0; i < lStatusByte.Length; i++ )
                    {
                        lStatusByte[i] = 0x00;
                    }
                    break;
                case DataSourceStatus.DataInWorking:
                    for ( int i = 0; i < lStatusByte.Length; i++ )
                    {
                        lStatusByte[i] = 0xff;
                    }
                    break;
                default:
                    for ( int i = 0; i < lStatusByte.Length; i++ )
                    {
                        lStatusByte[i] = 0x00;
                    }
                    break;
            }

            return ( lStatusByte );
        }
        private DataSourceStatus GetStatus( byte[] abyte, int aLength )
        {
            DataSourceStatus lRerurnStatus = DataSourceStatus.NoData;
            byte[] lVal = abyte;

            Byte lDataInWorking = 0xFF;
            Byte lNoData = 0x00;

            for (int lIndex = 0 ; lIndex < aLength; lIndex++ )
            {
                lDataInWorking &= lVal[lIndex];
                lNoData |= lVal[lIndex];
            }

            if (lDataInWorking == 0xFF) {
                lRerurnStatus = DataSourceStatus.DataInWorking;
            }
            else if (lNoData == 0x00) {
                lRerurnStatus = DataSourceStatus.NoData;
            }

            return ( lRerurnStatus );
        }
        /// <summary>
        /// RfmGetsData1Tatus  
        /// </summary>
        /// <remarks>
        /// RfmGetsData1Tatus 
        /// </remarks>
        /// <exception>
        /// null
        /// </exception>
        /// <returns>
        /// bool 
        /// </returns>
        public bool RfmGetData1Status( ref DataSourceStatus aStatus )
        {
            bool lResult = false;
            Rfm2gDriverCsharp.RFM2G_STATUS_ENUM_EXPORT lCardStatus = Rfm2gDriverCsharp.RFM2G_STATUS_ENUM_EXPORT.STATUS_SUCCESS;
            byte[] lDataRead = new byte[8];
            uint lDataOffSet = 0;
            SourceSelection lSource = mSourceSelection;
            switch ( lSource )
            {
                case SourceSelection.IrionBird:
                    lDataOffSet = mOffsetData1IronBirdStatus;
                    break;
                case SourceSelection.FCSMiniRig:
                    lDataOffSet = mOffsetData1FCSMiniRigStatus;
                    break;
                default:
                    lDataOffSet = mOffsetData1FCSMiniRigStatus;
                    LogGlobalManager.LogMgr.PrintLine( "Current Source selection is not supported, get the default IronBird Data Source Status!" );
                    break;
            }
            lCardStatus = Rfm2gDriverCsharp.Rfm2gDriverCsharp.CSharpRFM2gReadSafe( lDataOffSet, ref lDataRead, 8 );
            if ( lCardStatus == Rfm2gDriverCsharp.RFM2G_STATUS_ENUM_EXPORT.STATUS_SUCCESS )
            {
                aStatus = GetStatus( lDataRead, 8 );
                lResult = true;
            }
            else
            {
                LogGlobalManager.LogMgr.PrintLine( lCardStatus );
            }
            return ( lResult );
        }

        /// <summary>
        /// RfmGetsData1Tatus  
        /// </summary>
        /// <remarks>
        /// RfmGetsData1Tatus 
        /// </remarks>
        /// <exception>
        /// null
        /// </exception>
        /// <returns>
        /// bool 
        /// </returns>
        public bool RfmSetData4Status( DataSourceStatus aStatus )
        {
            bool lResult = false;
            Rfm2gDriverCsharp.RFM2G_STATUS_ENUM_EXPORT lCardStatus = Rfm2gDriverCsharp.RFM2G_STATUS_ENUM_EXPORT.STATUS_SUCCESS;
            byte[] lBytes = new byte[8];
            lBytes = SetStatus( aStatus );
            lCardStatus = Rfm2gDriverCsharp.Rfm2gDriverCsharp.CSharpRFM2gWrite( mOffsetData4Status, lBytes, 8 );
            if ( lCardStatus == Rfm2gDriverCsharp.RFM2G_STATUS_ENUM_EXPORT.STATUS_SUCCESS )
            {
                lResult = true;
            }
            else
            {
                LogGlobalManager.LogMgr.PrintLine( lCardStatus );
            }
            return ( lResult );
        }


        /// <summary>
        /// Write Data4  
        /// </summary>
        /// <remarks>
        /// Write Data4 to the VMIC net on the initialzed address(by rmf_init) 
        /// </remarks>
        /// <exception>
        /// null
        /// </exception>
        /// <returns>
        /// bool 
        /// </returns>
        public bool RfmWrite()
        {
            bool lResult = false;
            Rfm2gDriverCsharp.RFM2G_STATUS_ENUM_EXPORT lCardStatus = Rfm2gDriverCsharp.RFM2G_STATUS_ENUM_EXPORT.STATUS_SUCCESS;
            lCardStatus = Rfm2gDriverCsharp.Rfm2gDriverCsharp.CSharpRFM2gWrite( mOffsetData4, mData4Write, (uint)mData4Write.Length );
            if ( lCardStatus == Rfm2gDriverCsharp.RFM2G_STATUS_ENUM_EXPORT.STATUS_SUCCESS )
            {
                lResult = true;
            }
            else
            {
                LogGlobalManager.LogMgr.PrintLine( lCardStatus );
            }
            return ( lResult );
        }
        //write()
        //setSwap(swap)
        //open(id)
        //question: when read/write needs to set the byteswap or byhand?

        /// <summary>
        /// delete rfm card drive software  
        /// </summary>
        /// <remarks>
        /// delete rfm card drive software and can not be opened only restart the entire program. 
        /// </remarks>
        /// <exception>
        /// null
        /// </exception>
        /// <returns>
        /// void 
        /// </returns>
        public void RfmDelete()
        {
            Rfm2gDriverCsharp.Rfm2gDriverCsharp.CSharpRFM2gClose();
            Rfm2gDriverCsharp.Rfm2gDriverCsharp.CSharpDeleteRFMDevice();
        }

        public byte[] ParseRawBytes(byte[] aBytes) {
            uint lDataoffset;
            byte[] lVAISMessages = new byte[mData1Read.Length];
            List<ParameterAddress> lparaAddress;
            if (mSourceSelection == SourceSelection.IrionBird)
            {
                lDataoffset = mOffsetData1IronBird;
                lparaAddress = mRfmConfig.DataBlocks.IronBirdParametersAddress;
            }
            else {
                lDataoffset = mOffsetData1FCSMiniRig;
                lparaAddress = mRfmConfig.DataBlocks.FCSMiniRIGParametersAddress;
            }

            UInt32 lDestIndex = 0;
            foreach (ParameterAddress lAddr in lparaAddress) 
            {
                Array.Copy(aBytes, (long)(lAddr.from - lDataoffset), lVAISMessages, 
                    (long)lDestIndex, (long)(lAddr.to - lAddr.from + 1));
                lDestIndex += (lAddr.to - lAddr.from + 1);
            }

            return lVAISMessages;
        }
    }
}
