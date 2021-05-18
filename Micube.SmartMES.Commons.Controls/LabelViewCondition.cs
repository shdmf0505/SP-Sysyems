using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Micube.SmartMES.Commons.Controls
{
        public class Conditionals
        {
            public readonly IDictionary<string, object> dynamicProperties =
           new Dictionary<string, object>();

        public readonly IDictionary<string, bool> controlsVisibles =
 new Dictionary<string, bool>();
    }

        public class DynamicPropertyDescriptor : PropertyDescriptor
        {
            private readonly Conditionals businessObject;
            private readonly Type propertyType;

            public DynamicPropertyDescriptor(Conditionals businessObject,
                string propertyName, Type propertyType, Attribute[] propertyAttributes)
                : base(propertyName, propertyAttributes)
            {
                this.businessObject = businessObject;
                this.propertyType = propertyType;
            }

            public override bool CanResetValue(object component)
            {
                return true;
            }

            public override object GetValue(object component)
            {
                return businessObject.dynamicProperties[Name];
            }


            public override void SetValue(object component, object value)
            {
                businessObject.dynamicProperties[Name] = value;
            }

            public override bool ShouldSerializeValue(object component)
            {
                return false;
            }

            public override void ResetValue(object component)
            {
                return;
            }

            public override Type ComponentType
            {
                get { return typeof(Conditionals); }
            }

            public override bool IsReadOnly
            {
                get { return false; }
            }

            public override Type PropertyType
            {
                get { return propertyType; }
            }


        }

}