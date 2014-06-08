using System;


namespace Acr.XamForms.Mobile {
    
    public class PositionEventArgs : EventArgs {
        public Position Position { get; private set; }


        public PositionEventArgs(Position position) {
            this.Position = position;
        }
    }
}
