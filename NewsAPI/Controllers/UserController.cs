﻿using Entities;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using NewsAPI.Aspect;
using Service;
using System;
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
    public class UserController : ControllerBase
    {
        /*
        * UserService should  be injected through constructor injection. 
        * Please note that we should not create userservice object using the new keyword
        */
        IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        /* Implement HttpVerbs and Functionalities asynchronously*/

        /*
         * Define a handler method which will get us the user by a userId.
         * 
         * This handler method should return any one of the status messages basis on
         * different situations: 
         * 1. 200(OK) - If the news found successfully.
         * This handler method should map to the URL "/api/user/{userId}" using HTTP GET method
         */
        [HttpGet]
        [Route("{userId}")]
        public async Task<IActionResult> Get(string userId)
        {
            return Ok(await _userService.GetUser(userId));
             
        }
        /*
        * Define a handler method which will create a specific UserProfile by reading the
        * Serialized object from request body and save the user details in a User table
        * in the database.
        * 
        * Please note that AddUser method should add a userdetails and also handle the exception using 
        * ExceptionHandler.This handler method should return any one of the status messages 
        * basis on different situations: 
        * 1. 201(CREATED) - If the userProfile details created successfully. 
        * 2. 409(CONFLICT) - If the userId conflicts with any existing userId
        * 
        * This handler method should map to the URL "/api/user" using HTTP POST method
        */
        [HttpPost]
        public async Task<IActionResult> Post(UserProfile user)
        {
             bool flag = await _userService.AddUser(user);
             return Created("", flag);
        }
        /*
        * Define a handler method which will update a specific user by reading the
        * Serialized object from request body and save the updated user details in a
        * user table in database handle exception as well.
        * This handler method should return any one of the status messages basis on different situations: 
        * 1. 200(OK) - If the user updated successfully. 
        * 2. 404(NOT FOUND) - If the user with specified userId is not found. 
        * 
        * This handler method should map to the URL "/api/user/{id}" using HTTP PUT method.
        */
        [HttpPut]
        [Route("{userId}")]
        public async Task<IActionResult> Put(string userId, UserProfile user)
        {
            bool flag = await _userService.UpdateUser(userId, user);
            return Ok(flag);
        }
        /*
         * Define a handler method which will delete a specified UserProfile details from a database.
         * This handler method should return any one of the status messages basis on
         * different situations: 
         * 1. 200(OK) - If the userProfile deleted successfully from database. 
         * 2. 404(NOT FOUND) - If the userProfile details with specified userId is not found.
         * This handler method should map to the URL "/api/user/{id}" using HTTP Delete
         * method" where "id" should be replaced by a valid userId without {}
         */
        [HttpDelete]
        [Route("{userId}")]
        public async Task<IActionResult> Delete(string userId)
        {
            bool flag = await _userService.DeleteUser(userId);
            return Ok(flag);
        }
     }
}
