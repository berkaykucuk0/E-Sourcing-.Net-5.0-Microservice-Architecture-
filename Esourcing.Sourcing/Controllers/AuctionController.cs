using AutoMapper;
using Esourcing.Sourcing.Entities;
using Esourcing.Sourcing.Repositories.Abstract;
using EventBusRabbitMQ.Core;
using EventBusRabbitMQ.Events.Concrede;
using EventBusRabbitMQ.Producer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Esourcing.Sourcing.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuctionController : ControllerBase
    {
        private readonly IAuctionRepository _auctionRepository;
        private readonly IBidRepository _bidRepository;
        private readonly ILogger<AuctionController> _logger;
        private readonly IMapper _mapper;
        private readonly EventBusRabbitMQProducer _eventBusRabbitMQProducer;
        public AuctionController(IAuctionRepository auctionRepository, IBidRepository bidRepository, ILogger<AuctionController> logger, IMapper mapper, EventBusRabbitMQProducer eventBusRabbitMQProducer)
        {
            _auctionRepository = auctionRepository;
            _bidRepository = bidRepository;
            _logger = logger;
            _mapper = mapper;
            _eventBusRabbitMQProducer = eventBusRabbitMQProducer;
        }

        //ProducesResponseType daha secure olması için ve OpenAPI lerin daha iyi tanıması amacıyla yazılmıştır.
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Auction>),(int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Auction>>> GetAuctions()
        {
           var auctions = await  _auctionRepository.GetAuctions();
           
           return Ok(auctions);
        }

        [HttpGet("{id:length(24)}",Name ="GetAuction")]
        //Auction bulunduysa dönüş tipini vs belirtiyoruz openAPI'ler vs. için
        [ProducesResponseType(typeof(Auction),(int)HttpStatusCode.OK)]
        //auction== null sa dönüş tipimiz yok ve notfound dönüyoruz demek
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<Auction>> GetAction(string id)
        {
            var auction = await _auctionRepository.GetAuction(id);
            if (auction==null)
            {
                return NotFound();
            }
            return Ok(auction);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Auction), (int)HttpStatusCode.Created)]
        public async Task<ActionResult<Auction>> CreateAuction([FromBody] Auction auction)
        {
            await _auctionRepository.Create(auction);

            //ekleme işleminden sonra GetAuction actionunu çağırıp eklenen datayı ekrana basıyor
            return CreatedAtRoute("GetAuction", new { id = auction.Id }, auction);
        }

        [HttpPut]
        [ProducesResponseType(typeof(Auction), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Auction>> UpdateAuction([FromBody] Auction auction)
        {
            return Ok(await _auctionRepository.Update(auction));
        }

        [HttpDelete("{id:length(24)}")]
        [ProducesResponseType(typeof(Auction), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Auction>> DeleteAuction(string id)
        {
            return Ok( await _auctionRepository.Delete(id));
        }


        //Kazananı belirleyen Controller ve Order kısmına sipariş gönder eventi bırakacağımız Controller
        [HttpPost("CompleteAuction")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        public async Task<ActionResult> CompleteAuction([FromBody] string id)
        {
            Auction auction = await _auctionRepository.GetAuction(id);
            if (auction == null)
            {
                return NotFound();
            }
            if (auction.Status != (int)Status.Active)
            {
                _logger.LogError("Auction can not be completed.");
                return BadRequest();
            }

            Bid bid = await _bidRepository.GetWinnerBid(id);
            if (bid == null)
            {
                return NotFound();
            }

            var eventMessage = _mapper.Map<OrderCreateEvent>(bid);
            eventMessage.Quantity = auction.Quantity;
            auction.Status = (int)Status.Closed;
            bool updateResult = await _auctionRepository.Update(auction);
            if (updateResult == false)
            {
                _logger.LogError("Auction can not updated");
                return BadRequest();
            }
            try
            {
                _eventBusRabbitMQProducer.Publish(EventBusConstants.OrderCreateQueue, eventMessage);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error ,Publishing integration event: {EventId} from {AppName} ", eventMessage.Id, "Sourcing");
                throw;
            }
            return Accepted();
        }


        // Dummy datalarla test
        [HttpPost("TestEvent")]
        public ActionResult<OrderCreateEvent> TestEvent()
        {
            OrderCreateEvent eventMessage = new OrderCreateEvent();
            eventMessage.AuctionId = "608a984dc21084e63287303c";
            eventMessage.ProductId = "608822990c39df47cc0be383";
            eventMessage.Price = 10;
            eventMessage.Quantity = 100;
            eventMessage.SellerUserName = "test@test.com";

            try
            {
                _eventBusRabbitMQProducer.Publish(EventBusConstants.OrderCreateQueue, eventMessage);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ERROR Publishing integration event: {EventId} from {AppName}", eventMessage.Id, "Sourcing");
                throw;
            }

            return Accepted(eventMessage);
        }
    }
}
