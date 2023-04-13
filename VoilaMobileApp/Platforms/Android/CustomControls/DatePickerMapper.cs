using System;
using Android.Graphics.Drawables;
using Microsoft.Maui.Controls.Compatibility.Platform.Android;
using Microsoft.Maui.Controls.Platform;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using VoilaMobileApp.Src.CustomControls;

namespace VoilaMobileApp.Platforms.Android.CustomControls
{
    public static class DatePickerMapper
    {
        public static void Map(IElementHandler handler, IElement view)
        {
            if (view is CustomDatePicker)
            {
                var casted = (DatePickerHandler)handler;
                var viewData = (CustomDatePicker)view;
                var gd = new GradientDrawable();

                gd.SetCornerRadius((int)handler.MauiContext?.Context.ToPixels(viewData.CornerRadius));
                gd.SetStroke((int)handler.MauiContext?.Context.ToPixels(viewData.BorderThickness), viewData.BorderColor.ToAndroid());
                gd.SetColor(viewData.BackgroundColor.ToAndroid());


                casted.PlatformView?.SetBackground(gd);
                if (viewData.PlaceHolder != null)
                {
                    casted.PlatformView?.Text.Insert(0, viewData.PlaceHolder);

                }

                var padTop = (int)handler.MauiContext?.Context.ToPixels(viewData.Padding.Top);
                var padBottom = (int)handler.MauiContext?.Context.ToPixels(viewData.Padding.Bottom);
                var padLeft = (int)handler.MauiContext?.Context.ToPixels(viewData.Padding.Left);
                var padRight = (int)handler.MauiContext?.Context.ToPixels(viewData.Padding.Right);

                casted.PlatformView?.SetPadding(padLeft, padTop, padRight, padBottom);
            }
        }
    }
}

