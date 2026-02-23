using LibrarySystemMcv.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Reflection;
using System.Diagnostics;
using System.ComponentModel.DataAnnotations;

namespace LibrarySystemMcv.Utils {
    public static class Functions {
        public static List<T> Filter<T>(List<T> data, Predicate<T> match) {
            return data.Where(d => match(d)).ToList();
        }

        public static List<T> SortByProperty<T, TProperty>(
            List<T> data,
            Func<T, TProperty> propertySelector,
            SortDirection direction = SortDirection.Ascending
        ) {
            if (direction == SortDirection.Ascending)
                return data.OrderBy(propertySelector).ToList();
            else
                return data.OrderByDescending(propertySelector).ToList();
        }

        public static List<T> SearchBySubstring<T>(List<T> data, string substring) {
            if (data == null)
                throw new ArgumentNullException(nameof(data));
            if (string.IsNullOrEmpty(substring))
                return new List<T>(data);

            var stringProperties = typeof(T)
                .GetProperties()
                .Where(p => p.PropertyType == typeof(string) && p.CanRead)
                .ToArray();

            return data
                .Where(item => item != null &&
                    stringProperties.Any(prop => {
                        try {
                            var value = prop.GetValue(item) as string;
                            return !string.IsNullOrEmpty(value) &&
                        value.IndexOf(substring, StringComparison.OrdinalIgnoreCase) >= 0;
                        } catch {
                            return false;
                        }
                    }))
                .ToList();
        }
    }
}