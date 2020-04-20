using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace jHackson.Core.Localization
{
    public class LocalizationExtension : MarkupExtension
    {
        protected DependencyObject targetObject;
        protected DependencyProperty targetProperty;
        protected Type targetType;
        protected TypeConverter typeConverter;

        public LocalizationExtension()
        {
            LocalizationManager.CultureChanged += new EventHandler(this.LocalisationManager_CultureChanged);
        }

        public LocalizationExtension(string key) : this()
        {
            this.Key = key;
        }

        public IValueConverter Converter { get; set; }

        public object DefaultValue { get; set; }

        public string Format { get; set; }

        [ConstructorArgument("key")]
        public string Key { get; set; }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (targetType == null)
            {
                var targetHelper = (IProvideValueTarget)serviceProvider.GetService(typeof(IProvideValueTarget));

                targetObject = targetHelper.TargetObject as DependencyObject;
                targetProperty = targetHelper.TargetProperty as DependencyProperty;

                if (targetProperty != null)
                {
                    targetType = targetProperty.PropertyType;
                    typeConverter = TypeDescriptor.GetConverter(targetType);
                }
                else if (targetHelper.TargetProperty != null)
                {
                    targetType = targetHelper.TargetProperty.GetType();
                    typeConverter = TypeDescriptor.GetConverter(targetType);
                }
            }

            return ProvideValueInternal();
        }

        protected object ProvideValueInternal()
        {
            // Get the localized value.
            object value = LocalizationManager.GetMessage(Key);

            // Automatically convert the type if a matching converter is available.
            if (value != null && typeConverter != null && typeConverter.CanConvertFrom(value.GetType()))
                value = typeConverter.ConvertFrom(value);

            // If the value is null and we have a default value, use it.
            if (value == null && DefaultValue != null)
                value = DefaultValue;

            // If we have no fallback value, return the key.
            if (value == null)
            {
                if (targetType == typeof(string))
                    value = string.Concat("?", Key, "?");
                else if (targetProperty != null)
                    return DependencyProperty.UnsetValue;
                else
                    return null;
            }

            if (Converter != null)
                value = Converter.Convert(value, targetType, null, LocalizationManager.CurrentCulture);

            if (Format != null && value is IFormattable)
                ((IFormattable)value).ToString(Format, LocalizationManager.CurrentCulture);

            return value;
        }

        protected virtual void UpdateTarget()
        {
            if (this.targetObject != null && this.targetProperty != null)
            {
                this.targetObject.SetValue(this.targetProperty, ProvideValueInternal());
            }
        }

        private void LocalisationManager_CultureChanged(object sender, EventArgs e)
        {
            this.UpdateTarget();
        }
    }
}