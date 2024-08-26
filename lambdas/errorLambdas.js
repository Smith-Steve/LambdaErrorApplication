const AWS = require("aws-sdk");
AWS.config.update({ region: "us-east-1" });
const DynamoDbObject = new AWS.DynamoDb({ apiVersion: "2012-08-10" });

exports.handler = async (event) => {
  //Message ID
  const awsMessageId = JSON.stringify(event.Records[0].Sns.MessageId);
  console.log(`AWS MessageId: ${awsMessageId}`);

  //Alarm Name
  const message = JSON.parse(event.Records[0].Sns.Message);
  const awsAlarmName = JSON.stringify(message.AlarmName);
  console.log(`awsAlarmName: ${awsAlarmName}`);

  //InstanceId
  const awsInstanceId = JSON.stringify(message.AWSAccountId);
  console.log(`awsInstanceId: ${awsInstanceId}`);
  // We will use the InstanceId as a look up, so that we can insert the client name. If there is a better way to
  // determine who the client is (like say naming the alarm) then we should do that.
  console.log(
    "This is the Lambda Name where the Dimensions attribute is causing issues"
  );
  const awsLambdaName = JSON.stringify(message.Trigger.Dimensions[0].value);
  console.log(`AWSLambdaName: ${awsLambdaName}`);

  //Timestamp
  const awsTimeStamp = JSON.stringify(message.StateChangeTime);
  console.log(`Timestamp: ${awsTimeStamp}`);

  //Console Log Of Entire Message Statement.
  console.log(message);
  return {
    statusCode: 200,
    headers: { "Content-Type": "text/plain" },
    body: JSON.stringify({ message: "Hello, World!" }),
  };
};
