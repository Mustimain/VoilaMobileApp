using System;
using Android.Graphics.Drawables;
using Microsoft.Maui.Controls.Compatibility.Platform.Android;
using Microsoft.Maui.Controls.Platform;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using VoilaMobileApp.Src.CustomControls;

namespace VoilaMobileApp.Platforms.Android.CustomControls
{
    public static class EntryMapper
    {
        public static void Map(IElementHandler handler, IElement view)
        {
            if (view is CustomEntry)
            {
                var casted = (EntryHandler)handler;
                var viewData = (CustomEntry)view;
                var gd = new GradientDrawable();

                gd.SetCornerRadius((int)handler.MauiContext?.Context.ToPixels(viewData.CornerRadius));
                gd.SetStroke((int)handler.MauiContext?.Context.ToPixels(viewData.BorderThickness), viewData.BorderColor.ToAndroid());
                gd.SetColor(viewData.BackgroundColor.ToAndroid());

                casted.PlatformView?.SetBackground(gd);

                var padTop = (int)handler.MauiContext?.Context.ToPixels(viewData.Padding.Top);
                var padBottom = (int)handler.MauiContext?.Context.ToPixels(viewData.Padding.Bottom);
                var padLeft = (int)handler.MauiContext?.Context.ToPixels(viewData.Padding.Left);
                var padRight = (int)handler.MauiContext?.Context.ToPixels(viewData.Padding.Right);

                casted.PlatformView?.SetPadding(padLeft, padTop, padRight, padBottom);
            }
        }
    }
}

