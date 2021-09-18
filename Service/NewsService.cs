using DAL;
using Entities;
using Service.Exceptions;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace Service
{
    //Inherit the respective interface and implement the methods in 
    // this class i.e NewsService by inheriting INewsService
    public class NewsService:INewsService
    {
        /*
        * NewsRepository should  be injected through constructor injection. 
        * Please note that we should not create NewsRepository object using the new keyword
        */
        INewsRepository _newsRepo;
        public NewsService(INewsRepository newsRepository)
        {
            _newsRepo = newsRepository;
        }

        public async Task<News> AddNews(News news)
        {
            bool exist = await _newsRepo.IsNewsExist(news);
            if (!exist)
            {
                return await _newsRepo.AddNews(news);

            }
            else
                throw new NewsAlreadyExistsException($"This news is already added");
        }

        public async Task<List<News>> GetAllNews(string userId)
        {
            List<News> newsList = await _newsRepo.GetAllNews(userId);
            if (newsList.Count != 0)
                return newsList;
            else
                throw new NewsNotFoundException($"No news found for user: {userId}");
        }

        public async Task<News> GetNewsById(int newsId)
        {
            News news = await _newsRepo.GetNewsById(newsId);
            if (news != null)
                return news;
            else
                throw new NewsNotFoundException($"No news found with Id: {newsId}");
        }

        public async Task<bool> RemoveNews(int newsId)
        {
            News news = await _newsRepo.GetNewsById(newsId);
            if (news != null)
                return await _newsRepo.RemoveNews(news);
            else
                throw new NewsNotFoundException($"No news found with Id: {newsId}");
        }
        /* Implement all the methods of respective interface asynchronously*/

        /* Implement AddNews method to add the new news details*/
        /* Implement GetAllNews method to get the news details of perticular userid*/
        /* Implement GetNewsById method to get the existing news by news id*/
        /* Implement RemoveNews method to remove the existing news*/

        // Throw your own custom Exception whereever its required in AddNews,GetAllNews,GetNewsById and RemoveNews 
        // functionalities
    }
}
