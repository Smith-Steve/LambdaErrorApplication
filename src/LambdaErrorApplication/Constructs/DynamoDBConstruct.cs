using Constructs;
//AWS
using Amazon.CDK;
using Amazon.CDK.AWS.DynamoDB;
using System.Collections.Generic;

namespace LambdaErrorApplication.Constructs
{
    public class DynamoDBConstruct : Construct
    {
        public string returnTableARN { get;}
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
                },
            });
            returnTableARN = table.TableArn;
        }
    }
}