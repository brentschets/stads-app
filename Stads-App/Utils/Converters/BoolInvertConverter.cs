﻿using System;
using Windows.UI.Xaml.Data;

namespace Stads_App.Utils.Converters
{
    public class BoolInvertConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return !(bool) value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return !(bool) value;
        }
    }
}
