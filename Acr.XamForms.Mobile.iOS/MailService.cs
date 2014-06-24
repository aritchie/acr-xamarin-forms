//using System;
//using System.IO;
//using System.Linq;
//using MonoTouch.Foundation;
//using MonoTouch.MessageUI;
//using MonoTouch.UIKit;
//using Xamarin.Forms;


//namespace Acr.XamForms.Mobile.iOS {
    
//    [Preserve]
//    public class MailService : AbstractMailService {

//        public override void Send(MailMessage mail) {
//            if (!MFMailComposeViewController.CanSendMail)
//                throw new MvxException("This device cannot send mail");

//            var controller = new MFMailComposeViewController();
//            controller.SetSubject(mail.Subject ?? String.Empty);
//            controller.SetMessageBody(mail.Message ?? String.Empty, mail.IsHtml);

//            if (mail.To != null)
//                controller.SetToRecipients(mail.To.Select(x => x.Address).ToArray());

//            if (mail.Cc != null)
//                controller.SetCcRecipients(mail.Cc.Select(x => x.Address).ToArray());

//            if (mail.Bcc != null)
//                controller.SetBccRecipients(mail.Bcc.Select(x => x.Address).ToArray());


//            //if (mail.Attachments != null) 
//            //    foreach (var att in mail.Attachments)
//            //        controller.AddAttachmentData(
//            //            NSData.FromFile(att.FileName), 
//            //            att.ContentType, 
//            //            Path.GetFileName(att.FileName)
//            //        );
////            _mail.Finished += HandleMailFinished;
////            _modalHost.PresentModalViewController(_mail, true);
//        }


//        //private void PushTopController() {
//        //        var windows = UIApplication.SharedApplication.Windows;
//        //        Array.Reverse(windows);
				
//        //    foreach (var win in windows) {
//        //        if (window.WindowLevel == UIWindow.LevelNormal && !window.Hidden) {
//        //            window.AddSubview(OverlayView);
//        //            break;
//        //        }
//        //    }  
//        //}
//    }
//}

///*
//    */

//        //public bool CanSendEmail
//        //{
//        //    get
//        //    {
//        //        return MFMailComposeViewController.CanSendMail;
//        //    }
//        //}

//        //public bool CanSendAttachments
//        //{
//        //    get
//        //    {
//        //        // if we can send email, then we can send attachments
//        //        return CanSendEmail;
//        //    }
//        //}

//        //private void HandleMailFinished(object sender, MFComposeResultEventArgs e)
//        //{
//        //    var uiViewController = sender as UIViewController;
//        //    if (uiViewController == null)
//        //    {
//        //        throw new ArgumentException("sender");
//        //    }

//        //    uiViewController.DismissViewController(true, () => { });
//        //    _modalHost.NativeModalViewControllerDisappearedOnItsOwn();
//        //}