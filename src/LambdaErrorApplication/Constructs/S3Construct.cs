using Constructs;
using Amazon.CDK.AWS.S3;
using Amazon.CDK.AWS.IAM;

namespace LambdaErrorApplication.Constructs
{
    public class S3Constructs : Construct
    {
        public S3Constructs(Construct scope, string nameId) : base(scope, nameId)
        {
            var bucketRole = new Role(this, "LambdaErrorAppBucketRole212", new RoleProps{
                AssumedBy = new ServicePrincipal("s3.amazonaws.com")
            });
            
            var bucket = new Bucket(this, "LambdaErrorAppBucke222t", new BucketProps {
                BucketName = nameId,
                Versioned = false,
                Encryption = BucketEncryption.S3_MANAGED,
                WebsiteIndexDocument = "index.html",
                ObjectLockEnabled = false,
                BlockPublicAccess = new BlockPublicAccess(new BlockPublicAccessOptions {
                    BlockPublicAcls = false,
                    BlockPublicPolicy = false,
                    IgnorePublicAcls = false,
                    RestrictPublicBuckets = false
                })
                //static website hosting?
                //BucketPolicy
                // - Appears a bucket policy is set up, and then the bucket is named in the policy configurations.
                // No Bucket Metrics Config
            });

            string bucketArnString = bucket.BucketArn + "/*";

            //Adding Resource Policy
            bucket.AddToResourcePolicy(
                new PolicyStatement(new PolicyStatementProps{
                    Sid = "PublicReadForGetBucketObjects",
                    Effect = Effect.ALLOW,
                    Principals = new [] {new ServicePrincipal("*")},
                    Actions = new [] {"s3:GetObject"},
                    Resources = new [] {bucketArnString},
                })
            );
        }
    }
}