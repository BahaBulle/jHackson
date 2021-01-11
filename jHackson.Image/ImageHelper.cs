// <copyright file="ImageHelper.cs" company="BahaBulle">
// Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace JHackson.Image
{
    using System;
    using JHackson.Core.Common;
    using JHackson.Core.Exceptions;
    using JHackson.Core.Localization;
    using JHackson.Image.ImageFormat;

    public static class ImageHelper
    {
        public static IImageFormat GetImageFormat(string format)
        {
            if (string.IsNullOrWhiteSpace(format))
            {
                throw new JHacksonException(LocalizationManager.GetMessage("core.imageFormatNotSpecified"));
            }

            var type = DataContext.GetImageFormat(format);

            return type != null ? Activator.CreateInstance(type) as IImageFormat : null;
        }
    }
}