using System.Text.RegularExpressions;

namespace Techs.Api.Shared;

public partial class ValidationGenerators
{
    [GeneratedRegex(@".+\@.+\..+")]
    public static partial Regex ValidEmailRegularExpression();
}
