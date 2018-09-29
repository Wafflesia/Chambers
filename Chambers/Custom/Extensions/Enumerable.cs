using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Chambers.Custom.Extensions
{

public static class EnumerableExtensions
    {
        public static List<SelectListItem> ToSelectList<T>(this IEnumerable<T> enumerable, Func<T, string> text, Func<T, string> value)
        {
            return ToSelectList<T>(enumerable, text, value, null, null);
        }

        public static List<SelectListItem> ToSelectList<T>(this IEnumerable<T> enumerable, Func<T, string> text, Func<T, string> value, string defaultOption)
        {
            return ToSelectList<T>(enumerable, text, value, defaultOption, null);
        }

        /// <summary>
        /// Creates a SelectList from the current IEnumerable&lt;T&gt;.
        /// </summary>
        /// <returns></returns>
        public static List<SelectListItem> ToSelectList<T>(this IEnumerable<T> enumerable, Func<T, string> text, Func<T, string> value, string defaultOption, string selectedValue)
        {
            var items = enumerable.Select(f => new SelectListItem()
            {
                Text = text(f),
                Value = value(f),
                Selected = value(f) == selectedValue
            }).ToList();

            if (defaultOption != null)
            {
                items.Insert(0, new SelectListItem()
                {
                    Text = defaultOption,
                    Value = ""
                });
            }
            return items;
        }
    }
}