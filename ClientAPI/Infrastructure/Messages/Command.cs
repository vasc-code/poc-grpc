using FluentValidation.Results;
using MediatR;
using System;

namespace Infrastructure.Messages
{
    public abstract class Command<TResponse> : Message, IRequest<TResponse>
    {
        public DateTime Timestamp { get; private set; }
        public ValidationResult ValidationResult { get; set; }

        protected Command()
        {
            Timestamp = DateTime.Now;
        }

        public abstract bool IsValid();
    }
}