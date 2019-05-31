//Copyright (c) 2018 Yardi Technology Limited. Http://www.kooboo.com 
//All rights reserved.
using System;
using System.Collections.Generic;
using Kooboo.Data.Models;
using Kooboo.Data.Template;
using Kooboo.Data;
using Kooboo.Sites.Extensions;

namespace Kooboo.Web.ViewModel
{
    public class TemplateItemViewModel  
    { 
        public Guid Id  {  get;set;   }
  
        public string UserName { get; set; }
          
        public string ThumbNail { get; set; }
  
        public string Name { get; set; }
  
        public string Category { get; set; }
          
        public int Size { get; set; }

        public string Description { get; set; }

        public int Score { get; set; } = 100;

        public string Tags { get; set; }

        public DateTime LastModified { get; set; }
        
        public long DownloadCount { get; set; }

        public int PageCount { get; set; }

        public int ContentCount { get; set; }

        public int ImageCount { get; set; }

        public int LayoutCount { get; set; }

        public int MenuCount { get; set; }

        public int ViewCount { get; set; }

        public decimal Price { get; set; }
        
        public CurrencyViewModel Currency { get; set; }

        public string PreviewUrl { get; set; }

        public string DownloadUrl { get; set; }

        public int Status { get; set; }

        public string Suggest { get; set; }

        public TemplateItemViewModel()
        {

        }

        public TemplateItemViewModel(TemplatePackage template)
        {
            this.Id = template.Id;
            this.Name = template.Name;
            this.Description = template.Description;
            this.UserName = template.UserName;
            this.Size = template.Size;
            this.Score = template.Score;
            this.LastModified = template.LastModified;
            this.ContentCount = template.ContentCount;
            this.ImageCount = template.ImageCount;
            this.LayoutCount = template.LayoutCount;
            this.MenuCount = template.MenuCount;
            this.PageCount = template.PageCount;
            this.ViewCount = template.ViewCount;
            this.Status = template.Status;
            this.Suggest = template.Suggest;
            this.ThumbNail = string.Format("/_api/download/themeimg/{0}", template.ThumbNail);

            this.Price = template.Price;
            this.Currency = new CurrencyViewModel() { Code = template.Currency };

            if (string.IsNullOrEmpty(this.UserName) && template.UserId != default(Guid))
            {
                var user = Kooboo.Data.GlobalDb.Users.Get(template.UserId);
                if (user != null)
                {
                    this.UserName = user.FullName;
                }
            }
            //var site = GlobalDb.WebSites.Get(template.SiteId);
            //string baseurl = site.BaseUrl();
            //this.PreviewUrl = Lib.Helper.UrlHelper.Combine(baseurl, site.GetStartRelativeUrl());

            this.DownloadCount = template.DownloadCount;
            this.DownloadUrl = "/_api/download/package?id=" + template.Id;
        }

    }

    public class TemplateDetailViewModel 
    { 
        public Guid Id     {get;set;  }
 
        public Guid UserId { get; set; }

        public string UserName { get; set; }
           
        public List<string> Images { get; set; } = new List<string>();

        public string Name { get; set; }

        public string Link { get; set; }

        public string Category { get; set; }
          
        public int Size { get; set; }

        public string Description { get; set; }

        public int Score { get; set; } = 100;

        public string Tags { get; set; }

        public DateTime LastModified { get; set; }

        public string PreviewUrl  {  get; set; }

        public string DownloadUrl { get; set; }

        public string DownloadCode { get; set; }

        public long DownloadCount { get; set; }

        public bool IsPrivate { get; set; }


        public int PageCount { get; set; }

        public int ContentCount { get; set; }

        public int ImageCount { get; set; }

        public int LayoutCount { get; set; }

        public int MenuCount { get; set; }

        public int ViewCount { get; set; }


        public string ThumbNail { get; set; }

        public decimal Price { get; set; }

        public CurrencyViewModel Currency { get; set; }

        public int Status { get; set; }

        public TemplateDetailViewModel()
        {

        }

        public TemplateDetailViewModel(TemplatePackage package)
        {
            this.Id = package.Id;
            this.Link = package.Link;
            this.Name = package.Name;
            this.Score = package.Score;
            this.Size = package.Size;
            this.UserId = package.UserId;
            this.UserName = package.UserName;
            this.Description = package.Description;
            this.LastModified = package.LastModified;
            this.Tags = package.Tags;

            this.IsPrivate = package.OrganizationId != default(Guid);

            this.Price = package.Price;
            this.Currency = new CurrencyViewModel() { Code = package.Currency };
            if (string.IsNullOrEmpty(this.UserName) && package.UserId != default(Guid))
            {
                var user = GlobalDb.Users.Get(package.UserId);
                if (user != null)
                {
                    this.UserName = user.FullName;
                }
            }
            
            var site = GlobalDb.WebSites.Get(package.SiteId);
            string baseurl = site.BaseUrl();

            this.PreviewUrl = Lib.Helper.UrlHelper.Combine(baseurl, site.GetStartRelativeUrl());

            this.DownloadCode = package.Id.ToString();
            this.DownloadUrl = "/_api/download/package?id=" + package.Id;

            this.DownloadCount = package.DownloadCount;
            //this.DownloadCount = Service.DownloadCounterService.GetCounter(package.Id);

            this.ContentCount = package.ContentCount;
            this.ImageCount = package.ImageCount;
            this.LayoutCount = package.LayoutCount;
            this.MenuCount = package.MenuCount;
            this.PageCount = package.PageCount;
            this.ViewCount = package.ViewCount;
            this.ThumbNail = package.ThumbNail;
            this.Images = package.Images;

        }

    }


    public class TemplateSearchContext
    {
        public string Keyword { get; set; }

        public List<string> Tags { get; set; }

        public TemplateSortType Sort { get; set; }
    }

    public enum TemplateSortType
    {
        Default,
        Download
    }

}
