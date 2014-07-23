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


        #region IViewModelResolver Members

        public virtual IViewModel ResolveViewModel(Type viewModelType, object args = null) {
            var viewModel = this.resolveViewModel(viewModelType);
            if (viewModel == null)
                throw new ArgumentException("Invalid viewmodel type - " + viewModelType);
            
            viewModel.Init(args);
            return viewModel;
        }


        public virtual ContentPage ResolvePage(Type viewModelType, object args = null) {
            var pageType = this.FindPageForViewModel(viewModelType);
            var page = this.resolvePage(pageType);
            var viewModel = this.ResolveViewModel(viewModelType, args);
            this.Bind(page, viewModel);
            return page;
        }

        #endregion

        #region Internals

        protected virtual void Bind(ContentPage page, IViewModel viewModel) {
            page.BindingContext = viewModel;

            page.Appearing += (sender, args1) => viewModel.OnAppearing();
            page.Disappearing += (sender, args1) => viewModel.OnDisappearing();            
        }


        protected virtual Type FindPageForViewModel(Type viewModelType) {
            var pageTypeName = viewModelType
                .AssemblyQualifiedName
                .Replace("ViewModel", "View");

            var pageType = Type.GetType(pageTypeName);
            if (pageType == null)
                throw new ArgumentException(pageTypeName + " type not exist");

            return pageType;
        }

        #endregion
    }
}
