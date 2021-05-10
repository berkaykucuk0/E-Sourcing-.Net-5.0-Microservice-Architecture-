using Esourcing.UI.Clients;
using Esourcing.UI.Models;
using ESourcing.Core.Repository;
using ESourcing.Core.ResultModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Esourcing.UI.Controllers
{
    public class AuctionController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly ProductClient _productClient;
        private readonly AuctionClient _auctionClient;
        private readonly BidClient _bidClient;

        public AuctionController(IUserRepository userRepository, ProductClient productClient, AuctionClient auctionClient, BidClient bidClient)
        {
            _userRepository = userRepository;
            _productClient = productClient;
            _auctionClient = auctionClient;
            _bidClient = bidClient;
        }

        public async Task<IActionResult> Index()
        {
            var auctions = await _auctionClient.GetAuctions();
            if (auctions.IsSuccess)
            {
                return View(auctions.Data);
            }
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var userList = await _userRepository.GetAllAsync();
            var productList = await _productClient.GetProducts();
            ViewBag.ProductList = productList.Data;
            ViewBag.UserList = userList;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AuctionModel model)
        {
            model.Status = 0;
            model.CreatedAt = DateTime.Now;
            model.IncludedSellers.Add(model.SellerId);
            var createAuction = await _auctionClient.CreateAuction(model);
            if (createAuction.IsSuccess)
                return RedirectToAction("Index");
            return View(model);
        }

        public async Task<IActionResult> Detail(string id)
        {
            AuctionBidModel model = new AuctionBidModel();

            var auctionResponse = await _auctionClient.GetAuctionById(id);
            var bidsResponse = await _bidClient.GetAllBidsByAuctionId(id);


            model.SellerUserName = HttpContext.User?.Identity.Name;
            model.AuctionId = auctionResponse.Data.Id;
            model.ProductId = auctionResponse.Data.ProductId;
            model.Bids = bidsResponse.Data;
            var isAdmin = HttpContext.Session.GetString("IsAdmin");
            model.IsAdmin = Convert.ToBoolean(isAdmin);
            return View(model);
        }

        [HttpPost]
        public async Task<Result<string>> SendBid(BidModel model)
        {
            model.CreateAt = DateTime.Now;
            var sendBidResponse = await _bidClient.SendBid(model);
            return sendBidResponse;
        }

        [HttpPost]
        public async Task<Result<string>> ComPleteBid(string id)
        {
            var completeBidResponse = await _auctionClient.CompleteBid(id);
            return completeBidResponse;
        }


    }
}
