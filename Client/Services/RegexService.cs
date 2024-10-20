﻿using AsanRegEx.Client.Models;
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

    public string EscapeString(EscapeRequestModel request)
    {
        return Regex.Escape(request.Input);
    }

    public string UnescapeString(EscapeRequestModel request)
    {
        return Regex.Unescape(request.Input);
    }

    public string Replace(ReplaceRequestModel request)
    {
        var options = CreateRegexOptions(request);
        return Regex.Replace(request.Input, request.Pattern, request.Replacement, options);
    }

    public SplitResultModel Split(SplitRequestModel request)
    {
        var options = CreateRegexOptions(request);
        var temp = Regex.Split(request.Input, request.Pattern, options);
        return new SplitResultModel(string.Join('\n', temp), temp.Length);
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