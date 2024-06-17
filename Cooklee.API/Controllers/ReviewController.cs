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
        private readonly IClientProfileRepo _clientProfileRepository;
        private readonly IMapper _mapper;


        public ReviewController(IReviewRepository reviewRepository,IMapper mapper,IClientProfileRepo clientProfileRepo)
        {
            _reviewRepository = reviewRepository;
            _clientProfileRepository = clientProfileRepo;
           _mapper = mapper;
        }
        [HttpGet("/api/Review/{mealId}")]
        public async Task<ActionResult<IEnumerable<ReviewDto>>> GetReviewsByMealId(int mealId)
        {
            var reviews = await _reviewRepository.GetReviewsByMealIdAsync(mealId);
            if (reviews == null || !reviews.Any())
            {
                return NotFound(new ApiResponse(404, "No reviews found for this meal."));
            }

            var reviewsDto = _mapper.Map<IEnumerable<ReviewDto>>(reviews);
            return Ok(reviewsDto);
        }
        [HttpPost("/api/Review")]
        public async Task<IActionResult> AddReview([FromBody] ReviewDto reviewDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Validate that the client exists
                var client = await _clientProfileRepository.GetAsync(reviewDto.ClientId);
                if (client == null)
                {
                    return BadRequest("Invalid client specified for the review.");
                }

                var review = new Review
                {
                    Comment = reviewDto.Comment,
                    Rate = reviewDto.Rate,
                    ClientId = reviewDto.ClientId,
                    MealId = reviewDto.MealId
                };

                await _reviewRepository.AddAsync(review);

                if (await _reviewRepository.SaveChanges() > 0)
                {
                    var mappedReviewDto = _mapper.Map<ReviewDto>(review);
                    return CreatedAtAction(nameof(GetReviewById), new { id = review.Id }, mappedReviewDto);
                }
                else
                {
                    return StatusCode(500, "Failed to add review. Please try again later.");
                }
            }
            catch (Exception ex)
            {
                // Log the exception including inner exceptions
                Console.WriteLine(ex.ToString());

                // Return an error response to the client
                return StatusCode(500, "An error occurred while saving the entity changes. Please try again later.");
            }
        }


        [HttpGet("/api/Reviews/{id}", Name = "GetReviewById")]
        public async Task<ActionResult<ReviewDto>> GetReviewById(int id)
        {
            var review = await _reviewRepository.GetAsync(id);
            if (review == null)
            {
                return NotFound(new ApiResponse(404, "Review not found."));
            }

            var reviewDto = _mapper.Map<ReviewDto>(review);
            return reviewDto;
        }

        [HttpGet("reviews")]
        public async Task<ActionResult<IEnumerable<ReviewDto>>> GetAllReviews()
        {
            var reviews = await _reviewRepository.GetAllReviewsAsync();
            var reviewsDto = _mapper.Map<IEnumerable<ReviewDto>>(reviews);
            return Ok(reviewsDto);
        }

        //[HttpDelete("{id}/reviews")]
        //public async Task<IActionResult> DeleteReview(int id)
        //{
        //    var result = await _reviewRepository.DeleteAsync(id);
        //    if (!result)
        //    {
        //        return NotFound(new ApiResponse(404, "Review not found."));
        //    }

        //    return NoContent();
        //}
    }
}
