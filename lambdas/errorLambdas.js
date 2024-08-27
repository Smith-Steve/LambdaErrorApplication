const AWS = require("aws-sdk");
AWS.config.update({ region: "us-east-1" });
const DynamoDbObject = new AWS.DynamoDB();

exports.handler = async (event) => {
  //Message ID
  const awsMessageId = JSON.stringify(event.Records[0].Sns.MessageId);
  console.log(`AWS MessageId: ${awsMessageId}`);

  //Alarm Name
  const message = JSON.parse(event.Records[0].Sns.Message);
  const awsAlarmName = JSON.stringify(message.AlarmName);
  //InstanceId
  const awsInstanceId = JSON.stringify(message.AWSAccountId);
  // We will use the InstanceId as a look up, so that we can insert the client name. If there is a better way to
  // determine who the client is (like say naming the alarm) then we should do that.
  const awsLambdaName = JSON.stringify(message.Trigger.Dimensions[0].value);
  //Timestamp
  const awsTimeStamp = JSON.stringify(message.StateChangeTime);

  //Console Log Of Entire Message Statement.
  console.log(message);
  const entryParameters = {
    TableName: process.env.DynamoDBTableName,
    Item: {
      MessageId: { S: awsMessageId },
      AlarmName: { S: awsAlarmName },
      InstanceId: { S: awsInstanceId },
      LambdaName: { S: awsLambdaName },
      TimeStamp: { S: awsTimeStamp },
    },
    ReturnValues: "NONE",
  };
  try {
    await DynamoDbObject.putItem(entryParameters, function (error, data) {
      if (error) {
        console.log("Call Back Function: ", error);
        console.log("Error Type: ", error.__type);
      } else {
        console.log("Call Back Function Success: ", data);
      }
    }).promise();
  } catch (error) {
    console.log("Catch Block Of Try Catch");
    console.log();
  }
};
