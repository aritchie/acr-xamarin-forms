using System;
using Acr.XamForms.SignaturePad.Views;
using Acr.XamForms.SignaturePad.WindowsPhone;
using Xamarin.Forms;
using Xamarin.Forms.Platform.WinPhone;


[assembly: ExportRenderer(typeof(SignaturePadView), typeof(SignaturePadRenderer))]


namespace Acr.XamForms.SignaturePad.WindowsPhone {
    
    public class SignaturePadRenderer : IViewRenderer {

        #region IViewRenderer Members

        public System.Windows.UIElement ContainerElement {
            get { throw new NotImplementedException(); }
        }


        public SizeRequest GetDesiredSize(double widthConstraint, double heightConstraint) {
            throw new NotImplementedException();
        }


        public void SetModel(VisualElement model) {
            throw new NotImplementedException();
        }

        #endregion
    }
}