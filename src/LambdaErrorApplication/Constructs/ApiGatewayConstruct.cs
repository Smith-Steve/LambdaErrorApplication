using Constructs;
//AWS
using Amazon.CDK;
using Amazon.CDK.AWS.APIGateway;
using Amazon.CDK.AWS.Lambda;
using Amazon.CDK.AwsApigatewayv2Integrations;
using Amazon.CDK.AWS.CloudFront;
using Amazon.CDK.AWS.SES.Actions;

namespace LambdaErrorApplication.Constructs
{
    public class ApiGateWayConstruct : Construct
    {
        //Referenced full library in third clause of syntax because there was a disambiguation error.
        public ApiGateWayConstruct(Construct scope, string nameId, Amazon.CDK.AWS.Lambda.Function function) : base(scope, nameId)
        {
            // var api = new RestApi(this, "LambdaErrorsAPI", new LambdaRestApiProps{
            //     Handler = backend
            // });

            // var books = api.Root.AddResource("LambdaErrors");
            // books.AddMethod("GET");
            // books.AddMethod("OPTIONS");
            // books.AddMethod("POST");

            var lambdaErrorAPI = new LambdaRestApi(this, "ErrorLambdaAPIGateway", new LambdaRestApiProps{
                Handler = function,
                Description = "The API used to pull information out of the dynamoDB table. There are 'GET' and 'POST' calls to it.",
                IntegrationOptions = new LambdaIntegrationOptions {
                    AllowTestInvoke = false,
                    Timeout = Duration.Seconds(10)
                }
            });

            var errors = lambdaErrorAPI.Root.AddResource("ErrorLambdaAPIGateway");
            errors.AddMethod("GET");
            errors.AddMethod("OPTIONS");
            errors.AddMethod("POST");
        }
    }
}