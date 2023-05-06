using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalismanFilter
{
    public class DuplicateLineInfo : BindableBase
    {
        private string _Skill1 = string.Empty;
        public string Skill1
        {
            get { return _Skill1; }
            set { SetProperty(ref _Skill1, value); }
        }

        private int _Skill1Level = 0;
        public int Skill1Level
        {
            get { return _Skill1Level; }
            set { SetProperty(ref _Skill1Level, value); }
        }

        public string Skill1WithLevel
        {
            get { return Skill1 + Skill1Level.ToString(); }
        }

        private string _Skill2 = string.Empty;
        public string Skill2
        {
            get { return _Skill2; }
            set { SetProperty(ref _Skill2, value); }
        }

        private int _Skill2Level = 0;
        public int Skill2Level
        {
            get { return _Skill2Level; }
            set { SetProperty(ref _Skill2Level, value); }
        }
        public string Skill2WithLevel
        {
            get { return Skill2 + Skill2Level.ToString(); }
        }

        private int _Slot1 = 0;
        public int Slot1
        {
            get { return _Slot1; }
            set { SetProperty(ref _Slot1, value); }
        }

        private int _Slot2 = 0;
        public int Slot2
        {
            get { return _Slot2; }
            set { SetProperty(ref _Slot2, value); }
        }

        private int _Slot3 = 0;
        public int Slot3
        {
            get { return _Slot3; }
            set { SetProperty(ref _Slot3, value); }
        }

        private int _Num = 0;
        public int Num
        {
            get { return _Num; }
            set { SetProperty(ref _Num, value); }
        }

        public DuplicateLineInfo(string skill1, string skill1Level, string skill2, string skill2Level, string slot1, string slot2, string slot3, int num)
        {
            _Skill1 = skill1;
            _Skill1Level = int.Parse(skill1Level);
            _Skill2 = skill2;
            _Skill2Level = int.Parse(skill2Level);
            _Slot1 = int.Parse(slot1);
            _Slot2 = int.Parse(slot2);
            _Slot3 = int.Parse(slot3);
            _Num = num;
        }
    }
}
