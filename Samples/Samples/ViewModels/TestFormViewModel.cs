using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Input;
using Acr.XamForms;
using Acr.XamForms.ViewModels;


namespace Samples.ViewModels {
    
    public class TestFormViewModel : FormViewModel {

        public ICommand Save { get; private set; }
        public Property<string> FirstName { get; private set; }
        public Property<string> LastName { get; private set; }
        public Property<string> Email { get; private set; }


        public TestFormViewModel() {
            this.FirstName = new Property<string>((x, errors) => ValidateName(x, "First", errors));
            this.LastName = new Property<string>((x, errors) => ValidateName(x, "Last", errors));
            this.Email = new Property<string>((x, errors) => {
                
            });
        }


        private static void ValidateName(string name, string nameType, IList<string> errors) {
            if (name.IsEmpty())
                errors.Add(String.Format("{0} name is required", nameType));

            else if (name.Length > 20)
                errors.Add(String.Format("Your {0} name is too long", nameType));

            if (!Regex.IsMatch("^[a-zA-Z_]*$", name))
                errors.Add(String.Format("{0} name contains invalid characters", nameType));
        }
    }
}
