namespace Library.Services
{
    using Library.Models;
    using Plugin.Connectivity;
    using System.Threading.Tasks;


    public static class Connection
    {
        public static async Task<Response> CheckConnection()
        {
            if (!CrossConnectivity.Current.IsConnected)
                return new Response
                {
                    IsSuccess = false,
                    Message = "Por favor active su internet!"
                };

            return new Response
            {
                IsSuccess = true,
                Message = "OK"
            };
        }
    }
}
