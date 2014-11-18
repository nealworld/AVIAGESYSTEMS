using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CSharpWrapper_ASN1Decoder;
using System.Collections;
namespace ElectronicLogbookDataLib.DataProcessor
{
    /// <summary>
    /// This class is to maintain the message ASN.1 format. Then this format will be used to read the values
    /// in a message.
    /// 
    /// The code here is same as the code in model "OMS-ACR".
    /// </summary>
    public class ASN1_Decoder_ConfigReport
    {
        static public ASN1_Wrapper cTopASN1Wrapper = new ASN1_Wrapper();
        static public ASN1_Decoder_ConfigReport cASN1Format = new ASN1_Decoder_ConfigReport();//this is the root of ASN.1 format.

        const Byte cMemberSystemIDParameterSTATUS = 17;//"config report" message tag
        const int cMaxMScount = 30;
        const int cMaxHWData = 25;
        const int cMaxSWData = 40;
        const int cMaxSWPartData = 175;
        const int cMaxConfigData = 15;

        public int Tag { get; set; }
        public string DisplayName { get; set; }

        public enum TagValueType
        {
            //for "config report" message, only these 3 types are used in format.
            nestedType = 0,
            intType = 1,
            stringType = 2,
        }
        public TagValueType ValueType { get; set; }

        public Hashtable NestedASN1 = new Hashtable();

        public ASN1_Wrapper ASN1Wrapper { get; set; }//this is the real decoder which is a 3rd party software.
        //when "this" is not a nested tag, it is null.

