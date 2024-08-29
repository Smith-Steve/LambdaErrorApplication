const AWS = require("aws-sdk");
AWS.config.update({ region: "us-east-1" });
const DynamoDbObject = new AWS.DynamoDB();

exports.handler = async (event) => {
  console.log("Weve entered the function.");
  var entryParameters = {
    TableName:
      "arn:aws:lambda:us-east-1:531698586584:function:LambdaErrorApplicationSta-ErrorLoggingLambdaGetErr-D5Zwj2eADHqo",
  };
  DynamoDbObject.scan(entryParameters, (error, data) => {
    console.log("Entering Scan Function");
    if (error) {
      console.error(
        "UNable to scan the table:",
        JSON.stringify(error, null, 2)
      );
    } else {
      console.log("Scan Succeeded");
      data.Items.forEach((item) => {
        console.log("Item: ", JSON.stringify(item));
      });
    }
  });
};
