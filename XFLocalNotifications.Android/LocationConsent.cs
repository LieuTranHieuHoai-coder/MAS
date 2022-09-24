using System.Threading.Tasks;
using Xamarin.Essentials;

namespace XFLocalNotifications.Droid
{
    public class LocationConsent : ILocationConsent
    {
        public async Task GetLocationConsent()
        {
            await Permissions.RequestAsync<Permissions.LocationAlways>();
        }
    }
}