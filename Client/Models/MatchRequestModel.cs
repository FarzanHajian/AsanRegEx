namespace AsanRegEx.Client.Models;

public class MatchRequestModel
{
    public string Pattern { get; set; } = "";
    public string Input { get; set; } = "";
    public bool IgnoreCase { get; set; }
    public bool Multiline { get; set; }
    public bool Singleline { get; set; }
    public bool ExplicitCapture { get; set; }
    public bool IgnorePatternWhitespace { get; set; }
    public bool RightToLeft { get; set; }
    public bool ECMAScript { get; set; }
    public bool CultureInvariant { get; set; }
    public bool NonBacktracking { get; set; }
}