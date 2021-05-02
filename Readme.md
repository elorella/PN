# About

This solution is dependent on AWS and it won't successfully run in local environments. (Dependenies are AWS account to interact DynamoDb/SNS)
I tried dockerizing the dependencies but given the time limitation I couldn't complete it. 

serverless.template is created by SAM and it only includes Lamba & API Gateway definitions.

Lambda is deployed to my personel AWS account and you can see the swagger documentation here : 

https://2mw1kkhti4.execute-api.eu-central-1.amazonaws.com/Prod/swagger/index.html 

Here is the curl to create a Parking Right. After a successfull post operation an SNS event will be published. Consumer service has not been implemented and  I created an email subsction to be able to test the flow. 


```
curl -X 'POST' \
  'https://2mw1kkhti4.execute-api.eu-central-1.amazonaws.com/Prod/ParkingRight' \
  -H 'accept: text/plain' \
  -H 'Content-Type: application/json' \
  -d '{
  
  "licensePlate": "34EL727",
  "operatorId": 2,
  "parkingZoneId": 4,
  "startDate": "2021-05-02T16:25:42.476Z",
  "endDate": "2021-05-02T16:25:42.476Z",
  "amountPaid": 300,
  "customerProfile": 1
}'

```