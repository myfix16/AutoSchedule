using AutoSchedule.Core.Models;

namespace AutoSchedule.UI.Server.Models
{
    public class DisplayedSession
    {
        public string SessionType { get; set; }
        public SessionTime SessionTime { get; set; }
        public string Instructor { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public int RowSpan { get; set; }
        public bool IsPlaceHolder { get; set; } = false;

        public override string ToString() => $"{Name} {Code} {Instructor} {SessionTime}";
    }
}
