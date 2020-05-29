namespace JHackson.Core.Localization
{
    using System;
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Data;
    using System.Windows.Markup;

    public class LocalizationExtension : MarkupExtension
    {
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

        protected DependencyObject TargetObject { get; set; }

        protected DependencyProperty TargetProperty { get; set; }

        protected Type TargetType { get; set; }

        protected TypeConverter TypeConverter { get; set; }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (serviceProvider == null)
            {
                throw new ArgumentNullException(nameof(serviceProvider));
            }

            if (this.TargetType == null)
            {
                var targetHelper = (IProvideValueTarget)serviceProvider.GetService(typeof(IProvideValueTarget));

                this.TargetObject = targetHelper.TargetObject as DependencyObject;
                this.TargetProperty = targetHelper.TargetProperty as DependencyProperty;

                if (this.TargetProperty != null)
                {
                    this.TargetType = this.TargetProperty.PropertyType;
                    this.TypeConverter = TypeDescriptor.GetConverter(this.TargetType);
                }
                else if (targetHelper.TargetProperty != null)
                {
                    this.TargetType = targetHelper.TargetProperty.GetType();
                    this.TypeConverter = TypeDescriptor.GetConverter(this.TargetType);
                }
            }

            return this.ProvideValueInternal();
        }

        protected object ProvideValueInternal()
        {
            // Get the localized value.
            object value = LocalizationManager.GetMessage(this.Key);

            // Automatically convert the type if a matching converter is available.
            if (value != null && this.TypeConverter != null && this.TypeConverter.CanConvertFrom(value.GetType()))
            {
                value = this.TypeConverter.ConvertFrom(value);
            }

            // If the value is null and we have a default value, use it.
            if (value == null && this.DefaultValue != null)
            {
                value = this.DefaultValue;
            }

            // If we have no fallback value, return the key.
            if (value == null)
            {
                if (this.TargetType == typeof(string))
                {
                    value = string.Concat("?", this.Key, "?");
                }
                else
                {
                    return this.TargetProperty != null ? DependencyProperty.UnsetValue : null;
                }
            }

            if (this.Converter != null)
            {
                value = this.Converter.Convert(value, this.TargetType, null, LocalizationManager.CurrentCulture);
            }

            if (this.Format != null && value is IFormattable)
            {
                ((IFormattable)value).ToString(this.Format, LocalizationManager.CurrentCulture);
            }

            return value;
        }

        protected virtual void UpdateTarget()
        {
            if (this.TargetObject != null && this.TargetProperty != null)
            {
                this.TargetObject.SetValue(this.TargetProperty, this.ProvideValueInternal());
            }
        }

        private void LocalisationManager_CultureChanged(object sender, EventArgs e)
        {
            this.UpdateTarget();
        }
    }
}