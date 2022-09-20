using Application.Features.ProgrammingLanguages.Dtos;
using Application.Features.ProgrammingLanguages.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ProgrammingLanguages.Commands.DeleteProgrammingLanguage
{
    public class DeleteProgrammingLanguageCommand:IRequest<DeletedProgrammingLanguageDto>
    {
        public int Id { get; set; }
        public class DeleteProgrammingLanguageHandler:IRequestHandler<DeleteProgrammingLanguageCommand, DeletedProgrammingLanguageDto>
        {
            private readonly IMapper _mapper;
            private readonly IProgrammingLanguageRepository _programmingLanguageRepository;
            private readonly ProgrammingLanguageBusinessRules _programingLanguageBusinessRule;

            public DeleteProgrammingLanguageHandler(IMapper mapper, IProgrammingLanguageRepository programmingLanguageRepository, ProgrammingLanguageBusinessRules programingLanguageBusinessRule)
            {
                _mapper = mapper;
                _programmingLanguageRepository = programmingLanguageRepository;
                _programingLanguageBusinessRule = programingLanguageBusinessRule;
            }

            public async Task<DeletedProgrammingLanguageDto> Handle(DeleteProgrammingLanguageCommand request, CancellationToken cancellationToken)
            {
                ProgrammingLanguage? programmingLanguage = await _programmingLanguageRepository.GetAsync(b => b.Id == request.Id);

                _programingLanguageBusinessRule.ProgrammingLanguageShouldExistWhenRequested(programmingLanguage);

                ProgrammingLanguage mappedProgrammingLangugage = _mapper.Map<ProgrammingLanguage>(request);

                ProgrammingLanguage deletedProgrammingLanguage = await _programmingLanguageRepository.DeleteAsync(mappedProgrammingLangugage);

                DeletedProgrammingLanguageDto deletedProgrammingLanguageDto = _mapper.Map<DeletedProgrammingLanguageDto>(deletedProgrammingLanguage);

                return deletedProgrammingLanguageDto;

            }
        }
       
    }
}
