using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataBase.Models.Visitors
{
    public class ConvertVisitPage//Rename to VisitPage
    {
        public string Hash { get; set; }
        public string Url { get; set; }
    }
}