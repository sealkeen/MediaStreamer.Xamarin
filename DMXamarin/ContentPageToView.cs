using System;
using System.Collections.Generic;
using System.Text;

namespace DMXamarin.Extensions
{
    public class ContentPageToView
    {
        public static Xamarin.Forms.View Convert(Xamarin.Forms.Page page)
        {
#if ANDROID           
            return new Xamarin.Forms.Platform.Android.NativeViewWrapper(ConvertToNativeView(page));
#elif __IOS__
            return new Xamarin.Forms.Platform.iOS.NativeViewWrapper(ConvertToNativeView(page));
#endif
            return null;
        }
#if ANDROID

        public static Android.Views.View ConvertToNativeView(Xamarin.Forms.Page page)
        {
            var view = Xamarin.Forms.Platform.Android.PageExtensions.CreateFragment(page, Xamarin.Forms.Forms.Context);
            return view.OnCreateView(Android.Views.LayoutInflater.From(Xamarin.Forms.Forms.Context), null/* new Android.Widget.FrameLayout(Xamarin.Forms.Forms.Context)*/, null);
        }
#elif __IOS__
        public static UIKit.UIView ConvertToNativeView(Xamarin.Forms.Page page)
        {
            var view = Xamarin.Forms.PageExtensions.CreateViewController(page);
            //var vw = new UIKit.UIView();
            //var aa = view.View.GetType().FullName;
            //foreach(var one in view.ChildViewControllers)
            //{
            //    var ss = one.GetType().FullName;
            //}
            view.ViewWillAppear(false);
            //vw.AddSubview(view.View);
            //view.ViewDidAppear(false);//我也不知道要不要加这个
            return view.View;
        }
#endif
    }
}
