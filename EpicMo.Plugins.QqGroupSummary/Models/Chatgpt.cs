namespace EpicMo.Plugins.QqGroupSummary.Models;

public class Chatgpt
{
    public string model { get; set; } = "gpt-3.5-turbo";
    public List<GptSaying> messages { get; set; } = new();
}