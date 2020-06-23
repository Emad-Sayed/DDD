using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Application.Common.Validators
{
    public static class GuidValidator

    {

        private static Regex isGuid = new Regex(@"^({){0,1}[0-9a-fA-F]{8}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{12}(}){0,1}$", RegexOptions.Compiled);

        public static bool IsGuid(string candidate)

        {

            if (candidate != null)

            {

                if (isGuid.IsMatch(candidate))

                {

                    return true;

                }

            }

            return false;

        }

    }
}
