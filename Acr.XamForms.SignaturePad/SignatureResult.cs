using System;
using System.IO;
using System.Collections.Generic;


namespace Acr.XamForms.SignaturePad {

    public class SignatureResult {

		private readonly Func<Stream> getStreamFunc;

        public bool Cancelled { get; private set; }
        public IEnumerable<DrawPoint> Points { get; private set; }


		public SignatureResult(bool cancelled, Func<Stream> getStream, IEnumerable<DrawPoint> points) {
            this.Cancelled = cancelled;
            this.Points = points;
			this.getStreamFunc = getStream;
        }


		public virtual Stream GetStream() {
			var s = this.getStreamFunc();
			s.Position = 0;
			return s;
		}
    }
}
