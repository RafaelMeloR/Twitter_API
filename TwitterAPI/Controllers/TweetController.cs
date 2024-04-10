using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using TwitterAPI.DTOS;
using TwitterAPI.Models;

namespace TwitterAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TweetController : Controller
    {
        private readonly Context _context;
        private readonly IConfiguration _configuration;

        public TweetController(Context context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpGet]
        [ApiVersion("1.0")]
        [Route("Tweets")]
        public ActionResult<TweetDTO> GetTweets()
        {
            var Tweets = _context.Tweet
        .OrderByDescending(tweets => tweets.CreatedAt) // Order by CreatedAt in descending order
        .Select(tweets =>
            new
            {
                tweets.Id,
                tweets.AuthorId,
                tweets.Body,
                tweets.Likes,
                tweets.ImageUrl,
                tweets.Status,
                tweets.CreatedAt,
                tweets.UpdatedAt
            })
        .ToList();

            return Ok(Tweets);
        }

        [HttpPost]
        [ApiVersion("1.0")]
        [Route("postTweet")]
        public ActionResult<Tweet> postTweet([FromBody] TweetDTO tweetDTO)
        {
            if (tweetDTO != null)
            {
                Tweet tweet = new Tweet();
                tweet.AuthorId = tweetDTO.AuthorId;
                tweet.Body = tweetDTO.Body;
                tweet.Likes = tweetDTO.Likes;
                tweet.ImageUrl = tweetDTO.ImageUrl;
                tweet.Status = tweetDTO.Status;
                tweet.CreatedAt = DateTime.Now;
                tweet.UpdatedAt = DateTime.Now;
                _context.Tweet.Add(tweet);
                _context.SaveChanges();
                return Ok(new { message = "Tweet posted" });
            }
            else
            return BadRequest("Tweet not posted");
        }

        [HttpPut]
        [ApiVersion("1.0")]
        [Route("updateTweet/{id}")]
        public ActionResult<Tweet> UpdateTweet(int id, [FromBody] TweetDTO tweetDTO)
        {
            var existingTweet = _context.Tweet.FirstOrDefault(t => t.Id == id);

            if (existingTweet == null)
            {
                return NotFound("Tweet not found");
            }

            existingTweet.Body = tweetDTO.Body;
            existingTweet.Likes = tweetDTO.Likes;
            existingTweet.ImageUrl = tweetDTO.ImageUrl;
            existingTweet.Status = tweetDTO.Status;
            existingTweet.UpdatedAt = DateTime.Now;

            _context.SaveChanges();

            return Ok("Tweet updated successfully");
        }

        [HttpPost]
        [ApiVersion("1.0")]
        [Route("likeTweet/{id}")]
        public ActionResult<Tweet> Like(int id, [FromBody] TweetDTO tweetDTO)
        {
            var existingTweet = _context.Tweet.FirstOrDefault(t => t.Id == id);

            if (existingTweet == null)
            {
                return NotFound("Tweet not found");
            }  
            existingTweet.Likes = existingTweet.Likes+1;  
            _context.SaveChanges();

            return Ok("Tweet updated successfully");
        }

        [HttpDelete]
        [ApiVersion("1.0")]
        [Route("deleteTweet/{id}")]
        public ActionResult DeleteTweet(int id)
        {
            var existingTweet = _context.Tweet.FirstOrDefault(t => t.Id == id);

            if (existingTweet == null)
            {
                return NotFound("Tweet not found");
            }

            _context.Tweet.Remove(existingTweet);
            _context.SaveChanges();

            return Ok("Tweet deleted successfully");
        }



        [HttpGet]
        [ApiVersion("1.0")]
        [Route("Users")]
        public ActionResult<Users> GetUsers()
        {
            var users = _context.Users.Select(user =>
            new
            {
                user.Username,
                user.Name,
                user.Email,
                user.PhoneNo,
                user.Password,
                user.ProfileImage,
                user.Gender,
                user.Status,
                user.CreatedAt,
                user.UpdatedAt,
            }).ToList();
            return Ok(users);
        }

        [HttpGet]
        [ApiVersion("1.0")]
        [Route("UsersByName")]
        public ActionResult<Users> GetUsersByName([FromQuery] string username)
        {
            var users = _context.Users
          .Where(user => user.Username == username)
          .Select(user => new
          {
              user.Username,
              user.Name,
              user.Email,
              user.PhoneNo,
              user.Password,
              user.ProfileImage,
              user.Gender,
              user.Status,
              user.CreatedAt,
              user.UpdatedAt,
          })
          .ToList();

            return Ok(users);
        }
        [HttpPost]
        [ApiVersion("1.0")]
        [Route("createUser")]
        public ActionResult<Users> createUser([FromBody] UsersDTO userDTO)
        {
            if (userDTO != null)
            {
                Users user = new Users();
                user.Username = userDTO.Username;
                user.Name = userDTO.Name;
                user.Email = userDTO.Email;
                user.PhoneNo = userDTO.PhoneNo;
                user.Password = userDTO.Password;
                user.ProfileImage = userDTO.ProfileImage;
                user.Gender = userDTO.Gender;
                user.Status = userDTO.Status;
                user.CreatedAt = DateTime.Now;
                user.UpdatedAt = DateTime.Now;
                _context.Users.Add(user);
                _context.SaveChanges();
                return Ok("Tweet posted");
            }
            return BadRequest("Tweet not posted");
        }
    }

}
