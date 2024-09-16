using Constructs;
//AWS
using Amazon.CDK;
using Amazon.CDK.AWS.APIGateway;
using Amazon.CDK.AwsApigatewayv2Integrations;

namespace LambdaErrorApplication.Constructs
{
    public class ApiGateWayConstruct : Construct
    {
        public ApiGateWayConstruct(Construct scope, string nameId) : base(scope, nameId)
        {
            var api = new RestApi(this, "TestAPI");

            var books = api.Root.AddResource("books");
            books.AddMethod("GET");
            books.AddMethod("POST");
        }
    }
}