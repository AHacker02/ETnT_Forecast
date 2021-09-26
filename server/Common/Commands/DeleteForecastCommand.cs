using System;
using MediatR;

namespace Common.Commands
{
    public class DeleteForecastCommand : IRequest<bool>
    {
        public DeleteForecastCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}