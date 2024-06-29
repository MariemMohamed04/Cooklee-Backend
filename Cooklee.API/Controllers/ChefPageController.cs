using AutoMapper;
using Cooklee.API.Errors;
using Cooklee.Core.DTOs;
using Cooklee.Data.Entities;
using Cooklee.Data.Entities.Identity;
using Cooklee.Data.Repository.Contract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Cooklee.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChefPageController : BaseApiController
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly RoleManager<IdentityRole> _roleManager;

        public ChefPageController(IUnitOfWork unitOfWork, UserManager<AppUser> userManager, IMapper mapper, RoleManager<IdentityRole> roleManager
)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _mapper = mapper;
            _roleManager = roleManager;
        }

        [HttpGet("PageSettings")]
        [Produces("application/json")]
        public async Task<ActionResult<ChefPageDto>> PageSettings(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return Unauthorized(new ApiResponse(401, "User not found"));
            }


            ChefPage PageFound = await _unitOfWork.ChefPageRepo.GetPageByUser(userId);
            if (PageFound != null)
            {

                var pageFoundDro = _mapper.Map<ChefPage, ChefPageDto>(PageFound);
                return Ok(pageFoundDro);

            }

           Client client = await _unitOfWork.ClientProfileRepo.GetProfileAsync(userId);

            if (client == null)
            {
                return BadRequest(new ApiResponse(401));

            }

            ChefPage chefPage = new ChefPage
            {
                DisplayName = client.FirstName + " "+ client.LastName,
                Email= client.Email,
                ClientId= client.Id,
                PhoneNumber= client.PhoneNumber,
            };

            //check by user id if exit befor add
            await _unitOfWork.ChefPageRepo.AddAsync(chefPage);
            int succeed = await _unitOfWork.ClientProfileRepo.SaveChanges();

            if (succeed < 1)
            {
                return BadRequest(new ApiResponse(400, "Failed to get page setting"));
            }


            var roles = await _userManager.GetRolesAsync(user);
            if (roles.Count == 1)
            {

                var checkRole = await _roleManager.RoleExistsAsync("Chef");

                if (!checkRole)
                {
                    IdentityRole ClentRole = new IdentityRole("Chef");
                    await _roleManager.CreateAsync(ClentRole);

                }
                await _userManager.AddToRoleAsync(user, "Chef");
            }

            //role to user chef


            var pageDto = _mapper.Map<ChefPage, ChefPageDto>(chefPage);
            return Ok(pageDto);
        }


        [HttpPost("UpdatePage")]
        public async Task<ActionResult<ClientProfileDto>> UpdatePage(string userId, ChefPageDto ChefPageDto)
        {
            var user = await _userManager.FindByIdAsync(userId);

            Client clientFound = await _unitOfWork.ClientProfileRepo.GetProfileAsync(userId);

            if (clientFound == null || !ModelState.IsValid)
            {
                return BadRequest(new ApiResponse(400));
            }


            var ChefPage = _mapper.Map<ChefPageDto, ChefPage>(ChefPageDto);


            var updatedPage = await _unitOfWork.ChefPageRepo.UpdatePageAsync(clientFound.Id,ChefPage);
            int succeed = await _unitOfWork.ClientProfileRepo.SaveChanges();

            if (succeed < 1)
            {
                return BadRequest(new ApiResponse(400, "Failed to Update chef page"));
            }


            var uptatedPageDto = _mapper.Map<ChefPage, ChefPageDto>(ChefPage);

            return Ok(uptatedPageDto);

        }



        [HttpGet("GetPage")]
        [Produces("application/json")]
        public async Task<ActionResult<ChefPageDto>> GetPage(string userId)
        {

            var page = await _unitOfWork.ChefPageRepo.GetPageByUser(userId);

            if (page == null)
            {
                return NotFound(new ApiResponse(400, "page not found"));
            }

            var pageDto = _mapper.Map<ChefPage, ChefPageDto>(page);


            return Ok(pageDto);
        }

        
    }
}