        /// <summary>
        /// Setup the "config report" message grammar tree according to ASN.1 format. For each node, we record its tag,
        /// display name, value type, and its child nodes.
        /// </summary>
        public void BuildValueTree()
        {
            ValueType = TagValueType.nestedType;//this is the top level ASN1 node.
            DisplayName = "Config Report (All)";
            Tag = cMemberSystemIDParameterSTATUS;

            ASN1Wrapper = new ASN1_Wrapper();//since this is a "Nested" type
            cTopASN1Wrapper.addTag(Tag, ASN1Wrapper);

            for (int lMSIndex = 0; lMSIndex < cMaxMScount; lMSIndex++)
            {
                ASN1_Decoder_ConfigReport lMemberSystemSet = new ASN1_Decoder_ConfigReport();
                lMemberSystemSet.DisplayName = "Config Report (This LRU)";
                lMemberSystemSet.Tag = lMSIndex;
                lMemberSystemSet.ValueType = TagValueType.nestedType;
                NestedASN1.Add(lMemberSystemSet.Tag, lMemberSystemSet);  //Add Tag for Member system Set

                lMemberSystemSet.ASN1Wrapper = new ASN1_Wrapper();
                ASN1Wrapper.addTag(lMemberSystemSet.Tag, lMemberSystemSet.ASN1Wrapper);

                ASN1_Decoder_ConfigReport lMemberSystemID = new ASN1_Decoder_ConfigReport();
                lMemberSystemID.DisplayName = "Equipment_ID";
                lMemberSystemID.Tag = 0;
                lMemberSystemID.ValueType = TagValueType.intType;  // Add Tag for Member system ID
                lMemberSystemSet.NestedASN1.Add(lMemberSystemID.Tag, lMemberSystemID);

                lMemberSystemSet.ASN1Wrapper.addTag(lMemberSystemID.Tag, DecodeType.DecodeInt);

                for (int lHWIndex = 0; lHWIndex < cMaxHWData; lHWIndex++)
                {
                    ASN1_Decoder_ConfigReport lHWData = new ASN1_Decoder_ConfigReport();
                    lHWData.DisplayName = "HW_Part";
                    lHWData.Tag = lHWIndex + 1;
                    lHWData.ValueType = TagValueType.nestedType;
                    lMemberSystemSet.NestedASN1.Add(lHWData.Tag, lHWData);  // Add Tag for hardware data

                    lHWData.ASN1Wrapper = new ASN1_Wrapper();
                    lMemberSystemSet.ASN1Wrapper.addTag(lHWData.Tag, lHWData.ASN1Wrapper);

                    ASN1_Decoder_ConfigReport lHWPartNumber = new ASN1_Decoder_ConfigReport();
                    lHWPartNumber.DisplayName = "HW_Part_Number";
                    lHWPartNumber.Tag = 0;
                    lHWPartNumber.ValueType = TagValueType.stringType;
                    lHWData.NestedASN1.Add(lHWPartNumber.Tag, lHWPartNumber);

                    lHWData.ASN1Wrapper.addTag(lHWPartNumber.Tag, DecodeType.DecodeString);

                    ASN1_Decoder_ConfigReport lHWPartDesc = new ASN1_Decoder_ConfigReport();
                    lHWPartDesc.DisplayName = "HW_Part_Description";
                    lHWPartDesc.Tag = 1;
                    lHWPartDesc.ValueType = TagValueType.stringType;
                    lHWData.NestedASN1.Add(lHWPartDesc.Tag, lHWPartDesc);

                    lHWData.ASN1Wrapper.addTag(lHWPartDesc.Tag, DecodeType.DecodeString);

                    ASN1_Decoder_ConfigReport lHWPartStatus = new ASN1_Decoder_ConfigReport();
                    lHWPartStatus.DisplayName = "HW_Part_Status";
                    lHWPartStatus.Tag = 2;
                    lHWPartStatus.ValueType = TagValueType.stringType;
                    lHWData.NestedASN1.Add(lHWPartStatus.Tag, lHWPartStatus);

                    lHWData.ASN1Wrapper.addTag(lHWPartStatus.Tag, DecodeType.DecodeString);

                    ASN1_Decoder_ConfigReport lHWSerialNumber = new ASN1_Decoder_ConfigReport();
                    lHWSerialNumber.DisplayName = "HW_Part_Serial_Number";
                    lHWSerialNumber.Tag = 3;
                    lHWSerialNumber.ValueType = TagValueType.stringType;
                    lHWData.NestedASN1.Add(lHWSerialNumber.Tag, lHWSerialNumber);

                    lHWData.ASN1Wrapper.addTag(lHWSerialNumber.Tag, DecodeType.DecodeString);
                }

                for (int lSWIndex = 0; lSWIndex < cMaxSWData; lSWIndex++)
                {
                    ASN1_Decoder_ConfigReport lSWData = new ASN1_Decoder_ConfigReport();
                    lSWData.DisplayName = "SW_Config";
                    lSWData.Tag = cMaxHWData + lSWIndex + 1;
                    lSWData.ValueType = TagValueType.nestedType;
                    lMemberSystemSet.NestedASN1.Add(lSWData.Tag, lSWData); // Add tag for software data

                    lSWData.ASN1Wrapper = new ASN1_Wrapper();
                    lMemberSystemSet.ASN1Wrapper.addTag(lSWData.Tag, lSWData.ASN1Wrapper);

                    ASN1_Decoder_ConfigReport lSWLocationID = new ASN1_Decoder_ConfigReport();
                    lSWLocationID.DisplayName = "SW_Location_ID";
                    lSWLocationID.Tag = 0;
                    lSWLocationID.ValueType = TagValueType.stringType;
                    lSWData.NestedASN1.Add(lSWLocationID.Tag, lSWLocationID);

                    lSWData.ASN1Wrapper.addTag(lSWLocationID.Tag, DecodeType.DecodeString);

                    ASN1_Decoder_ConfigReport lSWLocationDesc = new ASN1_Decoder_ConfigReport();
                    lSWLocationDesc.DisplayName = "SW_Location_Description";
                    lSWLocationDesc.Tag = 1;
                    lSWLocationDesc.ValueType = TagValueType.stringType;
                    lSWData.NestedASN1.Add(lSWLocationDesc.Tag, lSWLocationDesc);

                    lSWData.ASN1Wrapper.addTag(lSWLocationDesc.Tag, DecodeType.DecodeString);

                    for (int lSWPartIndex = 0; lSWPartIndex < cMaxSWPartData; lSWPartIndex++)
                    {
                        ASN1_Decoder_ConfigReport lSWPartData = new ASN1_Decoder_ConfigReport();
                        lSWPartData.DisplayName = "SW_Part";
                        lSWPartData.Tag = lSWPartIndex + 2;
                        lSWPartData.ValueType = TagValueType.nestedType;
                        lSWData.NestedASN1.Add(lSWPartData.Tag, lSWPartData); // Add tag for sw part data

                        lSWPartData.ASN1Wrapper = new ASN1_Wrapper();
                        lSWData.ASN1Wrapper.addTag(lSWPartData.Tag, lSWPartData.ASN1Wrapper);

                        ASN1_Decoder_ConfigReport lSWPartNumber = new ASN1_Decoder_ConfigReport();
                        lSWPartNumber.DisplayName = "LSAP_Part_Number";
                        lSWPartNumber.Tag = 0;
                        lSWPartNumber.ValueType = TagValueType.stringType;
                        lSWPartData.NestedASN1.Add(lSWPartNumber.Tag, lSWPartNumber);

                        lSWPartData.ASN1Wrapper.addTag(lSWPartNumber.Tag, DecodeType.DecodeString);

                        ASN1_Decoder_ConfigReport lSWPartDesc = new ASN1_Decoder_ConfigReport();
                        lSWPartDesc.DisplayName = "LSAP_Description";
                        lSWPartDesc.Tag = 1;
                        lSWPartDesc.ValueType = TagValueType.stringType;
                        lSWPartData.NestedASN1.Add(lSWPartDesc.Tag, lSWPartDesc);

                        lSWPartData.ASN1Wrapper.addTag(lSWPartDesc.Tag, DecodeType.DecodeString);
                    }
                }
                for (int lConfigIndex = 0; lConfigIndex < cMaxConfigData; lConfigIndex++)
                {
                    ASN1_Decoder_ConfigReport lConfigInfo = new ASN1_Decoder_ConfigReport();
                    lConfigInfo.DisplayName = "Config_Info";
                    lConfigInfo.Tag = cMaxHWData + cMaxSWData + lConfigIndex + 1;
                    lConfigInfo.ValueType = TagValueType.stringType;
                    lMemberSystemSet.NestedASN1.Add(lConfigInfo.Tag, lConfigInfo);

                    lMemberSystemSet.ASN1Wrapper.addTag(lConfigInfo.Tag, DecodeType.DecodeString);
                }
            }
        }
    }
}
