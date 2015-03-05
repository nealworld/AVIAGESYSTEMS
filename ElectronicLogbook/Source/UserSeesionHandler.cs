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

        public void StartSession() {
            mUser = new UserSessionViewModel(GetUserName());
        }

        public void EndSession() {
            mUser.mEndTime = DateTime.UtcNow.ToLocalTime().ToString();
            FileStream lfs = new FileStream("ELBRemark\\User.txt", FileMode.Append);
            StreamWriter lsw = new StreamWriter(lfs);
            lsw.WriteLine(mUser.ToString());
            lsw.Flush();
            lsw.Close();
            lfs.Close();
        }
        public String GetUserName() 
        {
            String lName = "Your Name";
            while(Utility.InputBox("User Session", "Name:", ref lName)== DialogResult.Cancel){
                
            }
            return lName;
        }


    }
}
