using LibrarySystemMcv.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Reflection;
using System.Diagnostics;

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
            //if (string.IsNullOrEmpty(substring)) {
            //    return data;
            //}

            //return data.Where(item =>
            //    typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance)
            //        .Where(p => p.PropertyType == typeof(string))
            //        .Any(p => {
            //            var value = p.GetValue(item)?.ToString();
            //            return !string.IsNullOrEmpty(value) &&
            //                value.IndexOf(substring, StringComparison.OrdinalIgnoreCase) >= 0;
            //        })
            //).ToList();
            Debug.WriteLine($"inputed substring: {substring}");
            if (string.IsNullOrEmpty(substring)) return data;

            var filteredData = new List<T>();
            var i = 0;

            data.ForEach(p => {
                var fullData = "";
                foreach (var property in typeof(T).GetProperties()) {
                    fullData += property.GetValue(p).ToString();
                }
                if (fullData.ToLower().Contains(substring.ToLower())) {
                    filteredData.Add(p);
                    Debug.WriteLine($"full data: {fullData}");
                }
                i++;
            });

            Debug.WriteLine($"iter-s: {i}");
            Debug.WriteLine($"data count: {data.Count()}");
            Debug.WriteLine($"filtered data count: {filteredData.Count()}");

            return filteredData;
        }
    }
}