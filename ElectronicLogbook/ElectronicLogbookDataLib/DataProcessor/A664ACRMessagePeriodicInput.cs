using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GEAviation.CommonSim;

namespace ElectronicLogbookDataLib.DataProcessor
{
    /// <summary>
    /// This is the "Configuration Report" message from IMA platform.
    /// One instance of this class is for each LRU.
    /// 
    /// The code here is same as the code in model "OMS-ACR".
    /// </summary>
    class A664ACRMessagePeriodicInput
    {
        public UtilityParticipant Participant { get; set; }

        public GEAviation.CommonSim.Arinc664.Arinc664Message OriginalACRMsgFromIMA { get; set; }

        //From IMA platform, this message is in ASN.1, so it is a byte array.
        //Which means there is no data set and parameters.
        public byte[] RawData { get; set; }

        public string mLRUName = "";
        public string mMsgName = "";

        /// <summary>
        /// constructor
        /// </summary>
        public A664ACRMessagePeriodicInput(string aLRUName, string aMsgName)
        {
            mLRUName = aLRUName;
            mMsgName = aMsgName;

            Participant = null;
            OriginalACRMsgFromIMA = null;
        }

        /// <summary>
        /// register or create message according to message definition
        /// </summary>
        virtual public Error registerMessage(UtilityParticipant aParticipant)
        {
            Participant = aParticipant;
            if (Participant == null)
                return new Error("Participant is NULL");

            //Each LRU has its own message, so create this message with different name.
            string lStrName = mMsgName;

            OriginalACRMsgFromIMA = Participant.GetArinc664Message(lStrName);

            if (OriginalACRMsgFromIMA != null)
            {
                OriginalACRMsgFromIMA.Subscribe(CommonSimTypes.QueueType.Snapshot);//we receive this message from other model
            }
            else
            {
                return new Error("Failed to get collection. Collection Name = " + lStrName);
            }
            return Error.OK;
        }

        /// <summary>
        /// try to receive a message from IMA platform
        /// </summary>
        public bool GetMessage()
        {
            if (OriginalACRMsgFromIMA.Receive())
            {
                RawData = OriginalACRMsgFromIMA.GetRawMessage();
                if (RawData.Length > 0)
                {
                    return true;
                }
                else
                    return false;
            }

            return false;
        }
    }
}
