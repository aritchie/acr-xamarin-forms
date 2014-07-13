using System;
using Acr.XamForms.ViewModels;
using Xamarin.Forms;


namespace Acr.XamForms.Infrastructure {
    
    public class ViewModelResolver : IViewModelResolver {
        private readonly Func<Type, ContentPage> resolvePage;
        private readonly Func<Type, IViewModel> resolveViewModel; 


        public ViewModelResolver(Func<Type, IViewModel> resolveViewModel = null, Func<Type, ContentPage> resolvePage = null) {
            this.resolveViewModel = resolveViewModel ?? (x => Activator.CreateInstance(x) as IViewModel);
            this.resolvePage = resolvePage ?? (x => Activator.CreateInstance(x) as ContentPage);
        }


        protected virtual IViewModel ResolveViewModel<T>(object args) where T : IViewModel {
            var viewModel = this.resolveViewModel(typeof(T));
            if (viewModel == null)
                throw new ArgumentException("Invalid viewmodel type - " + typeof(T));
            
            viewModel.Init(args);
            return viewModel;
        }


        protected virtual ContentPage CreatePage(Type pageType) {
            var page = this.resolvePage(pageType);
            if (page == null)
                throw new ArgumentException(pageType + " does not inherit from contentpage");
            
            return page;
        }


        protected virtual ContentPage ResolvePage<T>() where T : IViewModel {
            var pageTypeName = typeof(T)
                .AssemblyQualifiedName
                .Replace("ViewModel", "View");
         
            var pageType = Type.GetType(pageTypeName);
            if (pageType == null)
                throw new ArgumentException(pageTypeName + " type not exist");

            return this.CreatePage(pageType);
        }


        public virtual ContentPage Resolve<TViewModel>(object args = null) where TViewModel : IViewModel {
            var page = this.ResolvePage<TViewModel>();
            var viewModel = this.ResolveViewModel<TViewModel>(args);
            page.BindingContext = viewModel;

            page.Appearing += (sender, args1) => viewModel.OnAppearing();
            page.Disappearing += (sender, args1) => viewModel.OnDisappearing();
            
            return page;
        }
    }
}
