using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ElectronicLogbook.ViewModel
{
    public class UserSessionViewModel
    {
        public String mName;
        public String mStartTime;
        public String mEndTime;

        public UserSessionViewModel(String aName) {
            mName = aName;
            mStartTime = DateTime.UtcNow.ToLocalTime().ToString();
        }

        public override string ToString()
        {
            return "Name:" + mName + ",Start Time:" + mStartTime + 
                ",End Time:" + mEndTime + "\n";
        } 

    }
}
