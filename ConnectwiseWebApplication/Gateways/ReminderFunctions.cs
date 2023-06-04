using ConnectwiseWebApplication.Models;
using Newtonsoft.Json;

namespace ConnectwiseWebApplication.Gateways
{
	public class ReminderFunctions:IReminderFunctions
	{
		public async Task<IEnumerable<Reminder>> GetPendingInvoiceReminders()
		{
			IEnumerable<Reminder> reminders;
			using (var httpClient = new HttpClient())
			{
				using (var response = await httpClient.GetAsync("https://localhost:7266/api/ReminderPending"))
				{
					string apiResponse = await response.Content.ReadAsStringAsync();
					reminders = JsonConvert.DeserializeObject<List<Reminder>>(apiResponse);
				}
			}
			return reminders;
		}

		public async Task<bool> CreateReminderLog(ReminderLog reminderLog)
		{
			using(var httpClient = new HttpClient())
			{
				var response = await httpClient.PostAsJsonAsync("https://localhost:7266/api/ReminderLogs", reminderLog);
				if(response.IsSuccessStatusCode)
				{
					return true;
				}
				return false;
			}
		}

        public async void UpdateDate(Reminder reminder)
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.PutAsJsonAsync("https://localhost:7266/api/Reminders/"+reminder.ReminderId, reminder);
                
            }
        }
    }
}
