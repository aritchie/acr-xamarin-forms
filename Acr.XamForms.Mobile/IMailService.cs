using System;


namespace Acr.XamForms.Mobile {

    public interface IMailService {
    }
}
/*
    public interface IMvxComposeEmailTaskEx
        : IMvxComposeEmailTask
    {
        void ComposeEmail(IEnumerable<string> to, IEnumerable<string> cc, string subject, string body, bool isHtml, IEnumerable<EmailAttachment> attachments);
        bool CanSendEmail { get; }
        bool CanSendAttachments { get; }
    }
 
     public class EmailAttachment
    {
        public string ContentType { get; set; }
        public string FileName { get; set; }
        public Stream Content { get; set; }
    }
 * 
 * 
         public string Build(string to, string cc, string subject, string body)
        {
            var builder = new StringBuilder();
            builder.Append("mailto:" + to);

            string sep = "?";
            AddParam(builder, "cc", cc, ref sep);
            AddParam(builder, "subject", subject, ref sep);
            AddParam(builder, "body", body, ref sep);

            var url = builder.ToString();
            return url;
        }

        private void AddParam(StringBuilder builder, string param, string value, ref string separator)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return;
            }

            builder.Append(separator);
            separator = "&";
            builder.Append(param);
            builder.Append("=");
            builder.Append(Uri.EscapeDataString(value));
        }
 */