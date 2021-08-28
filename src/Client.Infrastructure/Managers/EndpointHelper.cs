using System.Linq;

namespace BlazorSchoolManager.Client.Infrastructure.Managers
{
    public static class EndpointHelper
    {
        public static string BASE_API_URL = "api/v1";
        public static string ChangePassword = "api/identity/account/changepassword";
        public static string UpdateProfile = "api/identity/account/updateprofile";

        public static string Save(string controllerName )=> $"{BASE_API_URL}/{controllerName}";

        public static string Delete(string controllerName) => $"{BASE_API_URL}/{controllerName}";

        public static string GetCount(string controllerName) => $"{BASE_API_URL}/{controllerName}/count";

        public static string Export(string controllerName) => $"{BASE_API_URL}/{controllerName}/export";

        public static string GetImage(string controlerName, int id) => $"{BASE_API_URL}/{controlerName}/image/{id}";

        public static string GetAllPaged(string controlerName, int pageNumber, int pageSize, string searchString, string[] orderBy)
        {
            var url = $"{BASE_API_URL}/{controlerName}?pageNumber={pageNumber}&pageSize={pageSize}&searchString={searchString}&orderBy=";
            if (orderBy?.Any() == true)
            {
                foreach (var orderByPart in orderBy)
                {
                    url += $"{orderByPart},";
                }
                url = url[..^1]; // loose training ,
            }
            return url;
        }

        public static string ExportFiltered(string controllerName, string searchString) => 
            $"{Export(controllerName)}?searchString={searchString}";
    }
}
