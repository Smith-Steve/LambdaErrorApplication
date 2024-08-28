const AWS = require("aws-sdk");
AWS.config.update({ region: "us-east-1" });
const DynamoDbObject = new AWS.DynamoDB();

exports.handler = async (event) => {
  const parameters = {
    Key: {
      MessageId: {
        S: "8da94790-4ecf-578c-bf70-18d714a49e80",
      },
    },
    TableName:
      "LambdaErrorApplicationStack-LambdaErrorLoggingTableEB453C29-18EJ2AU1F8HB9",
  };
  DynamoDbObject.getItem(parameters, function (error, data) {
    if (error) console.log(error);
    else console.log("Success: ", data);
  });
};
