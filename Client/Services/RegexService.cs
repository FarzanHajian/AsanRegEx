using AsanRegEx.Client.Models;
using AsanRegEx.Client.Models.Abstractions;
using System.Text.RegularExpressions;

namespace AsanRegEx.Client.Services;

public class RegexService
{
    public MatchResultModel[] GetMatches(MatchRequestModel request)
    {
        var options = CreateRegexOptions(request);
        var matches = Regex.Matches(request.Input, request.Pattern, options);

        var result = new MatchResultModel[matches.Count];
        for (int i = 0; i < result.Length; i++)
        {
            var match = matches[i];
            if (!match.Success) continue;

            var item = new MatchResultModel(match.Name, match.Value, match.Index, match.Length);

            foreach (Capture cap in match.Captures.Cast<Capture>())
            {
                item.Captues.Add(new CaptureModel(cap.Value, cap.Index, cap.Length));
            }

            foreach (Group grp in match.Groups.Cast<Group>())
            {
                if (!grp.Success) continue;
                item.Groups.Add(new GroupModel(grp.Name, grp.Value, grp.Index, grp.Length));
            }

            result[i] = item;
        }

        return result;
    }

    public void EscapeString(EscapeRequestModel request)
    {
        request.Input = Regex.Escape(request.Input);
    }

    public void UnescapeString(EscapeRequestModel request)
    {
        request.Input = Regex.Unescape(request.Input);
    }

    public ReplaceResultModel Replace (ReplaceRequestModel request)
    {
        var options = CreateRegexOptions(request);
        return new ReplaceResultModel { ReplacedString = Regex.Replace(request.Input, request.Pattern, request.Replacement, options) };
    }

    private RegexOptions CreateRegexOptions(IMatchOptionsModel model)
    {
        var result = RegexOptions.None;
        if (model.IgnoreCase) result |= RegexOptions.IgnoreCase;
        if (model.Multiline) result |= RegexOptions.Multiline;
        if (model.ExplicitCapture) result |= RegexOptions.ExplicitCapture;
        if (model.Singleline) result |= RegexOptions.Singleline;
        if (model.IgnorePatternWhitespace) result |= RegexOptions.IgnorePatternWhitespace;
        if (model.RightToLeft) result |= RegexOptions.RightToLeft;
        if (model.ECMAScript) result |= RegexOptions.ECMAScript;
        if (model.CultureInvariant) result |= RegexOptions.CultureInvariant;
        if (model.NonBacktracking) result |= RegexOptions.NonBacktracking;
        return result;
    }
}