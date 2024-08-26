using Amazon.CDK;
using Amazon.CDK.AWS.Lambda.EventSources;
using Amazon.CDK.AWS.SES.Actions;
using Constructs;
using LambdaErrorApplication.Constructs;

namespace LambdaErrorApplication
{
    public class LambdaErrorApplicationStack : Stack
    {
        internal LambdaErrorApplicationStack(Construct scope, string id, IStackProps props = null) : base(scope, id, props)
        {
            // The code that defines your stack goes here
            LambdaConstruct errorLoggingLambda = new LambdaConstruct(this, "ErrorLoggingLambda");
            SnsTopicConstruct topicSNS = new SnsTopicConstruct(this, "LambdaErrorSubscription", errorLoggingLambda.FunctionArn);
            errorLoggingLambda.HandlerFunction.AddEventSource(new SnsEventSource(topicSNS.lambdaErrorTopic));
        }
    }
}
