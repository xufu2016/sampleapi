using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Metadata;
using System.Web.Http.ModelBinding;
using System.Web.Http.ValueProviders;

namespace HelloApi.Models
{
    public class ProductParameterBinder : HttpParameterBinding
    {
        public ProductParameterBinder(HttpParameterDescriptor descriptor) : base(descriptor)
        {
        }

        public override Task ExecuteBindingAsync(ModelMetadataProvider metadataProvider, HttpActionContext actionContext,
            CancellationToken cancellationToken)
        {
            var productRawString = actionContext.Request.Headers.GetValues(Descriptor.ParameterName).FirstOrDefault();
            Product product;
            if(Product.TryParse(productRawString, out product))
            {
                actionContext.ActionArguments[Descriptor.ParameterName] = product;
            }

            Debugger.Break();

            return Task.FromResult(0);
        }
    }

    public class ProductBindingAttribute : ParameterBindingAttribute
    {
        public override HttpParameterBinding GetBinding(HttpParameterDescriptor parameter)
        {
            if (parameter.ParameterType == typeof(Product))
            {
                return new ProductParameterBinder(parameter);
            }
            return parameter.BindAsError("Wrong parameter type");
        }
    }

    public class ProductModelBinderAttribute : ModelBinderAttribute
    {
        public override HttpParameterBinding GetBinding(HttpParameterDescriptor parameter)
        {
            //Debugger.Break();

            HttpConfiguration config = parameter.Configuration;
            IEnumerable<ValueProviderFactory> valueProviderFactories = GetValueProviderFactories(config);

            return new ModelBinderParameterBinding(parameter, new ProductModelBinder(), valueProviderFactories);
        }
    }
}