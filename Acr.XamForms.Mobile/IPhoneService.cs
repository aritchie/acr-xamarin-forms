using System;


namespace Acr.XamForms.Mobile {
    
    public interface IPhoneService {

        void Call(string person, string number);
        void Sms(string number, string message);
        //bool IsRoaming IsAvailable
    }
}
