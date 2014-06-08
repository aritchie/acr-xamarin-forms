using System;
using Acr.XamForms.SignaturePad.iOS;
using Acr.XamForms.SignaturePad.Views;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;


[assembly: ExportRenderer(typeof(SignaturePadView), typeof(SignaturePadRenderer))]


namespace Acr.XamForms.SignaturePad.iOS {
    
    public class SignaturePadRenderer : IViewRenderer {

        #region IViewRenderer Members

        public SizeRequest GetSizeRequest(double widthConstraint, double heightConstraint) {
            throw new NotImplementedException();
        }

        public VisualElement Model {
            get { throw new NotImplementedException(); }
        }

        public MonoTouch.UIKit.UIView NativeView {
            get { throw new NotImplementedException(); }
        }

        public void SetModel(VisualElement model) {
            throw new NotImplementedException();
        }

        public void SetModelSize(Size size) {
            throw new NotImplementedException();
        }

        public MonoTouch.UIKit.UIViewController ViewController {
            get { throw new NotImplementedException(); }
        }

        #endregion

        #region IDisposable Members

        public void Dispose() {
            throw new NotImplementedException();
        }

        #endregion
    }
}