﻿namespace ENINET.TransparentPortal.API.Configuration
{
    public class MailServiceSettings
    {
        public string SmtpServer { get; set; } = default!;
        public int SmtpPort { get; set; }
        public string SmtpUser { get; set; } = default!;
        public string SmtpPassword { get; set; } = default!;
        public string From { get; set; } = default!;
        public string Subject { get; set; } = default!;
        public string Object { get; set; } = default!;
    }
}
