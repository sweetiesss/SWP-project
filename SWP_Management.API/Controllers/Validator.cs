using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Text.RegularExpressions;

namespace SWP_Management.API.Controllers
{
    public class Validator
    {
        public bool validate(string sample, string regex)
        {
            bool check = false;
            Regex re = new Regex(regex);
            return re.IsMatch(sample);
        }
    }
}
