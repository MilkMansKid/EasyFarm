﻿
/*///////////////////////////////////////////////////////////////////
Copyright (C) <Zerolimits>

This program is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
*/
///////////////////////////////////////////////////////////////////

using FFACETools;
using System;
using System.Collections.Generic;
using System.Linq;
using ChatLine = FFACETools.FFACE.ChatTools.ChatLine;
using ChatTools = FFACETools.FFACE.ChatTools;

namespace EasyFarm.Classes
{
    public static class Extensions
    {
        /// <summary>
        /// Adds qoutes around the string. 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static String QuoteWord(this String value)
        {
            return '"' + value + '"';
        }

        /// <summary>
        /// Returns the objects that repeats the most. 
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        public static IEnumerable<T> Repeats<T>(this IEnumerable<T> values)
        {
            return RepeatsN(values, 1);
        }

        public static IEnumerable<T> RepeatsN<T>(this IEnumerable<T> values, int count)
        {
            return values.GroupBy(x => x)
                .Where(x => x.Count() >= count)
                .SelectMany(x => Enumerable.Repeat(x.Key, x.Count()));
        }
    }
}
