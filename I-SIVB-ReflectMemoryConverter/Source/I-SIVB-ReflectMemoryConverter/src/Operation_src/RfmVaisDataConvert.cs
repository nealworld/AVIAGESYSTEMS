using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GEAviation.CommonSim;
using I_SIVB_ReflectMemoryConverter.src.Configuration_src;

namespace I_SIVB_ReflectMemoryConverter.src.Operation_src
{
    /// <summary>
    /// class RfmVaisDataConvert is to operate the data convert functions   
    /// </summary>
    /// <remarks>
    /// class RfmVaisDataConvert is to operate the data convert functions 
    /// </remarks>
    /// <exception>
    /// null
    /// </exception>
    /// <returns>
    /// null
    /// </returns>
    class RfmVaisDataConvert
    {
        /// <summary>
        /// get the reflect memory datas total byte length  
        /// </summary>
        /// <remarks>
        /// get the reflect memory datas total byte length for one VaisMessageConfig
        /// </remarks>
        /// <exception>
        /// null
        /// </exception>
        /// <returns>
        /// int 
        /// </returns>
        public static int getRfmlength( VaisMessageConfig aMessgaconfig )
        {
            VaisMessageConfig mMessgaconfig = aMessgaconfig;
            int lLength = 0;
            foreach ( VaisParamConfig lParam in mMessgaconfig.parameters )
            {
                switch ( lParam.type )
                {
                    case "double":
                        lLength += 8;
                        break;
                    case "char":
                        lLength += 1;
                        break;
                    default:
                        lLength += 8;
                        break;
                }
            }
            return ( lLength );
        }

        /// <summary>
        /// get the vais datas total byte length  
        /// </summary>
        /// <remarks>
        /// get the vais datas total byte length for one VaisMessageConfig
        /// </remarks>
        /// <exception>
        /// string 
        /// </exception>
        /// <returns>
        /// int 
        /// </returns>
        public static int getVaislength( List<Parameter> aParamList )
        {
            int lIndex = 0;
            List<Parameter> lParamList = aParamList;
            foreach ( Parameter lParam in lParamList )
            {
                switch ( lParam.GetDataType() )
                {
                    case CommonSimTypes.ValueType.Bool:
                        lIndex++;
                        break;
                    case CommonSimTypes.ValueType.Char:
                        lIndex++;
                        break;
                    case CommonSimTypes.ValueType.Double:
                        lIndex = lIndex + 8;
                        break;
                    case CommonSimTypes.ValueType.Float:
                        lIndex = lIndex + 4;
                        break;
                    case CommonSimTypes.ValueType.Int16:
                        lIndex = lIndex + 2;
                        break;
                    case CommonSimTypes.ValueType.Int32:
                        lIndex = lIndex + 4;
                        break;
                    case CommonSimTypes.ValueType.Int64:
                        lIndex = lIndex + 8;
                        break;
                    case CommonSimTypes.ValueType.Int8:
                        lIndex = lIndex++;
                        break;
                    case CommonSimTypes.ValueType.UInt16:
                        lIndex = lIndex + 2;
                        break;
                    case CommonSimTypes.ValueType.UInt32:
                        lIndex = lIndex + 4;
                        break;
                    case CommonSimTypes.ValueType.UInt64:
                        lIndex = lIndex + 8;
                        break;
                    case CommonSimTypes.ValueType.UInt8:
                        lIndex = lIndex++;
                        break;
                    default:
                        throw new Exception( lParam.GetDataType().ToString() + " is not supported yet." );
                }

            }

            return ( lIndex );
        }

        /// <summary>
        /// convert the Vais data bytes to rfm memory data bytes  
        /// </summary>
        /// <remarks>
        /// convert the Vais data bytes to rfm memory data bytes
        /// </remarks>
        /// <exception>
        /// null
        /// </exception>
        /// <returns>
        /// byte[] 
        /// </returns>
        public static byte[] VaisToRfmbytes( byte[] aBytes, DataSwap aDataSwap )
        {

            byte[] lbytes = aBytes;
            return ( lbytes );
        }

        /// <summary>
        /// convert the rfm memory data bytes to vais data bytes  
        /// </summary>
        /// <remarks>
        /// convert the rfm memory data bytes to vais data bytes
        /// </remarks>
        /// <exception>
        /// null
        /// </exception>
        /// <returns>
        /// byte[] 
        /// </returns>
        public static byte[] RfmToVaisbytes( byte[] aBytes, DataSwap aDataSwap )
        {
            byte[] lbytes = aBytes;
            return ( lbytes );
        }
    }
}
