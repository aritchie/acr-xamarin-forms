using System;
using System.Collections.Generic;


namespace Acr.XamForms.SignaturePad {
    
    public interface ISignatureService {

        SignaturePadConfiguration DefaultConfiguration { get; }
        //Task<SignatureResult> RequestSignatureAsync(SignaturePadConfiguration cfg = null);
        void RequestSignature(Action<SignatureResult> onAction, SignaturePadConfiguration cfg = null);
        void LoadSignature(IEnumerable<DrawPoint> points, SignaturePadConfiguration cfg = null);
    }
}
