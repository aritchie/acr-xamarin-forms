using System;
using XamAddressBook = Xamarin.Contacts.AddressBook;


namespace Acr.XamForms.Mobile {

    public class AddressBookService : IAddressBookService {
        private readonly XamAddressBook book;


        public AddressBookService() {
#if __ANDROID__
            this.book = new XamAddressBook(Xamarin.Forms.Forms.Context);
#else
            this.book = new XamAddressBook();
#endif
        }
    }
}
