using AdessoRideShare.Api.Common.Responses;
using AdessoRideShare.Data.Access.Base;
using AdessoRideShare.Data.Model.Base;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdessoRideShare.Controllers
{
    /// <summary>
    /// MongoDB controller
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    public abstract class BaseMongoController<TModel> : ControllerBase
         where TModel : BaseEntity
    {
        public BaseMongoRepository<TModel> BaseMongoRepository { get; set; }

        public BaseMongoController(BaseMongoRepository<TModel> baseMongoRepository)
        {
            this.BaseMongoRepository = baseMongoRepository;
        }

        protected virtual ActionResult GetModel(string id)
        {
            BaseResponse<TModel> response = new BaseResponse<TModel>();
            response.Data = this.BaseMongoRepository.GetById(id);
            return Ok(response);
        }

        protected virtual ActionResult GetModelList()
        {
            BaseResponse<IEnumerable<TModel>> response = new BaseResponse<IEnumerable<TModel>>();
            response.Data = this.BaseMongoRepository.GetList();
            return Ok(response);
        }

        protected virtual ActionResult AddModel(TModel model)
        {
            BaseResponse<TModel> response = new BaseResponse<TModel>();
            response.Data = this.BaseMongoRepository.Create(model);
            return Ok(response);
        }

        protected virtual ActionResult UpdateModel(string id, TModel model)
        {
            this.BaseMongoRepository.Update(id, model);
            return Ok(new BaseResponse<int?>());
        }

        protected virtual void DeleteModel(string id)
        {
            this.BaseMongoRepository.Delete(id);
        }

    }
}
