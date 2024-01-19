namespace BibleTalkAI.OpenAI.Threads.Models;

public struct MessageContent
{
    public string Type { get; set; } // "text" "image_file"
    public MessageContentText? Text { get; set; }
}
