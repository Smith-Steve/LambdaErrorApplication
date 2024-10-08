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
            DynamoDBConstruct dbTable = new DynamoDBConstruct(this, "LEADynamo");
            // The code that defines your stack goes here
            LambdaConstruct errorLoggingLambda = new LambdaConstruct(this, "LEALambda2", dbTable.returnTableName);
            SnsTopicConstruct topicSNS = new SnsTopicConstruct(this, "LeaSNS2", errorLoggingLambda.FunctionArn);
            errorLoggingLambda.HandlerFunction.AddEventSource(new SnsEventSource(topicSNS.lambdaErrorTopic));
            //adding a comment.
            new S3Constructs(this, "leas322");
        }
    }
}
