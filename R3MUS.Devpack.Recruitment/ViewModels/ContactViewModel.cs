using R3MUS.Devpack.ESI.Models.Character;

namespace R3MUS.Devpack.Recruitment.ViewModels
{
    public class ContactViewModel : ContactModel
    {
        public string Name { get; set; }

        public string AlertStyle { get; set; }

        public void SetAlertStatus()
        {
            if (Standing > 0)
            {
                AlertStyle = "alert-success";
            }
            else if (Standing < 0)
            {
                AlertStyle = "alert-danger";
            }
            else
            {
                AlertStyle = string.Empty;
            }
        }
    }
}