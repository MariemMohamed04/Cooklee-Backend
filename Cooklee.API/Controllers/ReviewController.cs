using AutoMapper;
using Cooklee.API.Errors;
using Cooklee.Core.DTOs;
using Cooklee.Data.Entities;
using Cooklee.Data.Repository.Contract;
using CookLeeProject.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cooklee.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IMapper _mapper;


        public ReviewController(IReviewRepository reviewRepository,IMapper mapper)
        {
            _reviewRepository = reviewRepository;
           _mapper = mapper;
        }
        [HttpPost]
        public async Task<IActionResult> PostReview([FromBody] ReviewDto reviewDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var review = new Review
            {
                Comment = reviewDto.Comment,
                Rate = reviewDto.Rate,
                ClientId = reviewDto.ClientId,
                MealId = reviewDto.MealId,
            };

            await _reviewRepository.AddAsync(review);
            if (await _reviewRepository.SaveChanges()>0)
            {
                return CreatedAtAction(nameof(GetReview), new { id = review.Id }, review);
            }
            else
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }
        }
        //[HttpPost]
        //public async Task<ActionResult<ReviewDto>> CreateReview(ReviewDto reviewDto)
        //{
        //    var review = _mapper.Map<Review>(reviewDto);
        //    await _reviewRepository.AddAsync(review);
        //    return CreatedAtAction(nameof(GetReview), new { id = review.Id }, new ApiResponse(201, "Review created successfully."));
        //}



        [HttpGet("reviews")]
        public async Task<ActionResult<IEnumerable<ReviewDto>>> GetAllReviews()
        {
            var reviews = await _reviewRepository.GetAllReviewsAsync();
            var reviewsDto = _mapper.Map<IEnumerable<ReviewDto>>(reviews);
            return Ok(reviewsDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReviewDto>> GetReview(int id)
        {
            var review = await _reviewRepository.GetAsync(id);
            if (review == null)
            {
                return NotFound(new ApiResponse(404));
            }

            var reviewDto = _mapper.Map<ReviewDto>(review);
            return reviewDto;
        }



        //[HttpPut("{id}")]
        //public async Task<IActionResult> UpdateReview(int id, ReviewDto reviewDto)
        //{
        //    if (id != reviewDto.Id)
        //    {
        //        return BadRequest(new ApiResponse(400, "Review ID mismatch."));
        //    }

        //    var existingReview = await _reviewRepository.GetAsync(id);
        //    if (existingReview == null)
        //    {
        //        return NotFound(new ApiResponse(404, "Review not found."));
        //    }

        //    var updatedReview = _mapper.Map(reviewDto, existingReview);
        //    await _reviewRepository.UpdateAsync(id, updatedReview);
        //    return Ok(new ApiResponse(200, "Review updated successfully."));
        //}


        [HttpDelete("{id}/reviews")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            var result = await _reviewRepository.DeleteAsync(id);
            if (!result)
            {
                return NotFound(new ApiResponse(404, "Review not found."));
            }

            return NoContent();
        }
    }
}
