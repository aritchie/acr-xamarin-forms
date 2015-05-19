using System;
using Samples.Views;
using Xamarin.Forms;


namespace Samples {

	public class App : Xamarin.Forms.Application {

		public App() {
			this.MainPage = new NavigationPage(new HomeView());
		}
    }
}
