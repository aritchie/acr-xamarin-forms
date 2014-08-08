using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;


namespace Acr.XamForms.ViewModels {
    
    public interface IProperty {
        ObservableList<string> Errors { get; }

        IEnumerable<string> Validate();
        bool IsValid { get; }
    }
}
