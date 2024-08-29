const AWS = require("aws-sdk");
AWS.config.update({ region: "us-east-1" });
const DynamoDbObject = new AWS.DynamoDB();

exports.handler = async (event) => {
  console.log("Weve entered the function.");
  var entryParameters = {
    TableName:
      "LambdaErrorApplicationStack-LambdaErrorLoggingTableEB453C29-18EJ2AU1F8HB9",
  };

  DynamoDbObject.scan(entryParameters, (error, data) => {
    if (error) {
      console.error("Error: ", JSON.stringify(error, null, 2));
      console.error("More Error Information: ", JSON.stringify(error.__type));
    } else {
      console.log("Success!");
      data.Items.forEach((item) => {
        console.log("Item: ", JSON.stringify(item));
      });
    }
  });
};
