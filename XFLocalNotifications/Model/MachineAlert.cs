using System;
using System.Collections.Generic;
using System.Text;

namespace XFLocalNotifications.Model
{
    public class MachineAlert
    {
        public string ID_Button { get; set; }
        public string ButtonName { get; set; }
        public string ID_Line { get; set; }
        public string NameLine { get; set; }
        public string ID_Factory { get; set; }
        public string NameFactory { get; set; }
        public string CreateTime { get; set; }
        public string BeginFixTime { get; set; }
    }
}
