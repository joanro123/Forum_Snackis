using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Forum_Snackis.Models;
using System.Net.Http;
using System.Text.Json;

namespace Forum_Snackis.Gateway
{
    public class InsertGateway : IInsertGateway
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public InsertGateway(IConfiguration configuration, HttpClient httpClient)
        {
            _configuration = configuration;
            _httpClient = httpClient;
        }

        public async Task<List<Insert>> GetInserts()
        {
            var response = await _httpClient.GetAsync(_configuration["InsertAPI"]);
            string apiResponse = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<Insert>>(apiResponse);
        }
    }
}
