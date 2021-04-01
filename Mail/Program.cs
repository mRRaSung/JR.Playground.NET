using MailKit;
using MailKit.Net.Imap;
using System;

namespace Mail
{
    class Program
    {
        static void Main(string[] args)
        {
            IMailer mailer = new Mailer();
            mailer.SendEmalAsync("receiver@gmail.com", "HelloWorld", "Beautiful").Wait();
        }
    }
}
