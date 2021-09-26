using System.Collections.Generic;
using MediatR;

namespace Common.Commands
{
    public class AddUpdateForecastCommand : IRequest<object>
    {
        public IEnumerable<ForecastCommand> Forecasts { get; set; }
    }
}