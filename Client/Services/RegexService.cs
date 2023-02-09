using AsanRegEx.Client.Models;
using Microsoft.Extensions.Options;
using System.Text.RegularExpressions;

namespace AsanRegEx.Client.Services;

public class RegexService
{
    public MatchResultModel[] GetMatches(MatchRequestModel request)
    {
        var options = RegexOptions.None;
        if (request.IgnoreCase) options |= RegexOptions.IgnoreCase;
        if (request.Multiline) options |= RegexOptions.Multiline;
        if (request.ExplicitCapture) options |= RegexOptions.ExplicitCapture;
        if (request.Singleline) options |= RegexOptions.Singleline;
        if (request.IgnorePatternWhitespace) options |= RegexOptions.IgnorePatternWhitespace;
        if (request.RightToLeft) options |= RegexOptions.RightToLeft;
        if (request.ECMAScript) options |= RegexOptions.ECMAScript;
        if (request.CultureInvariant) options |= RegexOptions.CultureInvariant;
        if (request.NonBacktracking) options |= RegexOptions.NonBacktracking;

        var matches = Regex.Matches(request.Input, request.Pattern, options);

        var result = new MatchResultModel[matches.Count];
        for (int i = 0; i < result.Length; i++)
        {
            var match = matches[i];
            if (!match.Success) continue;

            var item = new MatchResultModel(match.Name, match.Value, match.Index, match.Length);

            foreach (Capture cap in match.Captures)
            {
                item.Captues.Add(new CaptureModel(cap.Value, cap.Index, cap.Length));
            }

            foreach (Group grp in match.Groups)
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
}