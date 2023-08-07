namespace DisprzAssmnt.Models
{
    public class ZoomAuth
    {
        public string code { get; set; }
        public string grant_type { get; set; }
        public string redirect_uri { get; set; }
    }

    public class ZoomToken
    {
        public string Access_Token { get; set; }
        public string Token_Type { get; set; }
        public string Scope { get; set; }
    }
}
