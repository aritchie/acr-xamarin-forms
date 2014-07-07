using System;
using System.Collections;
using System.Collections.Generic;


namespace Acr.XamForms.Services.Impl {
    
    public class MessageBusImpl : IMessageBus {

        private readonly IDictionary<Type, IList<MessageSubscriptionToken>> subscriptions = new Dictionary<Type, IList<MessageSubscriptionToken>>();


        public MessageSubscriptionToken Subscribe<TMessage>(Action<object, TMessage> onMessage) {
            var token = new MessageSubscriptionToken();
            if (!this.subscriptions.ContainsKey(typeof(TMessage))) {
                lock (this.subscriptions) {
                    this.subscriptions.Add(typeof(TMessage), new List<MessageSubscriptionToken>());
                }
            }
            var list = this.subscriptions[typeof(TMessage)];
            lock (list) {
                list.Add(token);
            }
            return null;
        }


        public void Publish<TMessage>(object sender, TMessage message) {

        }


        public void Clear(Type messageType) {
            lock (this.subscriptions) {
                // TODO: do I have to flush out tokens?
                this.subscriptions.Remove(messageType);
            }
        }


        public void ClearAll() {
            lock (this.subscriptions) {
                this.subscriptions.Clear();
            }
        }


        #region Internals
        #endregion
    }
}