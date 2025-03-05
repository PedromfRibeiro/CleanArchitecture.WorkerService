namespace CleanArchitecture.Core.Entities.Message.cs;

public class Message(string content)
{
    public string Content { get; set; } = content;
}
