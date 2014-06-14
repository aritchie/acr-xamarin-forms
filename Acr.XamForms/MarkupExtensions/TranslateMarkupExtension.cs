using System;
using Xamarin.Forms.Xaml;
using Xamarin.Forms;

 
namespace Acr.XamForms.MarkupExtensions {

    //xmlns:acr="clr-namespace:Acr.XamForms.MarkupExtensions;assembly=Acr.XamForms"
    //<Label Text="{acr:Translate MyTextKey}" />

    [ContentProperty("TextKey")]
    public class TranslateMarkupExtension : IMarkupExtension {

        public static Func<string, string> TranslationFactory { get; set; }
        public string TextKey { get; set; }
    

        public object ProvideValue (IServiceProvider serviceProvider) {
            if (TranslationFactory == null)
                throw new ArgumentException("TranslationFactory is not set");

            return this.TextKey == null
                ? null
                : TranslationFactory(this.TextKey);
        }
    }
}