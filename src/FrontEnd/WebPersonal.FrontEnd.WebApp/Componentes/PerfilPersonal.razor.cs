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
        private string _profileValue { get; set; } //Propiedad privada para almacenar el valor actual
        public PersonalProfileDto PersonalProfile { get; set; }
        public List<ErrorDto> Erros { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            if (_profileValue != Profile) //Comparamos el valor, y si es distinto, consultamos la información
            {
                await CalculateProfile();
            }

            await base.OnParametersSetAsync();
        }

        private async Task CalculateProfile()
        {
            _profileValue = Profile; //ASignamos el valor
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
