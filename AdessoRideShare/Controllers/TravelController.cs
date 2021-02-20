using AdessoRideShare.Api.Model.Dtos;
using AdessoRideShare.Data.Access.Repositories;
using AdessoRideShare.Data.Common.CommonModel;
using AdessoRideShare.Data.Model.Entity;
using AdessoRideShare.Exceptions;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdessoRideShare.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TravelController : BaseMongoController<Travel>
    {
        private readonly IMapper _mapper;
        public TravelController(TravelRepository travelRepository, IMapper mapper) : base(travelRepository)
        {
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            return base.GetModelList();
        }

        [HttpGet("{id}")]
        public ActionResult GetById(string id)
        {
            return base.GetModel(id);
        }

        /// <summary>
        /// This method starts a Travel Record and saves it to MongoDb
        /// </summary>
        /// <param name="travelDto"> Gets a AddTravelDto and maps it to Travel Entity Object </param>
        /// <returns> Returns the generic response with the added Travel entity </returns>
        [HttpPost("add")]
        public ActionResult CreateTravel(AddTravelDto travelDto)
        {
            Travel travel = _mapper.Map<Travel>(travelDto);
            return base.AddModel(travel);
        }

        /// <summary>
        /// This is used to join (share) an existing ride. 
        /// It gets the PassengerCount Value and checks whether the specific Traveş has enough empty seats.
        /// 
        /// </summary>
        /// <param name="travelDto"> Gets a JoinTravelDto that has Travel Id and Passanger Count and maps it to Travel Entity Object </param>
        /// <returns> Returns a generic empty reponse </returns>
        [HttpPost("join")]
        public ActionResult JoinTravel(JoinTravelDto travelDto)
        {
            Travel travel = BaseMongoRepository.GetById(travelDto.Id);
            if (travel == null) throw new TravelNotFoundException();
            if (!IsJoinable(travel, travelDto))
            {
                throw new NotEnoughSeatException();
            }
            travel.AvailableSeats -= travelDto.PassengerCount;
            return base.UpdateModel(travelDto.Id, travel);
        }

        /// <summary>
        /// Used to Publish / Unpublish the Travel
        /// </summary>
        /// <param name="dto"> Gets a ChangeStatusDto with Id built in. Gets the Travel entity and changes its IsPublished field </param>
        /// <returns> Returns a generic empty response. </returns>
        [HttpPost("changestatus")]
        public ActionResult ChangePublishStatus(ChangeStatusDto dto)
        {
            Travel travel = BaseMongoRepository.GetById(dto.Id);
            if (travel == null) throw new TravelNotFoundException();
            travel.IsPublished = !travel.IsPublished;
            return base.UpdateModel(dto.Id, travel);
        }

        /// <summary>
        /// Updates a recent Travel
        /// </summary>
        /// <param name="updateTravelDto"> Gets the UpdateTravelDto and maps it to a Travel entity object. </param>
        /// <returns> Returns a generic empty response </returns>
        [HttpPut]
        public ActionResult UpdateTravel(UpdateTravelDto updateTravelDto)
        {
            Travel travel = _mapper.Map<Travel>(updateTravelDto);

            return base.UpdateModel(travel.Id, travel);
        }

        /// <summary>
        /// Filters the Travels in the MongoDB to find the best suited one.
        /// Gets a FilterTravelDto to specify the City object used in filter query.
        /// This is the Part2 (and also the filter metot of Part1 in the Task given)
        /// Gets a city and determines if the path of existing Travel includes that City
        /// </summary>
        /// <param name="dto"> 
        /// contains the From and To City objects and IsDirect attribute to filter either only specific Travels 
        /// or a path that the existing Travel includes.
        /// </param>
        /// <returns> Returns a generic response with the Filtered Travels </returns>
        [HttpPost("filter")]
        public ActionResult FilterTravelBy(FilterTravelDto dto)
        {
            if (dto.IsDirect)
            {
                return DirectTravels(dto);
            }
            return WithTransfer(dto);
        }

        /// <summary>
        /// Checks if the PassengerCount is less than or equal to the empty seats in a Travel
        /// </summary>
        /// <param name="travel"> Specific Travel that the user wants to join </param>
        /// <param name="dto"> JoinTravelDto object with the Travel Id and the PassangerCount value </param>
        /// <returns> True of False </returns>
        private bool IsJoinable(Travel travel, JoinTravelDto dto)
        {
            return dto.PassengerCount <= travel.AvailableSeats;
        }

        /// <summary>
        /// Checks if a City is in the path of an existing Travel using X-Y boundiries.
        /// </summary>
        /// <param name="travel"> Existing Travel that the user wants to join </param>
        /// <param name="dto"> FilterTravelDto object with the To/From Cities and the IsDirect attribute </param>
        /// <returns> True or False </returns>
        private bool IsInBetweenRoute(Travel travel, FilterTravelDto dto)
        {
            return travel.To.X >= dto.To.X
                && travel.To.Y >= dto.To.Y
                && travel.From.X <= dto.To.X
                && travel.From.Y <= dto.To.Y
                && travel.To.X >= dto.From.X
                && travel.To.Y >= dto.From.Y;
        }

        /// <summary>
        /// If the FilterTravelDto has a IsDirect = true attribute, then It filters only the specific Cities to find a Travel
        /// It does not take into account the Travels that includes the Cities given. Only the Travels that starts and ends with
        /// the specific Cities given
        /// </summary>
        /// <param name="dto"> FilterTravelDto </param>
        /// <returns> The filtered Travels  </returns>
        private ActionResult DirectTravels(FilterTravelDto dto)
        {
            if (dto.From == null || dto.To == null)
            {
                throw new CityNotValidException();
            }

            List<Travel> travels = new List<Travel>();
            travels = BaseMongoRepository.FilterBy(travel => travel.From.X == dto.From.X && travel.From.Y == dto.From.Y)
                .Where(travel =>  travel.To.X == dto.To.X && travel.To.Y == dto.To.Y).ToList();

            return Ok(travels);
        }

        /// <summary>
        /// If the FilterTravelDto has a IsDirect = false attribute, then It filters all the routes (paths) that
        /// includes the given Cities respectively.
        /// </summary>
        /// <param name="dto"> FilterTravelDto </param>
        /// <returns> The filtered Travels </returns>
        private ActionResult WithTransfer(FilterTravelDto dto)
        {
            if (dto.From == null || dto.To == null)
            {
                throw new CityNotValidException();
            }

            List<Travel> travels = new List<Travel>();
            travels = BaseMongoRepository.FilterBy(travel => travel.From.X <= dto.From.X && travel.From.Y <= dto.From.Y)
                .Where(travel => IsInBetweenRoute(travel, dto)).ToList();

            return Ok(travels);
        }
    }
}
