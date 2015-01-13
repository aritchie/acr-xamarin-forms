using System;
using System.Threading;
using System.Threading.Tasks;
using System.IO;


namespace Acr.XamForms.BarCodeScanner {

	public interface IBarCodeService {

		Task<BarCodeResult> Read(BarCodeReadConfiguration config = null, CancellationToken cancelToken = default(CancellationToken));
		Stream Create(BarCodeCreateConfiguration config);
	}
}

