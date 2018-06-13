using System;
using System.Collections.Generic;

namespace R3MUS.Devpack.Recruitment.Models
{
    public class MailSummaryModel
    {
        public long Id { get; set; }
        public long OwnerId { get; set; }
        public string Subject { get; set; }
        public DateTime TimeStamp { get; set; }
        public string InOrOut { get; set; }
        public string Sender { get; set; }
        public List<string> Recipients { get; set; }

        public string RecipientsLine
        {
            get
            {
                return string.Join(", ", Recipients);
            }
        }
    }    
}