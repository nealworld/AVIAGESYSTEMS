using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace ElectronicLogbook.ViewModel
{
    [XmlRoot("DailyRemark")]
    public class DailyRemarkViewModel : ViewModel
    {
        public override Boolean Compare(ViewModel aViewModel)
        {
            throw new NotImplementedException();
        }

        private String _time;

        [XmlAttribute("Time")]
        public String mTime {
            set
            {
                _time = value;
                this.OnPropertyChanged("mTime");
            }
            get
            {
                return _time;
            } 
        }

        private String _location;
        [XmlAttribute("Location")]
        public String mLocation {
            set
            {
                mIsModified = "*";
                _location = value;
                this.OnPropertyChanged("mLocation");
            }
            get
            {
                return _location;
            } 
        }

        private String _TestTeam;
        [XmlAttribute("TestTeam")]
        public String mTestTeam {
            set
            {
                mIsModified = "*";
                _TestTeam = value;
                this.OnPropertyChanged("mTestTeam");
            }
            get
            {
                return _TestTeam;
            } 
        }

        private String _TestPurposeNumber;
        [XmlAttribute("TestPurposeNumber")]
        public String mTestPurposeNumber{
            set
            {
                mIsModified = "*";
                _TestPurposeNumber = value;
                this.OnPropertyChanged("mTestPurposeNumber");
            }
            get
            {
                return _TestPurposeNumber;
            } 
        }

        private String _TestTitle;
        [XmlAttribute("TestTitle")]
        public String mTestTitle {
            set
            {
                mIsModified = "*";
                _TestTitle = value;
                this.OnPropertyChanged("mTestTitle");
            }
            get
            {
                return _TestTitle;
            } 
        }

        private String _TestEnvironment;
        [XmlAttribute("TestEnvironment")]
        public String mTestEnvironment {
            set
            {
                mIsModified = "*";
                _TestEnvironment = value;
                this.OnPropertyChanged("mTestEnvironment");
            }
            get
            {
                return _TestEnvironment;
            } 
        }

        private String _TestLeader;
        [XmlAttribute("TestLeader")]
        public String mTestLeader {
            set
            {
                mIsModified = "*";
                _TestLeader = value;
                this.OnPropertyChanged("mTestLeader");
            }
            get
            {
                return _TestLeader;
            } 
        }

        private String _OtherParticipants;
        [XmlAttribute("OtherParticipants")]
        public String mOtherParticipants {
            set
            {
                mIsModified = "*";
                _OtherParticipants = value;
                this.OnPropertyChanged("mOtherParticipants");
            }
            get
            {
                return _OtherParticipants;
            } 
        }

        private String _TestRecords;
        [XmlAttribute("TestRecords")]
        public String mTestRecords {
            set
            {
                mIsModified = "*";
                _TestRecords = value;
                this.OnPropertyChanged("mTestRecords");
            }
            get
            {
                return _TestRecords;
            } 
        }

        private String _ExistQuestion;
        [XmlAttribute("ExistQuestion")]
        public String mExistQuestion {
            set
            {
                mIsModified = "*";
                _ExistQuestion = value;
                this.OnPropertyChanged("mExistQuestion");
            }
            get
            {
                return _ExistQuestion;
            } 
        }

        private String _QuestionRecorder;
        [XmlAttribute("QuestionRecorder")]
        public String mQuestionRecorder {
            set
            {
                mIsModified = "*";
                _QuestionRecorder = value;
                this.OnPropertyChanged("mQuestionRecorder");
            }
            get
            {
                return _QuestionRecorder;
            } 
        }

        private String _QuestionVerifier;
        [XmlAttribute("QuestionVerifier")]
        public String mQuestionVerifier
        {
            set
            {
                mIsModified = "*";
                _QuestionVerifier = value;
                this.OnPropertyChanged("mQuestionVerifier");
            }
            get
            {
                return _QuestionVerifier;
            }
        }

        private String _Solution;
        [XmlAttribute("Solution")]
        public String mSolution {
            set
            {
                mIsModified = "*";
                _Solution = value;
                this.OnPropertyChanged("mSolution");
            }
            get
            {
                return _Solution;
            }
        }

        private String _Solver;
        [XmlAttribute("Solver")]
        public String mSolver
        {
            set
            {
                mIsModified = "*";
                _Solver = value;
                this.OnPropertyChanged("mSolver");
            }
            get
            {
                return _Solver;
            }
        }

        private String _SolutionVerifier;
        [XmlAttribute("SolutionVerifier")]
        public String mSolutionVerifier
        {
            set
            {
                mIsModified = "*";
                _SolutionVerifier = value;
                this.OnPropertyChanged("mSolutionVerifier");
            }
            get
            {
                return _SolutionVerifier;
            }
        }

        private String _TestName;
        [XmlAttribute("TestName")]
        public String mTestName {
            set
            {
                mIsModified = "*";
                _TestName = value;
                this.OnPropertyChanged("mTestName");
            }
            get
            {
                return _TestName;
            }
        }

        private String _IsModified;
        [XmlIgnore()]
        public String mIsModified {
            set
            {
                _IsModified = value;
                this.OnPropertyChanged("mIsModified");
            }
            get
            {
                return _IsModified;
            }
        }

        [XmlIgnore()]
        public String mRemarkFileDir {
            get
            {
                String lYear = String.Empty;
                String lMonth = String.Empty;
                String lDay = String.Empty;
                parseDate(ref lYear, ref lMonth, ref lDay, mTime);

                String lDir = lYear + "\\" + lMonth + "\\" + lDay;
                return lDir;
            }
        }

        private void parseDate(ref string aYear, ref string aMonth, ref string aDay, String aDate)
        {
            String[] lDate = aDate.Split('/');
            aYear = (lDate[2].Split(' '))[0];
            aMonth = lDate[0];
            aDay = lDate[1];
        }

        public DailyRemarkViewModel(string aTestName) 
        {
            mTestName = aTestName;
            mIsModified = "*";
        }

        public DailyRemarkViewModel()
        {
            mIsModified = String.Empty;
        }
    }
}
