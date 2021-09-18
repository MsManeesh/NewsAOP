using Entities;
using Microsoft.AspNetCore.Mvc;
using NewsAPI.Aspect;
using Service;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace NewsAPI.Controllers
{
    /*
    * As in this assignment, we are working with creating RESTful web service, hence annotate
    * the class with [ApiController] annotation and define the controller level route as per REST Api standard.
    * and also use ServiceFilter to handle the exception logic using ExceptionHandler
    */
    [Route("api/[controller]")]
    [ApiController]
    [ServiceFilter(typeof(ExceptionHandler))]
    public class NewsController : ControllerBase
    {
        /*
        * NewsService should  be injected through constructor injection. 
        * Please note that we should not create service object using the new keyword
        */
        INewsService _newsService;
        public NewsController(INewsService newsService)
        {
            _newsService = newsService;
        }
        /* Implement HttpVerbs and Functionalities asynchronously*/
        /*
         * Define a handler method which will get us the news by a userId.
         * 
         * This handler method should return any one of the status messages basis on
         * different situations: 
         * 1. 200(OK) - If the news found successfully.
         * This handler method should map to the URL "/api/news/{userId}" using HTTP GET method
         */
        [HttpGet]
        [Route("{userId}")]
        public async Task<IActionResult> Get(string userId)
        {
            List<News> newsList = await _newsService.GetAllNews(userId);
            return Ok(newsList);
            
        }

        /*
        * Define a handler method which will get us the news by a newsId.
        * 
        * This handler method should return any one of the status messages basis on
        * different situations: 
        * 1. 200(OK) - If the news found successfully.
        * This handler method should map to the URL "/api/news/{newsId:int}" using HTTP GET method
        */
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> Get(int Id)
        {
            News news = await _newsService.GetNewsById(Id);
            return Ok(news);
            
        }
        /*
         * Define a handler method which will create a specific news by reading the
         * Serialized object from request body and save the news details in a News table
         * in the database.
         * 
         * Please note that AddNews method should add a news and also handle the exception using 
         * ExceptionHandler.This handler method should return any one of the status messages 
         * basis on different situations: 
         * 1. 201(CREATED) - If the news created successfully. 
         * 2. 409(CONFLICT) - If the newsId conflicts with any existing newsid
         * 
         * This handler method should map to the URL "/api/news" using HTTP POST method
         */
        [HttpPost]
        public async Task<IActionResult> Post(News news)
        {
            News output = await _newsService.AddNews(news);
            return Created("", output);
        }
        /*
         * Define a handler method which will delete a news from a database.
         * 
         * This handler method should return any one of the status messages basis on
         * different situations: 
         * 1. 200(OK) - If the news deleted successfully from database. 
         * 2. 404(NOT FOUND) - If the news with specified newsId is not found.
         * 
         * This handler method should map to the URL "/api/news/{id}" using HTTP Delete
         * method" where "id" should be replaced by a valid newsId without {}
         */
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete(int Id)
        {
            bool flag = await _newsService.RemoveNews(Id);
            return Ok(flag);
        }
    }
}
