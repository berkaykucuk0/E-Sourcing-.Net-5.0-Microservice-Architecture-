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
    public class AuctionClient
    {
        public HttpClient _client { get; }

        public AuctionClient(HttpClient client)
        {
            _client = client;
            _client.BaseAddress = new Uri(CommonInfo.LocalAuctionBaseAddress);

        }

        public async Task<Result<AuctionModel>> CreateAuction(AuctionModel model)
        {
            var dataAsString = JsonConvert.SerializeObject(model);
            var content = new StringContent(dataAsString);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await _client.PostAsync("api/v1/Auction", content);
            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<AuctionModel>(responseData);
                if (result != null)
                    return new Result<AuctionModel>(true, ResultConstant.RecordCreateSuccessfully, result);
                else
                    return new Result<AuctionModel>(false, ResultConstant.RecordCreateNotSuccessfully);
            }
            return new Result<AuctionModel>(false, ResultConstant.RecordCreateNotSuccessfully);
        }

        public async Task<Result<IList<AuctionModel>>> GetAuctions()
        {
            var response = await  _client.GetAsync("api/v1/Auction");
            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<AuctionModel>>(responseData);
                if (result.Any())
                {
                    return new Result<IList<AuctionModel>>(isSuccess: true, ResultConstant.RecordFound, result.ToList(), result.Count);
                }
                else
                {
                    return new Result<IList<AuctionModel>>(isSuccess: false, ResultConstant.RecordNotFound);
                }
            }
            return new Result<IList<AuctionModel>>(isSuccess: false, ResultConstant.RecordNotFound);

        }


    }
}
