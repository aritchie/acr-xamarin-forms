using System;
using System.Collections.Generic;


namespace Acr.XamForms.SignaturePad {
    
    public interface ISignatureService {

        SignaturePadConfiguration Configuration { get; }
        //Task<SignatureResult> RequestSignatureAsync(SignaturePadConfiguration cfg = null);
        void RequestSignature(Action<SignatureResult> onAction);
        void LoadSignature(IEnumerable<DrawPoint> points);
    }
}
