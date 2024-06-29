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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IEmailSetting _emailSetting;

        public AdminChefController(IUnitOfWork unitOfWork, IMapper mapper , IEmailSetting emailSetting)

        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _emailSetting = emailSetting;
        }


        // get all chefs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ChefPageDto>>> GetAllChefPages()
        {

            var allChefPages = await _unitOfWork.ChefPageRepo.GetAllAsync();
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
            var unActivePages = await _unitOfWork.ChefPageRepo.GetUnActiveChefPages();
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

            var result = await _unitOfWork.ChefPageRepo.ActivatePage(chefId);
            var client = await _unitOfWork.ClientProfileRepo.GetClientBychefAsync(chefId);
            


            if (result == true)
            {
                Email email = new Email()
                {
                    To= client.Email,
                    Subject= "Page Accepted",
                    Body = $"Dear {client.FirstName}, Your Chef Page request has been Accepted.",


                };

                try
                {
                    _emailSetting.SendEmailAsync(email);


                    return Ok(true);
                }
                catch (Exception)
                {

                    return Ok(false);
                }
            }

            else 

                return BadRequest(new ApiResponse(400));
        
        
        }








        }



}
