# be-training

## Prerequisites

- **Docker**
- **Docker Compose**

## Getting Started

### 1. Clone the Repository

```bash
git clone https://github.com/tlis-radio/be-training.git
cd be-training
```
### 2. Build and Run with Docker Compose

Run the following commands to start the application in a Docker container:

#### Build the images:

```bash
docker-compose build
```
#### Start the application:
```bash
docker-compose up
```

### 3. Access the Application
   Once the application is running, it should be accessible at http://localhost:5247.

Use a web browser, curl, or a tool like Postman to connect to the application. For example:

```bash
curl http://localhost:5247/Notes/GetAll
```

Or use swagger: http://localhost:5247/swagger/index.html

#### Shutting Down
To stop and remove the containers, run:

```bash
docker-compose down
```
This will stop the application and clean up the containers.

#### Additional Configuration
##### Environment Variables:  
You can modify environment variables in the docker-compose.yml file as needed.  
Ports: By default, the application is exposed on port 5247 (host) to port 80 (container). You can change this in the [docker-compose.yml](./docker-compose.yml) file if necessary.