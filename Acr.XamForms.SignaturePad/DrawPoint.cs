using System;


namespace Acr.XamForms.SignaturePad {
   
    public class DrawPoint {

        public float X { get; set; }
        public float Y { get; set; }


        public DrawPoint() {}
        public DrawPoint(float x, float y) {
            this.X = x;
            this.Y = y;
        }
    }
}
