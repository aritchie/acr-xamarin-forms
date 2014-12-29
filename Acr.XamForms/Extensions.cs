using System;
using System.Collections.Generic;
using Acr.XamForms.ViewModels;
using Xamarin.Forms;


namespace Acr.XamForms {

    public static class Extensions {

        public static bool IsEmpty(this string @string) {
            return String.IsNullOrWhiteSpace(@string);
        }


        //public static IObservable<R> ToObservable<T, R>(this T target, Expression<Func<T, R>> property) where T : INotifyPropertyChanged {
        //    var body = property.Body;
        //    var propertyName = "";

        //    if (body is MemberExpression)
        //        propertyName = ((MemberExpression)body).Member.Name;
        //    else if (body is MethodCallExpression)
        //        propertyName = ((MethodCallExpression)body).Method.Name;
        //    else
        //        throw new NotSupportedException("Only use expressions that call a single property or method");

        //    var getValueFunc = property.Compile();
        //    return Observable.Create<R>(o => {
        //        var eventHandler = new PropertyChangedEventHandler((s, pce) => {
        //            if (pce.PropertyName == null || pce.PropertyName == propertyName)
        //                o.OnNext(getValueFunc(target));
        //        });
        //        target.PropertyChanged += eventHandler;
        //        return () => target.PropertyChanged -= eventHandler;
        //    });
        //}


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