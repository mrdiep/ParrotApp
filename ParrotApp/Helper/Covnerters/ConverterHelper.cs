using System;
using System.Collections.Generic;
using System.Globalization;
using Xamarin.Forms;

namespace ParrotApp.Helper.Covnerters
{
    public class ConverterHelper
    {
        public static Dictionary<string, IValueConverter> Converters = new Dictionary<string, IValueConverter>
        {
            ["InverseBool"] = new FuncConverter<bool, bool>(v => !v, v => !v),
            ["UpperText"] = new FuncConverter<string, string>(v => v.ToUpper()),
        };
    }

    public class FuncConverter<F, T> : IValueConverter
    {
        private Func<T, F> convert;
        private Func<F, T> convertBack;

        public FuncConverter(Func<T, F> convert = null, Func<F, T> convertBack = null)
        {
            this.convert = convert;
            this.convertBack = convertBack;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (convert != null)
                return convert.Invoke((T)value);

            throw new InvalidOperationException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (convertBack != null)
                return convertBack.Invoke((F)value);

            throw new InvalidOperationException();
        }
    }
}