using System;
using System.Globalization;
using System.Windows.Data;

namespace QQ_LoL_Localizer.Converters
{
    class BehaviorConverter : IValueConverter
    {
        /// <summary>
        /// Converts a value. 
        /// </summary>
        /// <returns>
        /// A converted value. If the method returns null, the valid null value is used.
        /// </returns>
        /// <param name="value">The value produced by the binding source.</param><param name="targetType">The type of the binding target property.</param><param name="parameter">The converter parameter to use.</param><param name="culture">The culture to use in the converter.</param>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is Behavior))
                return 0;
            
            var behavior = (Behavior)value;

            switch (behavior)
            {
                case Behavior.Minimize:
                    return 1;
                case Behavior.Close:
                    return 2;
                default:
                    return 0;
            }
        }

        /// <summary>
        /// Converts a value. 
        /// </summary>
        /// <returns>
        /// A converted value. If the method returns null, the valid null value is used.
        /// </returns>
        /// <param name="value">The value that is produced by the binding target.</param><param name="targetType">The type to convert to.</param><param name="parameter">The converter parameter to use.</param><param name="culture">The culture to use in the converter.</param>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is int))
                return Behavior.Nothing;

            var index = (int) value;
            switch (index)
            {
                case 1:
                    return Behavior.Minimize;
                case 2:
                    return Behavior.Close;
                default:
                    return Behavior.Nothing;
            }
        }
    }
}
