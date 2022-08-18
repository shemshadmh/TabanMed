
namespace Application.Dtos.Application
{
    public class NotificationDto
    {

        public NotificationDto(bool status, string text)
        {
            Status = status;
            Text = text;
        }

        public bool Status { get; set; }
        public string Text { get; set; }

    }
}
