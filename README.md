# Receipt Processor

## Intro

Primarily written in C# and the ASP.NET framework to handle the API and its request.

I also have the project written in Go linked here: [add link]

## API Specification

### Process Receipt

- Path: `/Receipts/process`
- Method: `POST`
- Payload: Receipt JSON
- Response: JSON containing an id for the receipt

Passing in JSON receipt into `POST` method:

```json
{
  "retailer": "M&M Corner Market",
  "purchaseDate": "2022-03-20",
  "purchaseTime": "14:33",
  "items": [
    {
      "shortDescription": "Gatorade",
      "price": "2.25"
    },
    {
      "shortDescription": "Gatorade",
      "price": "2.25"
    },
    {
      "shortDescription": "Gatorade",
      "price": "2.25"
    },
    {
      "shortDescription": "Gatorade",
      "price": "2.25"
    }
  ],
  "total": "9.00"
}
```

Response:

**Note: the `id` is always going to be randomly generated.**

```json
{
  "id": "e4b135a1-304f-4518-8562-9480dbc796ce"
}
```

### Get points

- Path: `/Receipts/{id}/points`
- Method: `GET`
- Response: A JSON object containing the receipt by the ID and returns an object specifying the points.

```json
{
  "points": 109
}
```

## Dockerfile
---
Included in this project is the `Dockerfile` to be able to run the .NET project.

To build the docker file run the command:

```bash
  docker build -t receipt-processor .
```

To run the docker file:

```bash
  docker run -d -p 8080:80 receipt-processor
```
