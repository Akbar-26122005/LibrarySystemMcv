using LibrarySystemMcv.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using System.Web.Helpers;

namespace LibrarySystemMcv.Models {
    public class ViewData<T> {
        public List<T> Data { get; set; }
        public Predicate<T> FilterCondition { get; set; }

        public string SortSelector { get; set; }
        public string SortType { get; set; }

        public string SearchSubstring { get; set; }
        public bool Filtered { get; set; } = false;
        public List<T> FilteredData { get; set; } = null;

        public delegate List<T> UpdatingData();
        private UpdatingData _updateData;

        public List<SelectListItem> SortSelectors { get; set; }
        public List<SelectListItem> SortTypes { get; set; }

        public ViewData() {
            Data = new List<T>();

            // Список для dropdown-выбора свойства для сортировки
            //SortSelectors = new List<SelectListItem>();

            //foreach (var item in typeof(T).GetProperties()) {
            //    var customName = item.GetCustomAttribute<DisplayAttribute>()?.Name ?? null;
            //    if (!string.IsNullOrEmpty(customName)) {
            //        SortSelectors.Add(new SelectListItem {
            //            Text = customName,
            //            Value = item.Name
            //        });
            //    }
            //}
            SortSelectors = typeof(T).GetProperties()
                .Where(p => !string.IsNullOrEmpty(p.GetCustomAttribute<DisplayAttribute>()?.Name))
                .Select(p => new SelectListItem { Value = p.Name, Text = p.GetCustomAttribute<DisplayAttribute>().Name })
                .ToList();

            SortSelectors[0] = new SelectListItem { Text = "Сортировка", Disabled = true, Selected = true };

            // Список для выбора типа сортировки
            SortTypes = new List<SelectListItem> {
                new SelectListItem { Text = "Тип сортировки", Disabled = true, Selected = true },
                new SelectListItem { Text = "По возростанию", Value = "ascending" },
                new SelectListItem { Text = "По убыванию", Value = "descending" }
            };
        }

        public void TryInit(UpdatingData updatingData) {
            _updateData = updatingData;
        }

        public void UpdateData() {
            if (_updateData != null) {
                Data = _updateData();
                var newData = _updateData();
                if (newData != null) {
                    Data = newData;
                }
            }
        }


        public void ClearFilters() {
            SearchSubstring = null;
            FilterCondition = null;
            SortSelector = null;
            Filtered = false;
            FilteredData = null;
            UpdateData();
        }

        public List<T> GetData() => FilteredData ?? Data;

        public List<T> GetFilteredData() {
            UpdateData();
            var finalData = new List<T>(Data);

            if (FilterCondition != null) {
                finalData = Functions.Filter(finalData, FilterCondition);
            }
            
            if (!string.IsNullOrEmpty(SortSelector)) {
                finalData = Functions.SortByProperty(finalData, CreateGetterFast(SortSelector), SortType == "ascending" ? SortDirection.Ascending : SortDirection.Descending);
            }
            
            if (!string.IsNullOrEmpty(SearchSubstring)) {
                finalData = Functions.SearchBySubstring(finalData, SearchSubstring);
            }

            return finalData;
        }

        public void ApplyChanges() {
            Filtered = true;
            FilteredData = GetFilteredData();
        }

        public void Set(ViewData<T> data) {
            SearchSubstring = data.SearchSubstring;
            SortSelector = data.SortSelector;
            SortType = data.SortType;
        }

        public Func<T, object> CreateGetterFast(string selector) {
            var property = typeof(T).GetProperties().FirstOrDefault(p => p.Name == selector);

            if (!property.CanRead)
                throw new ArgumentException("Property must have a getter");

            var parameter = Expression.Parameter(typeof(T), "x");
            var propertyAccess = Expression.Property(parameter, property);
            var convertToObject = Expression.Convert(propertyAccess, typeof(object));
            var lambda = Expression.Lambda<Func<T, object>>(convertToObject, parameter);
            return lambda.Compile();
        }
    }
}