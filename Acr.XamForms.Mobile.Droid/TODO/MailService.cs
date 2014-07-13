//using System;
//using System.Linq;
//using Android.Content;
//using Android.Text;
//using Xamarin.Forms;


//namespace Acr.XamForms.Mobile.Droid {
    
//    public class MailService : AbstractMailService {

//        public override void Send(MailMessage mail) {
//            var intent = new Intent(Intent.ActionSend);

//            intent.PutExtra(Intent.ExtraSubject, mail.Subject ?? String.Empty);
//            if (mail.To != null)
//                intent.PutExtra(Intent.ExtraEmail, mail.To.Select(x => x.Address).ToArray());

//            if (mail.Cc != null)
//                intent.PutExtra(Intent.ExtraCc, mail.Cc.Select(x => x.Address).ToArray());

//            if (mail.Bcc != null)
//                intent.PutExtra(Intent.ExtraBcc, mail.Bcc.Select(x => x.Address).ToArray());

//            if (mail.IsHtml) {
//                intent.SetType("text/html");
//                intent.PutExtra(Intent.ExtraText, Html.FromHtml(mail.Message));
//            }
//            else {
//                intent.SetType("text/plain");
//                intent.PutExtra(Intent.ExtraText, mail.Message);
//            }

//            //if (mail.Attachments != null) {
//            //    if (mail.Attachments.Count > 1)
//            //        throw new ArgumentException("Android only supports 1 attachment");

//            //    var attachment = mail.Attachments.First();

//        //        DoOnActivity(activity =>
//        //        {
//        //            var localFileStream = activity.OpenFileOutput(attachment.FileName, FileCreationMode.WorldReadable);
//        //            var localfile = activity.GetFileStreamPath(attachment.FileName);
//        //            attachment.Content.CopyTo(localFileStream);
//        //            localFileStream.Close();
//        //            var uri = Android.Net.Uri.FromFile(localfile);
//        //            emailIntent.PutExtra(Intent.ExtraStream, uri);

//        //            localfile.DeleteOnExit(); // Schedule to delete file when VM quits.
//        //        });
//            //}

//            Forms.Context.StartActivity(intent);

//        }
//    }
//}