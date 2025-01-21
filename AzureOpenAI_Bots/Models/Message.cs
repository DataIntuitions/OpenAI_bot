namespace AzureOpenAI_Bots.Models
{
    public class Message
    {
        public DateTime TimeStamp { get; set; }
        public bool IsRequest { get; set; }
        public string? Body { get; set; }

        public Message(bool IsRequest, string Body)
        {
            TimeStamp = DateTime.Now;
            this.IsRequest = IsRequest;
            this.Body = Body;
        }

    }
}
