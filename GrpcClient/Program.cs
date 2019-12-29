using Grpc.Core;
using Grpc.Net.Client;
using GrpcServer;
using System;
using System.Threading.Tasks;

namespace GrpcClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            #region Greeter Client
            //GrpcChannel channel = GrpcChannel.ForAddress("https://localhost:5001");
            //var client = new Greeter.GreeterClient(channel);
            //Console.WriteLine("Enter your name: ");
            //var input = Console.ReadLine();
            //var reply = await client.SayHelloAsync(new HelloRequest() { Name = input });
            //Console.WriteLine(reply.Message);
            //Console.ReadLine();
            #endregion

            #region Customer Client
            GrpcChannel channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new Customer.CustomerClient(channel);
            Console.WriteLine("Enter User ID: ");
            var input = Console.ReadLine();
            var reply = await client.GetCustomerInfoAsync(new CustomerLookupModel() { UserId = Convert.ToInt32(input) });
            Console.WriteLine($"{ reply.FirstName } { reply.LastName } { reply.Age }");

            using (var call = client.GetNewCustomers(new NewCustomerRequest()))
            {
                while (await call.ResponseStream.MoveNext())
                {
                    var currentCustomer = call.ResponseStream.Current;
                    Console.WriteLine($"{currentCustomer.FirstName} {currentCustomer.LastName} : {currentCustomer.Age} : {currentCustomer.EmailAddress}");
                }
            }

            Console.ReadKey();
            #endregion
        }
    }
}
