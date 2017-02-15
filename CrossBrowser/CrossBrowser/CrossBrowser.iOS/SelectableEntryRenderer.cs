using CrossBrowser;
using CrossBrowser.iOS;
using System;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(SelectableEntry), typeof(SelectableEntryRenderer))]
namespace CrossBrowser.iOS
{
    public class SelectableEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Entry> e)
        {
            base.OnElementChanged(e);
            var nativeTextField = Control;
            nativeTextField.BackgroundColor = UIColor.FromRGBA(231, 243, 253, 255);
            nativeTextField.TextColor = UIColor.FromRGBA(46, 110, 158, 255);
            nativeTextField.EditingDidBegin += (object sender, EventArgs eIos) => {
                nativeTextField.PerformSelector(new ObjCRuntime.Selector("selectAll"), null, 0.0f);
            };
        }
    }
}