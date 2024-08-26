const AWS = require("aws-sdk");
AWS.config.update({ region: "us-east-1" });

exports.handler = async (event) => {
  const lambda = new AWS.Lambda();
  console.log(event);
  return {
    statusCode: 200,
    headers: { "Content-Type": "text/plain" },
    body: JSON.stringify({ message: "Hello, World!" }),
  };
};
