using Newtonsoft.Json;

namespace StrategyGame.Api.Middlewares.ExceptionHandling
{
    public class ErrorDetails
    {
        public ErrorDetails()
        {
        }

        public int StatusCode { get; set; }
        // public int DetailedCode { get; set; }
        public string Message { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}