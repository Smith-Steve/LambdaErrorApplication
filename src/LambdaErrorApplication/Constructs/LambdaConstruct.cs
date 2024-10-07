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
            var iAmRole = new Role(this, "leaIAMlabda", new RoleProps{
                AssumedBy = new ServicePrincipal("lambda.amazonaws.com")
            });
            iAmRole.AddManagedPolicy(ManagedPolicy.FromAwsManagedPolicyName("service-role/AWSLambdaBasicExecutionRole"));
            iAmRole.AddManagedPolicy(ManagedPolicy.FromManagedPolicyArn(this, "leaIAMlabda3", "arn:aws:iam::aws:policy/AmazonDynamoDBFullAccess"));

            
            //LAMBDA Definition - Recieve Error Lambda
            HandlerFunction = new Function(this, "LEALambdaDepositError", new FunctionProps{
                Runtime = Runtime.NODEJS_20_X,
                Code = Code.FromAsset("lambdas"),
                Handler = "errorLambdas.handler",
                Description = "LEA: Lambda That Recieves The Error Event, and deposits the error event into DynamoDB",
                Role = iAmRole,
                Environment = new Dictionary<string, string>
                {
                    ["DynamoDBTableName"] = tableName
                }
            });
            FunctionArn = HandlerFunction.FunctionArn;

            var iAmRoleForDynamoDBLambda = new Role(this, "leaIAMlabda2", new RoleProps{
                AssumedBy = new ServicePrincipal("lambda.amazonaws.com")
            });
            iAmRoleForDynamoDBLambda.AddManagedPolicy(ManagedPolicy.FromAwsManagedPolicyName("service-role/AWSLambdaBasicExecutionRole"));
            iAmRoleForDynamoDBLambda.AddManagedPolicy(ManagedPolicy.FromManagedPolicyArn(this, "leaIAMlabda4", "arn:aws:iam::aws:policy/AmazonDynamoDBFullAccess"));

            var functionTwo = new Function(this, "GetErrorItemsLambda", new FunctionProps{
                Runtime = Runtime.NODEJS_20_X,
                Code = Code.FromAsset("lambdas"),
                Handler = "getErrors.handler",
                Description = "LEA: Lambda That Pulls Errors From DynamoDB For Front End",
                Role = iAmRoleForDynamoDBLambda,
                Environment = new Dictionary<string, string>
                {
                    ["DynamoDBTableName"] = tableName
                }
            });

            functionTwo.AddFunctionUrl(new FunctionUrlOptions{
                AuthType = FunctionUrlAuthType.NONE
            });

            //Lambda written for the purpose of generating AWS errors.
            //By design, this lambda has syntactically incorrect code. It's purpose is to generate errors that can be utilized to
            //test application
            // var iAMRoleForErrorLambda = new Role(this, "LeaLambdaError", new RoleProps{
            //     AssumedBy = new ServicePrincipal("lambda.amazonaws.com")
            // });
            // iAMRoleForErrorLambda.AddManagedPolicy(ManagedPolicy.FromAwsManagedPolicyName("service-role/AdministratorAccess"));

            // //Code generating lambda
            // new Function(this, "LeaTestingLambda", new FunctionProps{
            //     Runtime = Runtime.NODEJS_20_X,
            //     Code = Code.FromAsset("lambdas"),

            // });
        }
    }
}