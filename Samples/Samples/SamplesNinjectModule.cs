using Ninject.Modules;
using Samples.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Samples
{
    class SamplesNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<HomeView>().To<HomeView>().InSingletonScope();
        }
    }
}
