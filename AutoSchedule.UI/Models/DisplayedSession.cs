using AutoSchedule.Core.Models;

namespace AutoSchedule.UI.Models
{
    public class DisplayedSession
    {
        public string SessionType;
        public SessionTime SessionTime;
        public string Instructor;
        public string Code;
        public string Name;

        public int RowSpan;
        public bool IsPlaceHolder = false;

        public override string ToString() => $"{Name} {Code} {Instructor} {SessionTime}";
    }
}
