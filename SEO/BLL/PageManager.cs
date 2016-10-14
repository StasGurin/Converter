using DataBase.DAL;
using DataBase.Models.Projects;
using MongoDB.Bson;
using MongoDB.Driver;
using SEO.Models;
using System.Collections.Generic;

namespace SEO.BLL
{
    public class PageManager
    {
        private static readonly DatabaseContext dataBase = new DatabaseContext();

        public static List<Project> Projects = new List<Project>();

        static PageManager()
        {
            var filter = new BsonDocument();
            var result = dataBase.Projects.Find(filter).ToListAsync().Result;

            Projects.AddRange(result);
        }

        public static PageRenderingInfo ResponsePage(string url)
        {
            foreach (var project in Projects)
            {
                foreach (var page in project.Pages)
                {
                    var urlSEOPage = project.DomainName + "/" + page.Url;

                    if ((url.Replace("http://", "") == urlSEOPage) || (url.Replace("https://", "") == urlSEOPage))
                        return new PageRenderingInfo(page.Title, page.Body, page.Keywords);

                }
            }
            return new PageRenderingInfo(null, null, null);
        }


    }
}