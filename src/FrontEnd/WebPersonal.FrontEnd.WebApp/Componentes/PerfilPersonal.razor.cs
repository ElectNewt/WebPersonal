using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using WebPersonal.Shared.Dto;
using System.Text.Json;
using WebPersonal.Shared.ROP;
using System.Net.Http.Json;
using System.Linq;

namespace WebPersonal.FrontEnd.WebApp.Componentes
{
    public partial class PerfilPersonal
    {

        [Inject]
        private IHttpClientFactory ClientFactory { get; set; }

        [Parameter]
        public string Profile { get; set; }

        public PersonalProfileDto PersonalProfile { get; set; }

        public List<ErrorDto> Erros { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var result = await GetPersonalProfile(Profile);
            if (!result.Errors.Any())
                PersonalProfile = result.Value;
            else
                Erros = result.Errors;
        }

        private async Task<ResultDto<PersonalProfileDto>> GetPersonalProfile(string profileCode)
        {
            var client = ClientFactory.CreateClient("BackendApi");
            return await client.GetFromJsonAsync<ResultDto<PersonalProfileDto>>($"api/perfilpersonal/{profileCode}", new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }
    }
}
