using System;
using Acr.XamForms.ViewModels;
using Acr.XamForms.Mobile.IO;

namespace Samples {

	public class FileSystemViewModel : ViewModel {
		private readonly IFileSystem fileSystem;


		public FileSystemViewModel(IFileSystem fileSystem) {
			this.fileSystem = fileSystem;
		}


		public string AppDataDirectory {
			get { return this.fileSystem.AppData.FullName; }
		}


		public string CacheDirectory {
			get { return this.fileSystem.Cache.FullName; }
		}


		public string PublicDirectory {
			get { return this.fileSystem.Public.FullName; }
		}


		public string TempDirectory {
			get { return this.fileSystem.Temp.FullName; }
		}
	}
}

