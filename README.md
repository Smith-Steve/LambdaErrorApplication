# Lembda Error Application

An simple AWS application written to capture and display lambda errors.

## AWS Services Utilized:

- DynamoDB
- Lambda
- S3
- SNS

## Description

Application is a simple interface designed to capture lambda errors in a way that is easily reviewable, and requires a minimum implementation time in other AWS environments.

- Step 1#: Lambda Errors
- Step 2#: SNS Endpoint Is Hit
- Step 3#: Lambda Invoked to Capture Errors
- Step 4#: Lambda Sends Message to DynamoDB
- Step 5#: DynamoDB Recieves & Stores Message
- Step 6#: Fetch Sends Message to Lambda to 'Get' Table Values
- Step 7#: Displayed on Simple Front End
