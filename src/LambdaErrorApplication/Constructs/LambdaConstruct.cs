using Constructs;
using Amazon.CDK.AWS.Lambda;
using Amazon.CDK.AWS.IAM;
using System.Collections.Generic;
using Amazon.CDK.AWS.ServiceDiscovery;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Amazon.CDK;
using System.Threading.Tasks.Dataflow;

namespace LambdaErrorApplication.Constructs
{
    public class LambdaConstruct : Construct
    {
        public Function HandlerFunction { get; }
        public string FunctionArn { get; }
        public LambdaConstruct(Construct scope, string nameId, string tableName) : base(scope, nameId)
        {
            //IAM Role
            var iAmRole = new Role(this, "LambdaErrorApplicationIAMRole", new RoleProps{
                AssumedBy = new ServicePrincipal("lambda.amazonaws.com")
            });
            iAmRole.AddManagedPolicy(ManagedPolicy.FromAwsManagedPolicyName("service-role/AWSLambdaBasicExecutionRole"));
            iAmRole.AddManagedPolicy(ManagedPolicy.FromManagedPolicyArn(this, "DynamoDBRole", "arn:aws:iam::aws:policy/AmazonDynamoDBFullAccess"));

            
            //LAMBDA Definition - Recieve Error Lambda
            HandlerFunction = new Function(this, nameId, new FunctionProps{
                Runtime = Runtime.NODEJS_20_X,
                Code = Code.FromAsset("lambdas"),
                Handler = "errorLambdas.handler",
                Description = "Lambda That Recieves The Error Event, and deposits the error event into DynamoDB",
                Role = iAmRole,
                Environment = new Dictionary<string, string>
                {
                    ["DynamoDBTableName"] = tableName,
                    ["DBTable2"] = "Null"
                }
            });
            FunctionArn = HandlerFunction.FunctionArn;

            var iAmRoleForDynamoDBLambda = new Role(this, "DynamoDBLambdaRole222", new RoleProps{
                AssumedBy = new ServicePrincipal("lambda.amazonaws.com")
            });
            iAmRoleForDynamoDBLambda.AddManagedPolicy(ManagedPolicy.FromAwsManagedPolicyName("service-role/AWSLambdaBasicExecutionRole"));
            iAmRoleForDynamoDBLambda.AddManagedPolicy(ManagedPolicy.FromManagedPolicyArn(this, "DynamoDBRole222", "arn:aws:iam::aws:policy/AmazonDynamoDBFullAccess"));

            new Function(this, "GetErrorItemsLambda", new FunctionProps{
                Runtime = Runtime.NODEJS_20_X,
                Code = Code.FromAsset("lambdas"),
                Handler = "getErrors.handler",
                Description = "Lambda That Pulls Errors From DynamoDB For Front End",
                Role = iAmRoleForDynamoDBLambda,
                Environment = new Dictionary<string, string>
                {
                    ["DynamoDBTableName"] = tableName,
                    ["DBTable2"] = "Null"
                }
            });
        }
    }
}