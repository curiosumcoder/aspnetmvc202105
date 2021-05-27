using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Northwind.Store.UI.Web.Internet.Filters;

[assembly: WebActivatorEx.PostApplicationStartMethod(typeof(Northwind.Store.UI.Web.Internet.ErrorHandlerStartup), "Start")]
namespace Northwind.Store.UI.Web.Internet
{
	public static class ErrorHandlerStartup
	{
	    public static void Start()
	    {
	        var existingErrorFilter = GlobalFilters.Filters.FirstOrDefault(x => x.Instance is HandleErrorAttribute);
	        
            if (existingErrorFilter != null)
            {
                GlobalFilters.Filters.Remove(existingErrorFilter.Instance);
            }

	        GlobalFilters.Filters.Add(new CustomErrorFilter(), 10);
	    }
	}
}