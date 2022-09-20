using Application.Features.ProgrammingLanguages.Dtos;
using Application.Features.ProgrammingLanguages.Models;
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

namespace Application.Features.ProgrammingLanguages.Commands.CreateProgrammingLanguage
{
    public class CreateProgrammingLanguageCommand: IRequest<CreatedProgrammingLanguageDto>
    {
        public string Name { get; set; }

        public class CreatedProgrammingLanguageHandler : IRequestHandler<CreateProgrammingLanguageCommand, CreatedProgrammingLanguageDto>
        {
            private readonly IMapper _mapper;
            private readonly IProgrammingLanguageRepository _programmingLanguageRepository;
            private readonly ProgrammingLanguageBusinessRules _programingLanguageBusinessRule;
            public CreatedProgrammingLanguageHandler(IMapper mapper, IProgrammingLanguageRepository programmingLanguageRepository, ProgrammingLanguageBusinessRules programingLanguageBusinessRule)
            {
                _mapper = mapper;
                _programmingLanguageRepository = programmingLanguageRepository;
                _programingLanguageBusinessRule = programingLanguageBusinessRule;
            }

            public async Task<CreatedProgrammingLanguageDto> Handle(CreateProgrammingLanguageCommand request, CancellationToken cancellationToken)
            {
                await _programingLanguageBusinessRule.ProgrammingLanguageNameCanNotBeDuplicatedWhenInserted(request.Name);

                ProgrammingLanguage mappedprogrammingLanguage = _mapper.Map<ProgrammingLanguage>(request);
                ProgrammingLanguage createdProgrammingLanguage = await _programmingLanguageRepository.AddAsync(mappedprogrammingLanguage);
                CreatedProgrammingLanguageDto createdProgrammingLanguageDto = _mapper.Map<CreatedProgrammingLanguageDto>(createdProgrammingLanguage);
                
                return createdProgrammingLanguageDto;
            }
        }


    }
}
