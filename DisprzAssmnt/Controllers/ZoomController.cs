using DisprzAssmnt.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace DisprzAssmnt.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ZoomController : Controller
    {
        // GET: ZoomController
        private string ACCESS_TOKEN = "access_token_will_be_updated_after_getting_response";
        [HttpGet(Name = "redirect")]
        public async Task<ObjectResult> Redirect()
        {

            string clientID = "clientID";
            string clientSecret = "clientsecret";
            string code = Request.Query["code"];
            string oAuthPostUrl = "https://zoom.us/oauth/token";
            string contentType = "application/x-www-form-urlencoded";
            string authorization = string.Format("{0}:{1}", clientID, clientSecret);

            Dictionary<String, string> datas = new Dictionary<string, string>();
            datas.Add("code", code);
            datas.Add("grant_type", "authorization_code");
            datas.Add("redirect_uri", "http://localhost:5114/Zoom/Redirect");
     
            using HttpClient client = new();
            var req = new HttpRequestMessage(HttpMethod.Post, oAuthPostUrl);

            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(authorization);

            client.DefaultRequestHeaders.Add("Authorization", "Basic " + System.Convert.ToBase64String(plainTextBytes));
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(contentType));//ACCEPT header

         
            HttpContent content = new FormUrlEncodedContent(datas);
            content.Headers.ContentType = new MediaTypeHeaderValue(contentType);

            req.Content = content;
            var res = await client.SendAsync(req);
            var resp = await res.Content.ReadAsStringAsync();

            if (resp != null)
            {
                ZoomToken token = JsonConvert.DeserializeObject<ZoomToken>(resp);
                ACCESS_TOKEN = token.Access_Token;

            }
            return Ok(resp);
        }

        [HttpGet(Name = "CreateMeeting")]
        public async Task<IActionResult> CreateMeeting(string emailAddress)


        {
            string api = String.Format("https://api.zoom.us/v2/users/{0}/meetings", emailAddress);
            using HttpClient client = new();
            var req = new HttpRequestMessage(HttpMethod.Post, api);

            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + ACCESS_TOKEN);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            string strContent =  JsonConvert.SerializeObject(new { topic = "Meeting with Umesh", duration = "10", start_time = "2023-07-09T05:00:00", type = "2" });

            HttpContent content = new StringContent(strContent, Encoding.UTF8, "application/json");
            req.Content=content;

            var res = await client.SendAsync(req);
            var resp = await res.Content.ReadAsStringAsync();

            return Ok(resp); 

        }

        
    }
}
