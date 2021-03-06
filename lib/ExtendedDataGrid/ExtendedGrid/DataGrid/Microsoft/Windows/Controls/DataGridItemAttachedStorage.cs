//---------------------------------------------------------------------------
//
// Copyright (C) Microsoft Corporation.  All rights reserved.
//
//---------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Windows;

namespace ExtendedGrid.Microsoft.Windows.Controls
{
    /// <summary>
    ///     Holds all of the information that we need to attach to row items so that we can restore rows when they're devirtualized.
    /// </summary>
    internal class DataGridItemAttachedStorage
    {
        public void SetValue(object item, DependencyProperty property, object value)
        {
            var map = EnsureItem(item);
            map[property] = value;
        }
        
        public bool TryGetValue(object item, DependencyProperty property, out object value)
        {
            value = null;
            Dictionary<DependencyProperty, object> map;
            
            EnsureItemStorageMap();
            if (_itemStorageMap.TryGetValue(item, out map))
            {
                return map.TryGetValue(property, out value);
            }

            return false;
        }

        public void ClearValue(object item, DependencyProperty property)
        {
            Dictionary<DependencyProperty, object> map;

            EnsureItemStorageMap();
            if (_itemStorageMap.TryGetValue(item, out map))
            {
                map.Remove(property);
            }
        }

        public void ClearItem(object item)
        {
            EnsureItemStorageMap();
            _itemStorageMap.Remove(item);
        }

        public void Clear()
        {
            _itemStorageMap = null;
        }

        private void EnsureItemStorageMap()
        {
            if (_itemStorageMap == null)
            {
                _itemStorageMap = new Dictionary<object, Dictionary<DependencyProperty, object>>();
            }
        }

        private Dictionary<DependencyProperty, object> EnsureItem(object item)
        {
            Dictionary<DependencyProperty, object> map;
            
            EnsureItemStorageMap();
            if (!_itemStorageMap.TryGetValue(item, out map))
            {
                map = new Dictionary<DependencyProperty, object>();
                _itemStorageMap[item] = map;
            }

            return map;
        }

        /// <summary>
        ///     A map between row items and the associated data.
        /// </summary>
        private Dictionary<object, Dictionary<DependencyProperty, object>> _itemStorageMap;        
    }
}