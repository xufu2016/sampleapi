using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;
using System.Web.Http.ValueProviders;

namespace HelloApi.Models
{
    public class ProductModelBinder : IModelBinder
    {
        public bool BindModel(HttpActionContext actionContext, ModelBindingContext bindingContext)
        {
            Debugger.Break();

            if (bindingContext.ModelType != typeof(Product))
            {
                return false;
            }

            ValueProviderResult val = bindingContext.ValueProvider.GetValue(
                bindingContext.ModelName);
            if (val == null)
            {
                return false;
            }

            string key = val.RawValue as string;
            if (key == null)
            {
                bindingContext.ModelState.AddModelError(
                    bindingContext.ModelName, "Wrong value type");
                return false;
            }

            Product result;
            if (Product.TryParse(key, out result))
            {
                bindingContext.Model = result;
                return true;
            }

            bindingContext.ModelState.AddModelError(
                bindingContext.ModelName, "Cannot convert value to Product");
            return false;
        }
    }
}