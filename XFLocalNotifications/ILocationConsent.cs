using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace XFLocalNotifications
{
    public interface ILocationConsent
    {
        Task GetLocationConsent();
    }
}
