using System;
using System.Collections.Generic;


namespace Acr.XamForms.SignaturePad {
    
    public interface ISignatureService {

        SignaturePadConfiguration Configuration { get; }

        void Request(Action<SignatureResult> onAction);
        void Load(IEnumerable<DrawPoint> points);
    }
}
