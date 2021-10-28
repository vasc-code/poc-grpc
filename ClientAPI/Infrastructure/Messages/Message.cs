using System;

namespace Infrastructure.Messages
{
    public abstract class Message
    {
        protected Message()
        {
            MessageType = GetType().Name;
            AggregateId = Guid.NewGuid();
        }

        public string MessageType { get; protected set; }
        public Guid AggregateId { get; protected set; }
    }
}