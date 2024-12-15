using GYMFeeManagement_System_BE.Enums;

namespace GYMFeeManagement_System_BE.Entities
{
    public class EmailTemplate
    {
        public Guid Id { get; set; }
        public EmailTypes emailTypes { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
    }
}
