using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using WebPersonal.Shared.Dto;
using WebPersonal.Shared.ROP;

namespace WebPersonal.FrontEnd.WebApp.Componentes
{
    public partial class Contacto
    {
        [Inject]
        private IHttpClientFactory ClientFactory { get; set; }
        private ContactDto _contact { get; set; } = new ContactDto();

        private string MessageBoxCss { get; set; } = "oculto";
        private string FormCss { get; set; } = "visible";
        private async Task Enviar()
        {

            HttpClient client = ClientFactory.CreateClient("BackendApi");

            HttpResponseMessage result = await client.PostAsJsonAsync($"api/contact", _contact);

            ResultDto<ContactResponse> contactResponse = await result.Content.ReadFromJsonAsync<ResultDto<ContactResponse>>();

            if (contactResponse.Success)
            {
                MessageBoxCss = "visible";
                FormCss = "oculto";
            }

        }
    }
}
