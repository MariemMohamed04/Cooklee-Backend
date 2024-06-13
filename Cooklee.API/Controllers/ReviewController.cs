using AutoMapper;
using Cooklee.Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cooklee.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        #region field
        private readonly IMapper _mapper;
        #endregion

        public ReviewController(IMapper mapper)
        {
            _mapper = mapper;
        }

    }
}
