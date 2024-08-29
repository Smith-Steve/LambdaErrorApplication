const AWS = require("aws-sdk");
AWS.config.update({ region: "us-east-1" });
const DynamoDbObject = new AWS.DynamoDB();

exports.handler = async (event) => {
  console.log("Weve entered the function.");
  var entryParameters = {
    TableName:
      "arn:aws:lambda:us-east-1:531698586584:function:LambdaErrorApplicationSta-ErrorLoggingLambdaGetErr-D5Zwj2eADHqo",
  };

  await functionScan(entryParameters)
    .then((item) => {
      console.log("Success");
      console.log(JSON.stringify(item));
    })
    .catch((error) => {
      console.log("Catch Block");
      console.error(error);
    });
};

const functionScan = (paramters) => {
  DynamoDbObject.scan(paramters, (error, data) => {
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
