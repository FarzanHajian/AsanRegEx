namespace AsanRegEx.Client.Models.Abstractions;

public interface IMatchOptionsModel
{
    bool IgnoreCase { get; set; }
    bool Multiline { get; set; }
    bool Singleline { get; set; }
    bool ExplicitCapture { get; set; }
    bool IgnorePatternWhitespace { get; set; }
    bool RightToLeft { get; set; }
    bool ECMAScript { get; set; }
    bool CultureInvariant { get; set; }
    bool NonBacktracking { get; set; }

}
