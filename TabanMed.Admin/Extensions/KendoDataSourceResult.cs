namespace TabanMed.Admin.Extensions
{
    public class KendoDataSourceResult : Kendo.Mvc.UI.DataSourceResult
    {
        public KendoDataSourceResult(Kendo.Mvc.UI.DataSourceResult dataSourceResult)
        {
            this.AggregateResults = dataSourceResult.AggregateResults;
            this.Data = dataSourceResult.Data;
            this.Errors = dataSourceResult.Errors;
            this.Total = dataSourceResult.Total;
        }
        //This is my new Property - I want to be able to access this on the
        public string? UserMessage { get; set; }
    }
}
