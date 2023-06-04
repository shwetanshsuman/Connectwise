using ConnectwiseWebApplication.Models;
using Newtonsoft.Json;

namespace ConnectwiseWebApplication.Controllers
{
    public class EmailFunction : IEmailFuntion
    {
        public async Task<MasterEmail> GetEmailById(int id)
        {
            using (var client = new HttpClient()) {
                using (var response = await client.GetAsync("https://localhost:7266/api/MasterEmails/"+id)) 

                {
                    string stringresponse = await response.Content.ReadAsStringAsync();
                    MasterEmail masterEmail=JsonConvert.DeserializeObject<MasterEmail>(stringresponse);
                    return masterEmail;

                }
            }
        }
    }
}
