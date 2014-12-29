using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;
using I_SIVB_ReflectMemoryConverter.src.Configuration_src;

namespace I_SIVB_ReflectMemoryConverter.src
{
    /// <summary>
    /// class LogManager is to operate the logs  
    /// </summary>
    /// <remarks>
    /// class VaisOpr is to operate the logs for Console window and Log file
    /// </remarks>
    class LogManager
    {
        private string mTitle;

        private string mPath;

        private StreamWriter mFile = null;

        [DllImport( "user32.dll" )]
        public static extern IntPtr FindWindow( string lpClassName, string lpWindowName );

        [DllImport( "user32.dll" )]
        private static extern bool ShowWindow( IntPtr aWnd, int aCmdShow );

        /// <summary>
        /// Sets the visibility of console window.
        /// </summary>
        /// <param name="aVisible">Determine if the window is visible.</param>
        /// <param name="aTitle">Title of the console window.</param>
        private static void SetConsoleVisibility( bool aVisible, string aTitle )
        {
            IntPtr hWnd = FindWindow( null, aTitle );
            if ( hWnd != IntPtr.Zero )
            {
                if ( !aVisible )
                    ShowWindow( hWnd, 0 ); // 0 = SW_HIDE               
                else
                    ShowWindow( hWnd, 1 ); //1 = SW_SHOWNORMA          
            }
        }

        /// <summary>
        /// print the logs 
        /// </summary>
        /// <remarks>
        /// Write(Print) the logs into log file and if cannot , set the console window to be visible and write on the console window  
        /// </remarks>
        /// <param name="aLog"></param>
        public void PrintLine( object aLog )
        {
            if ( mFile == null )
            {
                SetConsoleVisibility( true, mTitle );
                Console.WriteLine( aLog );
                //Console.WriteLine( "Error : log file can not be Written! " );
            }
            else
            {
                mFile.WriteLine( aLog );
            }
        }

        /// <summary>
        /// Print the logs and wait for Quit  
        /// </summary>
        /// <remarks>
        /// Print the logs and wait for user input to choose Quit or continue
        /// </remarks>
        /// <param name="aLog"></param>
        public void PrintForQuit( string aLog )
        {
            SetConsoleVisibility( true, mTitle );
            if ( mFile != null )
            {
                mFile.WriteLine( aLog );
            }
            Console.WriteLine( aLog );
            Console.WriteLine( "Would you want to quit or continue? Y or Others" );
            char lKeyChar = Console.ReadKey().KeyChar;
            if ( lKeyChar == 'y' || lKeyChar == 'Y' )
            {
                try
                {
                    mFile.Close();
                    mFile = null;
                }
                catch
                {
                }
                finally
                {
                    Environment.Exit( 0 );
                }
            }
            else
            {
                if ( mFile != null )
                {
                    mFile.WriteLine( "Continue and ignore the error....." );
                }
                Console.WriteLine( "Continue and ignore the error....." );
            }
        }

        /// <summary>
        /// constructor  
        /// </summary>
        /// <param name="aPath"></param>
        public LogManager( string aPath )
        {
            mTitle = "I-SIVB App";
            Console.Title = mTitle;
            SetConsoleVisibility( false, mTitle );

            if ( aPath == "" )
            {
                mPath = "I-SIVBErrorLog.log";
            }
            else
            {
                mPath = aPath;
            }

            try
            {
                mFile = new StreamWriter( mPath );
            }
            catch ( Exception e )
            {
                PrintLine( e.Message );
            }

        }

        /// <summary>
        /// distructor  
        /// </summary>
        ~LogManager()
        {
            if ( mFile != null )
            {
                try
                {
                    mFile.Close();
                }
                catch { }
            }
        }
    }

    /// <summary>
    /// class LogGlobalManager is used for it's global static data  
    /// </summary>
    class LogGlobalManager
    {
        public static LogManager LogMgr = new LogManager( "" );
    }
}
