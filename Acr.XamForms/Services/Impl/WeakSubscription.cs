using System;


namespace Acr.XamForms.Services.Impl {
    
    internal class WeakSubscription<T> {
        private readonly WeakReference reference;


        public WeakSubscription(Action<object, T> action) {
            this.reference = new WeakReference(action);
        }


        public bool IsAlive {
            get { return this.reference.IsAlive; }
        }


        public void Invoke(object sender, T args) {
            if (this.reference.IsAlive)
                ((Action<object, T>)this.reference.Target).Invoke(sender, args);
        }
    }
}
