using System;


namespace Acr.XamForms.Services {
    
    public interface IMessageBus {

        MessageSubscriptionToken Subscribe<TMessage>(Action<object, TMessage> onMessage);
        void Publish<TMessage>(object sender, TMessage message);
        
        void Clear(Type messageType);
        void ClearAll();
    }
}
