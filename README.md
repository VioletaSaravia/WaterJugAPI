# WaterJugAPI

WaterJugAPI is a microservice designed to solve the classic water jug problem. It provides a single POST endpoint that
takes in the capacities of two jugs and a target volume, returning the steps to achieve the target volume using the two
jugs.

## Installation

1. Install the prerequisites:

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)

2. Clone the repository:
   ```sh
   git clone https://github.com/VioletaSaravia/WaterJugAPI.git
   cd WaterJugAPI
   ```

3. Build the project:

    ```sh
    dotnet build
    ```

4. Run the project:

    ```sh
    dotnet run --project WaterJugAPI
    ```

## Usage

To use the API, send a POST request to the /solve endpoint with the capacities of the two jugs and the target volume.

### POST /solve

- URL: /solve
- Method: POST
- Content-Type: application/json
- Body Parameters:
    - x (integer): Capacity of the first jug
    - y (integer): Capacity of the second jug
    - z (integer): Target volume

### Example Request

  ```json
  {
  "x": 3,
  "y": 5,
  "z": 4
}
```
