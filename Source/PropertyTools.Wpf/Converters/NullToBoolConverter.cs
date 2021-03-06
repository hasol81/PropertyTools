﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NullToBoolConverter.cs" company="PropertyTools">
//   The MIT License (MIT)
//   
//   Copyright (c) 2014 PropertyTools contributors
//   
//   Permission is hereby granted, free of charge, to any person obtaining a
//   copy of this software and associated documentation files (the
//   "Software"), to deal in the Software without restriction, including
//   without limitation the rights to use, copy, modify, merge, publish,
//   distribute, sublicense, and/or sell copies of the Software, and to
//   permit persons to whom the Software is furnished to do so, subject to
//   the following conditions:
//   
//   The above copyright notice and this permission notice shall be included
//   in all copies or substantial portions of the Software.
//   
//   THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS
//   OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
//   MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
//   IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY
//   CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT,
//   TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE
//   SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
// </copyright>
// <summary>
//   Null to bool value converter
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace PropertyTools.Wpf
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    /// <summary>
    /// Null to bool value converter
    /// </summary>
    [ValueConversion(typeof(bool), typeof(object))]
    public class NullToBoolConverter : IValueConverter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NullToBoolConverter" /> class.
        /// </summary>
        public NullToBoolConverter()
        {
            this.NullValue = true;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the source value is <c>null</c>.
        /// </summary>
        public bool NullValue { get; set; }

        /// <summary>
        /// Converts a value.
        /// </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>
        /// A converted value. If the method returns <c>null</c>, the valid <c>null</c> value is used.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return this.NullValue;
            }

            if (value is double && double.IsNaN((double)value))
            {
                return this.NullValue;
            }

            return !this.NullValue;
        }

        /// <summary>
        /// Converts a value.
        /// </summary>
        /// <param name="value">The value that is produced by the binding target.</param>
        /// <param name="targetType">The type to convert to.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>
        /// A converted value. If the method returns <c>null</c>, the valid <c>null</c> value is used.
        /// </returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var b = (bool)value;
            if (b != this.NullValue)
            {
                var ult = Nullable.GetUnderlyingType(targetType);
                if (ult != null)
                {
                    if (ult == typeof(DateTime))
                    {
                        return DateTime.Now;
                    }

                    return Activator.CreateInstance(ult);
                }

                if (targetType == typeof(string))
                {
                    return string.Empty;
                }

                return Activator.CreateInstance(targetType);
            }

            if (targetType == typeof(double))
            {
                return double.NaN;
            }

            return null;
        }
    }
}