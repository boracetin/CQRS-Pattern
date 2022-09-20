using Application.Features.ProgrammingLanguages.Models;
using Application.Features.ProgrammingLanguages.Queries.GetListProgrammingLanguage;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    public class ProgrammingLanguageController:BaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] Core.Application.Requests.PageRequest pageRequest)
        {
            GetListProgrammingLanguageQuery getListProgramminLanguageQuery = new() { PageRequest = pageRequest };
            ProgrammingLanguageListModel result = await Mediator.Send(getListProgramminLanguageQuery);
            return Ok(result);
        }


    }
}
