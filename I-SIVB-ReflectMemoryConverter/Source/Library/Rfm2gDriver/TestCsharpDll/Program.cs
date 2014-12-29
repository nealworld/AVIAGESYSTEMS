using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using GEAviation.CommonSim;
//using Rfm2gDriverCsharp;
namespace TestCsharpDll
{
    class Program
    {
        static void Main( string[] args )
        {
            ParticipantInformation lParticipantInfo = new ParticipantInformation();
            lParticipantInfo.Name = "test1";
            lParticipantInfo.Description = "a";

            Participant msParticipant = new Participant( lParticipantInfo );
            msParticipant.ConnectToMesh();

           UInt16 id=0;

          // Console.WriteLine( CPPDLL.Add1(1,6) );
           CPPDLL.CSharpRFM2gOpen(2);
           try
           {
               
               byte [] sss =new byte[10];
               for(int i=0;i<10;i++)
               {
                    sss[i]=Convert.ToByte(i.ToString());
               }
               Rfm2gDriverCsharp.RFM2G_STATUS_ENUM_EXPORT Sxsss = Rfm2gDriverCsharp.Rfm2gDriverCsharp.CSharpRFM2gWrite( 1, sss, 10 );
                  
               
               //Rfm2gDriverCsharp.Rfm2gDriverCsharp.
               //CPPDLL.CSharpRFM2gWrite( 1, sss, 10 );
              // Console.WriteLine( Sxsss );
                UInt16 f =9;
                Rfm2gDriverCsharp.Rfm2gDriverCsharp.CSharpRFM2gReadSafe( 0, ref sss,7 );
               byte[]bYTES= new byte[50];
               Console.WriteLine( Rfm2gDriverCsharp.Rfm2gDriverCsharp.CSharpRFM2gReadSafe( 0, ref bYTES ,40));
               //Console.WriteLine ( Rfm2gDriverCsharp.Rfm2gDriverCsharp.CSharpGetRFM2gNodeID( ref f ) );
               Rfm2gDriverCsharp.RFM2G_STATUS_ENUM_EXPORT Sxss = Rfm2gDriverCsharp.Rfm2gDriverCsharp.CSharpRFM2gOpen( 1 );
              Console.WriteLine(  Rfm2gDriverCsharp.Rfm2gDriverCsharp.CSharpRFM2gSetDMAByteSwap(true) );
      
               
               //Console.WriteLine( Sxss2 );
               
              // int x = CPPDLL.CSharpRFM2gOpen1( 1 );

               //Console.WriteLine( CPPDLL.CSharpRFM2gOpen1( 1 ) );
           }
           catch ( Exception e )
           {
               Console.WriteLine(e.Message);
           }
            //Rfm2gDriverCsharp.Rfm2gDriverCsharp.open(1);
    
            //Rfm2gDriverCsharp.Rfm2gDriverCsharp.CSharpRFM2gOpen(1);
          //  Console.WriteLine( Rfm2gDriverCsharp.Rfm2gDriverCsharp.Add1( 1, 28 ) ); ;
            //Rfm2gDriverCsharp.Rfm2gDriverCsharp.CSharpRFM2gOpen( 11 );

            Console.WriteLine( id );

            byte[] gDataSaved1 = new byte[50];

            Console.WriteLine( gDataSaved1[0] );

            Console.WriteLine( gDataSaved1[1] );

            Console.WriteLine( gDataSaved1[2] );
           
            Console.ReadKey();

            Console.ReadKey();
        }
    }
}
