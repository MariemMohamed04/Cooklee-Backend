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

namespace Cooklee.API.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminChefController : BaseApiController
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;
        private readonly IEmailSetting _emailSetting;

        public AdminChefController(IUnitOfWork unitOfWork, IMapper mapper , IEmailSetting emailSetting)

        {
            _unit = unitOfWork;
            _mapper = mapper;
            _emailSetting = emailSetting;
        }

        // get all chefs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ChefPageDto>>> GetAllChefPages()
        {

            var allChefPages = await _unit.ChefPageRepo.GetAllAsync();
            List< ChefPageDto> allChefPagesDto = new List<ChefPageDto>();

            foreach (var page in allChefPages)
            {
                var pageDto = _mapper.Map<ChefPage, ChefPageDto>(page);
                allChefPagesDto.Add(pageDto);
            }

            return  Ok(allChefPagesDto);
        }


        //get unActive chefs
        [HttpGet("UnActivePages")]
        public async Task<ActionResult<IEnumerable<ChefPageDto>>> GetUnActivePages()
        {
            var unActivePages = await _unit.ChefPageRepo.GetUnActiveChefPages();
            List<ChefPageDto> unActivePagesDto = new List<ChefPageDto>();

            foreach (var page in unActivePages)
            {
                var pageDto = _mapper.Map<ChefPage, ChefPageDto>(page);
                unActivePagesDto.Add(pageDto);
            }

            return Ok(unActivePagesDto);
        }


        [HttpPut("ActivatePage")]
        public async Task<ActionResult<bool>> ActivatePage( int chefId)
        {
            var result = await _unit.ChefPageRepo.ActivatePage(chefId);
            var client = await _unit.ClientProfileRepo.GetClientBychefAsync(chefId);          
            if (result == true)
            {
                Email email = new Email()
                {
                    To= client.Email,
                    Subject= "Page Accepted",
                    Body = $"Dear {client.FirstName}, Your Chef Page request has been Activated.",
                };
                try
                {
                    _emailSetting.SendEmailAsync(email);
                    return Ok(new { status = result, message = " chefPage has been Activated  please check your email" });
                }
                catch (Exception)
                {
                    return Ok(false);
                }
            }
            else 
                return BadRequest(new ApiResponse(400));
        }


        [HttpPost("SendFeedback")]
        public async Task<ActionResult<bool>> SendFeedback(int chefId, ChefFeedbackDto chefFeedbackDto)
        {
            var result = await _unit.ChefPageRepo.SendFeedback(chefId);
            var chef = await _unit.ChefPageRepo.GetAsync(chefId);
            if (chef == null)
            {
                return NotFound(new ApiResponse(404));
            }
            if (result == true)
            {
                var email = new Email
                {
                    To = chef.Email,
                    Subject = "Invalid Chef Details",
                    Body = chefFeedbackDto.Body
                };

                try
                {
                    _emailSetting.SendEmailAsync(email);
                        return Ok(new { status = result, message = " failed to activate your Page   please check your email for deails" });
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
                return  BadRequest(new { status = result, message = " failed to send feedback email" });
        }





    }
}
