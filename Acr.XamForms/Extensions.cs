using System;
using System.Collections.Generic;
using Acr.XamForms.ViewModels;
using Xamarin.Forms;


namespace Acr.XamForms {
    
    public static class Extensions {

        public static bool IsEmpty(this string @string) {
            return String.IsNullOrWhiteSpace(@string);
        }


        public static void BindViewModel(this Page page, IViewModel viewModel) {
            page.BindingContext = viewModel;

            page.Appearing += (sender, args1) => viewModel.OnAppearing();
            page.Disappearing += (sender, args1) => viewModel.OnDisappearing();             
        }


        public static void ForEach<T>(this IList<T> list, Action<T> action) {
            if (list == null)
                return;

            foreach (var item in list)
                action(item);
        }


        public static void ForEach<T>(this IList<T> list, Action<int, T> action) {
            if (list == null)
                return;

            for (var i = 0; i < list.Count; i++)
                action(i, list[i]);
        }
    }
}