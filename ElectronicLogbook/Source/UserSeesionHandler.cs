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
        public bool mIsStartSuccess;

        public UserSeesionHandler() {
            mIsStartSuccess = true;
        }

        public bool StartSession() {
            Utility.MakeDateDir("ELBRemark");
            mUser = new UserSessionViewModel(GetUserName());

            return mIsStartSuccess;
        }

        public void EndSession() {

            if (!mIsStartSuccess) return;

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
            DialogResult lresult = Utility.InputBox("User Session", "Name:", ref lName);
            if(Utility.InputBox("User Session", "Name:", ref lName) == DialogResult.Cancel){
                mIsStartSuccess = false;
            }
            return lName;
        }


    }
}
