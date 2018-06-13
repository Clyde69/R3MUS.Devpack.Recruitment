namespace R3MUS.Devpack.Recruitment.Models
{
    public class MailModel : MailSummaryModel
    {
        public new string RecipientsLine { get; set; }
        public string Body { get; set; }
    }
}