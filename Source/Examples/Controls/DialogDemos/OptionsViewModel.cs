﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OptionsViewModel.cs" company="PropertyTools">
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
// --------------------------------------------------------------------------------------------------------------------

using System.ComponentModel;
using DialogDemos.Properties;

namespace DialogDemos
{
    public class OptionsViewModel : Observable
    {
        private static Settings Settings
        {
            get { return Settings.Default; }
        }

        [Category("Environment|General")]
        public string ProjectPath
        {
            get { return Settings.ProjectPath; }
            set
            {
                Settings.ProjectPath = value;
                OnPropertyChanged("ReportPath");
            }
        }

        [Category("Environment|General")]
        public int UndoLevels
        {
            get { return Settings.UndoLevels; }
            set
            {
                Settings.UndoLevels = value;
                OnPropertyChanged("UndoLevels");
            }
        }

        [Category("Environment|Menus")]
        public int WindowMenuItems
        {
            get { return Settings.WindowMenuItems; }
            set
            {
                Settings.WindowMenuItems = value;
                OnPropertyChanged("WindowMenuItems");
            }
        }

        [Category("Environment|Menus")]
        public int MostRecentlyUsedItems
        {
            get { return Settings.MostRecentlyUsedItems; }
            set
            {
                Settings.MostRecentlyUsedItems = value;
                OnPropertyChanged("MostRecentlyUsedItems");
            }
        }

        [Category("Startup|Actions")]
        public bool ShowStartPage
        {
            get { return Settings.ShowStartPage; }
            set
            {
                Settings.ShowStartPage = value;
                OnPropertyChanged("NewsChannel");
            }
        }

        [Category("Startup|News channel")]
        public string NewsChannel
        {
            get { return Settings.NewsChannel; }
            set
            {
                Settings.NewsChannel = value;
                OnPropertyChanged("NewsChannel");
            }
        }

        [Category("Startup|Actions")]
        public StartupAction StartupAction
        {
            get { return Settings.StartupAction; }
            set
            {
                Settings.StartupAction = value;
                OnPropertyChanged("StartupAction");
            }
        }

        public void Save()
        {
            Settings.Save();
        }
    }
}