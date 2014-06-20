using System;
using Android.App;
using Android.OS;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;


namespace Samples.Droid {

    [Activity(Label = "ACR Xamarim Forms Examples", MainLauncher = true)]
    public class MainActivity : AndroidActivity {

        protected override void OnCreate(Bundle bundle) {
            base.OnCreate(bundle);
            Forms.Init(this, bundle);

            //var assemblies = AppDomain
            //    .CurrentDomain
            //    .GetAssemblies()
            //    .Where(x => x.FullName.StartsWith("Acr.XamForms"));

            //foreach (var assembly in assemblies)
            //    Console.WriteLine("ASSEMBLY: " + assembly.FullName);
            //    .SelectMany(x => x.DefinedTypes)
            //    .Where(x => 
            //        x.Namespace != null &&
            //        x.Namespace.StartsWith("Acr.XamForms")
            //    )
            //    .OrderBy(x => x.FullName);

            //foreach (var type in types)
            //    Console.WriteLine(type.FullName);
            
            this.SetPage(App.GetMainPage());
        }
    }
}

