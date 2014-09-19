using System;
using System.Windows.Input;
using Acr.XamForms.ViewModels;


namespace Samples.ViewModels {
    
    public class TestFormViewModel : FormViewModel {

        public ICommand Save { get; private set; }
        public Property<string> FirstName { get; private set; }
        public Property<string> LastName { get; private set; }
        public Property<string> Email { get; private set; }
        public Property<string> Password { get; private set; } 


        public TestFormViewModel() {
            // TODO: need ability to mask or shut down bad input OR allow it and throw validation on it
            this.FirstName = new Property<string>();
            this.LastName = new Property<string>();
            this.Password = new Property<string>();
            this.Email = new Property<string>();
            this.LastName.Subscribe(x => {
            });
            //    .ToObservable(x => x.Value)
            //    .Throttle
            //        //if (x.IsEmpty())
            //        //    errors.Add("Password is required");
            //        //else if (x.Length < 5)
            //        //    errors.Add("Password is too short");
            //    });
            //this.Password = new Property<string>((x, errors) => {

            //});
            //this.Email = new Property<string>((x, errors) => {
            //    if (x.IsEmpty())
            //        errors.Add("Email is required");
            //    else if (x.Length > 0 && x.Length < 6)
            //        errors.Add("Too Short");
            //    else if (x.Length > 20)
            //        errors.Add("Too Long");

            //    if (!x.Contains("@"))
            //        errors.Add("Invalid Address");
            //});
        }



        //private static void ValidateName(string name, string nameType, IList<string> errors) {
        //    if (name.IsEmpty())
        //        errors.Add(String.Format("{0} name is required", nameType));

        //    else if (name.Length > 10)
        //        errors.Add(String.Format("Your {0} name is too long", nameType));
        //}
    }
}
