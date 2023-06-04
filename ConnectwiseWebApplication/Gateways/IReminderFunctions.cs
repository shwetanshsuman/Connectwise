using ConnectwiseWebApplication.Models;

namespace ConnectwiseWebApplication.Gateways
{
	public interface IReminderFunctions
	{
        public Task<IEnumerable<Reminder>> GetPendingInvoiceReminders();
        public Task<bool> CreateReminderLog(ReminderLog reminderLog);
        public void UpdateDate(Reminder reminder);

    }
}