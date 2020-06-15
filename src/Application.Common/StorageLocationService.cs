using System;
using System.IO;

namespace Application.Common
{

    public sealed class StorageLocationService
    {
        public static IStorageService GetStorageService(string location)
        {
            if (!String.IsNullOrEmpty(location))
            {
                if (IsLocalStorage(location)) return new LocalStorageService(location);
                var result = IsOnlineStorage(location);
                if (result.IsOnline) return new OnlineStorageService(result.Uri);
            }

            return null;
        }


        private static (bool IsOnline, Uri Uri) IsOnlineStorage(string location)
        {
            try
            {
                Uri result;
                return (Uri.TryCreate(location, UriKind.Absolute, out result) &&
                        (result.Scheme == Uri.UriSchemeHttp || result.Scheme == Uri.UriSchemeHttps), result);
            }
            catch (System.Exception)
            {
                return (false, null);
            }
        }

        private static bool IsLocalStorage(string location)
        {
            try
            {
                return File.Exists(location);
            }
            catch (System.Exception)
            {
                return false;
            }
        }

    }
}