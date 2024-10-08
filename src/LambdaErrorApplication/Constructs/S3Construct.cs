using Constructs;
using Amazon.CDK.AWS.S3;
using Amazon.CDK.AWS.IAM;
using Amazon.CDK;
using Amazon.CDK.AWS.S3.Deployment;
using System.Threading.Tasks.Dataflow;

namespace LambdaErrorApplication.Constructs
{
    public class S3Constructs : Construct
    {
        public S3Constructs(Construct scope, string nameId) : base(scope, nameId)
        {
            var bucketRole = new Role(this, "LeaIAMRoleS31", new RoleProps{
                AssumedBy = new ServicePrincipal("s3.amazonaws.com")
            });
            
            var bucket = new Bucket(this, "LEAs3BucketWebsite22", new BucketProps {
                BucketName = nameId,
                Versioned = false,
                Encryption = BucketEncryption.S3_MANAGED,
                WebsiteIndexDocument = "index.html",
                ObjectLockEnabled = false,
                //Remove bucket. If bucket is orphaned from stack, delete.
                RemovalPolicy = RemovalPolicy.DESTROY,
                //Delete objects in bucket. This ensures that even if the bucekt in the stack as
                // objects in it, they will be removed.
                AutoDeleteObjects = true,
                //web
                PublicReadAccess = true,
                BlockPublicAccess = new BlockPublicAccess(new BlockPublicAccessOptions {
                    BlockPublicAcls = false,
                    BlockPublicPolicy = false,
                    IgnorePublicAcls = false,
                    RestrictPublicBuckets = false
                }),
                BucketKeyEnabled = false,

            });

            bucket.AddCorsRule(new CorsRule{
                AllowedHeaders = new [] {"*"},
                AllowedMethods = new [] {HttpMethods.GET, HttpMethods.POST, HttpMethods.PUT, HttpMethods.DELETE},
                AllowedOrigins = new [] {"*"},
                MaxAge = 100
            });

            string bucketArnString = bucket.BucketArn + "/*";

            //Adding Resource Policy
            bucket.AddToResourcePolicy(
                new PolicyStatement(new PolicyStatementProps{
                    Sid = "PublicReadForGetBucketObjects",
                    Effect = Effect.ALLOW,
                    //AWS discourages the use of 'StarPrinicipal'
                    Principals = new [] {new StarPrincipal()},
                    Actions = new [] {"s3:GetObject"},
                    Resources = new [] {bucketArnString}
                })
            );

            new BucketDeployment(this, "LEAS3BucketDeployment22", new BucketDeploymentProps{
                Sources = new [] {Source.Asset("./frontend/build")},
                DestinationBucket = bucket,
                MemoryLimit = 2048,
                EphemeralStorageSize = Size.Gibibytes(2)
            });
        }
    }
}