﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PropertyItemFactory.cs" company="PropertyTools">
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
//   Provides a custom property item factory.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace CustomFactoryDemo
{
    using System;
    using System.ComponentModel;

    using PropertyTools.Wpf;

    /// <summary>
    /// Provides a custom property item factory.
    /// </summary>
    public class PropertyItemFactory : DefaultPropertyItemFactory
    {
        protected override PropertyItem CreateCore(PropertyDescriptor pd, PropertyDescriptorCollection properties)
        {
            // Check if the property is decorated with an "ImportantAttribute"
            var ia = pd.GetFirstAttributeOrDefault<ImportantAttribute>();
            if (ia != null)
            {
                // Create a custom PropertyItem instance
                return new ImportantPropertyItem(pd, properties);
            }

            return base.CreateCore(pd, properties);
        }

        protected override string GetDisplayName(PropertyDescriptor pd, Type declaringType)
        {
            // Use the property name as display name - this will be passed to the GetLocalizedString later
            return pd.Name;
        }

        protected override string GetLocalizedString(string key, Type declaringType)
        {
            // Add a star to show that we have handled this
            // A localization mechanism can be used to localize the strings
            return key + "*";
        }
    }
}