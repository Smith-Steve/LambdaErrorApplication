const AWS = require("aws-sdk");
AWS.config.update({ region: "us-east-1" });
const DynamoDbObject = new AWS.DynamoDB();

exports.handler = async (event) => {
  var entryParameters = {
    TableName: process.env.DynamoDBTableName,
  };
  const result = await DynamoDbObject.scan(entryParameters, (error, data) => {
    if (error) {
      console.error("Error: ", JSON.stringify(error, null, 2));
      console.error("More Error Information: ", JSON.stringify(error.__type));
    } else {
      return data.Items;
    }
  }).promise();
  return {
    statusCode: 200,
    headers: { "Content-Type": "application/json" },
    body: result,
  };
};
