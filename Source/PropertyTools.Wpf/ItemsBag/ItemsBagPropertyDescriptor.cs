﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ItemsBagPropertyDescriptor.cs" company="PropertyTools">
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
//   Provides a property descriptor for an object in the <see cref="ItemsBag" />.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace PropertyTools.Wpf
{
    using System;
    using System.ComponentModel;

    /// <summary>
    /// Provides a property descriptor for an object in the <see cref="ItemsBag" />.
    /// </summary>
    public class ItemsBagPropertyDescriptor : PropertyDescriptor
    {
        /// <summary>
        /// The component type.
        /// </summary>
        private readonly Type componentType;

        /// <summary>
        /// The default descriptor.
        /// </summary>
        private readonly PropertyDescriptor defaultDescriptor;

        /// <summary>
        /// Initializes a new instance of the <see cref="ItemsBagPropertyDescriptor" /> class.
        /// </summary>
        /// <param name="defaultDescriptor">The default descriptor.</param>
        /// <param name="componentType">Type of the component.</param>
        public ItemsBagPropertyDescriptor(PropertyDescriptor defaultDescriptor, Type componentType)
            : base(defaultDescriptor)
        {
            this.defaultDescriptor = defaultDescriptor;
            this.componentType = componentType;
        }

        /// <summary>
        /// When overridden in a derived class, gets the type of the component this property is bound to.
        /// </summary>
        /// <value></value>
        /// <returns>
        /// A <see cref="T:System.Type" /> that represents the type of component this property is bound to. When the <see
        /// cref="M:System.ComponentModel.PropertyDescriptor.GetValue(System.Object)" /> or <see
        /// cref="M:System.ComponentModel.PropertyDescriptor.SetValue(System.Object,System.Object)" /> methods are invoked, the object specified might be an instance of this type.
        /// </returns>
        public override Type ComponentType
        {
            get
            {
                return this.componentType;
            }
        }

        /// <summary>
        /// When overridden in a derived class, gets a value indicating whether this property is read-only.
        /// </summary>
        /// <value></value>
        /// <returns>
        /// <c>true</c> if the property is read-only; otherwise, <c>false</c>.
        /// </returns>
        public override bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// When overridden in a derived class, gets the type of the property.
        /// </summary>
        /// <value></value>
        /// <returns>
        /// A <see cref="T:System.Type" /> that represents the type of the property.
        /// </returns>
        public override Type PropertyType
        {
            get
            {
                if (this.defaultDescriptor.PropertyType.IsValueType)
                {
                    var nt = this.GetNullableType(this.defaultDescriptor.PropertyType);
                    return nt;
                }

                return this.defaultDescriptor.PropertyType;
            }
        }

        /// <summary>
        /// When overridden in a derived class, returns whether resetting an object changes its value.
        /// </summary>
        /// <param name="component">The component to test for reset capability.</param>
        /// <returns>
        /// <c>true</c> if resetting the component changes its value; otherwise, <c>false</c>.
        /// </returns>
        public override bool CanResetValue(object component)
        {
            return false;
        }

        /// <summary>
        /// When overridden in a derived class, gets the current value of the property on a component.
        /// </summary>
        /// <param name="component">The component with the property for which to retrieve the value.</param>
        /// <returns>
        /// The value of a property for a given component.
        /// </returns>
        public override object GetValue(object component)
        {
            var bag = (ItemsBag)component;
            object value = null;
            bool isFirst = true;
            foreach (var obj in bag.Objects)
            {
                var type = obj.GetType();
                var pi = type.GetProperty(this.Name);
                if (pi == null)
                {
                    continue;
                }

                var itemValue = pi.GetValue(obj, null);
                if (value != null && !value.Equals(itemValue))
                {
                    value = null;
                }

                if (isFirst)
                {
                    value = itemValue;
                    isFirst = false;
                }
            }

            return value;
        }

        /// <summary>
        /// When overridden in a derived class, resets the value for this property of the component to the default value.
        /// </summary>
        /// <param name="component">The component with the property value that is to be reset to the default value.</param>
        public override void ResetValue(object component)
        {
        }

        /// <summary>
        /// When overridden in a derived class, sets the value of the component to a different value.
        /// </summary>
        /// <param name="component">The component with the property value that is to be set.</param>
        /// <param name="value">The new value.</param>
        public override void SetValue(object component, object value)
        {
            var bag = (ItemsBag)component;
            bag.SuspendNotifications = true;
            foreach (var obj in bag.Objects)
            {
                var type = obj.GetType();
                var pi = type.GetProperty(this.Name);
                pi.SetValue(obj, value, null);
            }

            bag.RaisePropertyChanged(this.Name);
            bag.SuspendNotifications = false;
        }

        /// <summary>
        /// When overridden in a derived class, determines a value indicating whether the value of this property needs to be persisted.
        /// </summary>
        /// <param name="component">The component with the property to be examined for persistence.</param>
        /// <returns>
        /// <c>true</c> if the property should be persisted; otherwise, <c>false</c>.
        /// </returns>
        public override bool ShouldSerializeValue(object component)
        {
            return false;
        }

        /// <summary>
        /// Gets the type of the <see cref="Nullable" />.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>
        /// The <see cref="Type" />.
        /// </returns>
        private Type GetNullableType(Type type)
        {
            // http://stackoverflow.com/questions/108104/how-do-i-convert-a-system-type-to-its-nullable-version
            // Use Nullable.GetUnderlyingType() to remove the Nullable<T> wrapper if type is already nullable.
            var underlyingType = Nullable.GetUnderlyingType(type);
            if (underlyingType != null)
            {
                return type;
            }

            // if (type.IsValueType)
            return typeof(Nullable<>).MakeGenericType(type);

            // else
            // return type;
        }
    }
}