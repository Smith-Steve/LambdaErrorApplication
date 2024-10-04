using Constructs;
using Amazon.CDK;
using Amazon.CDK.AWS.SNS;
using System.Buffers.Text;
using System.ComponentModel;
using Amazon.CDK.AWS.IAM;

namespace LambdaErrorApplication.Constructs
{
    public class SnsTopicConstruct : Construct
    {
        public Topic lambdaErrorTopic { get;}
        public SnsTopicConstruct(Construct scope, string nameId, string lambdaArn) : base(scope, nameId)
        {
            lambdaErrorTopic = new Topic(this, "LeaLambdaTopic",new TopicProps {
                DisplayName = "LeaLambdaTopic",
                Fifo = false
            });

            var iAmRole = new Role(this,"LeaIAMsnsLambda", new RoleProps {
                AssumedBy = new ServicePrincipal("sns.amazonaws.com")
            });
            iAmRole.AddManagedPolicy(ManagedPolicy.FromManagedPolicyArn(this, "LEASNSIAMPolicy", "arn:aws:iam::aws:policy/AmazonSNSFullAccess"));
            new Subscription(this, "nameId", new SubscriptionProps{
                Topic = lambdaErrorTopic,
                Protocol = SubscriptionProtocol.LAMBDA,
                Endpoint = lambdaArn
            });
        }
    }
}