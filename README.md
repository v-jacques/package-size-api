# package-size-api

## Build

`dotnet build`

## Tests

`dotnet test`

## Run

`dotnet run -p PackageSize.Web`

### Testing APIs with Swagger
1. Open your prefered browser.
2. Navigate to [http://localhost:5000/swagger](http://localhost:5000/swagger).

### Testing APIs with Postman/Insomnia
POST http://localhost:5000/api/order
Request body:
```
{
  "orderID": 1,
  "orderItems": [
    {
      "productType": "PhotoBook",
      "quantity": 2
    },
    {
      "productType": "Calendar",
      "quantity": 1
    },
    {
      "productType": "Canvas",
      "quantity": 3
    },
    {
      "productType": "Cards",
      "quantity": 10
    },
    {
      "productType": "Mug",
      "quantity": 3
    }
  ]
}
```
Response body:
```
{
  "orderID": 1,
  "orderItems": [
    {
      "orderItemID": 1,
      "productType": "PhotoBook",
      "quantity": 2
    },
    {
      "orderItemID": 2,
      "productType": "Calendar",
      "quantity": 1
    },
    {
      "orderItemID": 3,
      "productType": "Canvas",
      "quantity": 3
    },
    {
      "orderItemID": 4,
      "productType": "Cards",
      "quantity": 10
    },
    {
      "orderItemID": 5,
      "productType": "Mug",
      "quantity": 3
    }
  ],
  "requiredBinWidth": 237
}
```

GET http://localhost:5000/api/order/1
Response body:

```
{
  "orderID": 1,
  "orderItems": [
    {
      "orderItemID": 1,
      "productType": "PhotoBook",
      "quantity": 2
    },
    {
      "orderItemID": 2,
      "productType": "Calendar",
      "quantity": 1
    },
    {
      "orderItemID": 3,
      "productType": "Canvas",
      "quantity": 3
    },
    {
      "orderItemID": 4,
      "productType": "Cards",
      "quantity": 10
    },
    {
      "orderItemID": 5,
      "productType": "Mug",
      "quantity": 3
    }
  ],
  "requiredBinWidth": 237
}
```
