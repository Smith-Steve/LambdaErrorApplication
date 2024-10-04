using Constructs;
//AWS
using Amazon.CDK.AWS.DynamoDB;
using Amazon.CDK.AWS.ControlTower;
using Amazon.CDK.AWS.CloudWatch;

namespace LambdaErrorApplication.Constructs
{
    public class DynamoDBConstruct : Construct
    {
        public string returnTableName { get;}
        public DynamoDBConstruct(Construct scope, string id) : base(scope, id)
        {
            var table = new TableV2(this, id, new TablePropsV2
            {
                PartitionKey = new Attribute
                {
                    Name = "MessageId",
                    Type = AttributeType.STRING
                },
                GlobalSecondaryIndexes = new [] {
                    new GlobalSecondaryIndexPropsV2 {
                        IndexName = "AlarmName",
                        PartitionKey = new Attribute {Name = "MessageId", Type = AttributeType.STRING},
                    },
                    new GlobalSecondaryIndexPropsV2 {
                        IndexName = "CustomerId",
                        PartitionKey = new Attribute {Name = "MessageId", Type = AttributeType.STRING}
                    },
                    new GlobalSecondaryIndexPropsV2 {
                        IndexName = "FunctionName",
                        PartitionKey = new Attribute {Name = "MessageId", Type = AttributeType.STRING}
                    },
                    new GlobalSecondaryIndexPropsV2 {
                        IndexName = "Timestamp",
                        PartitionKey = new Attribute {Name = "MessageId", Type = AttributeType.STRING}
                    }
                }
            });
            returnTableName = table.TableName;
        }
    }
}