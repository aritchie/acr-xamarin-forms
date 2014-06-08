using System;
using System.Collections.Generic;
using Acr.XamForms.ViewModels;
using Samples.Models;


namespace Samples.ViewModels {
    
    public class HomeViewModel : ViewModel {

        public HomeViewModel() {
            this.List = new List<MenuItem> {
                new MenuItem {
                    Text = "",
                    Action = () => {}
                }
            };
        }


        public IList<MenuItem> List { get; private set; } 
    }
}
