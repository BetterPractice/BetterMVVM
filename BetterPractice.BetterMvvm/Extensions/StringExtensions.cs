﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace BetterPractice.BetterMvvm.Extensions
{
    public static class StringExtensions
    {
        public static string MaskToDigits(this string text)
        {
            return new string(text.Where(c => char.IsDigit(c)).ToArray());
        }

        public static string  MaskToAlphaNumeric(this string text)
        {
            return new string(text.Where(c => char.IsLetterOrDigit(c)).ToArray());
        }

        public static string Implode(this IEnumerable<char> charEnumeration)
        {
            return new string(charEnumeration.ToArray());
        }

    }
}
