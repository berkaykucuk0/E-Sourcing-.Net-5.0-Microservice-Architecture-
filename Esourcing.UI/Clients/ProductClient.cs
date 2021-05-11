using Esourcing.UI.Models;
using ESourcing.Core.Common;
using ESourcing.Core.ResultModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Esourcing.UI.Clients
{
    public class ProductClient
    {
        public HttpClient _client { get;}

        public ProductClient(HttpClient client)
        {
            _client = client;
            _client.BaseAddress = new Uri(CommonInfo.BaseAddress);
        }

        public async Task<Result<IList<ProductModel>>> GetProducts()
        {
            var response = await _client.GetAsync("/Product");
            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                var result =JsonConvert.DeserializeObject<List<ProductModel>>(responseData);
                if (result.Any())
                {
                    return new Result<IList<ProductModel>>(isSuccess: true, ResultConstant.RecordFound, result.ToList(), result.Count);
                }
                else
                {
                    return new Result<IList<ProductModel>>(isSuccess: false, ResultConstant.RecordNotFound);
                }
            }
            return new Result<IList<ProductModel>>(isSuccess: false, ResultConstant.RecordNotFound);
        }

       
     
    }
}
