using AsanRegEx.Client.Models;
using System.Text.RegularExpressions;

namespace AsanRegEx.Client.Services;

public class MatchService
{
    private SortedSet<string> inputCache = new();
    private SortedSet<string> patternCache = new();

    public MatchModel[] GetMatches(RequestModel request)
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

        var result = new MatchModel[matches.Count];
        for (int i = 0; i < result.Length; i++)
        {
            var match = matches[i];
            if (!match.Success) continue;

            var item = new MatchModel(match.Name, match.Value, match.Index, match.Length);

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

    public IEnumerable<string> GetCachedInputs()
    {
        return inputCache.AsEnumerable();
    }

    public void StoreInputInCache(string input)
    {
        if (string.IsNullOrEmpty(input)) return;
        inputCache.Add(input);
    }

    public IEnumerable<string> GetCachedPatterns()
    {
        return patternCache.AsEnumerable();
    }

    public void StorePatternInCache(string pattern)
    {
        if (string.IsNullOrEmpty(pattern)) return;
        patternCache.Add(pattern);
    }
}