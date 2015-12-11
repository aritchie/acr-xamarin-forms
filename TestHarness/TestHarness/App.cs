using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestHarness.Views;
using Xamarin.Forms;

namespace TestHarness
{
    public class App : Application
    {
        public App()
        {
            InitStyles();

            // The root page of your application
            MainPage = new NavigationPage(new SignatureImageView());
        }

        public void InitStyles()
        {
            Application.Current.Resources = new ResourceDictionary();
            var buttonStyle = new Style(typeof(Button))
            {
                Setters = {
                    new Setter { Property = Button.TextColorProperty, Value = Color.White },
                    new Setter { Property = Button.BackgroundColorProperty, Value = Color.FromRgb(71, 87, 112) }
                }
            };

            Application.Current.Resources.Add("ButtonStyle", buttonStyle);
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
