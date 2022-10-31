using System;
using System.Collections.Generic;
using System.Text;

namespace XFLocalNotifications.Global
{
    public class Global
    {
        public static string globalFactory { get; set; }
        public static string factoryName { get; set; }

        public static string apiURL = "http://mas.qve.com/sewinglineqvl/qrconnect.php?ID_Keys=";
        //public static string apiURL = "http://192.168.1.141/sewinglineqvl/qrconnect.php?ID_Keys=";

        public static int? countItems { get; set; }
    }
}
