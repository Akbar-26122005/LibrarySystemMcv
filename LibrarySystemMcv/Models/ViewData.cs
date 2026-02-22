using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using LibrarySystemMcv.Utils;

namespace LibrarySystemMcv.Models {
    public class ViewData<T> {
        public List<T> Data { get; set; }
        public Predicate<T> FilterCondition { get; set; }

        private Func<T, object> _sortSelector { get; set; }
        private bool _isAscending = true;

        public string SearchSubstring { get; set; }
        public bool Filtered { get; set; } = false;
        public List<T> FilteredData { get; set; } = null;

        public delegate List<T> UpdatingData();
        private UpdatingData _updateData;

        public ViewData() {
            Data = new List<T>();
        }

        public void Update() {
            if (_updateData != null) {
                Data = _updateData();
                var newData = _updateData();
                if (newData != null) {
                    Data = newData;
                    Debug.WriteLine($"Data обновлен. Элементов: {Data.Count}");
                }
            }
        }

        public void TryInit(UpdatingData updatingData) {
            _updateData = updatingData;
        }

        public void ClearFilters() {
            SearchSubstring = null;
            FilterCondition = null;
            _sortSelector = null;
            Filtered = false;
            FilteredData = null;
            Update();
        }

        public void SetSortCondition<TProperty>(Func<T, TProperty> sortSelector, bool ascending = true) {
            _sortSelector = item => (object)sortSelector(item);
            _isAscending = ascending;
        }

        public List<T> GetData() => FilteredData ?? Data;

        public List<T> GetFinalData() {
            Update();
            var finalData = new List<T>(Data);

            if (FilterCondition != null) {
                finalData = Functions.Filter(finalData, FilterCondition);
            }
            
            if (_sortSelector != null) {
                finalData = Functions.SortByProperty(finalData, _sortSelector);
            }
            
            if (!string.IsNullOrEmpty(SearchSubstring)) {
                finalData = Functions.SearchBySubstring(finalData, SearchSubstring);
            }

            return finalData;
        }

        public void ApplyChanges() {
            Filtered = true;
            FilteredData = GetFinalData();
        }
    }
}