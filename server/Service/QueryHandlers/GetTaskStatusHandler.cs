using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Common.Models;
using Common.Query;
using DataAccess.Abstractions;
using MediatR;

namespace Service.QueryHandlers
{
    public class GetTaskStatusHandler:IRequestHandler<GetTaskStatusQuery,TaskStatusViewModel>
    {
        private readonly ILookupRepository _repository;
        private readonly IMapper _mapper;

        public GetTaskStatusHandler(ILookupRepository repository,IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<TaskStatusViewModel> Handle(GetTaskStatusQuery request, CancellationToken cancellationToken)
            => _mapper.Map<TaskStatusViewModel>(await _repository.GetEventByIdAsync(request.Id));
    }
}