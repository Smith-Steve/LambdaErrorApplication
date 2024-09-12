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
            new RestApi(this, "TestAPI");
        }
    }
}