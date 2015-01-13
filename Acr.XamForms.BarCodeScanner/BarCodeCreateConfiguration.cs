using System;


namespace Acr.XamForms.BarCodeScanner {
	public class BarCodeCreateConfiguration {

		public string BarCode { get; set; }
		public ImageType ImageType { get; set; }
		public BarCodeFormat Format { get; set; }
		public int Margin { get; set; }
		public int Height { get; set; }
		public int Width { get; set; }
		public bool PureBarcode { get; set; }


		public BarCodeCreateConfiguration() {
			this.Width = 200;
			this.Height = 200;
			this.Format = BarCodeFormat.QR_CODE;
			this.ImageType = ImageType.Jpg;
		}
	}
}

