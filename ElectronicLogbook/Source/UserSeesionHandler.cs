using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ElectronicLogbook.ViewModel;
using System.Windows.Forms;
using ElectronicLogbook.ViewModel;
using System.IO;

namespace ElectronicLogbook
{
    public class UserSeesionHandler
    {
        UserSessionViewModel mUser;


        public UserSeesionHandler() {}

        public bool StartSession() {
            Utility.MakeDateDir("ELBRemark");
            mUser = new UserSessionViewModel(GetUserName());

            if (mUser.mName == String.Empty)
                return false;

            return true;
        }

        public void EndSession() {

            if (mUser.mName == String.Empty) return;

            mUser.mEndTime = DateTime.UtcNow.ToLocalTime().ToString();
            FileStream lfs = new FileStream("ELBRemark\\User.txt", FileMode.Append);
            StreamWriter lsw = new StreamWriter(lfs);
            lsw.WriteLine(mUser.ToString());
            lsw.Flush();
            lsw.Close();
            lfs.Close();
        }

        private String GetUserName() 
        {
            String lName = "Your Name";
            while(Utility.InputBox("User Session", "Name:", ref lName) == DialogResult.Cancel){
                lName = String.Empty;
            }
            return lName;
        }


    }
}
