using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Utils
{
    public static class FnUtil
    {
        // Xoá dấu tiếng Việt
        public static string RemoveVNAccents(string val)
        {
            if (string.IsNullOrWhiteSpace(val)) return val;

            var normalizedStr = val.Trim().Normalize(NormalizationForm.FormD);
            var strBuilder = new StringBuilder();

            foreach (char c in normalizedStr)
            {
                var category = CharUnicodeInfo.GetUnicodeCategory(c);
                if (category != UnicodeCategory.NonSpacingMark)
                {
                    strBuilder.Append(c);
                }
            }

            return strBuilder.ToString().Normalize(NormalizationForm.FormC);
        }
    }
}
