using Constructs;
using Amazon.CDK.AWS.S3;
using Amazon.CDK.AWS.IAM;

namespace LambdaErrorApplication.Constructs
{
    public class S3Constructs : Construct
    {
        public S3Constructs(Construct scope, string nameId) : base(scope, nameId)
        {
            var bucketRole = new Role(this, "LambdaErrorAppBuckeggggtRole2fdfdfhgh22", new RoleProps{
                AssumedBy = new ServicePrincipal("s3.amazonaws.com")
            });
            
            var bucket = new Bucket(this, "LambdaErrodfdfdfdrApgggpBucke2hghg2f2t", new BucketProps {
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
            });

            string bucketArnString = bucket.BucketArn + "/*";

            //Adding Resource Policy
            bucket.AddToResourcePolicy(
                new PolicyStatement(new PolicyStatementProps{
                    Sid = "PublicReadForGetBucketObjects",
                    Effect = Effect.ALLOW,
                    Principals = new [] {new StarPrincipal()},
                    Actions = new [] {"s3:GetObject"},
                    Resources = new [] {bucketArnString},
                })
            );
        }
    }
}