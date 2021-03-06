﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EnumMenuItemPage.xaml.cs" company="PropertyTools">
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
//   Interaction logic for EnumMenuItemPage.xaml
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ControlDemos
{
    using System.ComponentModel;
    using System.Windows.Controls;

    /// <summary>
    /// Interaction logic for EnumMenuItemPage.xaml
    /// </summary>
    public partial class EnumMenuItemPage : Page
    {
        public EnumMenuItemPage()
        {
            this.InitializeComponent();
            this.DataContext = new EnumMenuItemDemoViewModel();
        }
    }

    public enum Fruit
    {
        Apple,
        Pear,
        Banana
    }

    public class EnumMenuItemDemoViewModel : INotifyPropertyChanged
    {
        private Fruit favouriteFruit;

        public Fruit FavouriteFruit
        {
            get
            {
                return this.favouriteFruit;
            }

            set
            {
                if (favouriteFruit != value)
                {
                    this.favouriteFruit = value;
                    this.RaisePropertyChanged("FavouriteFruit");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(string property)
        {
            var handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(property));
            }
        }
    }
}