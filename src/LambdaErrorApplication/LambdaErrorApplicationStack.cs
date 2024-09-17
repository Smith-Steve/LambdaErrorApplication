using Amazon.CDK;
using Amazon.CDK.AWS.Lambda.EventSources;
using Amazon.CDK.AWS.APIGateway;
using Constructs;
using LambdaErrorApplication.Constructs;

namespace LambdaErrorApplication
{
    public class LambdaErrorApplicationStack : Stack
    {
        internal LambdaErrorApplicationStack(Construct scope, string id, IStackProps props = null) : base(scope, id, props)
        {
            DynamoDBConstruct dbTable = new DynamoDBConstruct(this, "LambdaErrorLoggingTable");
            // The code that defines your stack goes here
            LambdaConstruct errorLoggingLambda = new LambdaConstruct(this, "ErrorLoggingLambda", dbTable.returnTableARN);
            SnsTopicConstruct topicSNS = new SnsTopicConstruct(this, "LambdaErrorSubscription", errorLoggingLambda.FunctionArn);
            errorLoggingLambda.HandlerFunction.AddEventSource(new SnsEventSource(topicSNS.lambdaErrorTopic));
            //adding a comment.
            new S3Constructs(this, "errorlogging2342");
            new ApiGateWayConstruct(this, "New");
        }
    }
}
