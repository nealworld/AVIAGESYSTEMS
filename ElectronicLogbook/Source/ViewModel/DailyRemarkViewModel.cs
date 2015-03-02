using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ElectronicLogbook.ViewModel
{
    public class DailyRemarkViewModel : ViewModel
    {
        public override Boolean Compare(ViewModel aViewModel)
        {
            throw new NotImplementedException();
        }

        private String _time;
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
        public String mLocation {
            set
            {
                _location = value;
                this.OnPropertyChanged("mLocation");
            }
            get
            {
                return _location;
            } 
        }

        private String _TestTeam;
        public String mTestTeam {
            set
            {
                _TestTeam = value;
                this.OnPropertyChanged("mTestTeam");
            }
            get
            {
                return _TestTeam;
            } 
        }

        private String _TestPurposeNumber;
        public String mTestPurposeNumber{
            set
            {
                _TestPurposeNumber = value;
                this.OnPropertyChanged("mTestPurposeNumber");
            }
            get
            {
                return _TestPurposeNumber;
            } 
        }

        private String _TestTitle;
        public String mTestTitle {
            set
            {
                _TestTitle = value;
                this.OnPropertyChanged("mTestTitle");
            }
            get
            {
                return _TestTitle;
            } 
        }

        private String _TestEnvironment;
        public String mTestEnvironemnt {
            set
            {
                _TestEnvironment = value;
                this.OnPropertyChanged("mTestEnvironemnt");
            }
            get
            {
                return _TestEnvironment;
            } 
        }

        private String _TestLeader;
        public String mTestLeader {
            set
            {
                _TestLeader = value;
                this.OnPropertyChanged("mTestLeader");
            }
            get
            {
                return _TestLeader;
            } 
        }

        private String _OtherParticipants;
        public String mOtherParticipants {
            set
            {
                _OtherParticipants = value;
                this.OnPropertyChanged("mOtherParticipants");
            }
            get
            {
                return _OtherParticipants;
            } 
        }

        private String _TestRecords;
        public String mTestRecords {
            set
            {
                _TestRecords = value;
                this.OnPropertyChanged("mTestRecords");
            }
            get
            {
                return _TestRecords;
            } 
        }

        private String _ExistQuestion;
        public String mExistQuestion {
            set
            {
                _ExistQuestion = value;
                this.OnPropertyChanged("mExistQuestion");
            }
            get
            {
                return _ExistQuestion;
            } 
        }

        private String _QuestionRecorder;
        public String mQuestionRecorder {
            set
            {
                _QuestionRecorder = value;
                this.OnPropertyChanged("mQuestionRecorder");
            }
            get
            {
                return _QuestionRecorder;
            } 
        }

        private String _QuestionVerifier;
        public String mQuestionVerifier
        {
            set
            {
                _QuestionVerifier = value;
                this.OnPropertyChanged("mQuestionVerifier");
            }
            get
            {
                return _QuestionVerifier;
            }
        }

        private String _Solution;
        public String mSolution {
            set
            {
                _Solution = value;
                this.OnPropertyChanged("mSolution");
            }
            get
            {
                return _Solution;
            }
        }

        private String _Solver;
        public String mSolver
        {
            set
            {
                _Solver = value;
                this.OnPropertyChanged("mSolver");
            }
            get
            {
                return _Solver;
            }
        }

        private String _SolutionVerifier;
        public String mSolutionVerifier
        {
            set
            {
                _SolutionVerifier = value;
                this.OnPropertyChanged("mSolutionVerifier");
            }
            get
            {
                return _SolutionVerifier;
            }
        }

        private String _TestName;
        public String mTestName {
            set
            {
                _TestName = value;
                this.OnPropertyChanged("mTestName");
            }
            get
            {
                return _TestName;
            }
        }

        private String _IsModified;
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
        public DailyRemarkViewModel(string aTestName) {
            mTestName = aTestName;
            mIsModified = "*";
        }
    }
}
