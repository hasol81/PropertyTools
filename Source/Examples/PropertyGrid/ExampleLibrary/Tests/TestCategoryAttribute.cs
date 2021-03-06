﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestCategory.cs" company="PropertyTools">
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

namespace ExampleLibrary
{
    [PropertyGridExample]
    public class TestCategoryAttribute : TestBase
    {
        [System.ComponentModel.Category("Tab1|Category1")]
        public string Name1 { get; set; }

        [System.ComponentModel.Category("Tab1|Category2")]
        public string Name2 { get; set; }

        [System.ComponentModel.Category("Tab2|Category3")]
        public string Name3 { get; set; }

        [PropertyTools.DataAnnotations.Category("Tab1|Category1 (PropertyTools.DataAnnotations)")]
        public string Name11 { get; set; }

        [PropertyTools.DataAnnotations.Category("Tab1|Category2 (PropertyTools.DataAnnotations)")]
        public string Name12 { get; set; }

        [PropertyTools.DataAnnotations.Category("Tab2|Category3 (PropertyTools.DataAnnotations)")]
        public string Name13 { get; set; }

        public override string ToString()
        {
            return "CategoryAttribute";
        }
    }
}