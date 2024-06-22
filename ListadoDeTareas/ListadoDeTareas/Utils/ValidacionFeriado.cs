using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;

namespace ListadoDeTareas.Utils
{
    public class ValidacionFeriado
    {
        private readonly HttpClient _httpClient;

        public ValidacionFeriado(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public JsonResult ValidarFeriado(DateTime date)
        {
            int year = date.Year;
            int month = date.Month;
            int day = date.Day;

            string url = "https://apis.digital.gob.cl/fl/feriados/" + year + "/" + month + "/" + day;
            var request = new HttpRequestMessage(HttpMethod.Get, url);

            // Realizar la llamada 
            var response = _httpClient.Send(request);

            if (response.IsSuccessStatusCode)
            {
                var content = response.Content.ReadAsStringAsync().Result;
                return new JsonResult(content);
            }
            else
            {
                return new JsonResult("Error en la respuesta del servidor");
            }
        }
    }
}
