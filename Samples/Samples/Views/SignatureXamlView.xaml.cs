using System;
using Xamarin.Forms;


namespace Samples.Views {
    public partial class SignatureXamlView : ContentPage {

        public SignatureXamlView() {
            InitializeComponent();
        }


        async void OnChangeTheme(object sender, EventArgs e) {
            var action = await DisplayActionSheet("Change Theme", "Cancel", null, "White", "Black", "Aqua");
            switch (action) {
                case "White":
                    this.padView.BackgroundColor = Color.White;
                    this.padView.StrokeColor = Color.Black;
                    this.padView.ClearTextColor = Color.White;
                    this.padView.ClearText = "Clear Blackboard";
                    break;

                case "Black":
                    this.padView.BackgroundColor = Color.Black;
                    this.padView.StrokeColor = Color.White;
                    this.padView.ClearTextColor = Color.White;
                    this.padView.ClearText = "Clear Whiteboard";
                    break;

                case "Aqua":
                    this.padView.BackgroundColor = Color.Aqua;
                    this.padView.StrokeColor = Color.Red;
                    this.padView.ClearTextColor = Color.Black;
                    this.padView.ClearText = "Clear The Aqua";
                    break;
            }
        }
    }
}
