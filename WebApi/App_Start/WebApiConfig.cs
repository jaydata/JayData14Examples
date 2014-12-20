using Microsoft.OData.Edm;
using Microsoft.OData.Edm.Library;
using Microsoft.OData.Edm.Library.Values;
using ProductApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.OData.Builder;
using System.Web.OData.Extensions;
using WebApi.Controllers.Examples;
using WebApi.Models;

namespace WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            //config.MapHttpAttributeRoutes();

            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            ///OData routes
            ///
            ODataModelBuilder builder = new ODataConventionModelBuilder();
            
            builder.EntitySet<Product>("Products");
            builder.EntitySet<Category>("Categories");
            builder.Namespace = "ProductApi";
            var model = builder.GetEdmModel();
            
            
            //var pschema = model.SchemaElements.First(s => s.Name == "Product");
            //var ptype = pschema as IEdmEntityType;
            var ptype = model.FindType("ProductApi.Product") as IEdmEntityType;
            var value = new EdmStringConstant(EdmCoreModel.Instance.GetString(true), "data");
            var m = model.DirectValueAnnotationsManager;
            m.SetAnnotationValue(ptype.FindProperty("Name"), "jay", "jay", value);
            
            //m.SetAnnotationValue()
            //model.DirectValueAnnotationsManager.SetAnnotationValue()

            config.MapODataServiceRoute(
                routeName: "odata",
                routePrefix: "odata",
                model: model);
            RegisterExampleApi(config);
        }

        private static void RegisterExampleApi(HttpConfiguration config)
        {
            ODataModelBuilder builder = new ODataConventionModelBuilder();
            builder.EntitySet<Item>("Items");
            config.MapODataServiceRoute(
                routeName: "example",
                routePrefix: "examples",
                model: builder.GetEdmModel());
            
        }
    }
}
