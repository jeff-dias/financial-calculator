using System.Runtime.CompilerServices;
using Xunit;

namespace Fms.Shared.Tests.Attributes
{
    public class CustomFactAttribute : FactAttribute
    {
        private const string Underline = "_";
        private const string Empty = " ";

        public CustomFactAttribute(string charsToReplace = Underline, string replacementChars = Empty, [CallerMemberName] string testMethodName = null)
        {
            if (charsToReplace != null)
            {
                base.DisplayName = testMethodName?.Replace(charsToReplace, replacementChars);
            }
        }
    }
}
