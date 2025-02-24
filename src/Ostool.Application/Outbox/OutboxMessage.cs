using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ostool.Application.Outbox
{
    public class OutboxMessage
    {
        public OutboxMessage(DateTime occurredOnUtc, OutboxEventType type, string content)
        {
            OccurredOnUtc = occurredOnUtc;
            Content = content;
            EventType = type;
        }

        private OutboxMessage()
        {

        }

        public int Id { get; set; }

        public DateTime OccurredOnUtc { get; set; }

        public OutboxEventType EventType { get; set; }

        public string Content { get; set; } = string.Empty;

        public bool Processed { get; set; } = false;

    }
}