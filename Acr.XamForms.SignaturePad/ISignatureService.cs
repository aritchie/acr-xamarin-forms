using System;
using System.Threading;
using System.Threading.Tasks;


namespace Acr.XamForms.SignaturePad {

    public interface ISignatureService {

        Task<SignatureResult> Request(SignaturePadConfiguration config = null, CancellationToken cancelToken = default(CancellationToken));
    }
}
