﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RadioButtonList.cs" company="PropertyTools">
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
//   Represents a control that shows a list of radiobuttons.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace PropertyTools.Wpf
{
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Data;

    /// <summary>
    /// Represents a control that shows a list of radiobuttons.
    /// </summary>
    /// <remarks>See http://bea.stollnitz.com/blog/?p=28 and http://code.msdn.microsoft.com/wpfradiobuttonlist</remarks>
    [TemplatePart(Name = PartPanel, Type = typeof(StackPanel))]
    public class RadioButtonList : Control
    {
        /// <summary>
        /// The part panel.
        /// </summary>
        private const string PartPanel = "PART_Panel";

        /// <summary>
        /// Identifies the <see cref="DescriptionConverter"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty DescriptionConverterProperty =
            DependencyProperty.Register(
                "DescriptionConverter",
                typeof(IValueConverter),
                typeof(RadioButtonList),
                new UIPropertyMetadata(new EnumDescriptionConverter()));

        /// <summary>
        /// Identifies the <see cref="EnumType"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty EnumTypeProperty = DependencyProperty.Register(
            "EnumType", typeof(Type), typeof(RadioButtonList), new UIPropertyMetadata(null, ValueChanged));

        /// <summary>
        /// Identifies the <see cref="ItemMargin"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty ItemMarginProperty = DependencyProperty.Register(
            "ItemMargin", typeof(Thickness), typeof(RadioButtonList), new UIPropertyMetadata(new Thickness(0, 4, 0, 4)));

        /// <summary>
        /// Identifies the <see cref="ItemPadding"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty ItemPaddingProperty = DependencyProperty.Register(
            "ItemPadding", typeof(Thickness), typeof(RadioButtonList), new UIPropertyMetadata(new Thickness(4, 0, 0, 0)));

        /// <summary>
        /// Identifies the <see cref="Orientation"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty OrientationProperty = DependencyProperty.Register(
            "Orientation", typeof(Orientation), typeof(RadioButtonList), new UIPropertyMetadata(Orientation.Vertical));

        /// <summary>
        /// Identifies the <see cref="Value"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
            "Value",
            typeof(object),
            typeof(RadioButtonList),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, ValueChanged));

        /// <summary>
        /// The panel.
        /// </summary>
        private StackPanel panel;

        /// <summary>
        /// Initializes static members of the <see cref="RadioButtonList" /> class.
        /// </summary>
        static RadioButtonList()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(RadioButtonList), new FrameworkPropertyMetadata(typeof(RadioButtonList)));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RadioButtonList" /> class.
        /// </summary>
        public RadioButtonList()
        {
            this.DataContextChanged += this.RadioButtonListDataContextChanged;
        }

        /// <summary>
        /// Gets or sets the description converter.
        /// </summary>
        /// <value>The description converter.</value>
        public IValueConverter DescriptionConverter
        {
            get
            {
                return (IValueConverter)this.GetValue(DescriptionConverterProperty);
            }

            set
            {
                this.SetValue(DescriptionConverterProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the type of the enum.
        /// </summary>
        /// <value>The type of the enum.</value>
        public Type EnumType
        {
            get
            {
                return (Type)this.GetValue(EnumTypeProperty);
            }

            set
            {
                this.SetValue(EnumTypeProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the item margin.
        /// </summary>
        /// <value>The item margin.</value>
        public Thickness ItemMargin
        {
            get
            {
                return (Thickness)this.GetValue(ItemMarginProperty);
            }

            set
            {
                this.SetValue(ItemMarginProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the item padding.
        /// </summary>
        /// <value>The item padding.</value>
        public Thickness ItemPadding
        {
            get
            {
                return (Thickness)this.GetValue(ItemPaddingProperty);
            }

            set
            {
                this.SetValue(ItemPaddingProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the orientation.
        /// </summary>
        /// <value>The orientation.</value>
        public Orientation Orientation
        {
            get
            {
                return (Orientation)this.GetValue(OrientationProperty);
            }

            set
            {
                this.SetValue(OrientationProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        public object Value
        {
            get
            {
                return this.GetValue(ValueProperty);
            }

            set
            {
                this.SetValue(ValueProperty, value);
            }
        }

        /// <summary>
        /// When overridden in a derived class, is invoked whenever application code or internal processes call <see
        /// cref="M:System.Windows.FrameworkElement.ApplyTemplate" /> .
        /// </summary>
        public override void OnApplyTemplate()
        {
            if (this.panel == null)
            {
                this.panel = this.Template.FindName(PartPanel, this) as StackPanel;
            }

            this.UpdateContent();
        }

        /// <summary>
        /// Called when the Value changed or the EnumType changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args.</param>
        private static void ValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            ((RadioButtonList)sender).UpdateContent();
        }

        /// <summary>
        /// The radio button list data context changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args.</param>
        private void RadioButtonListDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            this.UpdateContent();
        }

        /// <summary>
        /// Updates the content.
        /// </summary>
        private void UpdateContent()
        {
            if (this.panel == null)
            {
                return;
            }

            this.panel.Children.Clear();

            var enumType = this.EnumType;
            if (enumType != null)
            {
                var ult = Nullable.GetUnderlyingType(enumType);
                if (ult != null)
                {
                    enumType = ult;
                }
            }

            if (this.Value != null)
            {
                enumType = this.Value.GetType();
            }

            if (enumType == null || !typeof(Enum).IsAssignableFrom(enumType))
            {
                return;
            }

            var enumValues = Enum.GetValues(enumType).FilterOnBrowsableAttribute();
            var converter = new EnumToBooleanConverter { EnumType = enumType };

            foreach (var itemValue in enumValues)
            {
                var rb = new RadioButton
                    {
                        Content =
                            this.DescriptionConverter.Convert(
                                itemValue, typeof(string), null, CultureInfo.CurrentCulture),
                        Padding = this.ItemPadding
                    };

                var isCheckedBinding = new Binding("Value")
                    {
                       Converter = converter, ConverterParameter = itemValue, Source = this, Mode = BindingMode.TwoWay
                    };
                rb.SetBinding(ToggleButton.IsCheckedProperty, isCheckedBinding);

                rb.SetBinding(MarginProperty, new Binding("ItemMargin") { Source = this });

                this.panel.Children.Add(rb);
            }
        }
    }
}