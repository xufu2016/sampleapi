using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.ValueProviders;

namespace HelloApi.Models
{
    public class HeadersValueProvider : IValueProvider
    {
        private HttpRequestHeaders _headers;

        public HeadersValueProvider(HttpActionContext actionContext)
        {
            if (actionContext == null)
            {
                throw new ArgumentNullException("actionContext");
            }

            _headers = actionContext.Request.Headers;
        }

        public bool ContainsPrefix(string prefix)
        {
            return _headers.Contains(prefix);
        }

        public ValueProviderResult GetValue(string key)
        {
            IEnumerable<string> value;
            if(_headers.TryGetValues(key, out value))
            {
                string concatenatedValue = "";
                value.ToList().ForEach((s) => concatenatedValue += s);
                return new ValueProviderResult(value, concatenatedValue, CultureInfo.InvariantCulture);
            }
            return null;
        }
    }

    public class HeadersValueProviderFactory : ValueProviderFactory
    {
        public override IValueProvider GetValueProvider(HttpActionContext actionContext)
        {
            return new HeadersValueProvider(actionContext);
        }
    }
}