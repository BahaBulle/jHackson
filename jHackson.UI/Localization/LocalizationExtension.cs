namespace JHackson.Core.Localization
{
    using System;
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Data;
    using System.Windows.Markup;

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
            if (this.targetType == null)
            {
                var targetHelper = (IProvideValueTarget)serviceProvider.GetService(typeof(IProvideValueTarget));

                this.targetObject = targetHelper.TargetObject as DependencyObject;
                this.targetProperty = targetHelper.TargetProperty as DependencyProperty;

                if (this.targetProperty != null)
                {
                    this.targetType = this.targetProperty.PropertyType;
                    this.typeConverter = TypeDescriptor.GetConverter(this.targetType);
                }
                else if (targetHelper.TargetProperty != null)
                {
                    this.targetType = targetHelper.TargetProperty.GetType();
                    this.typeConverter = TypeDescriptor.GetConverter(this.targetType);
                }
            }

            return this.ProvideValueInternal();
        }

        protected object ProvideValueInternal()
        {
            // Get the localized value.
            object value = LocalizationManager.GetMessage(this.Key);

            // Automatically convert the type if a matching converter is available.
            if (value != null && this.typeConverter != null && this.typeConverter.CanConvertFrom(value.GetType()))
            {
                value = this.typeConverter.ConvertFrom(value);
            }

            // If the value is null and we have a default value, use it.
            if (value == null && this.DefaultValue != null)
            {
                value = this.DefaultValue;
            }

            // If we have no fallback value, return the key.
            if (value == null)
            {
                if (this.targetType == typeof(string))
                {
                    value = string.Concat("?", this.Key, "?");
                }
                else
                {
                    return this.targetProperty != null ? DependencyProperty.UnsetValue : null;
                }
            }

            if (this.Converter != null)
            {
                value = this.Converter.Convert(value, this.targetType, null, LocalizationManager.CurrentCulture);
            }

            if (this.Format != null && value is IFormattable)
            {
                ((IFormattable)value).ToString(this.Format, LocalizationManager.CurrentCulture);
            }

            return value;
        }

        protected virtual void UpdateTarget()
        {
            if (this.targetObject != null && this.targetProperty != null)
            {
                this.targetObject.SetValue(this.targetProperty, this.ProvideValueInternal());
            }
        }

        private void LocalisationManager_CultureChanged(object sender, EventArgs e)
        {
            this.UpdateTarget();
        }
    }
}