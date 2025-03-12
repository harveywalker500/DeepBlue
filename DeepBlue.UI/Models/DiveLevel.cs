namespace DeepBlue.Models;

public class DiveLevel
{
    public int depth { get; set; }
    public int time { get; set; }
    public Gas gas { get; set; }
    public bool isDecoStop { get; set; }
}