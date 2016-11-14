using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace HelloWorldApi.Filters
{
    public interface IOrderedFilter : IFilter
    {
        int Order { get; }
    }


    public class OrderedFilterProvider : IFilterProvider
    {
        public OrderedFilterProvider() { }

        public IEnumerable<FilterInfo> GetFilters(HttpConfiguration configuration, HttpActionDescriptor actionDescriptor)
        {
            var globalUnOrderedFilters = configuration.Filters.Where(filter => !(filter.Instance is IOrderedFilter));
            var globalOrderedFilters = configuration.Filters.Where(filter => filter.Instance is IOrderedFilter).OrderBy(x => ((IOrderedFilter)x.Instance).Order);

            var controllerUnorderedFilters = actionDescriptor.ControllerDescriptor.GetFilters().Where(filter => !(filter is IOrderedFilter));
            var controllerOrderedFilters = actionDescriptor.ControllerDescriptor.GetFilters().OfType<IOrderedFilter>().OrderBy(filter => filter.Order);

            var actionUnorderedFilters = actionDescriptor.GetFilters().Where(filter => !(filter is IOrderedFilter));
            var actionOrderedFilters = actionDescriptor.GetFilters().OfType<IOrderedFilter>().OrderBy(filter => filter.Order);

            var globalFilters = globalUnOrderedFilters.Concat(globalOrderedFilters);

            var controllerFilters = controllerUnorderedFilters.Concat(controllerOrderedFilters).Select(filter => new FilterInfo(filter, FilterScope.Controller));

            var actionFilters = actionUnorderedFilters.Concat(actionOrderedFilters).Select(filter => new FilterInfo(filter, FilterScope.Action));

            return globalFilters.Concat(controllerFilters).Concat(actionFilters);
        }
    }
}
