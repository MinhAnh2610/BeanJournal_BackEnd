using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
  public class Message
  {
    public List<MailboxAddress> To { get; set; } = [];
    public string Subject { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public Message(IEnumerable<string> to, string subject, string content) 
    { 
      To = [.. to.Select(x => new MailboxAddress("email", x))];
      Subject = subject;
      Content = content;
    }
  }
}
