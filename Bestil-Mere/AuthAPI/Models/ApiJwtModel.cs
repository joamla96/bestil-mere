namespace AuthAPI.Models
{
    public class ApiJwtModel
    {
        public string access_token { get; set; }
        public int expires_in { get; set; }
        public string username { get; set; }
    }
}