using AutoMapper;
using Cooklee.API.Errors;
using Cooklee.Core.DTOs;
using Cooklee.Core.Helpers;
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
    public class ClientProfileController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly RoleManager<IdentityRole> _roleManager;

        public ClientProfileController(IUnitOfWork unitOfWork, UserManager<AppUser> userManager, IMapper mapper, RoleManager<IdentityRole> roleManager
)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _mapper = mapper;
            _roleManager = roleManager;
        }
        //add profile

        [HttpGet("ProfileSettings")]
        [Produces("application/json")]
        public async Task<ActionResult<ClientProfileDto>> ProfileSettings(string userId)
        {
            // Fetch the user
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return Unauthorized(new ApiResponse(401, "User not found"));
            }

            // Check user roles
            var roles = await _userManager.GetRolesAsync(user);
            if (roles.Count == 0)
            {
                // Ensure 'Client' role exists and assign it if it doesn't
                var checkRole = await _roleManager.RoleExistsAsync("Client");
                if (!checkRole)
                {
                    IdentityRole clientRole = new IdentityRole("Client");
                    await _roleManager.CreateAsync(clientRole);
                }

                // Assign 'Client' role to user
                await _userManager.AddToRoleAsync(user, "Client");
            }

            // Retrieve client profile
            Client clientFound = await _unitOfWork.ClientProfileRepo.GetProfileAsync(userId);
            if (clientFound != null)
            {
                var clientFoundDto = _mapper.Map<Client, ClientProfileDto>(clientFound);
                return Ok(clientFoundDto);
            }

            var FullName = user.UserName.Split(".");

            Client client = new Client
            {
                FirstName = FullName[0],
                LastName = FullName[1] == null ? "" : FullName[1],
                Email = user.Email,
                AppUserId = user.Id
            };

            // Check if the client already exists to avoid tracking conflicts
            var existingClient = await _unitOfWork.ClientProfileRepo.GetProfileAsync(userId);
            if (existingClient != null)
            {
                return BadRequest(new ApiResponse(400, "Client already exists."));
            }

            // Add the new client profile
            await _unitOfWork.ClientProfileRepo.AddAsync(client);
            int succeed = await _unitOfWork.ClientProfileRepo.SaveChanges();

            if (succeed < 1)
            {
                return BadRequest(new ApiResponse(400, "Failed to save client settings."));
            }

            var clientDto = _mapper.Map<Client, ClientProfileDto>(client);
            return Ok(clientDto);
        }




        [HttpPost("UpdateProfile")]
        public async Task<ActionResult<ClientProfileDto>> UpdateProfile(string userId, ClientProfileDto profileDto)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return Unauthorized(new ApiResponse(401, "User not found"));
            }


            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse(400));
            }

            var client  = _mapper.Map<ClientProfileDto,Client>(profileDto);


            var updatedClient  =  await _unitOfWork.ClientProfileRepo.UpdateProfileAsync(user.Id,client);
            int succeed = await _unitOfWork.ClientProfileRepo.SaveChanges();

            if (succeed < 1)
            {
                return BadRequest(new ApiResponse(400, "Failed to Update client profile"));
            }


            var updatedClientDto = _mapper.Map< Client, ClientProfileDto>(updatedClient);

            return Ok(updatedClientDto);

        }



        //get prpfile


        [HttpGet("GetProfile")]
        [Produces("application/json")]
        public async Task<ActionResult<ClientProfileDto>> GetProfile(string userId)
        {
          
            Client client = await _unitOfWork.ClientProfileRepo.GetProfileAsync(userId);

            if (client == null)
            {
                return NotFound(new ApiResponse(400, "client not found"));
            }

            var clientDto = _mapper.Map<Client, ClientProfileDto>(client);


            return Ok(clientDto);
        }



    }
}
