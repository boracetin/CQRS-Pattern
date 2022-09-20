using Application.Features.ProgrammingLanguages.Dtos;
using Application.Features.ProgrammingLanguages.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ProgrammingLanguages.Commands.UpdateProgrammingLanguage
{
    public class UpdateProgrammingLanguageCommand : IRequest<UpdatedProgrammingLanguageDto>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public class UpdateProgrammingLanguageHandler: IRequestHandler<UpdateProgrammingLanguageCommand,UpdatedProgrammingLanguageDto>
        {
            private readonly IMapper _mapper;
            private readonly IProgrammingLanguageRepository _programmingLanguageRepository;
            private readonly ProgrammingLanguageBusinessRules _programingLanguageBusinessRule;

            public UpdateProgrammingLanguageHandler(IMapper mapper, IProgrammingLanguageRepository programmingLanguageRepository, ProgrammingLanguageBusinessRules programingLanguageBusinessRule)
            {
                _mapper = mapper;
                _programmingLanguageRepository = programmingLanguageRepository;
                _programingLanguageBusinessRule = programingLanguageBusinessRule;
            }

            public async Task<UpdatedProgrammingLanguageDto> Handle(UpdateProgrammingLanguageCommand request, CancellationToken cancellationToken)
            {
                
                ProgrammingLanguage? programmingLanguage = await _programmingLanguageRepository.GetAsync(b => b.Id == request.Id);

                _programingLanguageBusinessRule.ProgrammingLanguageShouldExistWhenRequested(programmingLanguage);

                ProgrammingLanguage mappedProgrammingLangugage = _mapper.Map<ProgrammingLanguage>(request);

                ProgrammingLanguage updatedProgrammingLanguage = await _programmingLanguageRepository.UpdateAsync(mappedProgrammingLangugage);

                UpdatedProgrammingLanguageDto updatedProgrammingLanguageDto = _mapper.Map<UpdatedProgrammingLanguageDto>(updatedProgrammingLanguage);

                return updatedProgrammingLanguageDto;
            }
        }
    }
}
