using System;
using System.Threading.Tasks;
using Application.Common.Model;
using CustomerInvitationApp.Model;

namespace CustomerInvitationApp.Data
{
    public static class ExtensionMethods
    {
        public static Task<double> GetDistanceFromMainOfficeAsync(this Customer customer, Location officeLocation)
        {
            if (officeLocation == null) return Task.FromResult<double>(0);
            double customerLatitude = string.IsNullOrEmpty(customer.Latitude) ? 0 : double.Parse(customer.Latitude);
            double customerLongitude = string.IsNullOrEmpty(customer.Longitude) ? 0 : double.Parse(customer.Longitude);
            
            var phi1 = officeLocation.Latitude * Math.PI / 180;
            var phi2 = customerLatitude * Math.PI / 180;
            var spectralWidth = (customerLongitude - officeLocation.Longitude) * Math.PI / 180;
            var radians = 6371e3;

            var result = Math.Acos(Math.Sin(phi1) * Math.Sin(phi2) + Math.Cos(phi1) * Math.Cos(phi2) * Math.Cos(spectralWidth)) * radians;
            return Task.FromResult<double>(Math.Ceiling(result / 1000));
        }
    }
}