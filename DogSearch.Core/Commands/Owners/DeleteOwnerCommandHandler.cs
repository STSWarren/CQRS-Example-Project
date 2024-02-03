using DogSearch.Core.Commands.Dogs;
using DogSearch.Core.Interfaces.Infrastructure.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogSearch.Core.Commands.Owners;

internal class DeleteOwnerCommandHandler : IRequestHandler<DeleteOwnerCommand>
{
    private readonly IOwnerRepository _repository;

    public DeleteOwnerCommandHandler(IOwnerRepository repository)
    {
        _repository = repository;
    }

    public Task Handle(DeleteOwnerCommand request, CancellationToken cancellationToken)
    {
        return _repository.Delete(request.Id);
    }
}
