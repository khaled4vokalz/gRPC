using Grpc.Core;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace GrpcServer.Services
{
    public class CustomersService : Customer.CustomerBase
    {
        private ILogger<CustomersService> _logger;

        public CustomersService(ILogger<CustomersService> logger)
        {
            _logger = logger;
        }

        public override Task<CustomerModel> GetCustomerInfo(CustomerLookupModel request, ServerCallContext context)
        {
            CustomerModel output = new CustomerModel();
            if (request.UserId == 1)
            {
                output.FirstName = "Jamie";
                output.LastName = "Smith";
                output.Age = 12;
            }
            else if (request.UserId == 2)
            {
                output.FirstName = "Jhon";
                output.LastName = "Doe";
                output.Age = 14;
            }
            else
            {
                output.FirstName = "Khaled";
                output.LastName = "Maruf";
                output.Age = 30;
            }
            return Task.FromResult(output);
        }
    }
}
