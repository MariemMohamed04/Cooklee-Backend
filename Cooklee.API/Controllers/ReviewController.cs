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

        public ReviewController(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        [HttpGet("reviews")]
        public async Task<ActionResult<IEnumerable<Review>>> GetAllReviews()
        {
            var reviews = await _reviewRepository.GetAllReviewsAsync();
            return Ok(reviews);
        }

        [HttpGet("{id}/reviews")]
        public async Task<ActionResult<Review>> GetReview(int id)
        {
            var review = await _reviewRepository.GetAsync(id);
            if (review == null)
            {
                return NotFound();
            }
            return Ok(review);
        }

        [HttpPost]
        public async Task<ActionResult<Review>> CreateReview(Review review)
        {
            _reviewRepository.AddAsync(review);
            _reviewRepository.SaveChanges();
            return CreatedAtAction(nameof(GetReview), new { id = review.Id }, new ApiResponse(201, "Review created successfully."));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReview(int id, Review review)
        {
            if (id != review.Id)
            {
                return BadRequest(new ApiResponse(400, "Review ID mismatch."));
            }

            var existingReview = await _reviewRepository.GetAsync(id);
            if (existingReview == null)
            {
                return NotFound(new ApiResponse(404, "Review not found."));
            }

            var updatedReview = await _reviewRepository.UpdateAsync(id, review);
            return Ok(new ApiResponse(200, "Review updated successfully."));
        }


        [HttpDelete("{id}/reviews")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            var result = await _reviewRepository.DeleteAsync(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
